﻿namespace test.Symbol
{
    partial class frmSimpleRender
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.colorEditSimple = new System.Windows.Forms.ComboBox();
            this.cmbSelLayer = new System.Windows.Forms.ComboBox();
            this.labelControl2 = new System.Windows.Forms.Label();
            this.labelControl1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(307, 200);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 29);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(142, 200);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 29);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // colorEditSimple
            // 
            this.colorEditSimple.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.colorEditSimple.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorEditSimple.FormattingEnabled = true;
            this.colorEditSimple.Location = new System.Drawing.Point(142, 138);
            this.colorEditSimple.Margin = new System.Windows.Forms.Padding(4);
            this.colorEditSimple.Name = "colorEditSimple";
            this.colorEditSimple.Size = new System.Drawing.Size(265, 26);
            this.colorEditSimple.TabIndex = 9;
            this.colorEditSimple.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.colorEditSimple_DrawItem);
            this.colorEditSimple.SelectedIndexChanged += new System.EventHandler(this.colorEditSimple_SelectedIndexChanged);
            // 
            // cmbSelLayer
            // 
            this.cmbSelLayer.FormattingEnabled = true;
            this.cmbSelLayer.Location = new System.Drawing.Point(142, 38);
            this.cmbSelLayer.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSelLayer.Name = "cmbSelLayer";
            this.cmbSelLayer.Size = new System.Drawing.Size(265, 23);
            this.cmbSelLayer.TabIndex = 8;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.Location = new System.Drawing.Point(37, 141);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 15);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "颜色选择：";
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSize = true;
            this.labelControl1.Location = new System.Drawing.Point(37, 38);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 15);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "图层选择：";
            // 
            // frmSimpleRender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 266);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.colorEditSimple);
            this.Controls.Add(this.cmbSelLayer);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Name = "frmSimpleRender";
            this.Text = "单一符号化";
            this.Load += new System.EventHandler(this.frmSimpleRender_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox colorEditSimple;
        private System.Windows.Forms.ComboBox cmbSelLayer;
        private System.Windows.Forms.Label labelControl2;
        private System.Windows.Forms.Label labelControl1;
    }
}