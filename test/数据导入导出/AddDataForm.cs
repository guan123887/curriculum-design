using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace test
{
    public partial class AddDataForm : Form
    {
        public AddDataForm()
        {
            InitializeComponent();
        }

        string shapeFileFullName = string.Empty;
        string surveyDataFullName = string.Empty;
        #region 导入数据
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD3 = new OpenFileDialog();
            pOFD3.Multiselect = false;
            pOFD3.Title = "打开栅格数据";
            pOFD3.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            pOFD3.Filter = "文件(*.*)|*.*";
            if (pOFD3.ShowDialog() == DialogResult.OK)
            {
                surveyDataFullName = pOFD3.FileName;
                this.textBox1.Text = surveyDataFullName;
            }
        }
        #endregion
        #region 导出数据库
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog saveFolder = new FolderBrowserDialog();

            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "数据库(*.*) |*.*";
            DialogResult dialogResult = saveFolder.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFolder.SelectedPath;
            }
            else
            {
                shapeFileFullName = null;
                return;
            }
            this.textBox2.Text = shapeFileFullName;
        }
        #endregion

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(this.textBox1.Text.ToString());
            fi.CopyTo(this.textBox2.Text.ToString(),true);
            MessageBox.Show("运行成功");
            this.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
