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
using ESRI.ArcGIS.Controls;

namespace test
{
    public partial class biaoshi : Form
    {
        public biaoshi()
        {
            InitializeComponent();
        }
        private AxMapControl m_axmap;
        Geoprocessor gp = new Geoprocessor();   //创建GP对象
        IVariantArray parameters = new VarArrayClass(); //定义参数对象
        string shapeFileFullName = string.Empty;
        string surveyDataFullName = string.Empty;
        string surveyDataFullName1 = string.Empty;
        private void button3_Click(object sender, EventArgs e)
        {
            Mainform frm = new Mainform();
            m_axmap = frm.Put();
            this.comboBox1.Text = frm.GetMapUnit(m_axmap.Map.MapUnits);
            OpenFileDialog pOFD1 = new OpenFileDialog();
            pOFD1.Multiselect = false;
            pOFD1.Title = "打开shapefile数据";
            pOFD1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            pOFD1.Filter = "文件(*.shp)|*.shp";
            if (pOFD1.ShowDialog() == DialogResult.OK)
            {
                surveyDataFullName = pOFD1.FileName;
                this.textBox1.Text = surveyDataFullName;
                parameters.Add(this.textBox3.Text.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD1 = new OpenFileDialog();
            pOFD1.Multiselect = false;
            pOFD1.Title = "打开shapefile数据";
            pOFD1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            pOFD1.Filter = "文件(*.shp)|*.shp";
            if (pOFD1.ShowDialog() == DialogResult.OK)
            {
                surveyDataFullName1 = pOFD1.FileName;
                this.textBox1.Text = surveyDataFullName1;
                parameters.Add(this.textBox2.Text.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "shapefile文件(*.shp) |*.shp";
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
            parameters.Add(this.textBox4.Text.ToString());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            parameters.Add(this.comboBox2.Text.ToString());
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gp.AddToolbox(@"D:\Arcgis10.2\Desktop10.2\ArcToolbox\Toolboxes\Analysis Tools.tbx");
            gp.OverwriteOutput = true;
            gp.Execute("Identity_analysis", parameters, null);  //执行填洼
            MessageBox.Show("运行成功");
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            parameters.Add(this.textBox1.Text.ToString());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                parameters.Add("True");
            }
            else
            {
                parameters.Add("False");
            }
        }
    }
}
