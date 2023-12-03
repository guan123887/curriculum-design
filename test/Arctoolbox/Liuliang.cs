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
    public partial class Liuliang : Form
    {
        public Liuliang()
        {
            InitializeComponent();
        }

        Geoprocessor gp = new Geoprocessor();   //创建GP对象
        IVariantArray parameters = new VarArrayClass(); //定义参数对象
        string shapeFileFullName = string.Empty;
        string surveyDataFullName = string.Empty;
        string surveyDataFullName1 = string.Empty;
        private void button1_Click(object sender, EventArgs e)
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
                parameters.Add(this.textBox1.Text.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
            this.textBox2.Text = shapeFileFullName;
            parameters.Add(this.textBox2.Text.ToString());          
        }
        private void button3_Click(object sender, EventArgs e)
        {
                OpenFileDialog pOFD3 = new OpenFileDialog();
                pOFD3.Multiselect = false;
                pOFD3.Title = "打开栅格数据";
                pOFD3.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                pOFD3.Filter = "文件(*.*)|*.*";
                if (pOFD3.ShowDialog() == DialogResult.OK)
                {
                    surveyDataFullName1 = pOFD3.FileName;
                    this.textBox1.Text = surveyDataFullName1;
                    parameters.Add(this.textBox3.Text.ToString());
                }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            parameters.Add(this.comboBox1.Text.ToString());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gp.AddToolbox(@"D:\Arcgis10.2\Desktop10.2\ArcToolbox\Toolboxes\Spatial Analyst Tools.tbx");
            gp.OverwriteOutput = true;
            gp.Execute("FlowAccumulation", parameters, null);  //执行流向
            MessageBox.Show("运行成功");
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
