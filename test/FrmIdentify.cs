using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.IO;
using ESRI.ArcGIS.esriSystem;

namespace test
{
    public partial class FrmIdentify : Form
    {
        public FrmIdentify()
        {
            InitializeComponent();
              //添加选项
            for (int i = 0; i <40; i++)
           {
               listBoxName.Items.Add("Items " + i);
               listBoxAttribute.Items.Add("Items " + i);
           }
            //添加同步
           EventHandler handler = (s, e) =>
           {
               if (s == listBoxName)
                   listBoxAttribute.TopIndex = listBoxName.TopIndex;
               if (s == listBoxAttribute)
                   listBoxName.TopIndex = listBoxAttribute.TopIndex;
           };
           this.listBoxName.MouseCaptureChanged += handler;
           this.listBoxAttribute.MouseCaptureChanged += handler;
           this.listBoxName.SelectedIndexChanged += handler;
           this.listBoxAttribute.SelectedIndexChanged += handler;

        }


        public void clearlistbox()
        { 
            //清空列表框
            this.listBoxName.Items.Clear();
            this.listBoxAttribute.Items.Clear();
        }

         public void getselfeaturescursor(IFeatureCursor pFcursor)
        {
            IFeature pFeature = pFcursor.NextFeature();
            if (pFeature != null)
            {
                IFields pFields = pFeature.Fields;
                IField pField = null;
                for (int i = 0; i < (pFields.FieldCount - 1); i++)
                {
                     pField = pFields.get_Field(i);
                     //pDataRow = pDataTable.NewRow();
                    this.listBoxName.Items.Add(pField.Name);
                    this.listBoxAttribute.Items.Add(pFeature.get_Value(i).ToString());
                }
            }
        }
   }
}
