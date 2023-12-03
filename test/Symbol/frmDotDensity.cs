﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using test.Class;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace test.Symbol
{
    public partial class frmDotDensity : Form
    {
        List<IFeatureClass> _lstFeatCls = null;
        public delegate void DotDensityEventHandler(string sFeatClsName, string sFieldName, int intClsBreakNum);
        public event DotDensityEventHandler DotDensity = null;
        public frmDotDensity()
        {
            InitializeComponent();
        }
        private IMap _map;
        public IMap Map
        {
            get { return _map; }
            set { _map = value; }
        }
        public void InitUI()
        {
            string sClsName = string.Empty;
            IFeatureClass pFeatCls = null;
            cmbSelLyr.Items.Clear();
            OperateMap _OperateMap = new OperateMap();
            _lstFeatCls = _OperateMap.GetLstFeatCls(_map);
            for (int i = 0; i < _lstFeatCls.Count; i++)
            {
                pFeatCls = _lstFeatCls[i];
                sClsName = pFeatCls.AliasName;                
                if (!cmbSelLyr.Items.Contains(sClsName) && pFeatCls.ShapeType==esriGeometryType.esriGeometryPolygon)
                {
                    cmbSelLyr.Items.Add(sClsName);
                }
                
            }
            
        }
        private bool check()
        {
            if (cmbSelLyr.SelectedIndex == -1)
            {
                MessageBox.Show("请选择符号化图层！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (cmbSelField.SelectedIndex == -1)
            {
                MessageBox.Show("请选择符号化字段！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (RendererDensity.SelectedIndex == -1)
            {
                MessageBox.Show("请选择渲染密度值！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        private IFeatureClass GetFeatClsByName(string sFeatClsName)
        {
            IFeatureClass pFeatCls = null;
            for (int i = 0; i < _lstFeatCls.Count; i++)
            {
                pFeatCls = _lstFeatCls[i];
                if (pFeatCls.AliasName == sFeatClsName)
                {
                    break;
                }
            }
            return pFeatCls;
        }             
        private void cmbSelLyr_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSelField.Items.Clear();
            cmbSelField.Text = "";
            IField pField = null;
            IFeatureClass pFeatCls = GetFeatClsByName(cmbSelLyr.SelectedItem.ToString());
            for (int i = 0; i < pFeatCls.Fields.FieldCount; i++)
            {
                pField = pFeatCls.Fields.get_Field(i);
                if (pField.Type == esriFieldType.esriFieldTypeDouble ||
                    pField.Type == esriFieldType.esriFieldTypeInteger ||
                    pField.Type == esriFieldType.esriFieldTypeSingle ||
                    pField.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    if (!cmbSelField.Items.Contains(pField.Name))
                    {
                        cmbSelField.Items.Add(pField.Name);
                    }
                }
            }             
        }             
        private void btnOK_Click_1(object sender, EventArgs e)
        {
            if (!check()) return;
            DotDensity(cmbSelLyr.SelectedItem.ToString(),
                cmbSelField.SelectedItem.ToString(),
                Convert.ToInt32(RendererDensity.SelectedItem.ToString()));
            cmbSelField.Items.Clear();
            cmbSelField.Text = "";
            cmbSelLyr.Items.Clear();
            cmbSelLyr.Text = "";
            RendererDensity.SelectedIndex = -1;
            Close();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
