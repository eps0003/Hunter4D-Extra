
namespace vxlToCfgGui
{
    partial class Form1
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
            this.folderTree = new System.Windows.Forms.TreeView();
            this.fileList = new System.Windows.Forms.ListView();
            this.preview = new System.Windows.Forms.PictureBox();
            this.exportButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.preview)).BeginInit();
            this.SuspendLayout();
            // 
            // folderTree
            // 
            this.folderTree.Location = new System.Drawing.Point(13, 13);
            this.folderTree.Name = "folderTree";
            this.folderTree.Size = new System.Drawing.Size(200, 230);
            this.folderTree.TabIndex = 0;
            this.folderTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.folderTree_AfterSelect);
            // 
            // fileList
            // 
            this.fileList.HideSelection = false;
            this.fileList.Location = new System.Drawing.Point(12, 249);
            this.fileList.Name = "fileList";
            this.fileList.Size = new System.Drawing.Size(200, 230);
            this.fileList.TabIndex = 1;
            this.fileList.UseCompatibleStateImageBehavior = false;
            this.fileList.View = System.Windows.Forms.View.List;
            this.fileList.SelectedIndexChanged += new System.EventHandler(this.fileList_SelectedIndexChanged);
            // 
            // preview
            // 
            this.preview.BackColor = System.Drawing.SystemColors.Window;
            this.preview.Location = new System.Drawing.Point(219, 12);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(512, 512);
            this.preview.TabIndex = 2;
            this.preview.TabStop = false;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(13, 485);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(199, 39);
            this.exportButton.TabIndex = 3;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 536);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.fileList);
            this.Controls.Add(this.folderTree);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.preview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView folderTree;
        private System.Windows.Forms.ListView fileList;
        private System.Windows.Forms.PictureBox preview;
        private System.Windows.Forms.Button exportButton;
    }
}

