﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using System.Collections;
using ESRI.ArcGIS.Output;
using System.Drawing.Printing;
using DevExpress.DocumentView;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace test.PageLayout
{
    public partial class frmPrintPre : Form
    {
        #region 定义变量
        //定义页面设置、打印预览和打印对话框
        private PrintPreviewDialog printPreviewDialog1;
        private PrintDialog printDialog1;
        private PageSetupDialog pageSetupDialog1;
        private System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();
        private ITrackCancel m_TrackCancel = new CancelTrackerClass();
        private short m_CurrentPrintPage;// 定义页数变量
        #endregion
        public frmPrintPre(AxPageLayoutControl pageltcontrol)
        {
            InitializeComponent1();
            //PageLayoutControl同步函数
            syn(pageltcontrol);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent1()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintPre));
            this.btnPageSize = new System.Windows.Forms.Button();
            this.btnPrintpreview = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.PrintPagelayoutControl = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.PrintPagelayoutControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPageSize
            // 
            this.btnPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPageSize.Location = new System.Drawing.Point(746, 76);
            this.btnPageSize.Name = "btnPageSize";
            this.btnPageSize.Size = new System.Drawing.Size(100, 29);
            this.btnPageSize.TabIndex = 4;
            this.btnPageSize.Text = "页面设置";
            this.btnPageSize.Click += new System.EventHandler(this.btnPageSize_Click);
            // 
            // btnPrintpreview
            // 
            this.btnPrintpreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintpreview.Location = new System.Drawing.Point(746, 188);
            this.btnPrintpreview.Name = "btnPrintpreview";
            this.btnPrintpreview.Size = new System.Drawing.Size(100, 29);
            this.btnPrintpreview.TabIndex = 5;
            this.btnPrintpreview.Text = "打印预览";
            this.btnPrintpreview.Click += new System.EventHandler(this.btnPrintpreview_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(746, 282);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 29);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // PrintPagelayoutControl
            // 
            this.PrintPagelayoutControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintPagelayoutControl.Location = new System.Drawing.Point(4, 12);
            this.PrintPagelayoutControl.Name = "PrintPagelayoutControl";
            this.PrintPagelayoutControl.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("PrintPagelayoutControl.OcxState")));
            this.PrintPagelayoutControl.Size = new System.Drawing.Size(830, 566);
            this.PrintPagelayoutControl.TabIndex = 9;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(636, 81);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 10;
            // 
            // frmPrintPreview
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(854, 480);
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPrintpreview);
            this.Controls.Add(this.btnPageSize);
            this.Controls.Add(this.PrintPagelayoutControl);
            this.Name = "frmPrintPreview";
            this.Text = "打印";
            this.Load += new System.EventHandler(this.frmPrintPre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PrintPagelayoutControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private void frmPrintPre_Load(object sender, EventArgs e)
        {
            InitializePrintPreviewDialog(); //初始化打印预览对话框
            printDialog1 = new PrintDialog(); //实例化打印对话框
            InitializePageSetupDialog(); //初始化打印设置对话    
        }
        #region 页面设置
        private void InitializePageSetupDialog()
        {
            pageSetupDialog1 = new PageSetupDialog();
            //初始化页面设置对话框的页面设置属性为缺省设置
            pageSetupDialog1.PageSettings = new System.Drawing.Printing.PageSettings();
            //初始化页面设置对话框的打印机属性为缺省设置
            pageSetupDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
        }
        private void btnPageSize_Click(object sender, EventArgs e)
        {
            try
            {
                //实例化打印设置窗口
                DialogResult result = pageSetupDialog1.ShowDialog();
                //设置打印文档对象的打印机
                document.PrinterSettings = pageSetupDialog1.PrinterSettings;
                //设置打印文档对象的页面设置为用户在打印设置对话框中的设置
                document.DefaultPageSettings = pageSetupDialog1.PageSettings;
                //页面设置
                int i;
                IEnumerator paperSizes = pageSetupDialog1.PrinterSettings.PaperSizes.GetEnumerator();
                paperSizes.Reset();

                for (i = 0; i < pageSetupDialog1.PrinterSettings.PaperSizes.Count; ++i)
                {
                    paperSizes.MoveNext();
                    if (((PaperSize)paperSizes.Current).Kind == document.DefaultPageSettings.PaperSize.Kind)
                    {
                        document.DefaultPageSettings.PaperSize = ((PaperSize)paperSizes.Current);
                    }
                }

                //初始化纸张和打印机
                IPaper paper = new PaperClass();
                IPrinter printer = new EmfPrinterClass();
                //关联打印机对象和纸张对象 
                paper.Attach(pageSetupDialog1.PrinterSettings.GetHdevmode(pageSetupDialog1.PageSettings).ToInt32(), pageSetupDialog1.PrinterSettings.GetHdevnames().ToInt32());
                printer.Paper = paper;
                PrintPagelayoutControl.Printer = printer;
            }
            catch { }
        }
        #endregion
        #region 打印预览
        private void InitializePrintPreviewDialog()
        {
            printPreviewDialog1 = new PrintPreviewDialog();
            //设置打印预览的尺寸，位置，名称，以及最小尺寸
            printPreviewDialog1.ClientSize = new System.Drawing.Size(800, 600);
            printPreviewDialog1.Location = new System.Drawing.Point(29, 29);
            printPreviewDialog1.Name = "打印预览对话框";
            printPreviewDialog1.MinimumSize = new System.Drawing.Size(375, 250);
            printPreviewDialog1.UseAntiAlias = true;
            this.document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(document_PrintPage);
        }
        private void btnPrintpreview_Click(object sender, EventArgs e)
        {
            try
            {
                //初始化当前打印页码
                m_CurrentPrintPage = 0;
                document.DocumentName = PrintPagelayoutControl.DocumentFilename;
                printPreviewDialog1.Document = document;
                printPreviewDialog1.ShowDialog();
            }
            catch { }
        }

        private void document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //当 PrintPreviewDialog的Show方法触发时，引用这段代码 
                PrintPagelayoutControl.Page.PageToPrinterMapping = esriPageToPrinterMapping.esriPageMappingTile;
                //获取打印分辨率
                short dpi = (short)e.Graphics.DpiX;
                IEnvelope devBounds = new EnvelopeClass();
                //获取打印页面
                ESRI.ArcGIS.Carto.IPage page = PrintPagelayoutControl.Page;
                //获取打印的页数
                short printPageCount;
                printPageCount = PrintPagelayoutControl.get_PrinterPageCount(0);
                m_CurrentPrintPage++;

                //选择打印机
                IPrinter printer = PrintPagelayoutControl.Printer;
                //获得打印机页面大小
                page.GetDeviceBounds(printer, m_CurrentPrintPage, 0, dpi, devBounds);
                //获得页面大小的坐标范围，即四个角的坐标
                tagRECT deviceRect;
                double xmin, ymin, xmax, ymax;
                devBounds.QueryCoords(out xmin, out ymin, out xmax, out ymax);
                deviceRect.bottom = (int)ymax;
                deviceRect.left = (int)xmin;
                deviceRect.top = (int)ymin;
                deviceRect.right = (int)xmax;
                //确定当前打印页面的大小
                IEnvelope visBounds = new EnvelopeClass();
                page.GetPageBounds(printer, m_CurrentPrintPage, 0, visBounds);
                IntPtr hdc = e.Graphics.GetHdc();
                PrintPagelayoutControl.ActiveView.Output(hdc.ToInt32(), dpi, ref deviceRect, visBounds, m_TrackCancel);
                e.Graphics.ReleaseHdc(hdc);
                if (m_CurrentPrintPage < printPageCount)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch
            { }
        }
        #endregion
        #region 打印
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //显示帮助按钮            
                printDialog1.ShowHelp = true;
                printDialog1.Document = document;
                //显示打印窗口
                DialogResult result = printDialog1.ShowDialog();
                // 如果显示成功，则打印.
                if (result == DialogResult.OK) document.Print();
                Close();
            }
            catch { }
        }
        #endregion
        #region  PageLayoutconrol同步
        private void syn(AxPageLayoutControl mainlayoutControl)
        {
            IObjectCopy objectcopy = new ObjectCopyClass();
            object tocopymap = mainlayoutControl.ActiveView.GraphicsContainer;   //获取mapcontrol中的map   这个是原始的
            object copiedmap = objectcopy.Copy(tocopymap);       //复制一份map，是一个副本
            object tooverwritemap = PrintPagelayoutControl.ActiveView.GraphicsContainer;    //IActiveView.FocusMap : The map that tools and controls act on. 控件和工具作用的地图，大概是当前地图吧！！！
            objectcopy.Overwrite(copiedmap, ref tooverwritemap);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(objectcopy);
            IGraphicsContainer mainGraphCon = tooverwritemap as IGraphicsContainer;
            mainGraphCon.Reset();
            IElement pElement = mainGraphCon.Next();
            IArray pArray = new ArrayClass();
            while (pElement != null)
            {
                pArray.Add(pElement);
                pElement = mainGraphCon.Next();
            }
            int pElementCount = pArray.Count;
            IPageLayout PrintPageLayout = PrintPagelayoutControl.PageLayout;
            IGraphicsContainer PrintGraphCon = PrintPageLayout as IGraphicsContainer;
            PrintGraphCon.Reset();
            IElement pPrintElement = PrintGraphCon.Next();
            while (pPrintElement != null)
            {
                PrintGraphCon.DeleteElement(pPrintElement);
                pPrintElement = PrintGraphCon.Next();
            }
            for (int i = 0; i < pArray.Count; i++)
            {
                PrintGraphCon.AddElement(pArray.get_Element(pElementCount - 1 - i) as IElement, 0);
            }
            PrintPagelayoutControl.Refresh();

        }
        #endregion

    }
}
