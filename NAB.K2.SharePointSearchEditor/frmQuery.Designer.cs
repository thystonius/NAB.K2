namespace NAB.K2.SharePointSearchEditor
{
    partial class frmQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuery));
            this.label1 = new System.Windows.Forms.Label();
            this.txtQueryId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbEngine = new System.Windows.Forms.ComboBox();
            this.cmbFolder = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRows = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabProps = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.gridRequestColumns = new System.Windows.Forms.DataGridView();
            this.colReqColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonDetectParams = new System.Windows.Forms.Button();
            this.gridParameters = new System.Windows.Forms.DataGridView();
            this.colParamId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colParamRequired = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colParamDefaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamIsCalculated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colParamCalculation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParamDesignValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonColumnsAll = new System.Windows.Forms.Button();
            this.buttonColumnsNone = new System.Windows.Forms.Button();
            this.buttonDetectCols = new System.Windows.Forms.Button();
            this.gridColumns = new System.Windows.Forms.DataGridView();
            this.colColSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColDisplay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColSMOType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colColInlcude = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonAddParam = new System.Windows.Forms.Button();
            this.txtCacheSeconds = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCacheResult = new System.Windows.Forms.Label();
            this.txtSiteUrl = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonMakeCAML = new System.Windows.Forms.Button();
            this.buttonHelpCAML = new System.Windows.Forms.Button();
            this.buttonHelpKQL = new System.Windows.Forms.Button();
            this.buttonClearColumns = new System.Windows.Forms.Button();
            this.checkFQL = new System.Windows.Forms.CheckBox();
            this.tabProps.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRequestColumns)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridParameters)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridColumns)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Query Id:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQueryId
            // 
            this.txtQueryId.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQueryId.Location = new System.Drawing.Point(116, 12);
            this.txtQueryId.MaxLength = 100;
            this.txtQueryId.Name = "txtQueryId";
            this.txtQueryId.Size = new System.Drawing.Size(231, 30);
            this.txtQueryId.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(443, 12);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(539, 30);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(377, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(116, 51);
            this.txtDescription.MaxLength = 255;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(866, 30);
            this.txtDescription.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(44, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Engine:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbEngine
            // 
            this.cmbEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEngine.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEngine.FormattingEnabled = true;
            this.cmbEngine.Location = new System.Drawing.Point(116, 210);
            this.cmbEngine.Name = "cmbEngine";
            this.cmbEngine.Size = new System.Drawing.Size(354, 31);
            this.cmbEngine.TabIndex = 6;
            // 
            // cmbFolder
            // 
            this.cmbFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFolder.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFolder.FormattingEnabled = true;
            this.cmbFolder.Location = new System.Drawing.Point(116, 173);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(354, 31);
            this.cmbFolder.TabIndex = 5;
            this.cmbFolder.Click += new System.EventHandler(this.cmbFolder_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(49, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Folder:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQuery
            // 
            this.txtQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuery.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.Location = new System.Drawing.Point(76, 265);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuery.Size = new System.Drawing.Size(981, 235);
            this.txtQuery.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 23);
            this.label6.TabIndex = 13;
            this.label6.Text = "Query:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRows
            // 
            this.txtRows.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRows.Location = new System.Drawing.Point(685, 173);
            this.txtRows.MaxLength = 5;
            this.txtRows.Name = "txtRows";
            this.txtRows.Size = new System.Drawing.Size(134, 30);
            this.txtRows.TabIndex = 7;
            this.txtRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(589, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 23);
            this.label7.TabIndex = 15;
            this.label7.Text = "Max Rows:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabProps
            // 
            this.tabProps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabProps.Controls.Add(this.tabPage3);
            this.tabProps.Controls.Add(this.tabPage1);
            this.tabProps.Controls.Add(this.tabPage2);
            this.tabProps.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabProps.Location = new System.Drawing.Point(12, 519);
            this.tabProps.Name = "tabProps";
            this.tabProps.SelectedIndex = 0;
            this.tabProps.Size = new System.Drawing.Size(1160, 373);
            this.tabProps.TabIndex = 12;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.gridRequestColumns);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Location = new System.Drawing.Point(4, 32);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1152, 337);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Request Columns";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // gridRequestColumns
            // 
            this.gridRequestColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRequestColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRequestColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colReqColName});
            this.gridRequestColumns.Location = new System.Drawing.Point(3, 29);
            this.gridRequestColumns.Name = "gridRequestColumns";
            this.gridRequestColumns.RowTemplate.Height = 24;
            this.gridRequestColumns.Size = new System.Drawing.Size(1063, 265);
            this.gridRequestColumns.TabIndex = 1;
            // 
            // colReqColName
            // 
            this.colReqColName.HeaderText = "Column Name";
            this.colReqColName.MaxInputLength = 128;
            this.colReqColName.Name = "colReqColName";
            this.colReqColName.Width = 500;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(820, 23);
            this.label12.TabIndex = 0;
            this.label12.Text = "Columns used during query operation to retrieve more information.  Currently only" +
    " used by Search Engine";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonDetectParams);
            this.tabPage1.Controls.Add(this.gridParameters);
            this.tabPage1.Location = new System.Drawing.Point(4, 32);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1152, 337);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Parameters";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonDetectParams
            // 
            this.buttonDetectParams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDetectParams.Location = new System.Drawing.Point(6, 297);
            this.buttonDetectParams.Name = "buttonDetectParams";
            this.buttonDetectParams.Size = new System.Drawing.Size(107, 30);
            this.buttonDetectParams.TabIndex = 0;
            this.buttonDetectParams.Text = "Detect";
            this.buttonDetectParams.UseVisualStyleBackColor = true;
            this.buttonDetectParams.Click += new System.EventHandler(this.buttonDetectParams_Click);
            // 
            // gridParameters
            // 
            this.gridParameters.AllowUserToOrderColumns = true;
            this.gridParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridParameters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParamId,
            this.colParamName,
            this.colParamDescription,
            this.colParamType,
            this.colParamRequired,
            this.colParamDefaultValue,
            this.colParamIsCalculated,
            this.colParamCalculation,
            this.colParamDesignValue});
            this.gridParameters.Location = new System.Drawing.Point(3, 3);
            this.gridParameters.Name = "gridParameters";
            this.gridParameters.RowTemplate.Height = 32;
            this.gridParameters.Size = new System.Drawing.Size(1124, 288);
            this.gridParameters.TabIndex = 1;
            this.gridParameters.TabStop = false;
            // 
            // colParamId
            // 
            this.colParamId.HeaderText = "Id";
            this.colParamId.MaxInputLength = 128;
            this.colParamId.Name = "colParamId";
            this.colParamId.Width = 80;
            // 
            // colParamName
            // 
            this.colParamName.HeaderText = "Name";
            this.colParamName.MaxInputLength = 60;
            this.colParamName.Name = "colParamName";
            this.colParamName.Width = 175;
            // 
            // colParamDescription
            // 
            this.colParamDescription.HeaderText = "Description";
            this.colParamDescription.MaxInputLength = 128;
            this.colParamDescription.Name = "colParamDescription";
            this.colParamDescription.Width = 200;
            // 
            // colParamType
            // 
            this.colParamType.HeaderText = "Type";
            this.colParamType.Name = "colParamType";
            // 
            // colParamRequired
            // 
            this.colParamRequired.FalseValue = "";
            this.colParamRequired.HeaderText = "Required";
            this.colParamRequired.Name = "colParamRequired";
            this.colParamRequired.TrueValue = "";
            this.colParamRequired.Width = 80;
            // 
            // colParamDefaultValue
            // 
            this.colParamDefaultValue.HeaderText = "Default";
            this.colParamDefaultValue.Name = "colParamDefaultValue";
            this.colParamDefaultValue.Width = 200;
            // 
            // colParamIsCalculated
            // 
            this.colParamIsCalculated.HeaderText = "Calculated";
            this.colParamIsCalculated.Name = "colParamIsCalculated";
            this.colParamIsCalculated.Width = 95;
            // 
            // colParamCalculation
            // 
            this.colParamCalculation.HeaderText = "Calculation";
            this.colParamCalculation.Name = "colParamCalculation";
            this.colParamCalculation.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colParamCalculation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colParamCalculation.Width = 200;
            // 
            // colParamDesignValue
            // 
            this.colParamDesignValue.HeaderText = "Design Value";
            this.colParamDesignValue.Name = "colParamDesignValue";
            this.colParamDesignValue.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonClearColumns);
            this.tabPage2.Controls.Add(this.buttonColumnsAll);
            this.tabPage2.Controls.Add(this.buttonColumnsNone);
            this.tabPage2.Controls.Add(this.buttonDetectCols);
            this.tabPage2.Controls.Add(this.gridColumns);
            this.tabPage2.Location = new System.Drawing.Point(4, 32);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1152, 337);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Columns";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonColumnsAll
            // 
            this.buttonColumnsAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonColumnsAll.Location = new System.Drawing.Point(960, 300);
            this.buttonColumnsAll.Name = "buttonColumnsAll";
            this.buttonColumnsAll.Size = new System.Drawing.Size(78, 30);
            this.buttonColumnsAll.TabIndex = 22;
            this.buttonColumnsAll.Text = "All";
            this.buttonColumnsAll.UseVisualStyleBackColor = true;
            this.buttonColumnsAll.Click += new System.EventHandler(this.buttonColumnsAll_Click);
            // 
            // buttonColumnsNone
            // 
            this.buttonColumnsNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonColumnsNone.Location = new System.Drawing.Point(1044, 300);
            this.buttonColumnsNone.Name = "buttonColumnsNone";
            this.buttonColumnsNone.Size = new System.Drawing.Size(80, 30);
            this.buttonColumnsNone.TabIndex = 21;
            this.buttonColumnsNone.Text = "None";
            this.buttonColumnsNone.UseVisualStyleBackColor = true;
            this.buttonColumnsNone.Click += new System.EventHandler(this.buttonColumnsNone_Click);
            // 
            // buttonDetectCols
            // 
            this.buttonDetectCols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDetectCols.Location = new System.Drawing.Point(6, 301);
            this.buttonDetectCols.Name = "buttonDetectCols";
            this.buttonDetectCols.Size = new System.Drawing.Size(107, 30);
            this.buttonDetectCols.TabIndex = 20;
            this.buttonDetectCols.Text = "Detect";
            this.buttonDetectCols.UseVisualStyleBackColor = true;
            this.buttonDetectCols.Click += new System.EventHandler(this.buttonDetectCols_Click);
            // 
            // gridColumns
            // 
            this.gridColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColSource,
            this.colColName,
            this.colColDisplay,
            this.colColDescription,
            this.colColDataType,
            this.colColSMOType,
            this.colColInlcude});
            this.gridColumns.Location = new System.Drawing.Point(3, 3);
            this.gridColumns.Name = "gridColumns";
            this.gridColumns.RowTemplate.Height = 32;
            this.gridColumns.Size = new System.Drawing.Size(1124, 291);
            this.gridColumns.TabIndex = 0;
            this.gridColumns.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.gridColumns_DataError);
            // 
            // colColSource
            // 
            this.colColSource.HeaderText = "Source";
            this.colColSource.MaxInputLength = 60;
            this.colColSource.Name = "colColSource";
            this.colColSource.ToolTipText = "Name of the column at the source";
            this.colColSource.Width = 120;
            // 
            // colColName
            // 
            this.colColName.HeaderText = "Name";
            this.colColName.MaxInputLength = 60;
            this.colColName.Name = "colColName";
            this.colColName.ToolTipText = "Column Name that will be provided by SMO";
            this.colColName.Width = 120;
            // 
            // colColDisplay
            // 
            this.colColDisplay.HeaderText = "Display Name";
            this.colColDisplay.MaxInputLength = 255;
            this.colColDisplay.Name = "colColDisplay";
            this.colColDisplay.Width = 160;
            // 
            // colColDescription
            // 
            this.colColDescription.HeaderText = "Description";
            this.colColDescription.MaxInputLength = 255;
            this.colColDescription.Name = "colColDescription";
            this.colColDescription.Width = 200;
            // 
            // colColDataType
            // 
            this.colColDataType.HeaderText = "Source Type";
            this.colColDataType.MaxInputLength = 255;
            this.colColDataType.Name = "colColDataType";
            this.colColDataType.Width = 200;
            // 
            // colColSMOType
            // 
            this.colColSMOType.HeaderText = "SO Type";
            this.colColSMOType.MaxDropDownItems = 10;
            this.colColSMOType.Name = "colColSMOType";
            this.colColSMOType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colColSMOType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colColSMOType.Width = 160;
            // 
            // colColInlcude
            // 
            this.colColInlcude.HeaderText = "Include";
            this.colColInlcude.Name = "colColInlcude";
            this.colColInlcude.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colColInlcude.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // txtTimeout
            // 
            this.txtTimeout.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimeout.Location = new System.Drawing.Point(685, 211);
            this.txtTimeout.MaxLength = 3;
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(134, 30);
            this.txtTimeout.TabIndex = 8;
            this.txtTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(526, 214);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(153, 23);
            this.label8.TabIndex = 18;
            this.label8.Text = "Timeout (seconds):";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtURL
            // 
            this.txtURL.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtURL.Location = new System.Drawing.Point(116, 127);
            this.txtURL.MaxLength = 255;
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(866, 30);
            this.txtURL.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(2, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 23);
            this.label9.TabIndex = 20;
            this.label9.Text = "Relative URL:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(1042, 898);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(130, 45);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTest.Image = global::NAB.K2.SharePointSearchEditor.Properties.Resources.table_lightning;
            this.buttonTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTest.Location = new System.Drawing.Point(11, 898);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(107, 45);
            this.buttonTest.TabIndex = 13;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Image = ((System.Drawing.Image)(resources.GetObject("buttonOk.Image")));
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(906, 898);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(130, 45);
            this.buttonOk.TabIndex = 14;
            this.buttonOk.Text = "&OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonAddParam
            // 
            this.buttonAddParam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddParam.Location = new System.Drawing.Point(1063, 468);
            this.buttonAddParam.Name = "buttonAddParam";
            this.buttonAddParam.Size = new System.Drawing.Size(109, 32);
            this.buttonAddParam.TabIndex = 11;
            this.buttonAddParam.Text = "Add Param";
            this.buttonAddParam.UseVisualStyleBackColor = true;
            this.buttonAddParam.Click += new System.EventHandler(this.buttonAddParam_Click);
            // 
            // txtCacheSeconds
            // 
            this.txtCacheSeconds.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCacheSeconds.Location = new System.Drawing.Point(971, 211);
            this.txtCacheSeconds.MaxLength = 6;
            this.txtCacheSeconds.Name = "txtCacheSeconds";
            this.txtCacheSeconds.Size = new System.Drawing.Size(89, 30);
            this.txtCacheSeconds.TabIndex = 9;
            this.txtCacheSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCacheSeconds.TextChanged += new System.EventHandler(this.txtCacheSeconds_TextChanged);
            this.txtCacheSeconds.Leave += new System.EventHandler(this.txtCacheSeconds_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(828, 214);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 23);
            this.label10.TabIndex = 23;
            this.label10.Text = "Cache (seconds):";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCacheResult
            // 
            this.lblCacheResult.AutoSize = true;
            this.lblCacheResult.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCacheResult.Location = new System.Drawing.Point(1066, 214);
            this.lblCacheResult.Name = "lblCacheResult";
            this.lblCacheResult.Size = new System.Drawing.Size(72, 23);
            this.lblCacheResult.TabIndex = 25;
            this.lblCacheResult.Text = "00:00:00";
            this.lblCacheResult.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSiteUrl
            // 
            this.txtSiteUrl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSiteUrl.Location = new System.Drawing.Point(116, 87);
            this.txtSiteUrl.MaxLength = 255;
            this.txtSiteUrl.Name = "txtSiteUrl";
            this.txtSiteUrl.Size = new System.Drawing.Size(487, 30);
            this.txtSiteUrl.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(30, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 23);
            this.label11.TabIndex = 27;
            this.label11.Text = "Site URL:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonMakeCAML
            // 
            this.buttonMakeCAML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMakeCAML.Location = new System.Drawing.Point(1063, 265);
            this.buttonMakeCAML.Name = "buttonMakeCAML";
            this.buttonMakeCAML.Size = new System.Drawing.Size(109, 32);
            this.buttonMakeCAML.TabIndex = 28;
            this.buttonMakeCAML.Text = "Gen. CAML";
            this.buttonMakeCAML.UseVisualStyleBackColor = true;
            this.buttonMakeCAML.Click += new System.EventHandler(this.buttonMakeCAML_Click);
            // 
            // buttonHelpCAML
            // 
            this.buttonHelpCAML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelpCAML.Location = new System.Drawing.Point(1063, 303);
            this.buttonHelpCAML.Name = "buttonHelpCAML";
            this.buttonHelpCAML.Size = new System.Drawing.Size(109, 32);
            this.buttonHelpCAML.TabIndex = 29;
            this.buttonHelpCAML.Text = "CAML Help";
            this.buttonHelpCAML.UseVisualStyleBackColor = true;
            this.buttonHelpCAML.Click += new System.EventHandler(this.buttonHelpCAML_Click);
            // 
            // buttonHelpKQL
            // 
            this.buttonHelpKQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelpKQL.Location = new System.Drawing.Point(1063, 341);
            this.buttonHelpKQL.Name = "buttonHelpKQL";
            this.buttonHelpKQL.Size = new System.Drawing.Size(109, 32);
            this.buttonHelpKQL.TabIndex = 30;
            this.buttonHelpKQL.Text = "KQL/FQL Help";
            this.buttonHelpKQL.UseVisualStyleBackColor = true;
            this.buttonHelpKQL.Click += new System.EventHandler(this.buttonHelpKQL_Click);
            // 
            // buttonClearColumns
            // 
            this.buttonClearColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClearColumns.Location = new System.Drawing.Point(119, 300);
            this.buttonClearColumns.Name = "buttonClearColumns";
            this.buttonClearColumns.Size = new System.Drawing.Size(107, 30);
            this.buttonClearColumns.TabIndex = 23;
            this.buttonClearColumns.Text = "Clear";
            this.buttonClearColumns.UseVisualStyleBackColor = true;
            this.buttonClearColumns.Click += new System.EventHandler(this.buttonClearColumns_Click);
            // 
            // checkFQL
            // 
            this.checkFQL.AutoSize = true;
            this.checkFQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkFQL.Location = new System.Drawing.Point(832, 180);
            this.checkFQL.Name = "checkFQL";
            this.checkFQL.Size = new System.Drawing.Size(99, 24);
            this.checkFQL.TabIndex = 31;
            this.checkFQL.Text = "Use FQL";
            this.checkFQL.UseVisualStyleBackColor = true;
            this.checkFQL.Visible = false;
            // 
            // frmQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1184, 955);
            this.Controls.Add(this.checkFQL);
            this.Controls.Add(this.buttonHelpKQL);
            this.Controls.Add(this.buttonHelpCAML);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.buttonMakeCAML);
            this.Controls.Add(this.txtSiteUrl);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblCacheResult);
            this.Controls.Add(this.txtCacheSeconds);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonAddParam);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtTimeout);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tabProps);
            this.Controls.Add(this.txtRows);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbFolder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbEngine);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtQueryId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.label6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Query: <new query>";
            this.Load += new System.EventHandler(this.frmQuery_Load);
            this.tabProps.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRequestColumns)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridParameters)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridColumns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQueryId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbEngine;
        private System.Windows.Forms.ComboBox cmbFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRows;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabProps;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView gridParameters;
        private System.Windows.Forms.DataGridView gridColumns;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonDetectCols;
        private System.Windows.Forms.Button buttonDetectParams;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColDataType;
        private System.Windows.Forms.DataGridViewComboBoxColumn colColSMOType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colColInlcude;
        private System.Windows.Forms.Button buttonAddParam;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamDescription;
        private System.Windows.Forms.DataGridViewComboBoxColumn colParamType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colParamRequired;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamDefaultValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colParamIsCalculated;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamCalculation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParamDesignValue;
        private System.Windows.Forms.TextBox txtCacheSeconds;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCacheResult;
        private System.Windows.Forms.TextBox txtSiteUrl;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonColumnsAll;
        private System.Windows.Forms.Button buttonColumnsNone;
        private System.Windows.Forms.Button buttonMakeCAML;
        private System.Windows.Forms.Button buttonHelpCAML;
        private System.Windows.Forms.Button buttonHelpKQL;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView gridRequestColumns;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReqColName;
        private System.Windows.Forms.Button buttonClearColumns;
        private System.Windows.Forms.CheckBox checkFQL;
    }
}