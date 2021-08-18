using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace vxlToCfgGui
{
    public partial class Form1 : Form
    {
        private string filePath = "";
        private string fileName = "";
        private byte[] fileBytes;
        private int[,,] map = new int[512, 64, 512];
        private Color[,,] color = new Color[512, 64, 512];
        private Bitmap img;

        public Form1()
        {
            InitializeComponent();
            PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            DirectoryInfo info = new DirectoryInfo(@"../../..");
            if (info.Exists)
            {
                TreeNode rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                folderTree.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode parentNode)
        {
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                TreeNode node = new TreeNode(subDir.Name, 0, 0);
                node.Tag = subDir;
                //node.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();

                parentNode.Nodes.Add(node);

                if (subSubDirs.Length > 0)
                {
                    GetDirectories(subSubDirs, node);
                }
            }
        }

        private void folderTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            fileList.Items.Clear();

            DirectoryInfo nodeDirInfo = (DirectoryInfo)e.Node.Tag;

            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                if (file.Extension != ".vxl") continue;

                ListViewItem item = new ListViewItem(file.Name, 1);
                ListViewSubItem[] subItems = new ListViewSubItem[]
                {
                    new ListViewSubItem(item, file.DirectoryName)
                };

                item.SubItems.AddRange(subItems);
                fileList.Items.Add(item);
            }

            fileList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void fileList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            preview.Image = null;

            if (fileList.SelectedItems.Count > 0)
            {
                filePath = fileList.SelectedItems[0].SubItems[1].Text;
                fileName = fileList.SelectedItems[0].Text;

                fileBytes = File.ReadAllBytes(filePath + "/" + fileName);

                new Thread(new ThreadStart(LoadFile)).Start();
            }
        }

        private void LoadFile()
        {
            void SetGeom(int x, int y, int z, int t)
            {
                if (y < 0 || y >= 64)
                {
                    MessageBox.Show("setgeom z is oob: " + y);
                    Thread.CurrentThread.Abort();
                    return;
                }

                map[x, 63 - y, z] = t;
            }

            void SetColor(int x, int y, int z, Color c)
            {
                if (y < 0 || y >= 64)
                {
                    MessageBox.Show("setcolor z is oob: " + y);
                    Thread.CurrentThread.Abort();
                    return;
                }

                color[x, 63 - y, z] = c;
            }

            {
                int v = 0;
                int y;
                for (int z = 0; z < 512; ++z)
                {
                    for (int x = 0; x < 512; ++x)
                    {
                        for (y = 0; y < 64; ++y)
                        {
                            SetGeom(x, y, z, 1);
                        }
                        y = 0;
                        while (true)
                        {
                            int number_4byte_chunks = fileBytes[v + 0];
                            int top_color_start = fileBytes[v + 1];
                            int top_color_end = fileBytes[v + 2];

                            for (int i = y; i < top_color_start; i++)
                                SetGeom(x, i, z, 0);

                            int color_index = v + 4;
                            for (y = top_color_start; y <= top_color_end; y++)
                            {
                                SetColor(x, y, z, Color.FromArgb(fileBytes[color_index + 2], fileBytes[color_index + 1], fileBytes[color_index]));
                                color_index += 4;
                            }

                            int len_bottom = top_color_end - top_color_start + 1;

                            // check for end of data marker
                            if (number_4byte_chunks == 0)
                            {
                                // infer ACTUAL number of 4-byte chunks from the length of the color data
                                v += 4 * (len_bottom + 1);
                                break;
                            }

                            // infer the number of bottom colors in next span from chunk length
                            int len_top = (number_4byte_chunks - 1) - len_bottom;

                            // now skip the v pointer past the data to the beginning of the next span
                            v += number_4byte_chunks * 4;

                            int bottom_color_end = fileBytes[v + 3]; // aka air start
                            int bottom_color_start = bottom_color_end - len_top;

                            for (y = bottom_color_start; y < bottom_color_end; ++y)
                            {
                                SetColor(x, y, z, Color.FromArgb(fileBytes[color_index + 2], fileBytes[color_index + 1], fileBytes[color_index]));
                                color_index += 4;
                            }
                        }
                    }
                }
            }

            UpdatePreview();
        }

        private void UpdatePreview()
        {
            img = new Bitmap(512, 512);

            for (int x = 0; x < 512; x++)
            {
                for (int z = 0; z < 512; z++)
                {
                    for (int y = 63; y >= 0; y--)
                    {
                        if (map[x, y, z] == 0) continue;

                        Color col = color[x, y, z];
                        img.SetPixel(x, z, col);
                        break;
                    }
                }
            }

            preview.Image = img;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (fileName != "")
            {
                new Thread(new ThreadStart(ExportFile)).Start();
            }
        }

        public void ExportFile()
        {
            uint EncodeColor(Color color)
            {
                return (uint)((color.R << 18) | (color.G << 10) | (color.B << 2) | 1);
            }

            MessageBox.Show("Exporting.");

            StringBuilder data = new StringBuilder("blocks = ");

            bool prevVisible = true;
            int lastVisibleIndex = 0;
            int i = 0;

            for (int y = 0; y < 64; y++)
            {
                for (int z = 0; z < 512; z++)
                {
                    for (int x = 0; x < 512; x++)
                    {
                        bool visible = map[x, y, z] == 1;

                        if (visible)
                        {
                            if (!prevVisible)
                            {
                                data.Append((i - lastVisibleIndex - 2) << 1);
                                data.Append(";");
                            }

                            Color block = color[x, y, z];

                            if (block.IsEmpty)
                            {
                                data.Append(3);
                            }
                            else
                            {
                                data.Append(EncodeColor(block));
                            }

                            data.Append(";");

                            lastVisibleIndex = i;
                        }

                        prevVisible = visible;
                        i++;
                    }
                }
            }

            data.Append("\nsize = 512;64;512;\n");

            string name = fileName.Remove(fileName.Length - 4);
            File.WriteAllText(filePath + "/" + name + ".cfg", data.ToString());

            MessageBox.Show("Done.");
        }
    }
}
