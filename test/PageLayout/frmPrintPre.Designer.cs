namespace test.PageLayout
{
    partial class frmPrintPre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintPre));
            this.btnPageSize = new System.Windows.Forms.Button();
            this.btnPrintpreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.PrintPagelayoutControl = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.PrintPagelayoutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPageSize
            // 
            this.btnPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPageSize.Location = new System.Drawing.Point(746, 76);
            this.btnPageSize.Name = "btnPageSize";
            this.btnPageSize.Size = new System.Drawing.Size(100, 29);
            this.btnPageSize.TabIndex = 4;
            this.btnPageSize.Text = "页面设置";
            this.btnPageSize.Click += new System.EventHandler(this.btnPageSize_Click);
            // 
            // btnPrintpreview
            // 
            this.btnPrintpreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintpreview.Location = new System.Drawing.Point(746, 188);
            this.btnPrintpreview.Name = "btnPrintpreview";
            this.btnPrintpreview.Size = new System.Drawing.Size(100, 29);
            this.btnPrintpreview.TabIndex = 5;
            this.btnPrintpreview.Text = "打印预览";
            this.btnPrintpreview.Click += new System.EventHandler(this.btnPrintpreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(746, 282);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 29);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // PrintPagelayoutControl
            // 
            this.PrintPagelayoutControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintPagelayoutControl.Location = new System.Drawing.Point(4, 12);
            this.PrintPagelayoutControl.Name = "PrintPagelayoutControl";
            this.PrintPagelayoutControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("PrintPagelayoutControl.OcxState")));
            this.PrintPagelayoutControl.Size = new System.Drawing.Size(830, 566);
            this.PrintPagelayoutControl.TabIndex = 9;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(636, 81);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 10;
            // 
            // frmPrintPre
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(854, 480);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPrintpreview);
            this.Controls.Add(this.btnPageSize);
            this.Controls.Add(this.PrintPagelayoutControl);
            this.Name = "frmPrintPre";
            this.Text = "打印";
            this.Load += new System.EventHandler(this.frmPrintPre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PrintPagelayoutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxPageLayoutControl PrintPagelayoutControl;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private System.Windows.Forms.Button btnPageSize;
        private System.Windows.Forms.Button btnPrintpreview;
        private System.Windows.Forms.Button btnPrint;
    }
}