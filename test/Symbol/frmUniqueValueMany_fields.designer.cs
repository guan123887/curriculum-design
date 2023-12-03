namespace test.Symbol
{
    partial class frmUniqueValueMany_fields
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSelLyr = new System.Windows.Forms.ComboBox();
            this.lstboxField = new System.Windows.Forms.ListBox();
            this.btnAddOne = new System.Windows.Forms.Button();
            this.btnDelOne = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupControl1 = new System.Windows.Forms.GroupBox();
            this.groupControl2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.groupControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "请选择图层";
            // 
            // cmbSelLyr
            // 
            this.cmbSelLyr.FormattingEnabled = true;
            this.cmbSelLyr.Location = new System.Drawing.Point(151, 48);
            this.cmbSelLyr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSelLyr.Name = "cmbSelLyr";
            this.cmbSelLyr.Size = new System.Drawing.Size(291, 23);
            this.cmbSelLyr.TabIndex = 1;
            this.cmbSelLyr.SelectedIndexChanged += new System.EventHandler(this.cmbSelLyr_SelectedIndexChanged);
            // 
            // lstboxField
            // 
            this.lstboxField.FormattingEnabled = true;
            this.lstboxField.ItemHeight = 15;
            this.lstboxField.Location = new System.Drawing.Point(8, 26);
            this.lstboxField.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstboxField.Name = "lstboxField";
            this.lstboxField.Size = new System.Drawing.Size(112, 184);
            this.lstboxField.TabIndex = 2;
            // 
            // btnAddOne
            // 
            this.btnAddOne.Location = new System.Drawing.Point(152, 175);
            this.btnAddOne.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddOne.Name = "btnAddOne";
            this.btnAddOne.Size = new System.Drawing.Size(73, 29);
            this.btnAddOne.TabIndex = 4;
            this.btnAddOne.Text = ">";
            this.btnAddOne.UseVisualStyleBackColor = true;
            this.btnAddOne.Click += new System.EventHandler(this.btnAddOne_Click);
            // 
            // btnDelOne
            // 
            this.btnDelOne.Location = new System.Drawing.Point(152, 278);
            this.btnDelOne.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelOne.Name = "btnDelOne";
            this.btnDelOne.Size = new System.Drawing.Size(73, 29);
            this.btnDelOne.TabIndex = 5;
            this.btnDelOne.Text = "<";
            this.btnDelOne.UseVisualStyleBackColor = true;
            this.btnDelOne.Click += new System.EventHandler(this.btnDelOne_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView.Location = new System.Drawing.Point(8, 26);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(289, 197);
            this.dataGridView.TabIndex = 6;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "字段";
            this.Column1.Name = "Column1";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(136, 359);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 29);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(332, 359);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 29);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lstboxField);
            this.groupControl1.Location = new System.Drawing.Point(13, 120);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl1.Size = new System.Drawing.Size(131, 231);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.TabStop = false;
            this.groupControl1.Text = "字段选择";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.dataGridView);
            this.groupControl2.Location = new System.Drawing.Point(243, 120);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupControl2.Size = new System.Drawing.Size(305, 231);
            this.groupControl2.TabIndex = 10;
            this.groupControl2.TabStop = false;
            this.groupControl2.Text = "渲染字段";
            // 
            // frmUniqueValueMany_fields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 467);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnDelOne);
            this.Controls.Add(this.btnAddOne);
            this.Controls.Add(this.cmbSelLyr);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmUniqueValueMany_fields";
            this.Text = "唯一值多字段";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSelLyr;
        private System.Windows.Forms.ListBox lstboxField;
        private System.Windows.Forms.Button btnAddOne;
        private System.Windows.Forms.Button btnDelOne;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupControl1;
        private System.Windows.Forms.GroupBox groupControl2;
    }
}