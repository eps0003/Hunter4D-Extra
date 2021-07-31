using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace vxlForHunter4D
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PopulateTreeView();
            this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
        }

        private bool selected = false;
        //bool loaded = false;

        private string file_path = "";
        private string file_name = "";
        private byte[] fileBytes;

        private int[,,] map = new int[512, 512, 64];
        private Color[,,] color = new Color[512, 512, 64];
        private Color base_ground_color = Color.FromArgb(103, 64, 30);
        private Bitmap img = new Bitmap(512, 512);
        private Bitmap img_with_edits = new Bitmap(512, 512);

        private delegate void SetButtonEnableCallback(bool enable);

        private uint ExportX1 = 0;
        private uint ExportX2 = 512;
        private uint ExportY1 = 0;
        private uint ExportY2 = 512;

        private static uint RGBtoUInt(uint R, uint G, uint B)
        {
            return (R << 16 | G << 8 | B);
        }

        private void PopulateTreeView()
        {
            //DirectoryInfo info = new DirectoryInfo(Application.StartupPath);
            DirectoryInfo info = new DirectoryInfo(@"../..");
            if (info.Exists)
            {
                TreeNode rootNode;
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                if (file.Extension != ".vxl") continue;

                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]{
                    new ListViewItem.ListViewSubItem(item, file.DirectoryName)};

                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            selected = true;
            button1.Enabled = true;
            file_path = listView1.SelectedItems[0].SubItems[1].Text;
            file_name = listView1.SelectedItems[0].Text;
            label1.Text = "Selected: " + file_name;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected = false;
            button1.Enabled = false;
            label1.Text = "Selected: none";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!selected) return;

            fileBytes = File.ReadAllBytes(file_path + "/" + file_name);

            pictureBox1.Image = imageList2.Images[0];
            ThreadStart childref = new ThreadStart(CallToChildThread_LoadFile);
            Thread childThread = new Thread(childref);
            childThread.Start();
        }

        public void CallToChildThread_LoadFile()
        {
            void setgeom(int _x, int _y, int _z, int t)
            {
                if (_z >= 0 && _z < 64) map[_x, _y, _z] = t;
                else { MessageBox.Show("setgeom z is oob: " + _z.ToString()); pictureBox1.Image = null; Thread.CurrentThread.Abort(); }
            }

            void setcolor(int _x, int _y, int _z, Color c)
            {
                if (_z >= 0 && _z < 64) color[_x, _y, _z] = c;
                else { MessageBox.Show("setcolor z is oob: " + _z.ToString()); pictureBox1.Image = null; Thread.CurrentThread.Abort(); }
            }

            {
                uint v = 0;
                int x, y, z;
                for (y = 0; y < 512; ++y)
                {
                    for (x = 0; x < 512; ++x)
                    {
                        for (z = 0; z < 64; ++z)
                        {
                            setgeom(x, y, z, 1);
                            setcolor(x, y, z, base_ground_color);
                        }
                        z = 0;
                        for (; ; )
                        {
                            uint color_index;
                            int i;
                            int number_4byte_chunks = fileBytes[v + 0];
                            int top_color_start = fileBytes[v + 1];
                            int top_color_end = fileBytes[v + 2]; // inclusive
                            int bottom_color_start;
                            int bottom_color_end; // exclusive
                            int len_top;
                            int len_bottom;

                            for (i = z; i < top_color_start; i++)
                                setgeom(x, y, i, 0);

                            color_index = v + 4;
                            for (z = top_color_start; z <= top_color_end; z++)
                            {
                                setcolor(x, y, z, Color.FromArgb(fileBytes[color_index + 2], fileBytes[color_index + 1], fileBytes[color_index]));
                                color_index += 4;
                            }

                            len_bottom = top_color_end - top_color_start + 1;

                            // check for end of data marker
                            if (number_4byte_chunks == 0)
                            {
                                // infer ACTUAL number of 4-byte chunks from the length of the color data
                                v += (uint)(4 * (len_bottom + 1));
                                break;
                            }

                            // infer the number of bottom colors in next span from chunk length
                            len_top = (number_4byte_chunks - 1) - len_bottom;

                            // now skip the v pointer past the data to the beginning of the next span
                            v += (uint)(number_4byte_chunks * 4);

                            bottom_color_end = fileBytes[v + 3]; // aka air start
                            bottom_color_start = bottom_color_end - len_top;

                            for (z = bottom_color_start; z < bottom_color_end; ++z)
                            {
                                setcolor(x, y, z, Color.FromArgb(fileBytes[color_index + 2], fileBytes[color_index + 1], fileBytes[color_index]));
                                color_index += 4;
                            }
                        }
                    }
                }
            }
            {
                img = new Bitmap(512, 512);
                uint x, y, z;
                for (x = 0; x < 512; x++)
                {
                    for (y = 0; y < 512; y++)
                    {
                        for (z = 0; z < 64; z++)
                        {
                            if (map[x, y, z] == 0) continue;

                            Color col = color[x, y, z];
                            img.SetPixel((int)x, (int)y, col);
                            break;
                        }
                    }
                }
                pictureBox1.Image = img;
            }

            bool ass = true;
            if (ExportBtn.InvokeRequired)
            {
                SetButtonEnableCallback d = new SetButtonEnableCallback(buttonEnable);
                Invoke(d, new object[] { ass });
            }
            else
            {
                // It's on the same thread, no need for Invoke
                ExportBtn.Enabled = ass;
            }
        }

        private void buttonEnable(bool enable)
        {
            ExportBtn.Enabled = enable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateMapImage();
        }

        private void UpdateMapImage()
        {
            img_with_edits = new Bitmap(img);
            ExportX1 = (uint)StartXBox.Value;
            ExportX2 = (uint)EndXBox.Value;
            ExportY1 = (uint)StartYBox.Value;
            ExportY2 = (uint)EndYBox.Value;

            for (uint i = ExportX1; i < ExportX2; i++)
            {
                img_with_edits.SetPixel((int)i, (int)ExportY1, Color.CadetBlue);
                img_with_edits.SetPixel((int)i, (int)ExportY2, Color.CadetBlue);
            }
            for (uint i = ExportY1; i < ExportY2; i++)
            {
                img_with_edits.SetPixel((int)ExportX1, (int)i, Color.CadetBlue);
                img_with_edits.SetPixel((int)ExportX2, (int)i, Color.CadetBlue);
            }
            pictureBox1.Refresh();
            pictureBox1.Image = img_with_edits;
        }

        private bool waiting_for_mouse = false;
        private int id = 0;

        private void PickFromMouseX1_Click(object sender, EventArgs e)
        {
            //label2.Text = Convert.ToChar((byte)StartXBox.Value).ToString();
            waiting_for_mouse = true;
            id = 1;
        }

        private void PickFromMouseX2_Click(object sender, EventArgs e)
        {
            waiting_for_mouse = true;
            id = 2;
        }

        private void PickFromMouseY1_Click(object sender, EventArgs e)
        {
            waiting_for_mouse = true;
            id = 3;
        }

        private void PickFromMouseY2_Click(object sender, EventArgs e)
        {
            waiting_for_mouse = true;
            id = 4;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (waiting_for_mouse)
            {
                waiting_for_mouse = false;
                Point mouse_pos = this.PointToClient(Cursor.Position);
                mouse_pos.X = mouse_pos.X - pictureBox1.Location.X;
                mouse_pos.Y = mouse_pos.Y - pictureBox1.Location.Y;
                switch (id)
                {
                    case 1:
                        ExportX1 = (uint)mouse_pos.X;
                        StartXBox.Value = (uint)mouse_pos.X;
                        break;

                    case 2:
                        ExportX2 = (uint)mouse_pos.X;
                        EndXBox.Value = (uint)mouse_pos.X;
                        break;

                    case 3:
                        ExportY1 = (uint)mouse_pos.Y;
                        StartYBox.Value = (uint)mouse_pos.Y;
                        break;

                    case 4:
                        ExportY2 = (uint)mouse_pos.Y;
                        EndYBox.Value = (uint)mouse_pos.Y;
                        break;
                }
                UpdateMapImage();
            }
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            //ThreadStart childref = new ThreadStart(CallToChildThread_ExportFile);
            //Thread childThread = new Thread(childref);
            //MessageBox.Show("Exporting.");
            //childThread.Start();
            //byte[] stream;
            MemoryStream ms = new MemoryStream();

            imageList2.Images[0].Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            byte[] stream = ms.ToArray();
            StringBuilder data = new StringBuilder();
            foreach (byte num in stream)
            {
                data.Append(ToLetter((int)num));//num.ToString()+" ");
            }
            //label6.Text = data.ToString();
            File.WriteAllText(Application.StartupPath + "/crap.txt", data.ToString());

            Image img = Image.FromStream(ms);
            pictureBox1.Image = img;
        }

        public void CallToChildThread_ExportFile()
        {
            Point map_size = new Point((int)(ExportX2 - ExportX1), (int)(ExportY2 - ExportY1));
            //StringBuilder map_data = new StringBuilder();
            //map_data.Append("Vec3f MAP_SIZE(" + map_size.X + ", 64, " + map_size.Y + ");\n\n#define SERVER_ONLY\n\nstring mapData = \"");
            //Image img = new

            uint x, y, z;
            for (x = ExportX1; x < ExportX2; x++)
            {
                for (y = ExportY1; y < ExportY2; y++)
                {
                    int amount = 0;
                    for (int _z = 0; _z < 64; _z++)
                    {
                        if (map[x, y, _z] == 1) amount += 1;
                    }

                    //map_data.Append(ToLetter(amount));
                    //MessageBox.Show(Convert.ToChar(Convert.ToByte(2)).ToString());
                    for (z = 0; z < 64; z++)
                    {
                        if (map[x, y, z] == 0) continue;

                        Color col = color[x, y, z];
                        //map_data.Append(ToLetter(64 - (int)z) + ToLetter(col.R) + ToLetter(col.G) + ToLetter(col.B));
                    }
                }
            }
            //map_data.Remove(map_data.Length - 1, 1);
            //map_data.Append("\";");

            string name_no_ext = file_name.Remove(file_name.Length - 4, 4);

            //File.WriteAllText(file_path + "/" + name_no_ext + ".as", map_data.ToString());
            MessageBox.Show("Done.");
        }

        private string ToLetter(int number)
        {
            //Convert.ToChar((byte)amount).ToString()
            string letter = "";
            /*switch (number)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                //    case 10:
                case 11:
                case 12:
                //    case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 22:
                case 60:
                    letter = "\\";
                    break;
            }
            if (number == 10 || number == 13) number++;*/
            letter += Convert.ToChar((byte)number).ToString();
            //letter += ",";

            return letter;
        }

        /*public void CallToChildThread_ExportFile()
        {
            Point map_size = new Point((int)(ExportX2 - ExportX1), (int)(ExportY2 - ExportY1));
            StringBuilder map_data = new StringBuilder();
            map_data.Append("Vec3f MAP_SIZE(" + map_size.X + ", 64, " + map_size.Y + ");\n\n#define SERVER_ONLY\n\nuint[] mapData = {");

            uint x, y, z;
            for (x = ExportX1; x < ExportX2; x++)
            {
                for (y = ExportY1; y < ExportY2; y++)
                {
                    int amount = 0;
                    for (int _z = 0; _z < 64; _z++)
                    {
                        if (map[x, y, _z] == 1) amount += 1;
                    }

                    map_data.Append(amount + ",");
                    for (z = 0; z < 64; z++)
                    {
                        if (map[x, y, z] == 0) continue;

                        Color col = color[x, y, z];
                        map_data.Append((64 - z) + "," + (RGBtoUInt(col.R, col.G, col.B)) + ",");
                    }
                }
            }
            map_data.Remove(map_data.Length - 1, 1);
            map_data.Append("};");

            string name_no_ext = file_name.Remove(file_name.Length - 4, 4);

            File.WriteAllText(file_path + "/" + name_no_ext + ".as", map_data.ToString());
            MessageBox.Show("Done.");
        }*/
    }
}
