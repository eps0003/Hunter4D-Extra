namespace vxlForHunter4D
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.StartXBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StartYBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.EndYBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.EndXBox = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.PickFromMouseX2 = new System.Windows.Forms.Button();
            this.PickFromMouseY2 = new System.Windows.Forms.Button();
            this.PickFromMouseY1 = new System.Windows.Forms.Button();
            this.PickFromMouseX1 = new System.Windows.Forms.Button();
            this.ExportBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartXBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartYBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndYBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndXBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "foldr.png");
            this.imageList1.Images.SetKeyName(1, "fil.png");
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Indent = 5;
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(178, 261);
            this.treeView1.TabIndex = 1;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(196, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(170, 261);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(12, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(372, 11);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(512, 512);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(93, 284);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Selected: none";
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.White;
            this.imageList2.Images.SetKeyName(0, "loading.png");
            // 
            // StartXBox
            // 
            this.StartXBox.Location = new System.Drawing.Point(47, 9);
            this.StartXBox.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.StartXBox.Name = "StartXBox";
            this.StartXBox.Size = new System.Drawing.Size(52, 20);
            this.StartXBox.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "start X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "start Y:";
            // 
            // StartYBox
            // 
            this.StartYBox.Location = new System.Drawing.Point(47, 35);
            this.StartYBox.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.StartYBox.Name = "StartYBox";
            this.StartYBox.Size = new System.Drawing.Size(52, 20);
            this.StartYBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "end Y:";
            // 
            // EndYBox
            // 
            this.EndYBox.Location = new System.Drawing.Point(183, 35);
            this.EndYBox.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.EndYBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EndYBox.Name = "EndYBox";
            this.EndYBox.Size = new System.Drawing.Size(52, 20);
            this.EndYBox.TabIndex = 14;
            this.EndYBox.Value = new decimal(new int[] {
            511,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(145, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "end X:";
            // 
            // EndXBox
            // 
            this.EndXBox.Location = new System.Drawing.Point(183, 9);
            this.EndXBox.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.EndXBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EndXBox.Name = "EndXBox";
            this.EndXBox.Size = new System.Drawing.Size(52, 20);
            this.EndXBox.TabIndex = 12;
            this.EndXBox.Value = new decimal(new int[] {
            511,
            0,
            0,
            0});
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(10, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 25);
            this.button2.TabIndex = 16;
            this.button2.Text = "Apply";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PickFromMouseX2
            // 
            this.PickFromMouseX2.Location = new System.Drawing.Point(241, 11);
            this.PickFromMouseX2.Name = "PickFromMouseX2";
            this.PickFromMouseX2.Size = new System.Drawing.Size(19, 18);
            this.PickFromMouseX2.TabIndex = 19;
            this.PickFromMouseX2.Text = "^";
            this.PickFromMouseX2.UseVisualStyleBackColor = true;
            this.PickFromMouseX2.Click += new System.EventHandler(this.PickFromMouseX2_Click);
            // 
            // PickFromMouseY2
            // 
            this.PickFromMouseY2.Location = new System.Drawing.Point(241, 37);
            this.PickFromMouseY2.Name = "PickFromMouseY2";
            this.PickFromMouseY2.Size = new System.Drawing.Size(19, 18);
            this.PickFromMouseY2.TabIndex = 20;
            this.PickFromMouseY2.Text = "^";
            this.PickFromMouseY2.UseVisualStyleBackColor = true;
            this.PickFromMouseY2.Click += new System.EventHandler(this.PickFromMouseY2_Click);
            // 
            // PickFromMouseY1
            // 
            this.PickFromMouseY1.Location = new System.Drawing.Point(105, 37);
            this.PickFromMouseY1.Name = "PickFromMouseY1";
            this.PickFromMouseY1.Size = new System.Drawing.Size(19, 18);
            this.PickFromMouseY1.TabIndex = 22;
            this.PickFromMouseY1.Text = "^";
            this.PickFromMouseY1.UseVisualStyleBackColor = true;
            this.PickFromMouseY1.Click += new System.EventHandler(this.PickFromMouseY1_Click);
            // 
            // PickFromMouseX1
            // 
            this.PickFromMouseX1.Location = new System.Drawing.Point(105, 11);
            this.PickFromMouseX1.Name = "PickFromMouseX1";
            this.PickFromMouseX1.Size = new System.Drawing.Size(19, 18);
            this.PickFromMouseX1.TabIndex = 21;
            this.PickFromMouseX1.Text = "^";
            this.PickFromMouseX1.UseVisualStyleBackColor = true;
            this.PickFromMouseX1.Click += new System.EventHandler(this.PickFromMouseX1_Click);
            // 
            // ExportBtn
            // 
            this.ExportBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExportBtn.Location = new System.Drawing.Point(12, 449);
            this.ExportBtn.Name = "ExportBtn";
            this.ExportBtn.Size = new System.Drawing.Size(75, 33);
            this.ExportBtn.TabIndex = 23;
            this.ExportBtn.Text = "Export";
            this.ExportBtn.UseVisualStyleBackColor = true;
            this.ExportBtn.Click += new System.EventHandler(this.ExportBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.EndYBox);
            this.panel1.Controls.Add(this.PickFromMouseY1);
            this.panel1.Controls.Add(this.StartXBox);
            this.panel1.Controls.Add(this.PickFromMouseX1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.PickFromMouseY2);
            this.panel1.Controls.Add(this.StartYBox);
            this.panel1.Controls.Add(this.PickFromMouseX2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.EndXBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(12, 326);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 98);
            this.panel1.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoEllipsis = true;
            this.label6.Location = new System.Drawing.Point(116, 440);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(250, 90);
            this.label6.TabIndex = 25;
            this.label6.Text = "label6";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(894, 539);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ExportBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.treeView1);
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "EXPORTER";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartXBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartYBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndYBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndXBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.NumericUpDown StartXBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown StartYBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown EndYBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown EndXBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button PickFromMouseX2;
        private System.Windows.Forms.Button PickFromMouseY2;
        private System.Windows.Forms.Button PickFromMouseY1;
        private System.Windows.Forms.Button PickFromMouseX1;
        private System.Windows.Forms.Button ExportBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
    }
}

