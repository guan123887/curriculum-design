﻿namespace test
{
    partial class frmSymbolSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSymbolSelector));
            this.btnMoreSymbols = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOutlineColor = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.lblOutlineColor = new System.Windows.Forms.Label();
            this.lblAngle = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.nudAngle = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ptbPreview = new System.Windows.Forms.PictureBox();
            this.SymbologyCtr = new ESRI.ArcGIS.Controls.AxSymbologyControl();
            this.btnOK = new System.Windows.Forms.Button();
            this.contextMenuStripMoreSymbol = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SymbologyCtr)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMoreSymbols
            // 
            this.btnMoreSymbols.Location = new System.Drawing.Point(435, 404);
            this.btnMoreSymbols.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoreSymbols.Name = "btnMoreSymbols";
            this.btnMoreSymbols.Size = new System.Drawing.Size(100, 29);
            this.btnMoreSymbols.TabIndex = 12;
            this.btnMoreSymbols.Text = "更多符号";
            this.btnMoreSymbols.UseVisualStyleBackColor = true;
            this.btnMoreSymbols.Click += new System.EventHandler(this.btnMoreSymbols_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(501, 368);
            this.btnCancle.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(100, 29);
            this.btnCancle.TabIndex = 11;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOutlineColor);
            this.groupBox2.Controls.Add(this.btnColor);
            this.groupBox2.Controls.Add(this.lblOutlineColor);
            this.groupBox2.Controls.Add(this.lblAngle);
            this.groupBox2.Controls.Add(this.lblWidth);
            this.groupBox2.Controls.Add(this.lblSize);
            this.groupBox2.Controls.Add(this.lblColor);
            this.groupBox2.Controls.Add(this.nudAngle);
            this.groupBox2.Controls.Add(this.nudWidth);
            this.groupBox2.Controls.Add(this.nudSize);
            this.groupBox2.Location = new System.Drawing.Point(370, 158);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(221, 202);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置";
            // 
            // btnOutlineColor
            // 
            this.btnOutlineColor.Location = new System.Drawing.Point(95, 166);
            this.btnOutlineColor.Margin = new System.Windows.Forms.Padding(4);
            this.btnOutlineColor.Name = "btnOutlineColor";
            this.btnOutlineColor.Size = new System.Drawing.Size(100, 29);
            this.btnOutlineColor.TabIndex = 14;
            this.btnOutlineColor.UseVisualStyleBackColor = true;
            this.btnOutlineColor.Click += new System.EventHandler(this.btnOutlineColor_Click);
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(95, 21);
            this.btnColor.Margin = new System.Windows.Forms.Padding(4);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(100, 29);
            this.btnColor.TabIndex = 13;
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // lblOutlineColor
            // 
            this.lblOutlineColor.AutoSize = true;
            this.lblOutlineColor.Location = new System.Drawing.Point(-3, 172);
            this.lblOutlineColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutlineColor.Name = "lblOutlineColor";
            this.lblOutlineColor.Size = new System.Drawing.Size(67, 15);
            this.lblOutlineColor.TabIndex = 12;
            this.lblOutlineColor.Text = "外框颜色";
            // 
            // lblAngle
            // 
            this.lblAngle.AutoSize = true;
            this.lblAngle.Location = new System.Drawing.Point(8, 138);
            this.lblAngle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAngle.Name = "lblAngle";
            this.lblAngle.Size = new System.Drawing.Size(37, 15);
            this.lblAngle.TabIndex = 11;
            this.lblAngle.Text = "角度";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(8, 92);
            this.lblWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(37, 15);
            this.lblWidth.TabIndex = 10;
            this.lblWidth.Text = "宽度";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(8, 58);
            this.lblSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(37, 15);
            this.lblSize.TabIndex = 9;
            this.lblSize.Text = "大小";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(8, 28);
            this.lblColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(37, 15);
            this.lblColor.TabIndex = 8;
            this.lblColor.Text = "颜色";
            // 
            // nudAngle
            // 
            this.nudAngle.Location = new System.Drawing.Point(77, 135);
            this.nudAngle.Margin = new System.Windows.Forms.Padding(4);
            this.nudAngle.Name = "nudAngle";
            this.nudAngle.Size = new System.Drawing.Size(136, 25);
            this.nudAngle.TabIndex = 7;
            this.nudAngle.ValueChanged += new System.EventHandler(this.nudAngle_ValueChanged);
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(77, 92);
            this.nudWidth.Margin = new System.Windows.Forms.Padding(4);
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(136, 25);
            this.nudWidth.TabIndex = 6;
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // nudSize
            // 
            this.nudSize.Location = new System.Drawing.Point(77, 58);
            this.nudSize.Margin = new System.Windows.Forms.Padding(4);
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(136, 25);
            this.nudSize.TabIndex = 5;
            this.nudSize.ValueChanged += new System.EventHandler(this.nudSize_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ptbPreview);
            this.groupBox1.Location = new System.Drawing.Point(362, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(229, 125);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "预览";
            // 
            // ptbPreview
            // 
            this.ptbPreview.Location = new System.Drawing.Point(8, 25);
            this.ptbPreview.Margin = new System.Windows.Forms.Padding(4);
            this.ptbPreview.Name = "ptbPreview";
            this.ptbPreview.Size = new System.Drawing.Size(213, 92);
            this.ptbPreview.TabIndex = 1;
            this.ptbPreview.TabStop = false;
            // 
            // SymbologyCtr
            // 
            this.SymbologyCtr.Location = new System.Drawing.Point(1, 0);
            this.SymbologyCtr.Margin = new System.Windows.Forms.Padding(4);
            this.SymbologyCtr.Name = "SymbologyCtr";
            this.SymbologyCtr.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("SymbologyCtr.OcxState")));
            this.SymbologyCtr.Size = new System.Drawing.Size(339, 441);
            this.SymbologyCtr.TabIndex = 7;
            this.SymbologyCtr.OnDoubleClick += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnDoubleClickEventHandler(this.SymbologyCtr_OnDoubleClick);
            this.SymbologyCtr.OnStyleClassChanged += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnStyleClassChangedEventHandler(this.SymbologyCtr_OnStyleClassChanged);
            this.SymbologyCtr.OnItemSelected += new ESRI.ArcGIS.Controls.ISymbologyControlEvents_Ax_OnItemSelectedEventHandler(this.SymbologyCtr_OnItemSelected);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(381, 368);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 29);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // contextMenuStripMoreSymbol
            // 
            this.contextMenuStripMoreSymbol.Name = "contextMenuStrip1";
            this.contextMenuStripMoreSymbol.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStripMoreSymbol.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // frmSymbolSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 447);
            this.Controls.Add(this.btnMoreSymbols);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SymbologyCtr);
            this.Controls.Add(this.btnOK);
            this.Name = "frmSymbolSelector";
            this.Text = "选择符号";
            this.Load += new System.EventHandler(this.frmSymbolSelector_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SymbologyCtr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMoreSymbols;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOutlineColor;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Label lblOutlineColor;
        private System.Windows.Forms.Label lblAngle;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.NumericUpDown nudAngle;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox ptbPreview;
        private ESRI.ArcGIS.Controls.AxSymbologyControl SymbologyCtr;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMoreSymbol;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}