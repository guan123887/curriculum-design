namespace test.登录界面
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.checkCode = new DevExpress.XtraEditors.TextEdit();
            this.textCheck = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.LoginCancelsimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.userId = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginOksimpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ensurePw = new System.Windows.Forms.TextBox();
            this.userPw = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.checkCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCheck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userId.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // checkCode
            // 
            this.checkCode.Location = new System.Drawing.Point(394, 236);
            this.checkCode.Name = "checkCode";
            this.checkCode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.checkCode.Properties.Appearance.Options.UseBackColor = true;
            this.checkCode.Size = new System.Drawing.Size(74, 24);
            this.checkCode.TabIndex = 35;
            // 
            // textCheck
            // 
            this.textCheck.Location = new System.Drawing.Point(169, 236);
            this.textCheck.Name = "textCheck";
            this.textCheck.Size = new System.Drawing.Size(204, 24);
            this.textCheck.TabIndex = 34;
            this.textCheck.Click += new System.EventHandler(this.textCheck_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(57, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 24);
            this.label4.TabIndex = 33;
            this.label4.Text = "验证码：";
            // 
            // LoginCancelsimpleButton
            // 
            this.LoginCancelsimpleButton.Appearance.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginCancelsimpleButton.Appearance.ForeColor = System.Drawing.Color.Black;
            this.LoginCancelsimpleButton.Appearance.Options.UseFont = true;
            this.LoginCancelsimpleButton.Appearance.Options.UseForeColor = true;
            this.LoginCancelsimpleButton.Location = new System.Drawing.Point(362, 333);
            this.LoginCancelsimpleButton.Name = "LoginCancelsimpleButton";
            this.LoginCancelsimpleButton.Size = new System.Drawing.Size(106, 39);
            this.LoginCancelsimpleButton.TabIndex = 30;
            this.LoginCancelsimpleButton.Text = "取消";
            // 
            // userId
            // 
            this.userId.Location = new System.Drawing.Point(169, 78);
            this.userId.Name = "userId";
            this.userId.Size = new System.Drawing.Size(299, 24);
            this.userId.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label3.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(81, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 27;
            this.label3.Text = "密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(57, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 26;
            this.label2.Text = "用户名：";
            // 
            // LoginOksimpleButton
            // 
            this.LoginOksimpleButton.Appearance.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginOksimpleButton.Appearance.ForeColor = System.Drawing.Color.Black;
            this.LoginOksimpleButton.Appearance.Options.UseFont = true;
            this.LoginOksimpleButton.Appearance.Options.UseForeColor = true;
            this.LoginOksimpleButton.Location = new System.Drawing.Point(169, 333);
            this.LoginOksimpleButton.Name = "LoginOksimpleButton";
            this.LoginOksimpleButton.Size = new System.Drawing.Size(106, 39);
            this.LoginOksimpleButton.TabIndex = 29;
            this.LoginOksimpleButton.Text = "立即注册";
            this.LoginOksimpleButton.Click += new System.EventHandler(this.LoginOksimpleButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 36;
            this.label1.Text = "确认密码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(494, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 15);
            this.label5.TabIndex = 38;
            this.label5.Text = "该密码6位以上";
            // 
            // ensurePw
            // 
            this.ensurePw.Location = new System.Drawing.Point(169, 178);
            this.ensurePw.Name = "ensurePw";
            this.ensurePw.PasswordChar = '*';
            this.ensurePw.Size = new System.Drawing.Size(299, 25);
            this.ensurePw.TabIndex = 39;
            // 
            // userPw
            // 
            this.userPw.Location = new System.Drawing.Point(169, 135);
            this.userPw.Name = "userPw";
            this.userPw.PasswordChar = '*';
            this.userPw.Size = new System.Drawing.Size(299, 25);
            this.userPw.TabIndex = 40;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(740, 437);
            this.Controls.Add(this.userPw);
            this.Controls.Add(this.ensurePw);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkCode);
            this.Controls.Add(this.textCheck);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LoginCancelsimpleButton);
            this.Controls.Add(this.LoginOksimpleButton);
            this.Controls.Add(this.userId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Register";
            this.Text = "注册";
            ((System.ComponentModel.ISupportInitialize)(this.checkCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textCheck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userId.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit checkCode;
        private DevExpress.XtraEditors.TextEdit textCheck;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.SimpleButton LoginCancelsimpleButton;
        private DevExpress.XtraEditors.TextEdit userId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton LoginOksimpleButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ensurePw;
        private System.Windows.Forms.TextBox userPw;
    }
}