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
    public partial class define_project : Form
    {
        public define_project()
        {
            InitializeComponent();
        }

        Geoprocessor gp = new Geoprocessor();   //创建GP对象
        IVariantArray parameters = new VarArrayClass(); //定义参数对象
        
        string shapeFileFullName = string.Empty;
        string surveyDataFullName = string.Empty;
        private void openfile_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Multiselect = false;
            pOFD.Title = "打开未定义投影文件";
            pOFD.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            pOFD.Filter = "文件(*.*)|*.*";
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                surveyDataFullName = pOFD.FileName;
                this.textBox1.Text = surveyDataFullName;
                parameters.Add(this.textBox1.Text.ToString());
            }
        }
        private void project_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog pOFD2 = new OpenFileDialog();
            pOFD2.Multiselect = false;
            pOFD2.Title = "打开投影文件";
            pOFD2.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            pOFD2.Filter = "文件(*.prj)|*.prj";
            if (pOFD2.ShowDialog() == DialogResult.OK)
            {
                surveyDataFullName = pOFD2.FileName;
                this.textBox2.Text = surveyDataFullName;
                parameters.Add(this.textBox2.Text.ToString());
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gp.AddToolbox(@"D:\Arcgis10.2\Desktop10.2\ArcToolbox\Toolboxes\Data Management Tools.tbx"); 
            gp.OverwriteOutput = true;
            gp.Execute("DefineProjection_management", parameters,null);  //执行定义投影
            MessageBox.Show("运行成功");
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
