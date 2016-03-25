namespace NAB.K2.SharePointSearchEditor
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.buttonEditFolder = new System.Windows.Forms.Button();
            this.buttonDelFolder = new System.Windows.Forms.Button();
            this.buttonNewFolder = new System.Windows.Forms.Button();
            this.treeFolders = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.buttonCopyQuery = new System.Windows.Forms.Button();
            this.listQueries = new System.Windows.Forms.ListView();
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colColumns = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colParameters = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonDeleteQuery = new System.Windows.Forms.Button();
            this.buttonEditQuery = new System.Windows.Forms.Button();
            this.buttonNewQuery = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelFilename = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonNewDocument = new System.Windows.Forms.ToolStripButton();
            this.buttonSave = new System.Windows.Forms.ToolStripButton();
            this.buttonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonConfigSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonConnect = new System.Windows.Forms.ToolStripButton();
            this.dialogOpen = new System.Windows.Forms.OpenFileDialog();
            this.dialogSave = new System.Windows.Forms.SaveFileDialog();
            this.labelName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitMain.Location = new System.Drawing.Point(12, 101);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.buttonEditFolder);
            this.splitMain.Panel1.Controls.Add(this.buttonDelFolder);
            this.splitMain.Panel1.Controls.Add(this.buttonNewFolder);
            this.splitMain.Panel1.Controls.Add(this.treeFolders);
            this.splitMain.Panel1MinSize = 210;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.buttonCopyQuery);
            this.splitMain.Panel2.Controls.Add(this.listQueries);
            this.splitMain.Panel2.Controls.Add(this.buttonDeleteQuery);
            this.splitMain.Panel2.Controls.Add(this.buttonEditQuery);
            this.splitMain.Panel2.Controls.Add(this.buttonNewQuery);
            this.splitMain.Panel2MinSize = 450;
            this.splitMain.Size = new System.Drawing.Size(1466, 688);
            this.splitMain.SplitterDistance = 488;
            this.splitMain.SplitterWidth = 8;
            this.splitMain.TabIndex = 2;
            // 
            // buttonEditFolder
            // 
            this.buttonEditFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEditFolder.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditFolder.Image")));
            this.buttonEditFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditFolder.Location = new System.Drawing.Point(109, 644);
            this.buttonEditFolder.Name = "buttonEditFolder";
            this.buttonEditFolder.Size = new System.Drawing.Size(99, 41);
            this.buttonEditFolder.TabIndex = 10;
            this.buttonEditFolder.Text = "Edit";
            this.buttonEditFolder.UseVisualStyleBackColor = true;
            this.buttonEditFolder.Click += new System.EventHandler(this.btnEditFolder_Click);
            // 
            // buttonDelFolder
            // 
            this.buttonDelFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelFolder.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelFolder.Image")));
            this.buttonDelFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDelFolder.Location = new System.Drawing.Point(214, 644);
            this.buttonDelFolder.Name = "buttonDelFolder";
            this.buttonDelFolder.Size = new System.Drawing.Size(99, 41);
            this.buttonDelFolder.TabIndex = 2;
            this.buttonDelFolder.Text = "Delete";
            this.buttonDelFolder.UseVisualStyleBackColor = true;
            this.buttonDelFolder.Click += new System.EventHandler(this.buttonDelFolder_Click);
            // 
            // buttonNewFolder
            // 
            this.buttonNewFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNewFolder.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewFolder.Image")));
            this.buttonNewFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNewFolder.Location = new System.Drawing.Point(4, 644);
            this.buttonNewFolder.Name = "buttonNewFolder";
            this.buttonNewFolder.Size = new System.Drawing.Size(99, 41);
            this.buttonNewFolder.TabIndex = 1;
            this.buttonNewFolder.Text = "New";
            this.buttonNewFolder.UseVisualStyleBackColor = true;
            this.buttonNewFolder.Click += new System.EventHandler(this.buttonNewFolder_Click);
            // 
            // treeFolders
            // 
            this.treeFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeFolders.HideSelection = false;
            this.treeFolders.ImageIndex = 0;
            this.treeFolders.ImageList = this.imgList;
            this.treeFolders.Location = new System.Drawing.Point(4, 3);
            this.treeFolders.Name = "treeFolders";
            this.treeFolders.SelectedImageIndex = 0;
            this.treeFolders.Size = new System.Drawing.Size(481, 635);
            this.treeFolders.TabIndex = 0;
            this.treeFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFolders_AfterSelect);
            this.treeFolders.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeFolders_NodeMouseClick);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "folder");
            this.imgList.Images.SetKeyName(1, "folder_tool");
            this.imgList.Images.SetKeyName(2, "query");
            this.imgList.Images.SetKeyName(3, "page");
            // 
            // buttonCopyQuery
            // 
            this.buttonCopyQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCopyQuery.Image = ((System.Drawing.Image)(resources.GetObject("buttonCopyQuery.Image")));
            this.buttonCopyQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCopyQuery.Location = new System.Drawing.Point(365, 644);
            this.buttonCopyQuery.Name = "buttonCopyQuery";
            this.buttonCopyQuery.Size = new System.Drawing.Size(114, 41);
            this.buttonCopyQuery.TabIndex = 12;
            this.buttonCopyQuery.Text = "Duplicate";
            this.buttonCopyQuery.UseVisualStyleBackColor = true;
            this.buttonCopyQuery.Click += new System.EventHandler(this.buttonCopyQuery_Click);
            // 
            // listQueries
            // 
            this.listQueries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listQueries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCode,
            this.colName,
            this.colDescription,
            this.colColumns,
            this.colParameters});
            this.listQueries.FullRowSelect = true;
            this.listQueries.HideSelection = false;
            this.listQueries.Location = new System.Drawing.Point(3, 3);
            this.listQueries.Name = "listQueries";
            this.listQueries.Size = new System.Drawing.Size(948, 635);
            this.listQueries.TabIndex = 11;
            this.listQueries.UseCompatibleStateImageBehavior = false;
            this.listQueries.View = System.Windows.Forms.View.Details;
            this.listQueries.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listQueries_MouseDoubleClick);
            // 
            // colCode
            // 
            this.colCode.Text = "Id";
            this.colCode.Width = 111;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 256;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 399;
            // 
            // colColumns
            // 
            this.colColumns.Text = "Columns";
            this.colColumns.Width = 85;
            // 
            // colParameters
            // 
            this.colParameters.Text = "Parameters";
            this.colParameters.Width = 98;
            // 
            // buttonDeleteQuery
            // 
            this.buttonDeleteQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteQuery.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteQuery.Image")));
            this.buttonDeleteQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonDeleteQuery.Location = new System.Drawing.Point(213, 644);
            this.buttonDeleteQuery.Name = "buttonDeleteQuery";
            this.buttonDeleteQuery.Size = new System.Drawing.Size(99, 41);
            this.buttonDeleteQuery.TabIndex = 10;
            this.buttonDeleteQuery.Text = "Delete";
            this.buttonDeleteQuery.UseVisualStyleBackColor = true;
            this.buttonDeleteQuery.Click += new System.EventHandler(this.buttonDeleteQuery_Click);
            // 
            // buttonEditQuery
            // 
            this.buttonEditQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEditQuery.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditQuery.Image")));
            this.buttonEditQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditQuery.Location = new System.Drawing.Point(108, 644);
            this.buttonEditQuery.Name = "buttonEditQuery";
            this.buttonEditQuery.Size = new System.Drawing.Size(99, 41);
            this.buttonEditQuery.TabIndex = 9;
            this.buttonEditQuery.Text = "Edit";
            this.buttonEditQuery.UseVisualStyleBackColor = true;
            this.buttonEditQuery.Click += new System.EventHandler(this.buttonEditQuery_Click);
            // 
            // buttonNewQuery
            // 
            this.buttonNewQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNewQuery.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewQuery.Image")));
            this.buttonNewQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonNewQuery.Location = new System.Drawing.Point(3, 644);
            this.buttonNewQuery.Name = "buttonNewQuery";
            this.buttonNewQuery.Size = new System.Drawing.Size(99, 41);
            this.buttonNewQuery.TabIndex = 8;
            this.buttonNewQuery.Text = "New";
            this.buttonNewQuery.UseVisualStyleBackColor = true;
            this.buttonNewQuery.Click += new System.EventHandler(this.buttonNewQuery_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelFilename});
            this.statusStrip1.Location = new System.Drawing.Point(0, 789);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1490, 25);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelFilename
            // 
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(137, 20);
            this.labelFilename.Text = "C:\\Temp\\Filesname";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonNewDocument,
            this.buttonSave,
            this.buttonOpen,
            this.toolStripSeparator1,
            this.buttonConfigSettings,
            this.toolStripSeparator2,
            this.buttonConnect});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1490, 39);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonNewDocument
            // 
            this.buttonNewDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonNewDocument.Image = ((System.Drawing.Image)(resources.GetObject("buttonNewDocument.Image")));
            this.buttonNewDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNewDocument.Name = "buttonNewDocument";
            this.buttonNewDocument.Size = new System.Drawing.Size(36, 36);
            this.buttonNewDocument.Text = "New Configuration";
            this.buttonNewDocument.Click += new System.EventHandler(this.buttonNewDocument_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(36, 36);
            this.buttonSave.Text = "Save";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonOpen.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpen.Image")));
            this.buttonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(36, 36);
            this.buttonOpen.Text = "Open";
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // buttonConfigSettings
            // 
            this.buttonConfigSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonConfigSettings.Image = ((System.Drawing.Image)(resources.GetObject("buttonConfigSettings.Image")));
            this.buttonConfigSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonConfigSettings.Name = "buttonConfigSettings";
            this.buttonConfigSettings.Size = new System.Drawing.Size(36, 36);
            this.buttonConfigSettings.Text = "Configuration File Properties";
            this.buttonConfigSettings.Click += new System.EventHandler(this.buttonConfigSettings_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // buttonConnect
            // 
            this.buttonConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonConnect.Image = ((System.Drawing.Image)(resources.GetObject("buttonConnect.Image")));
            this.buttonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(36, 36);
            this.buttonConnect.Text = "Design Time Connection";
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // dialogOpen
            // 
            this.dialogOpen.DefaultExt = "json";
            this.dialogOpen.Filter = "Conf Files|*.conf|All files|*.*";
            this.dialogOpen.Title = "Load Query Configuration";
            // 
            // dialogSave
            // 
            this.dialogSave.DefaultExt = "json";
            this.dialogSave.Filter = "Conf Files|*.conf|All files|*.*";
            this.dialogSave.Title = "Save Query Configuration";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(9, 39);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(345, 38);
            this.labelName.TabIndex = 5;
            this.labelName.Text = "Name of the configuration";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(13, 77);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(108, 17);
            this.labelVersion.TabIndex = 6;
            this.labelVersion.Text = "Version: 1.0.0.0";
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1490, 814);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "NAB K2 SharePoint Search Broker Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel labelFilename;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonSave;
        private System.Windows.Forms.ToolStripButton buttonOpen;
        private System.Windows.Forms.OpenFileDialog dialogOpen;
        private System.Windows.Forms.SaveFileDialog dialogSave;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.TreeView treeFolders;
        private System.Windows.Forms.Button buttonDelFolder;
        private System.Windows.Forms.Button buttonNewFolder;
        private System.Windows.Forms.ListView listQueries;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ColumnHeader colColumns;
        private System.Windows.Forms.ColumnHeader colParameters;
        private System.Windows.Forms.Button buttonDeleteQuery;
        private System.Windows.Forms.Button buttonEditQuery;
        private System.Windows.Forms.Button buttonNewQuery;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button buttonEditFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonConfigSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton buttonConnect;
        private System.Windows.Forms.Button buttonCopyQuery;
        private System.Windows.Forms.ToolStripButton buttonNewDocument;
    }
}

