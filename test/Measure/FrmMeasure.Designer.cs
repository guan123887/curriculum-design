namespace test
{
    partial class FrmMeasure
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMeasure));
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnLine = new System.Windows.Forms.ToolStripButton();
            this.tbtnArea = new System.Windows.Forms.ToolStripButton();
            this.tbtnFeature = new System.Windows.Forms.ToolStripButton();
            this.tbtnSum = new System.Windows.Forms.ToolStripButton();
            this.tbtnUnit = new System.Windows.Forms.ToolStripSplitButton();
            this.DistanceToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kiloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decimetersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.milesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AreaToolItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kiloAreaTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metersAreaTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.milesAreaTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(0, 29);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(4);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(387, 154);
            this.txtMessage.TabIndex = 3;
            this.txtMessage.Text = "请点击上面的按钮，选择测量线、面、要素或者设置";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnLine,
            this.tbtnArea,
            this.tbtnFeature,
            this.tbtnSum,
            this.tbtnUnit,
            this.tbtnClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(385, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnLine
            // 
            this.tbtnLine.CheckOnClick = true;
            this.tbtnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnLine.Image = ((System.Drawing.Image)(resources.GetObject("tbtnLine.Image")));
            this.tbtnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnLine.Name = "tbtnLine";
            this.tbtnLine.Size = new System.Drawing.Size(23, 22);
            this.tbtnLine.Text = "测量线";
            this.tbtnLine.ToolTipText = "测量线";
            this.tbtnLine.Click += new System.EventHandler(this.tbtnLine_Click);
            // 
            // tbtnArea
            // 
            this.tbtnArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnArea.Image = ((System.Drawing.Image)(resources.GetObject("tbtnArea.Image")));
            this.tbtnArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnArea.Name = "tbtnArea";
            this.tbtnArea.Size = new System.Drawing.Size(23, 22);
            this.tbtnArea.Text = "测量面";
            this.tbtnArea.Click += new System.EventHandler(this.tbtnArea_Click);
            // 
            // tbtnFeature
            // 
            this.tbtnFeature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnFeature.Image = ((System.Drawing.Image)(resources.GetObject("tbtnFeature.Image")));
            this.tbtnFeature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnFeature.Name = "tbtnFeature";
            this.tbtnFeature.Size = new System.Drawing.Size(23, 22);
            this.tbtnFeature.Text = "测量要素";
            this.tbtnFeature.Click += new System.EventHandler(this.tbtnFeature_Click);
            // 
            // tbtnSum
            // 
            this.tbtnSum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSum.Image = ((System.Drawing.Image)(resources.GetObject("tbtnSum.Image")));
            this.tbtnSum.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnSum.Name = "tbtnSum";
            this.tbtnSum.Size = new System.Drawing.Size(23, 22);
            this.tbtnSum.Text = "总和";
            this.tbtnSum.Click += new System.EventHandler(this.tbtnSum_Click);
            // 
            // tbtnUnit
            // 
            this.tbtnUnit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnUnit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DistanceToolItem,
            this.AreaToolItem});
            this.tbtnUnit.Image = ((System.Drawing.Image)(resources.GetObject("tbtnUnit.Image")));
            this.tbtnUnit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnUnit.Name = "tbtnUnit";
            this.tbtnUnit.Size = new System.Drawing.Size(32, 22);
            this.tbtnUnit.Text = "选择单位";
            this.tbtnUnit.ToolTipText = "选择单位";
            // 
            // DistanceToolItem
            // 
            this.DistanceToolItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kiloToolStripMenuItem,
            this.metersToolStripMenuItem,
            this.decimetersToolStripMenuItem,
            this.milesToolStripMenuItem});
            this.DistanceToolItem.Name = "DistanceToolItem";
            this.DistanceToolItem.Size = new System.Drawing.Size(108, 24);
            this.DistanceToolItem.Text = "距离";
            // 
            // kiloToolStripMenuItem
            // 
            this.kiloToolStripMenuItem.CheckOnClick = true;
            this.kiloToolStripMenuItem.Name = "kiloToolStripMenuItem";
            this.kiloToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.kiloToolStripMenuItem.Text = "Kilometers";
            this.kiloToolStripMenuItem.Click += new System.EventHandler(this.kiloToolStripMenuItem_Click);
            // 
            // metersToolStripMenuItem
            // 
            this.metersToolStripMenuItem.Name = "metersToolStripMenuItem";
            this.metersToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.metersToolStripMenuItem.Text = "Meters";
            this.metersToolStripMenuItem.Click += new System.EventHandler(this.metersToolStripMenuItem_Click);
            // 
            // decimetersToolStripMenuItem
            // 
            this.decimetersToolStripMenuItem.Name = "decimetersToolStripMenuItem";
            this.decimetersToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.decimetersToolStripMenuItem.Text = "Decimeters";
            this.decimetersToolStripMenuItem.Click += new System.EventHandler(this.decimetersToolStripMenuItem_Click);
            // 
            // milesToolStripMenuItem
            // 
            this.milesToolStripMenuItem.Name = "milesToolStripMenuItem";
            this.milesToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.milesToolStripMenuItem.Text = "Miles";
            this.milesToolStripMenuItem.Click += new System.EventHandler(this.milesToolStripMenuItem_Click);
            // 
            // AreaToolItem
            // 
            this.AreaToolItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kiloAreaTSMenuItem,
            this.metersAreaTSMenuItem,
            this.milesAreaTSMenuItem});
            this.AreaToolItem.Name = "AreaToolItem";
            this.AreaToolItem.Size = new System.Drawing.Size(108, 24);
            this.AreaToolItem.Text = "面积";
            // 
            // kiloAreaTSMenuItem
            // 
            this.kiloAreaTSMenuItem.Name = "kiloAreaTSMenuItem";
            this.kiloAreaTSMenuItem.Size = new System.Drawing.Size(157, 24);
            this.kiloAreaTSMenuItem.Text = "Kilometers";
            this.kiloAreaTSMenuItem.Click += new System.EventHandler(this.kiloAreaTSMenuItem_Click);
            // 
            // metersAreaTSMenuItem
            // 
            this.metersAreaTSMenuItem.Name = "metersAreaTSMenuItem";
            this.metersAreaTSMenuItem.Size = new System.Drawing.Size(157, 24);
            this.metersAreaTSMenuItem.Text = "Meters";
            this.metersAreaTSMenuItem.Click += new System.EventHandler(this.metersAreaTSMenuItem_Click);
            // 
            // milesAreaTSMenuItem
            // 
            this.milesAreaTSMenuItem.Name = "milesAreaTSMenuItem";
            this.milesAreaTSMenuItem.Size = new System.Drawing.Size(157, 24);
            this.milesAreaTSMenuItem.Text = "Miles";
            this.milesAreaTSMenuItem.Click += new System.EventHandler(this.milesAreaTSMenuItem_Click);
            // 
            // tbtnClear
            // 
            this.tbtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tbtnClear.Image")));
            this.tbtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnClear.Name = "tbtnClear";
            this.tbtnClear.Size = new System.Drawing.Size(23, 22);
            this.tbtnClear.Text = "清空";
            this.tbtnClear.Click += new System.EventHandler(this.tbtnClear_Click);
            // 
            // FrmMeasure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 183);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmMeasure";
            this.Text = "FrmMeasure";
            this.Load += new System.EventHandler(this.FrmMeasure_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tbtnLine;
        private System.Windows.Forms.ToolStripButton tbtnArea;
        private System.Windows.Forms.ToolStripButton tbtnFeature;
        private System.Windows.Forms.ToolStripButton tbtnSum;
        private System.Windows.Forms.ToolStripSplitButton tbtnUnit;
        private System.Windows.Forms.ToolStripMenuItem DistanceToolItem;
        private System.Windows.Forms.ToolStripMenuItem kiloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem decimetersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem milesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AreaToolItem;
        private System.Windows.Forms.ToolStripMenuItem kiloAreaTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem metersAreaTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem milesAreaTSMenuItem;
        private System.Windows.Forms.ToolStripButton tbtnClear;
    }
}