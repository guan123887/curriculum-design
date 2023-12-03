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
    public partial class ASCIIzhuanshange : Form
    {
        public ASCIIzhuanshange()
        {
            InitializeComponent();
        }
        Geoprocessor gp = new Geoprocessor();   //创建GP对象
        IVariantArray parameters = new VarArrayClass(); //定义参数对象
        string shapeFileFullName = string.Empty;
        string surveyDataFullName = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD1 = new OpenFileDialog();
            pOFD1.Multiselect = false;
            pOFD1.Title = "打开ASCII数据";
            pOFD1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            pOFD1.Filter = "文件(*.*)|*.*";
            if (pOFD1.ShowDialog() == DialogResult.OK)
            {
                surveyDataFullName = pOFD1.FileName;
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gp.AddToolbox(@"D:\Arcgis10.2\Desktop10.2\ArcToolbox\Toolboxes\Conversion Tools.tbx");
            gp.OverwriteOutput = true;
            gp.Execute("ASCIIToRaster_conversion", parameters, null);  
            MessageBox.Show("运行成功");
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            parameters.Add(this.comboBox1.Text.ToString());
        }

 
    }
}
