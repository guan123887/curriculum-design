using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.esriSystem;

namespace test
{
    public partial class Fill : Form
    {
        public Fill()
        {
            InitializeComponent();
            //Geoprocessor gp;
            //IVariantArray parameters;
        }

        Geoprocessor gp = new Geoprocessor();   //创建GP对象
        IVariantArray parameters = new VarArrayClass(); //定义参数对象
        string shapeFileFullName = string.Empty;
        string surveyDataFullName = string.Empty;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD1 = new OpenFileDialog();
            pOFD1.Multiselect = false;
            pOFD1.Title = "打开栅格数据";
            pOFD1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            pOFD1.Filter = "文件(*.*)|*.*";
            if (pOFD1.ShowDialog() == DialogResult.OK)
            {
                surveyDataFullName = pOFD1.FileName;
                this.textBox1.Text = surveyDataFullName;
                parameters.Add(this.textBox1.Text.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "栅格文件(*.*) |*.*";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                shapeFileFullName = saveFileDialog.FileName;
            }
            else
            {
                shapeFileFullName = null;
                return;
            }
            this.textBox3.Text = shapeFileFullName;
            parameters.Add(this.textBox3.Text.ToString());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gp.AddToolbox(@"D:\Arcgis10.2\Desktop10.2\ArcToolbox\Toolboxes\Spatial Analyst Tools.tbx"); 
            gp.OverwriteOutput = true;
            gp.Execute("Fill", parameters, null);  //执行填洼
            MessageBox.Show("运行成功");
            this.Close();
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            if (this.textBox2.Text != null)
            {
                parameters.Add(this.textBox2.Text);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
