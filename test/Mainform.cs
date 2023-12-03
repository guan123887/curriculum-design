using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using test.Lable;
using test.Class;
using test.Class;
using test.Class.EnumType;
using ESRI.ArcGIS.Output;
using stdole;
using TextSymbols;
using test.PageLayout;
using test.Class.EnumType;
using test.Symbol;
using QueryAndStatistics;

namespace test
{
    public partial class Mainform : DevExpress.XtraBars.Ribbon.RibbonForm
    {        
        #region 定义变量
        IFeatureLayer pTocFeatureLayer = null;
        private FormMeasureResult frmMeasureResult = null;   //量算结果窗体
        private INewLineFeedback pNewLineFeedback;           //追踪线对象
        private INewPolygonFeedback pNewPolygonFeedback;     //追踪面对象
        private IPoint pPointPt = null;                      //鼠标点击点
        private IPoint pMovePt = null;                       //鼠标移动时的当前点
        private double dToltalLength = 0;                    //量测总长度
        private double dSegmentLength = 0;                   //片段距离
        private IPointCollection pAreaPointCol = new MultipointClass();  //面积量算时画的点进行存储；  
        private string sMapUnits = "未知单位";             //地图单位变量
        private object missing = Type.Missing;
        IExtentStack pExtentStack;
        public string pMouseOperate = null;
        private FormAttribute frmAttribute = null;        //图层属性窗体
        private ILayer pMoveLayer;                        //需要调整显示顺序的图层
        private int toIndex;
        FrmIdentify frmident = null;
        private frmAnnotation frmAnnotation = null;  // 注记
        private frmTextElement frmTextElement = null;//标注
        private frmMapTips frmMapTips = null;//MapTips       
        private OperateMap m_OperateMap = null;
        public string filepath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
         private test.PageLayout.frmSymbol frmSym = null;
         private frmPrintPre frmPrintPreview = null; // 打印                      
        //对地图的基本操作类
        private OperatePageLayout m_OperatePageLayout = null;         
        private INewEnvelopeFeedback pNewEnvelopeFeedback;      
        private EnumMapSurroundType _enumMapSurType = EnumMapSurroundType.None;
        private IStyleGalleryItem pStyleGalleryItem;   
        private IPoint m_MovePt = null;
        private IPoint m_PointPt = null;     
        private System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument(); //打印 
        string filepath1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        //地图到处窗体
        private frmUniqueValueRender frmUniqueValueRen = null;
        private frmUniqueValueMany_fields frmUniqueValueMany_fields = null;
        private frmSimpleRender frmSimRender = null;
        private frmDotDensity frmDotDensity = null;  // 点密度 
        private frmProportional frmProportional = null;//比例符号化
        private frmGraduatedcolors frmGraduatedcolors = null; // 分级色彩
        private frmGraduatedSymbols frmGraduatedSymbols = null;// 分级符号      
        private bool bCanDrag;              //鹰眼地图上的矩形框可移动的标志
        private IPoint pMoveRectPoint;      //记录在移动鹰眼地图上的矩形框时鼠标的位置
        private IEnvelope pEnv;             //记录数据视图的Extent
        //对地图的基本操作类
        #endregion
        public Mainform()
        {
            InitializeComponent();
            m_OperatePageLayout = new OperatePageLayout();
            m_OperateMap = new OperateMap();
            axTOCControl1.SetBuddyControl(axMapControl1);
        }//定义公共变量
        private void Mainform_Load(object sender, EventArgs e)
        {
            this.axTOCControl1.SetBuddyControl(this.axMapControl1);
            EagleMapControl.Extent = axMapControl1.FullExtent;
            pEnv = EagleMapControl.Extent;
        }
        public AxMapControl Put() {
            return axMapControl1;
        }
        public TreeView treeview;
        #region 属性查询
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //按属性查询
            //新创建属性查询窗体
            FormQueryByAttribute formQueryByAttribute1 = new FormQueryByAttribute();
            formQueryByAttribute1.CurrentMap = axMapControl1.Map;
            //显示属性查询窗体
            formQueryByAttribute1.Show();
        }
        #endregion
        #region 空间查询
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //新创建空间查询窗体
            FormQueryBySpatial formQueryBySpatial = new FormQueryBySpatial();
            //将当前主窗体中MapControl控件中的Map对象赋值给FormSelection窗体的CurrentMap属性
            formQueryBySpatial.CurrentMap = axMapControl1.Map;
            //显示空间查询窗体
            formQueryBySpatial.Show();
        }
        #endregion
        #region 地图操作
        #region 加载mxd
        private void btn_open_Click(object sender, EventArgs e)
        {
            //加载MXD
            try
            {
                OpenFileDialog pOpenFileDialog = new OpenFileDialog();
                pOpenFileDialog.CheckFileExists = true;
                pOpenFileDialog.Title = "打开地图文档";
                pOpenFileDialog.Filter = "ArcMap文档(*.mxd)|*.mxd;|ArcMap模板(*.mxt)|*.mxt|发布地图文件(*.pmf)|*.pmf|所有地图格式(*.mxd;*.mxt;*.pmf)|*.mxd;*.mxt;*.pmf";
                pOpenFileDialog.Multiselect = false;   //不允许多个文件同时选择
                pOpenFileDialog.RestoreDirectory = true;   //存储打开的文件路径
                if (pOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pFileName = pOpenFileDialog.FileName;
                    if (pFileName == "")
                    {
                        return;
                    }
                    if (axMapControl1.CheckMxFile(pFileName)) //检查地图文档有效性
                    {
                        //ClearAllData();是否需要清空其它数据？
                        axMapControl1.LoadMxFile(pFileName);
                    }
                    else
                    {
                        MessageBox.Show(pFileName + "是无效的地图文档!", "信息提示");
                        return;
                    }
                    IMap map = axMapControl1.ActiveView.FocusMap;
                    barEditItem1.Text = "1:"+map.MapScale.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开地图文档失败" + ex.Message);
            }
        }
        #endregion
        #region 加载数据
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //加载数据这里使用ICommand接口
            ICommand command = new ControlsAddDataCommand();
            command.OnCreate(axMapControl1.Object);
            command.OnClick();
            IMap map = axMapControl1.ActiveView.FocusMap;
            barEditItem1.Text = "1:"+map.MapScale.ToString();
        }
        #endregion
        #region 保存地图
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //保存地图
            try
            {
                string sMxdFileName = axMapControl1.DocumentFilename;
                IMapDocument pMapDocument = new MapDocument();
                if (sMxdFileName != null && axMapControl1.CheckMxFile(sMxdFileName))
                {
                    if (pMapDocument.get_IsReadOnly(sMxdFileName))
                    {
                        MessageBox.Show("本地图文档是只读的，不能保存!");
                        pMapDocument.Close();
                        return;
                    }
                }
                else
                {
                    SaveFileDialog pSaveFileDialog_main = new SaveFileDialog();
                    pSaveFileDialog_main.Title = "请选择保存路径";
                    pSaveFileDialog_main.OverwritePrompt = true;
                    pSaveFileDialog_main.Filter = "ArcMap文档（*.mxd）|*.mxd|ArcMap模板（*.mxt）|*.mxt";
                    pSaveFileDialog_main.RestoreDirectory = true;
                    if (pSaveFileDialog_main.ShowDialog() == DialogResult.OK)
                    {
                        sMxdFileName = pSaveFileDialog_main.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch { }
        }
        #endregion
        #region 全图显示
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
             //全图显示
            pMouseOperate = null;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault; 
            axMapControl1.Extent = axMapControl1.FullExtent;
        }
        #endregion
        #region 漫游
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //写在工具或菜单Click事件里
            axMapControl1.CurrentTool = null;
            pMouseOperate = "Pan";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerPan;
        }
        #endregion
        #region 逐级放大
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
              //逐级放大
            IEnvelope pEnvelope;
            pEnvelope = axMapControl1.Extent;
            pEnvelope.Expand(0.5, 0.5, true);     //这里设置放大为2倍，可以根据需要具体设置
            axMapControl1.Extent = pEnvelope;
            axMapControl1.ActiveView.Refresh();
        }
        #endregion
        #region 逐级缩小
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
              //逐级缩小
            IEnvelope pEnvelope;
            pEnvelope = axMapControl1.Extent;
            pEnvelope.Expand(2, 2, true);     //这里设置缩小一半，可以根据需要具体设置
            axMapControl1.Extent = pEnvelope;
            axMapControl1.ActiveView.Refresh();
        }
        #endregion
        #region 拉框放大
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            //拉框放大
            axMapControl1.CurrentTool = null;
            pMouseOperate = "ZoomIn";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
        }
        #endregion
        #region 拉框缩小
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            //拉框缩小
            axMapControl1.CurrentTool = null;
            pMouseOperate = "ZoomOut";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerZoomOut;
        }
        #endregion
        #region 前一视图
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            pMouseOperate = null;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault; 
            pExtentStack = axMapControl1.ActiveView.ExtentStack;
            //判断是否可以回到前一视图，第一个视图没有前一视图
            if (pExtentStack.CanUndo())
            {
                pExtentStack.Undo();
                barButtonItem11.Enabled = true;//后一视图
                if (!pExtentStack.CanUndo())
                {
                    barButtonItem10.Enabled = false;
                }
            }
            axMapControl1.ActiveView.Refresh(); 
        }
        #endregion
        #region 后一视图
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            pMouseOperate = null;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault; 
            pExtentStack = axMapControl1.ActiveView.ExtentStack;
            //判断是否可以回到后一视图，最后一个视图没有后一视图
            if (pExtentStack.CanRedo())
            {
                pExtentStack.Redo();
                barButtonItem10.Enabled = true;
                if (!pExtentStack.CanRedo())
                {
                    barButtonItem11.Enabled = false;
                }
            }
            axMapControl1.ActiveView.Refresh();
        }
        #endregion
        #region 刷新
        private void tbnrefresh_Click(object sender, EventArgs e)
        {
            IActiveView pActiveView = axPageLayoutControl1.ActiveView;
            IPageLayout pPageLayout = axPageLayoutControl1.PageLayout;
            DeleteMapGrid(pActiveView, pPageLayout);
            this.axMapControl1.Refresh();
        }   
        #endregion
        #region 选择要素
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////选择要素
            ControlsSelectFeaturesTool sel = new ControlsSelectFeaturesToolClass();
            sel.OnCreate(this.axMapControl1.Object);
            axMapControl1.CurrentTool = (ITool)sel;
        }
        #endregion
        #region 缩放至选择
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //缩放至选择
            ICommand pCommand = new ControlsZoomToSelectedCommand();
            pCommand.OnCreate(axMapControl.Object);
            pCommand.OnClick();
        }
        #endregion
        #region 清除选择
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //清除选择
            ICommand ClearSel = new ControlsClearSelectionCommand();
            ClearSel.OnCreate(axMapControl.Object);
            ClearSel.OnClick();
        }
        #endregion
        #region 查询
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            axMapControl1.CurrentTool = null;
            pMouseOperate = "selectbypoint";
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerIdentify;
        }
        #endregion
        #region 指针
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            //指针
            this.axMapControl1.CurrentTool = null;
            pMouseOperate = "None";
            this.axMapControl1.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }
        #endregion
        #endregion
        #region 同步到Layerout视图
        private void CopyToPageLayout()
        {
            IObjectCopy pObjectCopy = new ObjectCopyClass();
            object copyFromMap = axMapControl1.Map;
            object copiedMap = pObjectCopy.Copy(copyFromMap);//复制地图到copiedMap中
            object copyToMap = axPageLayoutControl1.ActiveView.FocusMap;
            pObjectCopy.Overwrite(copiedMap, ref copyToMap); //复制地图
            axPageLayoutControl1.ActiveView.Refresh();
        }
        #endregion
        #region 加载arctoolbox
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            arctoolbox arctbx = new arctoolbox();
            arctbx.Show();
        }
        #endregion
        #region 量测
        private void btnMeasure_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MeasuredisTool pTool = new MeasuredisTool();
            pTool.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = (ITool)pTool;
        }
        #endregion
        #region 清空相关量测对象
        private void frmMeasureResult_frmColsed()
        {
            //清空线对象
            if (pNewLineFeedback != null)
            {
                pNewLineFeedback.Stop();
                pNewLineFeedback = null;
            }
            //清空面对象
            if (pNewPolygonFeedback != null)
            {
                pNewPolygonFeedback.Stop();
                pNewPolygonFeedback = null;
                pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount); //清空点集中所有点
            }
            //清空量算画的线、面对象
            axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
            //结束量算功能
            pMouseOperate = string.Empty;
            axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }
        #endregion
        #region 获取地图单位
        public string GetMapUnit(esriUnits _esriMapUnit)
        {
            string sMapUnits = string.Empty;
            switch (_esriMapUnit)
            {
                case esriUnits.esriCentimeters:
                    sMapUnits = "厘米";
                    break;
                case esriUnits.esriDecimalDegrees:
                    sMapUnits = "十进制";
                    break;
                case esriUnits.esriDecimeters:
                    sMapUnits = "分米";
                    break;
                case esriUnits.esriFeet:
                    sMapUnits = "尺";
                    break;
                case esriUnits.esriInches:
                    sMapUnits = "英寸";
                    break;
                case esriUnits.esriKilometers:
                    sMapUnits = "千米";
                    break;
                case esriUnits.esriMeters:
                    sMapUnits = "米";
                    break;
                case esriUnits.esriMiles:
                    sMapUnits = "英里";
                    break;
                case esriUnits.esriMillimeters:
                    sMapUnits = "毫米";
                    break;
                case esriUnits.esriNauticalMiles:
                    sMapUnits = "海里";
                    break;
                case esriUnits.esriPoints:
                    sMapUnits = "点";
                    break;
                case esriUnits.esriUnitsLast:
                    sMapUnits = "UnitsLast";
                    break;
                case esriUnits.esriUnknownUnits:
                    sMapUnits = "未知单位";
                    break;
                case esriUnits.esriYards:
                    sMapUnits = "码";
                    break;
                default:
                    break;
            }
            return sMapUnits;
        }
        #endregion
        #region AxmapControl事件点击
        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            sMapUnits = GetMapUnit(axMapControl1.Map.MapUnits);
            barCoorTxt.Text = String.Format("当前坐标：X = {0:#.###} {1} Y = {2:#.###} {3}", e.mapX,sMapUnits,e.mapY,sMapUnits);
            pMovePt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            #region 长度量算
            if (pMouseOperate == "MeasureLength")
            {
                if (pNewLineFeedback != null)
                {
                    pNewLineFeedback.MoveTo(pMovePt);
                }
                double deltaX = 0; //两点之间X差值
                double deltaY = 0; //两点之间Y差值

                if ((pPointPt != null) && (pNewLineFeedback != null))
                {
                    deltaX = pMovePt.X - pPointPt.X;
                    deltaY = pMovePt.Y - pPointPt.Y;
                    dSegmentLength = Math.Round(Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY)), 3);
                    dToltalLength = dToltalLength + dSegmentLength;
                    if (frmMeasureResult != null)
                    {
                        frmMeasureResult.lblMeasureResult.Text = String.Format(
                            "当前线段长度：{0:.###}{1};\r\n总长度为: {2:.###}{1}",
                            dSegmentLength, sMapUnits, dToltalLength);
                        dToltalLength = dToltalLength - dSegmentLength; //鼠标移动到新点重新开始计算
                    }
                    frmMeasureResult.frmClosed += new FormMeasureResult.FormClosedEventHandler(frmMeasureResult_frmColsed);
                }
            }
            #endregion
            #region 面积量算
            if (pMouseOperate == "MeasureArea")
            {
                if (pNewPolygonFeedback != null)
                {
                    pNewPolygonFeedback.MoveTo(pMovePt);
                }

                IPointCollection pPointCol = new Polygon();
                IPolygon pPolygon = new PolygonClass();
                IGeometry pGeo = null;

                ITopologicalOperator pTopo = null;
                for (int i = 0; i <= pAreaPointCol.PointCount - 1; i++)
                {
                    pPointCol.AddPoint(pAreaPointCol.get_Point(i), ref missing, ref missing);
                }
                pPointCol.AddPoint(pMovePt, ref missing, ref missing);

                if (pPointCol.PointCount < 3) return;
                pPolygon = pPointCol as IPolygon;

                if ((pPolygon != null))
                {
                    pPolygon.Close();
                    pGeo = pPolygon as IGeometry;
                    pTopo = pGeo as ITopologicalOperator;
                    //使几何图形的拓扑正确
                    pTopo.Simplify();
                    pGeo.Project(axMapControl1.Map.SpatialReference);
                    IArea pArea = pGeo as IArea;

                    frmMeasureResult.lblMeasureResult.Text = String.Format(
                        "总面积为：{0:.####}平方{1};\r\n总长度为：{2:.####}{1}",
                        pArea.Area, sMapUnits, pPolygon.Length);
                    pPolygon = null;
                }
            }
            #endregion
        }
        private void axMapControl1_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            #region 长度量算
            if (pMouseOperate == "MeasureLength")
            {
                if (frmMeasureResult != null)
                {
                    frmMeasureResult.lblMeasureResult.Text = "线段总长度为：" + dToltalLength + sMapUnits;
                }
                if (pNewLineFeedback != null)
                {
                    pNewLineFeedback.Stop();
                    pNewLineFeedback = null;
                    //清空所画的线对象
                    (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                dToltalLength = 0;
                dSegmentLength = 0;
            }
            #endregion
            #region 面积量算
            if (pMouseOperate == "MeasureArea")
            {
                if (pNewPolygonFeedback != null)
                {
                    pNewPolygonFeedback.Stop();
                    pNewPolygonFeedback = null;
                    //清空所画的线对象
                    (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
                pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount); //清空点集中所有点
            }
            #endregion
        }
        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            //屏幕坐标点转化为地图坐标点
            pPointPt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);

            if (e.button == 1)
            {
                IActiveView pActiveView = axMapControl1.ActiveView;
                IEnvelope pEnvelope = null;
                switch (pMouseOperate)
                {
                    #region 拉框放大

                    case "ZoomIn":
                        pEnvelope = axMapControl1.TrackRectangle();
                        //如果拉框范围为空则返回
                        if (pEnvelope == null || pEnvelope.IsEmpty || pEnvelope.Height == 0 || pEnvelope.Width == 0)
                        {
                            return;
                        }
                        //如果有拉框范围，则放大到拉框范围
                        pActiveView.Extent = pEnvelope;
                        pActiveView.Refresh();
                        break;

                    #endregion
                    #region 拉框缩小

                    case "ZoomOut":
                        pEnvelope = axMapControl1.TrackRectangle();

                        //如果拉框范围为空则退出
                        if (pEnvelope == null || pEnvelope.IsEmpty || pEnvelope.Height == 0 || pEnvelope.Width == 0)
                        {
                            return;
                        }
                        //如果有拉框范围，则以拉框范围为中心，缩小倍数为：当前视图范围/拉框范围
                        else
                        {
                            double dWidth = pActiveView.Extent.Width * pActiveView.Extent.Width / pEnvelope.Width;
                            double dHeight = pActiveView.Extent.Height * pActiveView.Extent.Height / pEnvelope.Height;
                            double dXmin = pActiveView.Extent.XMin -
                                           ((pEnvelope.XMin - pActiveView.Extent.XMin) * pActiveView.Extent.Width /
                                            pEnvelope.Width);
                            double dYmin = pActiveView.Extent.YMin -
                                           ((pEnvelope.YMin - pActiveView.Extent.YMin) * pActiveView.Extent.Height /
                                            pEnvelope.Height);
                            double dXmax = dXmin + dWidth;
                            double dYmax = dYmin + dHeight;
                            pEnvelope.PutCoords(dXmin, dYmin, dXmax, dYmax);
                        }
                        pActiveView.Extent = pEnvelope;
                        pActiveView.Refresh();
                        break;

                    #endregion
                    #region 漫游

                    case "Pan":
                        axMapControl1.Pan();
                        break;

                    #endregion
                    #region 点击查询
                    case "selectbypoint":
                        //构造用于点选的矩形 
                        IPoint ppoint = this.axMapControl1.ToMapPoint(e.x, e.y);
                        ppoint.X = e.mapX;
                        ppoint.Y = e.mapY;
                        IEnvelope ppenv = this.axMapControl1.Extent;
                        double ratio = (ppenv.Width + ppenv.Height) / 2;
                        ppenv.Width = ratio / 50;
                        ppenv.Height = ratio / 50;
                        ppenv.CenterAt(ppoint);
                        IGeometry pgeo = ppenv as IGeometry;
                        //定义一个空间过滤器 
                        IQueryFilter pqueryfilter = null;
                        ISpatialFilter pspatialfilter = new SpatialFilterClass();
                        pspatialfilter.Geometry = pgeo;
                        pspatialfilter.GeometryField = "shape";
                        pspatialfilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        pqueryfilter = pspatialfilter as IQueryFilter;
                        //定义变量 
                        UID puid = new UIDClass();
                        puid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
                        IFeatureLayer pfeaturelyr = null;
                        IFeatureSelection pfeaturesel = null;
                        ISelectionSet pselset = null;
                        IFeatureCursor pfcursor = null;
                        ICursor pcursor = null;
                        IFeature pfeature = null;
                        esriGeometryType esrigeotype;
                        //弹出FrmIdentify
                        if (frmident == null || frmident.IsDisposed)
                        {
                            frmident = new FrmIdentify();
                            //frmident.Show(); 
                        }
                        frmident.Show();
                        //frmident.Location = new System.Drawing.Point(110,110);
                        //清空FrmIdentify的LISTBOX 
                        frmident.clearlistbox();
                        //读取地图中的所有特征层 
                        IEnumLayer penumlyr = this.axMapControl1.Map.get_Layers(puid, true);
                        //使用用于点选的矩形把与其将相交的要素加入选择集， 
                        penumlyr.Reset();
                        pfeaturelyr = penumlyr.Next() as IFeatureLayer;
                        while (pfeaturelyr != null)
                        {
                            pfeaturesel = pfeaturelyr as IFeatureSelection;
                            pfeaturesel.SelectFeatures(pqueryfilter, esriSelectionResultEnum.esriSelectionResultNew, false);
                            pfeaturesel.SelectionColor = GetColor(0, 200, 100);
                            //闪烁所有选中的要素 
                            if (pfeaturelyr.Visible == true)
                            {
                                ////将图层作为参数传递给FrmIdentify
                                //frmident.getselfeaturelayer(pfeaturelyr);
                                pselset = pfeaturesel.SelectionSet;
                                pselset.Search(null, true, out pcursor);
                                pfcursor = pcursor as IFeatureCursor;
                                //将游标作为参数传给FrmIdentify
                                frmident.getselfeaturescursor(pfcursor);
                                pfeature = pfcursor.NextFeature();
                                esrigeotype = pfeaturelyr.FeatureClass.ShapeType;
                                while (pfeature != null)
                                {
                                    //闪烁选中的特征
                                    this.axMapControl1.FlashShape(pfeature.Shape, 2, 50, GetSymbol(GetColor(255, 20, 20), esrigeotype));
                                    pfeature = pfcursor.NextFeature();
                                }
                            }
                            pfeaturelyr = penumlyr.Next() as IFeatureLayer;
                        }
                        this.axMapControl1.Refresh();
                        break;
                    #endregion
                    #region 距离量算
                    case "MeasureLength":
                        //判断追踪线对象是否为空，若是则实例化并设置当前鼠标点为起始点
                        if (pNewLineFeedback == null)
                        {
                            //实例化追踪线对象
                            pNewLineFeedback = new NewLineFeedbackClass();
                            pNewLineFeedback.Display = (axMapControl1.Map as IActiveView).ScreenDisplay;
                            //设置起点，开始动态线绘制
                            pNewLineFeedback.Start(pPointPt);
                            dToltalLength = 0;
                        }
                        else //如果追踪线对象不为空，则添加当前鼠标点
                        {
                            pNewLineFeedback.AddPoint(pPointPt);
                        }
                        //pGeometry = m_PointPt;
                        if (dSegmentLength != 0)
                        {
                            dToltalLength = dToltalLength + dSegmentLength;
                        }
                        break;
                    #endregion
                    #region 面积量算
                    case "MeasureArea":
                        if (pNewPolygonFeedback == null)
                        {
                            //实例化追踪面对象
                            pNewPolygonFeedback = new NewPolygonFeedback();
                            pNewPolygonFeedback.Display = (axMapControl1.Map as IActiveView).ScreenDisplay;
                            ;
                            pAreaPointCol.RemovePoints(0, pAreaPointCol.PointCount);
                            //开始绘制多边形
                            pNewPolygonFeedback.Start(pPointPt);
                            pAreaPointCol.AddPoint(pPointPt, ref missing, ref missing);
                        }
                        else
                        {
                            pNewPolygonFeedback.AddPoint(pPointPt);
                            pAreaPointCol.AddPoint(pPointPt, ref missing, ref missing);
                        }
                        break;
                    #endregion
                    default:
                        break;
                }
            }
        }
        private void axMapControl1_OnExtentUpdated(object sender, IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            pEnv = (IEnvelope)e.newEnvelope;
            //添加矩形框之前要清除掉鹰眼视图的所有图形元素            
            IGraphicsContainer pGraphicsContainer = EagleMapControl.Map as IGraphicsContainer;
            pGraphicsContainer.DeleteAllElements();
            //添加矩形框
            //首先获取矩形框的位置
            //矩形框的位置也就是主视图当前范围     
            //设置图形元素的几何图形（位置）
            IEnvelope pEnvelope = (IEnvelope)e.newEnvelope;// //获取主视图的包络线            
            IRectangleElement pRectangleElement = new RectangleElementClass();//声明一个矩形元素
            IElement pElement = (IElement)pRectangleElement;
            pElement.Geometry = pEnvelope; //图形元素的几何形状就是主视图的包络线
            //设置鹰眼图中的红线框
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255; pColor.Green = 0; pColor.Blue = 0;
            //产生一个线符号对象
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 3;
            pOutline.Color = pColor;
            //设置颜色属性
            pColor = new RgbColorClass();
            pColor.Transparency = 0;
            //设置填充符号的属性
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            //声明填充颜色pFillColor对象            
            IRgbColor pFillColor = new RgbColorClass();
            //填充图形元素
            IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);
            IActiveView pActiveView = pGraphicsContainer as IActiveView; pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            IActiveView pActiveView = (IActiveView)axPageLayoutControl1.ActiveView.FocusMap;
            IDisplayTransformation displayTransformation = pActiveView.ScreenDisplay.DisplayTransformation;
            displayTransformation.VisibleBounds = axMapControl1.Extent;
            axPageLayoutControl1.ActiveView.Refresh();
            CopyToPageLayout();
        }
        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            if (axMapControl1.LayerCount > 0)
            {
                EagleMapControl.Map = new MapClass();
            }
            for (int i = 0; i <= axMapControl1.Map.LayerCount - 1; i++)
            {
                EagleMapControl.AddLayer(axMapControl1.get_Layer(i));
            }
            EagleMapControl.Extent = axMapControl1.FullExtent;
            EagleMapControl.Refresh();
        }
        private void axTOCControl1_OnMouseDown_1(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
                IBasicMap pMap = null;
                ILayer pLayer = null;
                object unk = null;
                object data = null;
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                pTocFeatureLayer = pLayer as IFeatureLayer;
                if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    btnLayerSel.Enabled = !pTocFeatureLayer.Selectable;
                    btnLayerUnSel.Enabled = pTocFeatureLayer.Selectable;
                    contextMenuStrip.Show(Control.MousePosition);
                }
            }
        }
        private void axTOCControl1_OnDoubleClick_1(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            ILayer layer = null;
            object unk = null;
            object data = null;
            axTOCControl1.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);
            if (e.button == 1)
            {
                if (itemType == esriTOCControlItem.esriTOCControlItemLegendClass)
                {
                    //取得图例
                    ILegendClass pLegendClass = ((ILegendGroup)unk).get_Class((int)data);
                    //创建符号选择器SymbolSelector实例
                    frmSymbolSelector SymbolSelectorFrm = new frmSymbolSelector(pLegendClass, layer);
                    if (SymbolSelectorFrm.ShowDialog() == DialogResult.OK)
                    {
                        //局部更新主Map控件
                        axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                        //设置新的符号
                        pLegendClass.Symbol = SymbolSelectorFrm.pSymbol;
                        //更新主Map控件和图层控件
                        this.axMapControl1.ActiveView.Refresh();
                        this.axTOCControl1.Refresh();
                    }
                }
            }
        }
        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
          {
              try
              {
                  if (_enumMapSurType != EnumMapSurroundType.None)
                  {
                      IActiveView pActiveView = null;
                      pActiveView = axPageLayoutControl1.PageLayout as IActiveView;
                      m_PointPt = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                      if (pNewEnvelopeFeedback == null)
                      {
                          pNewEnvelopeFeedback = new NewEnvelopeFeedbackClass();
                          pNewEnvelopeFeedback.Display = pActiveView.ScreenDisplay;
                          pNewEnvelopeFeedback.Start(m_PointPt);
                      }
                      else
                      {
                          pNewEnvelopeFeedback.MoveTo(m_PointPt);
                      }

                  }
              }
              catch
              {
              }
          }
        private void axPageLayoutControl1_OnMouseMove(object sender, IPageLayoutControlEvents_OnMouseMoveEvent e)
          {
              try
              {
                  if (_enumMapSurType != EnumMapSurroundType.None)
                  {
                      if (pNewEnvelopeFeedback != null)
                      {
                          m_MovePt = (axPageLayoutControl1.PageLayout as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                          pNewEnvelopeFeedback.MoveTo(m_MovePt);
                      }
                  }
              }
              catch (Exception ex)
              {

              }
          }
        private void axPageLayoutControl1_OnMouseUp(object sender, IPageLayoutControlEvents_OnMouseUpEvent e)
          {
              if (_enumMapSurType != EnumMapSurroundType.None)
              {
                  if (pNewEnvelopeFeedback != null)
                  {
                      IActiveView pActiveView = null;
                      pActiveView = axPageLayoutControl1.PageLayout as IActiveView;
                      IEnvelope pEnvelope = pNewEnvelopeFeedback.Stop();
                      AddMapSurround(pActiveView, _enumMapSurType, pEnvelope);
                      pNewEnvelopeFeedback = null;
                      _enumMapSurType = EnumMapSurroundType.None;
                  }
              }
          }
        private void EagleMapControl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (EagleMapControl.Map.LayerCount > 0)
            {
                //按下鼠标左键移动矩形框
                if (e.button == 1)
                {
                    //如果指针落在鹰眼的矩形框中，标记可移动
                    if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
                    {
                        bCanDrag = true;
                    }
                    pMoveRectPoint = new PointClass();
                    pMoveRectPoint.PutCoords(e.mapX, e.mapY);  //记录点击的第一个点的坐标
                }
                //按下鼠标右键绘制矩形框
                else if (e.button == 2)
                {
                    IEnvelope pEnvelope = EagleMapControl.TrackRectangle();

                    IPoint pTempPoint = new PointClass();
                    pTempPoint.PutCoords(pEnvelope.XMin + pEnvelope.Width / 2, pEnvelope.YMin + pEnvelope.Height / 2);
                    axMapControl1.Extent = pEnvelope;
                    //矩形框的高宽和数据试图的高宽不一定成正比，这里做一个中心调整
                    axMapControl1.CenterAt(pTempPoint);
                }
            }
        }
        private void EagleMapControl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
            {
                //如果鼠标移动到矩形框中，鼠标换成小手，表示可以拖动
                EagleMapControl.MousePointer = esriControlsMousePointer.esriPointerHand;
                if (e.button == 2)  //如果在内部按下鼠标右键，将鼠标演示设置为默认样式
                {
                    EagleMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                }

            }
            else
            {
                //在其他位置将鼠标设为默认的样式
                EagleMapControl.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }
            if (bCanDrag)
            {
                double Dx, Dy;  //记录鼠标移动的距离
                Dx = e.mapX - pMoveRectPoint.X;
                Dy = e.mapY - pMoveRectPoint.Y;
                pEnv.Offset(Dx, Dy); //根据偏移量更改 pEnv 位置
                pMoveRectPoint.PutCoords(e.mapX, e.mapY);
                DrawRectangle(pEnv);
                axMapControl1.Extent = pEnv;
            }
        }
        private void EagleMapControl_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (e.button == 1 && pMoveRectPoint != null)
            {
                if (e.mapX == pMoveRectPoint.X && e.mapY == pMoveRectPoint.Y)
                {
                    axMapControl1.CenterAt(pMoveRectPoint);
                }
                bCanDrag = false;
            }
        }
        #endregion
        #region 改变比例尺
        private void barEditItem1_TextChanged(object sender, EventArgs e)
        {
            IMap map = axMapControl1.ActiveView.FocusMap;
            map.MapScale = Convert.ToDouble(this.barEditItem1.Text.ToString().Split(':')[1]);
            axMapControl1.ActiveView.Refresh();
            axPageLayoutControl1.ActiveView.Refresh();
        } 
        #endregion
        #region 自定义函数
        private IColor GetColor(byte red, byte green, byte blue)
        {
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = red;
            pRgbColor.Green = green;
            pRgbColor.Blue = blue;
            IColor pColor = pRgbColor as IColor;
            return pColor;
        }
        private ISymbol GetSymbol(IColor pColor, esriGeometryType geotype)
        {
            ISymbol pSymbol = null;
            if (geotype == esriGeometryType.esriGeometryPoint)
            {
                ISimpleMarkerSymbol pSMakersym = new SimpleMarkerSymbolClass();
                pSMakersym.Color = pColor;
                pSMakersym.Style = esriSimpleMarkerStyle.esriSMSDiamond;
                pSMakersym.Size = 10;
                pSymbol = pSMakersym as ISymbol;
            }
            else if (geotype == esriGeometryType.esriGeometryPolyline)
            {
                ISimpleLineSymbol pSLinesym = new SimpleLineSymbolClass();
                pSLinesym.Color = pColor;
                pSLinesym.Style = esriSimpleLineStyle.esriSLSDashDot;
                pSLinesym.Width = 2;
                pSymbol = pSLinesym as ISymbol;
            }
            else if (geotype == esriGeometryType.esriGeometryPolygon)
            {
                ISimpleFillSymbol pSFillsym = new SimpleFillSymbolClass();
                pSFillsym.Color = pColor;
                pSFillsym.Style = esriSimpleFillStyle.esriSFSCross;
                pSymbol = pSFillsym as ISymbol;
            }
            return pSymbol;
        }
        //输入RGB值，蝴蝶IRgbColor型值
        public IRgbColor GetRgbColor(int intR, int intG, int intB) 
        {
            IRgbColor pRgbColor = null;
            if (intR < 0 || intR > 255 || intG < 0 || intG > 255 || intB < 0 || intB > 255) {
                return pRgbColor;
            }
            pRgbColor = new RgbColorClass();
            pRgbColor.Red = intR;
            pRgbColor.Green = intG;
            pRgbColor.Blue = intB;
            return pRgbColor;
        }
        //输入HSV值，获得IHSVColor型值
        public IHsvColor GetHSVColor(int intH, int intS, int intV)
        {
            IHsvColor pHSVColor = null;
            if (intH < 0 || intH > 360 || intS < 0 || intS > 100 || intV < 0 || intV > 100)
            {
                return pHSVColor;
            }
            pHSVColor.Hue = intH;
            pHSVColor.Saturation = intS;
            pHSVColor.Value = intV;
            return pHSVColor;
        }
        //创建色带
        public IColorRamp CreateAlgorithmicColorRamp() {
            //创建一个新AlgorithmicColorRampClass对象
            IAlgorithmicColorRamp pAlgorithmicColorRamp = new AlgorithmicColorRampClass();
            IRgbColor pFormColor = new RgbColorClass();
            IRgbColor pToColor = new RgbColorClass();
            //创建起始颜色对象
            pFormColor.Red = 255; pFormColor.Green = 0; pFormColor.Blue = 0;
            //创建终止颜色对象
            pToColor.Red = 0; pToColor.Green = 255; pToColor.Blue = 0;
            //设置AlgorithmicColorRampClass的起止颜色属性
            pAlgorithmicColorRamp.ToColor = pFormColor; pAlgorithmicColorRamp.FromColor = pToColor;
            //设置梯度类型
            pAlgorithmicColorRamp.Algorithm = esriColorRampAlgorithm.esriCIELabAlgorithm;
            pAlgorithmicColorRamp.Size = 10;//设置颜色带颜色数量
            //创建颜色带
            bool bture = true;
            pAlgorithmicColorRamp.CreateRamp(out bture);
            return pAlgorithmicColorRamp;
        }
        private void AddMapSurround(IActiveView pAV, EnumMapSurroundType _enumMapSurroundType, IEnvelope pEnvelope)
        {
            try
            {
                switch (_enumMapSurroundType)
                {
                    case EnumMapSurroundType.NorthArrow:
                        addNorthArrow(axPageLayoutControl1.PageLayout, pEnvelope, pAV);
                        break;
                    case EnumMapSurroundType.ScaleBar:
                        makeScaleBar(pAV, axPageLayoutControl1.PageLayout, pEnvelope);
                        break;
                    case EnumMapSurroundType.Legend:
                        MakeLegend(pAV, axPageLayoutControl1.PageLayout, pEnvelope);
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void MakeLegend(IActiveView pActiveView, IPageLayout pPageLayout, IEnvelope pEnv)
        {
            UID pID = new UID();
            pID.Value = "esriCarto.Legend";
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveView.FocusMap) as IMapFrame;
            IMapSurroundFrame pMapSurroundFrame = pMapFrame.CreateSurroundFrame(pID, null);//根据唯一标示符，创建与之对应MapSurroundFrame
            IElement pDeletElement = axPageLayoutControl1.FindElementByName("Legend");//获取PageLayout中的图例元素
            if (pDeletElement != null)
            {
                pGraphicsContainer.DeleteElement(pDeletElement);  //如果已经存在图例，删除已经存在的图例
            }
            //设置MapSurroundFrame背景
            ISymbolBackground pSymbolBackground = new SymbolBackgroundClass();
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            ILineSymbol pLineSymbol = new SimpleLineSymbolClass();
            pLineSymbol.Color = m_OperatePageLayout.GetRgbColor(0, 0, 0);
            pFillSymbol.Color = m_OperatePageLayout.GetRgbColor(240, 240, 240);
            pFillSymbol.Outline = pLineSymbol;
            pSymbolBackground.FillSymbol = pFillSymbol;
            pMapSurroundFrame.Background = pSymbolBackground;
            //添加图例
            IElement pElement = pMapSurroundFrame as IElement;
            pElement.Geometry = pEnv as IGeometry;
            IMapSurround pMapSurround = pMapSurroundFrame.MapSurround;
            ILegend pLegend = pMapSurround as ILegend;
            pLegend.ClearItems();
            pLegend.Title = "图例";
            for (int i = 0; i < pActiveView.FocusMap.LayerCount; i++)
            {
                ILegendItem pLegendItem = new HorizontalLegendItemClass();
                pLegendItem.Layer = pActiveView.FocusMap.get_Layer(i);//获取添加图例关联图层             
                pLegendItem.ShowDescriptions = false;
                pLegendItem.Columns = 1;
                pLegendItem.ShowHeading = true;
                pLegendItem.ShowLabels = true;
                pLegend.AddItem(pLegendItem);//添加图例内容
            }
            pGraphicsContainer.AddElement(pElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);            
        }
        private void frmSym_GetSelSymbolItem(ref IStyleGalleryItem pStyleItem)
        {
            pStyleGalleryItem = pStyleItem;
        }
        void addNorthArrow(IPageLayout pPageLayout, IEnvelope pEnv, IActiveView pActiveView)
        {
            IMap pMap = pActiveView.FocusMap;
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            if (pStyleGalleryItem == null) return;
            IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
            pMapSurroundFrame.MapFrame = pMapFrame;
            INorthArrow pNorthArrow = new MarkerNorthArrowClass();
            pNorthArrow = pStyleGalleryItem.Item as INorthArrow;
            pNorthArrow.Size = pEnv.Width*50;
            pMapSurroundFrame.MapSurround = (IMapSurround)pNorthArrow;//根据用户的选取，获取相应的MapSurround            
            IElement pElement = axPageLayoutControl1.FindElementByName("NorthArrows");//获取PageLayout中的指北针元素
            if (pElement != null)
            {
                pGraphicsContainer.DeleteElement(pElement);  //如果存在指北针，删除已经存在的指北针
            }
            IElementProperties pElePro = null;
            pElement = (IElement)pMapSurroundFrame;
            pElement.Geometry = (IGeometry)pEnv;
            pElePro = pElement as IElementProperties;
            pElePro.Name = "NorthArrows";
            pGraphicsContainer.AddElement(pElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        public void makeScaleBar(IActiveView pActiveView, IPageLayout pPageLayout, IEnvelope pEnv)
        {           
            IMap pMap = pActiveView.FocusMap;
            IGraphicsContainer pGraphicsContainer = pPageLayout as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            if (pStyleGalleryItem == null) return;
            IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrameClass();
            pMapSurroundFrame.MapFrame = pMapFrame;
            pMapSurroundFrame.MapSurround = (IMapSurround)pStyleGalleryItem.Item;
            IElement pElement = axPageLayoutControl1.FindElementByName("ScaleBar");
            if (pElement != null)
            {
                pGraphicsContainer.DeleteElement(pElement);  //删除已经存在的比例尺
            }
            IElementProperties pElePro = null;
            pElement = (IElement)pMapSurroundFrame;
            pElement.Geometry = (IGeometry)pEnv;
            pElePro = pElement as IElementProperties;
            pElePro.Name = "ScaleBar";
            pGraphicsContainer.AddElement(pElement, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        public void CreateMeasuredGrid1(IActiveView pActiveView, IPageLayout pPageLayout)
        {
            IMap map = pActiveView.FocusMap;
            IMeasuredGrid pMeasuredGrid = new MeasuredGridClass();
            //设置格网基本属性           
            pMeasuredGrid.FixedOrigin = false;
            pMeasuredGrid.Units = map.MapUnits;
            pMeasuredGrid.XIntervalSize = 5;//纬度间隔           
            pMeasuredGrid.YIntervalSize = 5;//经度间隔.             
            //设置GridLabel格式
            IGridLabel pGridLabel = new FormattedGridLabelClass();
            IFormattedGridLabel pFormattedGridLabel = new FormattedGridLabelClass();
            INumericFormat pNumericFormat = new NumericFormatClass();
            pNumericFormat.AlignmentOption = esriNumericAlignmentEnum.esriAlignLeft;
            pNumericFormat.RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
            pNumericFormat.RoundingValue = 0;
            pNumericFormat.ZeroPad = true;
            pFormattedGridLabel.Format = pNumericFormat as INumberFormat;
            pGridLabel = pFormattedGridLabel as IGridLabel;
            StdFont myFont = new stdole.StdFontClass();
            myFont.Name = "宋体";
            myFont.Size = 25;
            pGridLabel.Font = myFont as IFontDisp;
            IMapGrid pMapGrid = new MeasuredGridClass();
            pMapGrid = pMeasuredGrid as IMapGrid;
            pMapGrid.LabelFormat = pGridLabel;
            //将格网添加到地图上           
            IGraphicsContainer graphicsContainer = pPageLayout as IGraphicsContainer;
            IFrameElement frameElement = graphicsContainer.FindFrame(map);
            IMapFrame mapFrame = frameElement as IMapFrame;
            IMapGrids mapGrids = null;
            mapGrids = mapFrame as IMapGrids;
            mapGrids.AddMapGrid(pMapGrid);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
        }
        //在鹰眼地图上面画矩形框
        private void DrawRectangle(IEnvelope pEnvelope)
        {
            //在绘制前，清除鹰眼中之前绘制的矩形框
            IGraphicsContainer pGraphicsContainer = EagleMapControl.Map as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            pGraphicsContainer.DeleteAllElements();
            //得到当前视图范围
            IRectangleElement pRectangleElement = new RectangleElementClass();
            IElement pElement = pRectangleElement as IElement;
            pElement.Geometry = pEnvelope;
            //设置矩形框（实质为中间透明度面）
            IRgbColor pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pColor.Transparency = 255;
            ILineSymbol pOutLine = new SimpleLineSymbolClass();
            pOutLine.Width = 2;
            pOutLine.Color = pColor;

            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pColor = new RgbColorClass();
            pColor.Transparency = 0;
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutLine;
            //向鹰眼中添加矩形框
            IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;
            pFillShapeElement.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement((IElement)pFillShapeElement, 0);
            //刷新
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
        #endregion
        #region 导入导出
        #region 导入数据
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddDataForm addform = new AddDataForm();
            addform.Show();
        }
        #endregion
        #region 批量导入
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BatAddForm badform = new BatAddForm();
            badform.Show();
        }
        #endregion
        #region 导出
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportForm exf = new ExportForm();
            exf.Show();
        }
        #endregion
        #region 批量导出
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            batchExport beform = new batchExport();
            beform.Show();
        }
        #endregion
        #endregion
        #region 右键菜单
        #region 属性表窗口
        private void btnAttribute_Click(object sender, EventArgs e)
        {
            if (frmAttribute == null || frmAttribute.IsDisposed)
            {
                frmAttribute = new FormAttribute();
            }
            frmAttribute.CurFeatureLayer = pTocFeatureLayer;
            frmAttribute.InitUI();
            frmAttribute.ShowDialog();
        }
        #endregion
        #region 缩放到图层
        private void btnZoomToLayer_Click(object sender, EventArgs e)
        {
            if (pTocFeatureLayer == null) return;
            (axMapControl1.Map as IActiveView).Extent = pTocFeatureLayer.AreaOfInterest;
            (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
        #endregion
        #region 移除图层
        private void btnRemoveLayer_Click(object sender, EventArgs e)
        {

            try
            {
                if (pTocFeatureLayer == null) return;
                DialogResult result = MessageBox.Show("是否删除[" + pTocFeatureLayer.Name + "]图层", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    axMapControl1.Map.DeleteLayer(pTocFeatureLayer);
                }
                axMapControl1.ActiveView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        #endregion
        #region 图层选择
        private void btnLayerSel_Click(object sender, EventArgs e)
        {
            pTocFeatureLayer.Selectable = true;
            btnLayerSel.Enabled = !btnLayerSel.Enabled;
        }
        #endregion
        #region 取消图层选择
        private void btnLayerUnSel_Click(object sender, EventArgs e)
        {
            pTocFeatureLayer.Selectable = false;
            btnLayerUnSel.Enabled = !btnLayerUnSel.Enabled;
        }
        #endregion
        #region 点状符号化
        #region 获取图层
        private IFeatureClass GetPFeatureClass()
        {
            IFeatureLayer pFeatureLayer = pTocFeatureLayer as IFeatureLayer;
                        IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            return pFeatureClass;
        }
        #endregion
        private void 简单符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPoint == pFeatureClass.ShapeType)
            {
                try
                {
                    ////获取目标图层
                    ILayer pLayer = new FeatureLayerClass();
                    pLayer = pTocFeatureLayer;
                    IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                    //设置点符号
                    ISimpleMarkerSymbol pMarkerSymbol = new SimpleMarkerSymbol();
                    pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSSquare;//设置点符号样式为方形
                    IRgbColor pRgbColor = new RgbColor();
                    pRgbColor = GetRgbColor(225, 100, 100);
                    pMarkerSymbol.Color = pRgbColor;//设置点符号颜色
                    ISymbol pSymbol = (ISymbol)pMarkerSymbol;
                    //更改符号样式
                    ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                    pSimpleRenderer.Symbol = pSymbol;
                    pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                    axMapControl1.Refresh();
                    axTOCControl1.Update();
                }
                catch
                {
                    MessageBox.Show("请输入有效图层!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("点击非点图层");
            }
        }
        private void 箭头符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPoint == pFeatureClass.ShapeType){
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = pTocFeatureLayer;
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置点符号
                IArrowMarkerSymbol pArrowMarkerSymbol = new ArrowMarkerSymbolClass();
                IRgbColor pRgbColor = new RgbColor();
                pRgbColor = GetRgbColor(255, 100, 0);
                pArrowMarkerSymbol.Angle = 90;
                pArrowMarkerSymbol.Color = pRgbColor;
                pArrowMarkerSymbol.Length = 20;//设置简单顶点到底边的距离
                pArrowMarkerSymbol.Width = 10;//设置箭头底边宽度
                pArrowMarkerSymbol.XOffset = 0;
                pArrowMarkerSymbol.YOffset = 0;
                pArrowMarkerSymbol.Style = esriArrowMarkerStyle.esriAMSPlain;//设置点符号样式
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pArrowMarkerSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch
            {
                MessageBox.Show("请输入有效图层", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
            else
            {
                MessageBox.Show("点击非点图层");
            }
        }
        private void 字符符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPoint == pFeatureClass.ShapeType){
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = pTocFeatureLayer;
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置点符号
                ICharacterMarkerSymbol pCharacterMarkerSymbol = new CharacterMarkerSymbol();
                stdole.IFontDisp pFontDisp = (stdole.IFontDisp)(new stdole.StdFontClass());
                IRgbColor pRgbColor = new RgbColor();
                pRgbColor = GetRgbColor(255, 0, 0);
                pFontDisp.Name = "arial";//设置字体样式          
                pFontDisp.Italic = true;//设置是否采用斜体字符
                pCharacterMarkerSymbol.Angle = 0;
                pCharacterMarkerSymbol.CharacterIndex = 65;
                pCharacterMarkerSymbol.Color = pRgbColor;
                pCharacterMarkerSymbol.Font = pFontDisp;
                pCharacterMarkerSymbol.Size = 10;
                pCharacterMarkerSymbol.XOffset = 3;
                pCharacterMarkerSymbol.YOffset = 3;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pCharacterMarkerSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch
            {
                MessageBox.Show("请输入有效图层", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
            else
            {
                MessageBox.Show("点击非点图层");
            }
        }      
        private void 图片类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPoint == pFeatureClass.ShapeType){
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = pTocFeatureLayer;
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置点符号           
                IPictureMarkerSymbol pPictureMarkerSymbol = new PictureMarkerSymbolClass();
                string fileName = m_OperateMap.getPath(filepath1) + "\\data\\Symbol\\city.bmp";
                pPictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, fileName);//图片类型和图片来源               
                pPictureMarkerSymbol.Angle = 0;
                pPictureMarkerSymbol.Size = 10;
                pPictureMarkerSymbol.XOffset = 0;
                pPictureMarkerSymbol.YOffset = 0;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pPictureMarkerSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch
            {
                MessageBox.Show("请输入有效图层", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            }
            else
            {
                MessageBox.Show("点击非点图层");
            }
        }
        private void 叠加类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPoint == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = pTocFeatureLayer;
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置点符号
                IMultiLayerMarkerSymbol pMultiLayerMarkerSymbol = new MultiLayerMarkerSymbolClass();
                IPictureMarkerSymbol pPictureMarkerSymbol = new PictureMarkerSymbolClass();
                ICharacterMarkerSymbol pCharacterMarkerSymbol = new CharacterMarkerSymbol();
                stdole.IFontDisp fontDisp = (stdole.IFontDisp)(new stdole.StdFontClass());
                IRgbColor pGgbColor = new RgbColor();
                pGgbColor = GetRgbColor(0, 0, 0);
                fontDisp.Name = "arial";
                fontDisp.Size = 12;
                fontDisp.Italic = true;
                //创建字符符号
                pCharacterMarkerSymbol.Angle = 0;
                pCharacterMarkerSymbol.CharacterIndex = 97;//字母a
                pCharacterMarkerSymbol.Color = pGgbColor;
                pCharacterMarkerSymbol.Font = fontDisp;
                pCharacterMarkerSymbol.Size = 24;
                //创建图片符号           
                string fileName = m_OperateMap.getPath(filepath1) + "\\data\\Symbol\\city.bmp"; ;
                pPictureMarkerSymbol.CreateMarkerSymbolFromFile(esriIPictureType.esriIPictureBitmap, fileName);
                pPictureMarkerSymbol.Angle = 0;
                pPictureMarkerSymbol.BitmapTransparencyColor = pGgbColor;
                pPictureMarkerSymbol.Size = 10;
                //添加图片、字符符号到组合符号中
                pMultiLayerMarkerSymbol.AddLayer(pCharacterMarkerSymbol);
                pMultiLayerMarkerSymbol.AddLayer(pPictureMarkerSymbol);
                pMultiLayerMarkerSymbol.Angle = 0;
                pMultiLayerMarkerSymbol.Size = 20;
                pMultiLayerMarkerSymbol.XOffset = 5;
                pMultiLayerMarkerSymbol.YOffset = 5;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pMultiLayerMarkerSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch
            {
                MessageBox.Show("请输入有效图层", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                        }
            else
            {
                MessageBox.Show("点击非点图层");
            }
        }       
        #endregion
        #region 线状符号化
        private void 简单符号ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolyline == pFeatureClass.ShapeType)
            {
             try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = pTocFeatureLayer;
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置线符号
                ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
                simpleLineSymbol.Width = 0;//定义线的宽度 
                simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame; //定义线的样式                               
                simpleLineSymbol.Color = GetRgbColor(255, 100, 0);//定义线的颜色
                ISymbol symbol = simpleLineSymbol as ISymbol;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = symbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch (Exception ex)
            {
                
            }
            }
            else
            {
                MessageBox.Show("点击非线图层");
            }
        }
        private void 制图符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolyline == pFeatureClass.ShapeType)
            {
             try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(1);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置线符号
                ICartographicLineSymbol pCartographicLineSymbol = new CartographicLineSymbolClass();
                pCartographicLineSymbol.Cap = esriLineCapStyle.esriLCSRound;//设置线要素首尾端点形状为圆形
                pCartographicLineSymbol.Join = esriLineJoinStyle.esriLJSRound; //设置线要素转折点出的样式为圆滑    
                pCartographicLineSymbol.Width = 2;
                //设置线要素符号模板
                ILineProperties pLineProperties;
                pLineProperties = pCartographicLineSymbol as ILineProperties;
                pLineProperties.Offset = 0;
                double[] dob = new double[6];
                dob[0] = 0;
                dob[1] = 1;
                dob[2] = 2;
                dob[3] = 3;
                dob[4] = 4;
                dob[5] = 5;
                ITemplate pTemplate = new TemplateClass();
                pTemplate.Interval = 1;
                for (int i = 0; i < dob.Length; i += 2)
                {
                    pTemplate.AddPatternElement(dob[i], dob[i + 1]);
                }
                pLineProperties.Template = pTemplate;
                IRgbColor pRgbColor = GetRgbColor(0, 255, 0);
                pCartographicLineSymbol.Color = pRgbColor;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pCartographicLineSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch
            { }
            }
            else
            {
                MessageBox.Show("点击非线图层");
            }
        }
        private void 多图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolyline == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(1);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置线符号
                IMultiLayerLineSymbol pMultiLayerLineSymbol = new MultiLayerLineSymbolClass();
                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
                pSimpleLineSymbol.Width = 2;
                IRgbColor pRgbColor = GetRgbColor(255, 0, 0);
                pSimpleLineSymbol.Color = pRgbColor;
                //ISymbol pSymbol = pSimpleLineSymbol as ISymbol;
                //pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen; //设置线要素的颜色为

                ICartographicLineSymbol pCartographicLineSymbol = new CartographicLineSymbolClass();
                pCartographicLineSymbol.Cap = esriLineCapStyle.esriLCSRound;
                pCartographicLineSymbol.Join = esriLineJoinStyle.esriLJSRound;
                pCartographicLineSymbol.Width = 2;
                ILineProperties pLineProperties;
                pLineProperties = pCartographicLineSymbol as ILineProperties;
                pLineProperties.Offset = 0;
                double[] dob = new double[6];
                dob[0] = 0;
                dob[1] = 1;
                dob[2] = 2;
                dob[3] = 3;
                dob[4] = 4;
                dob[5] = 5;
                ITemplate pTemplate = new TemplateClass();
                pTemplate.Interval = 1;
                for (int i = 0; i < dob.Length; i += 2)
                {
                    pTemplate.AddPatternElement(dob[i], dob[i + 1]);
                }
                pLineProperties.Template = pTemplate;

                pRgbColor = GetRgbColor(0, 255, 0);
                pCartographicLineSymbol.Color = pRgbColor;
                pMultiLayerLineSymbol.AddLayer(pSimpleLineSymbol);
                pMultiLayerLineSymbol.AddLayer(pCartographicLineSymbol);
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pMultiLayerLineSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch { }
                        }
            else
            {
                MessageBox.Show("点击非线图层");
            }
        }
        private void 图片符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolyline == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(1);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置线符号
                IPictureLineSymbol pictureLineSymbol = new PictureLineSymbolClass();
                //创建图片符号                                
                string fileName = m_OperateMap.getPath(filepath1) + "\\data\\Symbol\\border.bmp";
                pictureLineSymbol.CreateLineSymbolFromFile(esriIPictureType.esriIPictureBitmap, fileName);
                IRgbColor rgbColor = GetRgbColor(255, 0, 0);
                pictureLineSymbol.Color = rgbColor;
                pictureLineSymbol.Offset = 0;
                pictureLineSymbol.Width = 3;
                pictureLineSymbol.Rotate = false;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pictureLineSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch { }
            }
            else
            {
                MessageBox.Show("点击非线图层");
            }
        }
        private void 离散符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolyline == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(1);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置线符号
                IHashLineSymbol pHashLineSymbol = new HashLineSymbolClass();
                ILineProperties pLineProperties = pHashLineSymbol as ILineProperties;
                pLineProperties.Offset = 0;
                double[] dob = new double[6];
                dob[0] = 0;
                dob[1] = 1;
                dob[2] = 2;
                dob[3] = 3;
                dob[4] = 4;
                dob[5] = 5;
                ITemplate pTemplate = new TemplateClass();
                pTemplate.Interval = 1;
                for (int i = 0; i < dob.Length; i += 2)
                {
                    pTemplate.AddPatternElement(dob[i], dob[i + 1]);
                }
                pLineProperties.Template = pTemplate;
                pHashLineSymbol.Width = 2;
                pHashLineSymbol.Angle = 45;//设置单一线段的倾斜角度
                IRgbColor pColor = new RgbColor();
                pColor = GetRgbColor(0, 0, 255);
                pHashLineSymbol.Color = pColor;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pHashLineSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch { }
            }
            else
            {
                MessageBox.Show("点击非线图层");
            }
        }
        #endregion
        #region 面符号化
        private void 简单符号ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolygon == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(2);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置面填充符号           
                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
                pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSVertical;//设置面填充为垂直线填充
                pSimpleFillSymbol.Color = GetRgbColor(150, 150, 150);
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pSimpleFillSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch { }
            }
            else
            {
                MessageBox.Show("点击非面图层");
            }
        }
        private void 线填充符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolygon == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(2);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置面填充符号                       
                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot; //定义线的样式             
                pSimpleLineSymbol.Width = 2;//定义线的宽度
                IRgbColor pRgbColor = GetRgbColor(255, 0, 0);
                pSimpleLineSymbol.Color = pRgbColor;//定义线的颜色         
                ILineFillSymbol pLineFillSymbol = new LineFillSymbol();
                pLineFillSymbol.Angle = 45;
                pLineFillSymbol.Separation = 10;
                pLineFillSymbol.Offset = 5;
                pLineFillSymbol.LineSymbol = pSimpleLineSymbol;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pLineFillSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch { }
            }
            else
            {
                MessageBox.Show("点击非面图层");
            }
        }
        private void 点填充符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolygon == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(2);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置面填充符号          
                IArrowMarkerSymbol pArrowMarkerSymbol = new ArrowMarkerSymbolClass();//设置填充点点符号样式
                IRgbColor pRgbColor = GetRgbColor(255, 0, 0);
                pArrowMarkerSymbol.Color = pRgbColor as IColor;
                pArrowMarkerSymbol.Length = 2;
                pArrowMarkerSymbol.Width = 2;
                pArrowMarkerSymbol.Style = esriArrowMarkerStyle.esriAMSPlain;

                IMarkerFillSymbol pMarkerFillSymbol = new MarkerFillSymbolClass();
                pMarkerFillSymbol.MarkerSymbol = pArrowMarkerSymbol;
                pRgbColor = GetRgbColor(255, 0, 0);
                pMarkerFillSymbol.Color = pRgbColor;
                pMarkerFillSymbol.Style = esriMarkerFillStyle.esriMFSGrid;

                IFillProperties pFillProperties = pMarkerFillSymbol as IFillProperties;
                pFillProperties.XOffset = 2;
                pFillProperties.YOffset = 2;
                pFillProperties.XSeparation = 5;
                pFillProperties.YSeparation = 5;
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pFillProperties as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch { }
            }
            else
            {
                MessageBox.Show("点击非面图层");
            }
        }
        private void 渐变色填充符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolygon == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(2);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置面填充符号          
                IGradientFillSymbol pGradientFillSymbol = new GradientFillSymbolClass();
                IAlgorithmicColorRamp pAlgorithcColorRamp = new AlgorithmicColorRampClass();//设置颜色带
                pAlgorithcColorRamp.FromColor = GetRgbColor(255, 0, 0);//颜色带的起始颜色
                pAlgorithcColorRamp.ToColor = GetRgbColor(0, 255, 0);//颜色带的终点颜色
                pAlgorithcColorRamp.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
                pGradientFillSymbol.ColorRamp = pAlgorithcColorRamp;//填充颜色带
                pGradientFillSymbol.GradientAngle = 90;//设置填充方向
                pGradientFillSymbol.GradientPercentage = 1;//控制色彩饱和度
                pGradientFillSymbol.IntervalCount = 5;//设置填充颜色带的数目
                pGradientFillSymbol.Style = esriGradientFillStyle.esriGFSLinear;//设置颜色填充带样式为线性填充
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pGradientFillSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch { }
            }
            else
            {
                MessageBox.Show("点击非面图层");
            }
        }
        private void 图片填充符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolygon == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(2);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置面填充符号          
                IPictureFillSymbol pictureFillSymbol = new PictureFillSymbolClass();
                string fileName = m_OperateMap.getPath(filepath1) + "\\data\\Symbol\\states.bmp";
                pictureFillSymbol.CreateFillSymbolFromFile(esriIPictureType.esriIPictureBitmap, fileName);

                ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
                simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
                simpleLineSymbol.Color = GetRgbColor(255, 0, 0);
                ISymbol symbol = pictureFillSymbol as ISymbol;

                pictureFillSymbol.Outline = simpleLineSymbol;//设置面要素边线颜色
                pictureFillSymbol.Angle = 0;//设置图片显示方向
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pictureFillSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch { }
            }
            else
            {
                MessageBox.Show("点击非面图层");
            }
        }
        private void 多层填充符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureClass pFeatureClass = GetPFeatureClass();
            if (esriGeometryType.esriGeometryPolygon == pFeatureClass.ShapeType)
            {
            try
            {
                //获取目标图层
                ILayer pLayer = new FeatureLayerClass();
                pLayer = axMapControl1.get_Layer(2);
                IGeoFeatureLayer pGeoFeatLyr = pLayer as IGeoFeatureLayer;
                //设置渐变色填充面符号    
                IMultiLayerFillSymbol pMultiLayerFillSymbol = new MultiLayerFillSymbolClass();
                IGradientFillSymbol pGradientFillSymbol = new GradientFillSymbolClass();
                IAlgorithmicColorRamp pAlgorithcColorRamp = new AlgorithmicColorRampClass();
                pAlgorithcColorRamp.FromColor = GetRgbColor(255, 0, 0);
                pAlgorithcColorRamp.ToColor = GetRgbColor(0, 255, 0);
                pAlgorithcColorRamp.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
                pGradientFillSymbol.ColorRamp = pAlgorithcColorRamp;
                pGradientFillSymbol.GradientAngle = 45;
                pGradientFillSymbol.GradientPercentage = 0.9;
                pGradientFillSymbol.Style = esriGradientFillStyle.esriGFSLinear;
                //设置线填充面符号
                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                pSimpleLineSymbol.Style = esriSimpleLineStyle.esriSLSDashDotDot;
                pSimpleLineSymbol.Width = 2;
                IRgbColor pRgbColor = GetRgbColor(255, 0, 0);
                pSimpleLineSymbol.Color = pRgbColor;
                ILineFillSymbol pLineFillSymbol = new LineFillSymbol();
                pLineFillSymbol.Angle = 45;
                pLineFillSymbol.Separation = 10;
                pLineFillSymbol.Offset = 5;
                pLineFillSymbol.LineSymbol = pSimpleLineSymbol;
                //组合填充符号
                pMultiLayerFillSymbol.AddLayer(pGradientFillSymbol);
                pMultiLayerFillSymbol.AddLayer(pLineFillSymbol);
                //更改符号样式
                ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                pSimpleRenderer.Symbol = pMultiLayerFillSymbol as ISymbol;
                pGeoFeatLyr.Renderer = pSimpleRenderer as IFeatureRenderer;
                axMapControl1.Refresh();
                axTOCControl1.Update();
            }
            catch { }
            }
            else
            {
                MessageBox.Show("点击非面图层");
            }
        }
        #endregion 
        #region 标注
        #region textElement标注
        private void textElment标注ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        try
            {
                if (frmTextElement == null || frmTextElement.IsDisposed)
                {
                    frmTextElement = new frmTextElement();
                    frmTextElement.TextElement += new frmTextElement.TextElementLabelEventHandler(frmTextElement_TextElement);
                }
                frmTextElement.Map = axMapControl1.Map;
                frmTextElement.InitUI();
                frmTextElement.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void frmTextElement_TextElement(string sFeatClsName, string sFieldName)
        {
            IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);
            TextElementLabel(pFeatLyr, sFieldName);
        }
        private void TextElementLabel(IFeatureLayer pFeatLyr, string sFieldName)
        {
            try
            {
                IMap pMap = axMapControl1.Map;
                //获得图层所有要素
                IFeatureClass pFeatureClass = pFeatLyr.FeatureClass;
                IFeatureCursor pFeatCursor = pFeatureClass.Search(null, true);
                IFeature pFeature = pFeatCursor.NextFeature();
                while (pFeature != null)
                {
                    IFields pFields = pFeature.Fields;
                    //找出标注字段的索引号
                    int index = pFields.FindField(sFieldName);
                    //得到要素的Envelope
                    IEnvelope pEnve = pFeature.Extent;
                    IPoint pPoint = new PointClass();
                    pPoint.PutCoords(pEnve.XMin + pEnve.Width / 2, pEnve.YMin + pEnve.Height / 2);
                    //新建字体对象
                    stdole.IFontDisp pFont;
                    pFont = new stdole.StdFontClass() as stdole.IFontDisp;
                    pFont.Name = "arial";
                    //产生一个文本符号
                    ITextSymbol pTextSymbol = new TextSymbolClass();
                    //设置文本符号的大小
                    pTextSymbol.Size = 20;
                    pTextSymbol.Font = pFont;
                    pTextSymbol.Color = m_OperateMap.GetRgbColor(255, 0, 0);
                    //产生一个文本对象
                    ITextElement pTextElement = new TextElementClass();
                    pTextElement.Text = pFeature.get_Value(index).ToString();
                    pTextElement.ScaleText = true;
                    pTextElement.Symbol = pTextSymbol;
                    IElement pElement = pTextElement as IElement;
                    pElement.Geometry = pPoint;
                    IActiveView pActiveView = pMap as IActiveView;
                    IGraphicsContainer pGraphicsContainer = pMap as IGraphicsContainer;
                    //添加元素
                    pGraphicsContainer.AddElement(pElement, 0);
                    pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                    pPoint = null;
                    pElement = null;
                    pFeature = pFeatCursor.NextFeature();
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion
        #region Annotation注记
        private void annotation注记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             try
            {
                if (frmAnnotation == null || frmAnnotation.IsDisposed)
                {
                    frmAnnotation = new frmAnnotation();
                    frmAnnotation.Annotation += new frmAnnotation.AnnotationEventHandler(frmAnnotation_Annotation);
                }
                frmAnnotation.Map = axMapControl1.Map;
                frmAnnotation.InitUI();
                frmAnnotation.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }

         //注记
        void frmAnnotation_Annotation(string sFeatClsName, string sFieldName)
        {
            IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);
            Annotation(pFeatLyr, sFieldName);
        }
        /// <summary>
        /// 注记
        /// </summary>
        /// <param name="pFeatLyr">注记图层名称</param>
        /// <param name="sFieldName">注记字段</param>
        private void Annotation(IFeatureLayer pFeatLyr, string sFieldName)
        {
            try
            {
                IGeoFeatureLayer pGeoFeatLyer = pFeatLyr as IGeoFeatureLayer;
                IAnnotateLayerPropertiesCollection pAnnoProps = pGeoFeatLyer.AnnotationProperties;
                pAnnoProps.Clear();
                //设置标注记体格式                                                                     
                ITextSymbol pTextSymbol = new TextSymbolClass();
                stdole.StdFont pFont = new stdole.StdFontClass();
                pFont.Name = "verdana";
                pFont.Size = 10;
                pTextSymbol.Font = pFont as stdole.IFontDisp;
                //设置注记放置格式
                ILineLabelPosition pPosition = new LineLabelPositionClass();
                pPosition.Parallel = false;
                pPosition.Perpendicular = true;
                ILineLabelPlacementPriorities pPlacement = new LineLabelPlacementPrioritiesClass();
                IBasicOverposterLayerProperties pBasic = new BasicOverposterLayerPropertiesClass();
                pBasic.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolyline;
                pBasic.LineLabelPlacementPriorities = pPlacement;//设置标注文本摆设路径权重
                pBasic.LineLabelPosition = pPosition;//控制文本的排放位置
                ILabelEngineLayerProperties pLableEngine = new LabelEngineLayerPropertiesClass();
                pLableEngine.Symbol = pTextSymbol;
                pLableEngine.BasicOverposterLayerProperties = pBasic;//设置标注文本的放置方式，以及处理文字间冲突的处理方式等
                pLableEngine.Expression = "[" + sFieldName + "]";//输入VBScript或JavaScript语言，设置要标注的字段
                IAnnotateLayerProperties pAnnoLayerProps = pLableEngine as IAnnotateLayerProperties;
                pAnnoProps.Add(pAnnoLayerProps);
                pGeoFeatLyer.DisplayAnnotation = true;
                 axMapControl1.Refresh(esriViewDrawPhase.esriViewBackground, null, null);
                 axMapControl1.Update();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
        #region MapTips显示
        private void mapTips显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (frmMapTips == null || frmMapTips.IsDisposed)
                {
                    frmMapTips = new frmMapTips();
                    frmMapTips.MapTips += new frmMapTips.MapTipsEventHandler(frmMapTips_MapTips);
                }
                frmMapTips.Map = axMapControl1.Map;
                frmMapTips.InitUI();
                frmMapTips.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }

        void frmMapTips_MapTips(string sFeatClsName, string sFieldName)
        {
            IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);            
            for (int i = 0; i <  axMapControl1.LayerCount; i++)
            {
                ILayer pLayers =  axMapControl1.get_Layer(i);
                IFeatureLayer pFeatLyrs = pLayers as IFeatureLayer;
                pFeatLyrs.DisplayField = null;
            }
            MapTips(pFeatLyr, sFieldName);
        }
        /// <summary>
        /// MapTips显示
        /// </summary>
        /// <param name="pFeatLyr"></param>
        /// <param name="sFieldName"></param>
        private void MapTips(IFeatureLayer pFeatLyr, string sFieldName)
        {                    
            ILayer pLayer = new FeatureLayerClass();
            pLayer = pFeatLyr;
            pLayer.ShowTips = true;
            ILayerFields pLayerFields = (ILayerFields)pFeatLyr;
            for (int i = 0; i <= pLayerFields.FieldCount - 1; i++)
            {
                IField field = pLayerFields.get_Field(i);
                if (field.Name == sFieldName)
                {
                    pFeatLyr.DisplayField = field.Name;
                    break;
                }
            }
            axMapControl1.ShowMapTips = true;
        }
        #endregion       
        #endregion
        #endregion
        #region 地图要素
        #region 添加图例
        private void AddLegend(IPageLayout pageLayout)
        {
            IActiveView pActiveView = pageLayout as IActiveView;
            IGraphicsContainer container = pageLayout as IGraphicsContainer;
            // 获得MapFrame  
            IMapFrame mapFrame = container.FindFrame(pActiveView.FocusMap) as IMapFrame;
            //根据MapSurround的uid，创建相应的MapSurroundFrame和MapSurround  
            UID uid = new UIDClass();
            uid.Value = "esriCarto.Legend";
            IMapSurroundFrame mapSurroundFrame = mapFrame.CreateSurroundFrame(uid, null);
            //设置图例的Title  
            ILegend2 legend = mapSurroundFrame.MapSurround as ILegend2;
            legend.Title = "地图图例";
            ILegendFormat format = new LegendFormatClass();
            ITextSymbol symbol = new TextSymbolClass();
            symbol.Size = 4;
            format.TitleSymbol = symbol;
            legend.Format = format;
            //QI，确定mapSurroundFrame的位置  
            IElement element = mapSurroundFrame as IElement;
            IEnvelope envelope = new EnvelopeClass();
            envelope.PutCoords(2, 2, 8, 8);
            element.Geometry = envelope;
            //使用IGraphicsContainer接口添加显示  
            container.AddElement(element, 0);
            pActiveView.Refresh();
        }  
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddLegend(this.axPageLayoutControl1.PageLayout);
        }
        #endregion
        #region 添加指北针
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _enumMapSurType = EnumMapSurroundType.NorthArrow;
                if (frmSym == null || frmSym.IsDisposed)
                {
                    frmSym = new test.PageLayout.frmSymbol();
                    frmSym.GetSelSymbolItem += new test.PageLayout.frmSymbol.GetSelSymbolItemEventHandler(frmSym_GetSelSymbolItem);
                }
                frmSym.EnumMapSurType = _enumMapSurType;
                frmSym.InitUI();
                frmSym.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        #region 添加比例尺
        private void barButtonItem16_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                _enumMapSurType = EnumMapSurroundType.ScaleBar;
                if (frmSym == null || frmSym.IsDisposed)
                {
                    frmSym = new test.PageLayout.frmSymbol();
                    frmSym.GetSelSymbolItem += new test.PageLayout.frmSymbol.GetSelSymbolItemEventHandler(frmSym_GetSelSymbolItem);
                }
                frmSym.EnumMapSurType = _enumMapSurType;
                frmSym.InitUI();
                frmSym.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
        #region Graticule格网
        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             try
            {
                IActiveView pActiveView = axPageLayoutControl1.ActiveView;
                IPageLayout pPageLayout = axPageLayoutControl1.PageLayout;
                DeleteMapGrid(pActiveView, pPageLayout);
                CreateGraticuleMapGrid(pActiveView, pPageLayout);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
         public void CreateGraticuleMapGrid(IActiveView pActiveView, IPageLayout pPageLayout)
        {
            IMap pMap = pActiveView.FocusMap;
            IGraticule pGraticule = new GraticuleClass();//看这个改动是否争取
            pGraticule.Name = "Map Grid";
            //设置网格线的符号样式
            ICartographicLineSymbol pLineSymbol;
            pLineSymbol = new CartographicLineSymbolClass();
            pLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
            pLineSymbol.Width = 1;
            pLineSymbol.Color = m_OperatePageLayout.GetRgbColor(166, 187, 208);
            pGraticule.LineSymbol = pLineSymbol;
            //设置网格的边框样式           
            ISimpleMapGridBorder simpleMapGridBorder = new SimpleMapGridBorderClass();
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
            simpleLineSymbol.Color = m_OperatePageLayout.GetRgbColor(100, 255, 0);
            simpleLineSymbol.Width = 2;
            simpleMapGridBorder.LineSymbol = simpleLineSymbol as ILineSymbol;
            pGraticule.Border = simpleMapGridBorder as IMapGridBorder;
            pGraticule.SetTickVisibility(true, true, true, true);
            //设置网格的主刻度的样式和可见性
            pGraticule.TickLength = 15;
            pLineSymbol = new CartographicLineSymbolClass();
            pLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
            pLineSymbol.Width = 1;
            pLineSymbol.Color = m_OperatePageLayout.GetRgbColor(255, 187, 208);
            pGraticule.TickMarkSymbol = null;
            pGraticule.TickLineSymbol = pLineSymbol;
            pGraticule.SetTickVisibility(true, true, true, true);
            //设置网格的次级刻度的样式和可见性
            pGraticule.SubTickCount = 5;
            pGraticule.SubTickLength = 10;
            pLineSymbol = new CartographicLineSymbolClass();
            pLineSymbol.Cap = esriLineCapStyle.esriLCSButt;
            pLineSymbol.Width = 0.1;
            pLineSymbol.Color = m_OperatePageLayout.GetRgbColor(166, 187, 208);
            pGraticule.SubTickLineSymbol = pLineSymbol;
            pGraticule.SetSubTickVisibility(true, true, true, true);
            //设置网格的标签的样式和可见性
            IGridLabel pGridLabel;
            pGridLabel = pGraticule.LabelFormat;
            pGridLabel.LabelOffset = 15;
            stdole.StdFont pFont = new stdole.StdFont();
            pFont.Name = "Arial";
            pFont.Size = 16;
            pGraticule.LabelFormat.Font = pFont as stdole.IFontDisp;
            pGraticule.Visible = true;
            //创建IMeasuredGrid对象
            IMeasuredGrid pMeasuredGrid = new MeasuredGridClass();
            IProjectedGrid pProjectedGrid = pMeasuredGrid as IProjectedGrid;
            pProjectedGrid.SpatialReference = pMap.SpatialReference;
            pMeasuredGrid = pGraticule as IMeasuredGrid;
            //获取坐标范围，设置网格的起始点和间隔
            double MaxX, MaxY, MinX, MinY;
            pProjectedGrid.SpatialReference.GetDomain(out MinX, out MaxX, out MinY, out MaxY);
            pMeasuredGrid.FixedOrigin = true;
            pMeasuredGrid.Units = pMap.MapUnits;
            pMeasuredGrid.XIntervalSize = (MaxX - MinX) / 200;//纬度间隔
            pMeasuredGrid.XOrigin = MinX;
            pMeasuredGrid.YIntervalSize = (MaxY - MinY) / 200;//经度间隔.
            pMeasuredGrid.YOrigin = MinY;
            //将网格对象添加到地图控件中                              
            IGraphicsContainer pGraphicsContainer = pActiveView as IGraphicsContainer;
            IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pMap) as IMapFrame;
            IMapGrids pMapGrids = pMapFrame as IMapGrids;
            pMapGrids.AddMapGrid(pGraticule);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
        }
        #endregion
         #region MeasuredGrid格网
         private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             IActiveView pActiveView = axPageLayoutControl1.ActiveView;
             IPageLayout pPageLayout = axPageLayoutControl1.PageLayout;
             DeleteMapGrid(pActiveView, pPageLayout);//删除已存在格网
             CreateMeasuredGrid1(pActiveView, pPageLayout);
        
        }
         public void CreateMeasuredGrid(IActiveView pActiveView, IPageLayout pPageLayout)
         {
             IMap map = pActiveView.FocusMap;
             IMeasuredGrid pMeasuredGrid = new MeasuredGridClass();
             //设置格网基本属性           
             pMeasuredGrid.FixedOrigin = false;
             pMeasuredGrid.Units = map.MapUnits;
             pMeasuredGrid.XIntervalSize = 5;//纬度间隔           
             pMeasuredGrid.YIntervalSize = 5;//经度间隔.             
             //设置GridLabel格式
             IGridLabel pGridLabel = new FormattedGridLabelClass();
             IFormattedGridLabel pFormattedGridLabel = new FormattedGridLabelClass();
             INumericFormat pNumericFormat = new NumericFormatClass();
             pNumericFormat.AlignmentOption = esriNumericAlignmentEnum.esriAlignLeft;
             pNumericFormat.RoundingOption = esriRoundingOptionEnum.esriRoundNumberOfDecimals;
             pNumericFormat.RoundingValue = 0;
             pNumericFormat.ZeroPad = true;
             pFormattedGridLabel.Format = pNumericFormat as INumberFormat;
             pGridLabel = pFormattedGridLabel as IGridLabel;
             StdFont myFont = new stdole.StdFontClass();
             myFont.Name = "宋体";
             myFont.Size = 25;
             pGridLabel.Font = myFont as IFontDisp;
             IMapGrid pMapGrid = new MeasuredGridClass();
             pMapGrid = pMeasuredGrid as IMapGrid;
             pMapGrid.LabelFormat = pGridLabel;
             //将格网添加到地图上           
             IGraphicsContainer graphicsContainer = pPageLayout as IGraphicsContainer;
             IFrameElement frameElement = graphicsContainer.FindFrame(map);
             IMapFrame mapFrame = frameElement as IMapFrame;
             IMapGrids mapGrids = null;
             mapGrids = mapFrame as IMapGrids;
             mapGrids.AddMapGrid(pMapGrid);
             pActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
         }
         #endregion
         #region IndexGrid格网
         private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            IActiveView pActiveView = axPageLayoutControl1.ActiveView;
            IPageLayout pPageLayout = axPageLayoutControl1.PageLayout;
            DeleteMapGrid(pActiveView, pPageLayout);
            CreateIndexGrid(pActiveView, pPageLayout);
        }
         public void CreateIndexGrid(IActiveView pActiveView, IPageLayout pPageLayout)
         {
             IIndexGrid pIndexGrid = new IndexGridClass();
             //设置Index属性
             pIndexGrid.ColumnCount = 5;
             pIndexGrid.RowCount = 5;
             String[] indexnum = { "A", "B", "C", "D", "E" };
             //设置IndexLabel
             int i = 0;
             for (i = 0; i <= (pIndexGrid.ColumnCount - 1); i++)
             {
                 pIndexGrid.set_XLabel(i, indexnum[i]);
             }
             for (i = 0; i <= (pIndexGrid.RowCount - 1); i++)
             {
                 pIndexGrid.set_YLabel(i, i.ToString());
             }
             //设置GridLabel格式
             IGridLabel pGridLabel = new RoundedTabStyleClass();
             StdFont myFont = new stdole.StdFontClass();
             myFont.Name = "宋体";
             myFont.Size = 18;
             pGridLabel.Font = myFont as IFontDisp;
             IMapGrid pmapGrid = new IndexGridClass();
             pmapGrid = pIndexGrid as IMapGrid;
             pmapGrid.LabelFormat = pGridLabel;
             //添加IndexGrid         
             IMapGrid pMapGrid = pIndexGrid;
             IMap pMap = pActiveView.FocusMap;
             IGraphicsContainer graphicsContainer = pPageLayout as IGraphicsContainer;
             IFrameElement frameElement = graphicsContainer.FindFrame(pMap);
             IMapFrame mapFrame = frameElement as IMapFrame;
             IMapGrids mapGrids = null;
             mapGrids = mapFrame as IMapGrids;
             mapGrids.AddMapGrid(pMapGrid);
             axPageLayoutControl1.Refresh();
         }
         #endregion       
         #region 删除已存在格网
         public void DeleteMapGrid(IActiveView pActiveView, IPageLayout pPageLayout)
         {
             IMap pMap = pActiveView.FocusMap;
             IGraphicsContainer graphicsContainer = pPageLayout as IGraphicsContainer;
             IFrameElement frameElement = graphicsContainer.FindFrame(pMap);
             IMapFrame mapFrame = frameElement as IMapFrame;
             IMapGrids mapGrids = null;
             mapGrids = mapFrame as IMapGrids;
             if (mapGrids.MapGridCount > 0)
             {
                 IMapGrid pMapGrid = mapGrids.get_MapGrid(0);
                 mapGrids.DeleteMapGrid(pMapGrid);
             }
             pActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
         }
         #endregion
         #region 制图模板
         private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             frmTemplate fTemplate = new frmTemplate(axPageLayoutControl1);
             fTemplate.Show();
         }
        #endregion
         #region  输出
         private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             ExportMapToImage();
         }
         private void  ExportMapToImage()
        {
            try
            {
                SaveFileDialog pSaveDialog = new SaveFileDialog();
                pSaveDialog.FileName = "";
                pSaveDialog.Filter = "JPG图片(*.JPG)|*.jpg|tif图片(*.tif)|*.tif|PDF文档(*.PDF)|*.pdf";
                if (pSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    double iScreenDispalyResolution = axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.Resolution;// 获取屏幕分辨率的值
                    IExporter pExporter = null;
                    if (pSaveDialog.FilterIndex == 1)
                    {
                        pExporter = new JpegExporterClass();
                    }
                    else if (pSaveDialog.FilterIndex == 2)
                    {
                        pExporter = new TiffExporterClass();
                    }
                    else if (pSaveDialog.FilterIndex == 3)
                    {
                        pExporter = new PDFExporterClass();
                    }
                    pExporter.ExportFileName = pSaveDialog.FileName;
                    pExporter.Resolution = (short)iScreenDispalyResolution; //分辨率
                    tagRECT deviceRect = axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
                    IEnvelope pDeviceEnvelope = new EnvelopeClass();
                    pDeviceEnvelope.PutCoords(deviceRect.left, deviceRect.bottom, deviceRect.right, deviceRect.top);
                    pExporter.PixelBounds = pDeviceEnvelope; // 输出图片的范围
                    ITrackCancel pCancle = new CancelTrackerClass();//可用ESC键取消操作
                    axPageLayoutControl1.ActiveView.Output(pExporter.StartExporting(), pExporter.Resolution, ref deviceRect, axPageLayoutControl1.ActiveView.Extent, pCancle);
                    Application.DoEvents();
                    pExporter.FinishExporting();                   
                }
               
            }
            catch (Exception Err)
            {
                 MessageBox.Show(Err.Message,"输出图片", MessageBoxButtons.OK, MessageBoxIcon.Information);              
            }
        }      
        #endregion
         #region 打印
         private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             frmPrintPreview = new frmPrintPre(axPageLayoutControl1);
             frmPrintPreview.ShowDialog();
         }
         #endregion
         #region 不同基准面的坐标转换
         public void ProjectExExample()
         {
             ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
             IPoint pFromPoint = new PointClass();
             IZAware pZAware = pFromPoint as IZAware;
             pZAware.ZAware = true;
             //定义两种不同基准下的坐标系
             ((IGeometry)pFromPoint).SpatialReference = CreateCustomProjectedCoordinateSystem();
             IProjectedCoordinateSystem pProjectedCoordinateSystem =
             pSpatialReferenceFactory.CreateProjectedCoordinateSystem((int)esriSRProjCS4Type.esriSRProjCS_Xian1980_GK_Zone_19);
             //因为目标基准面和原始基准面不在同一个上，所以牵扯到参数  
             ICoordinateFrameTransformation pCoordinateFrameTransformation = new
             CoordinateFrameTransformationClass();
             //pCoordinateFrameTransformation.PutParameters("double dx,double dy, double dz,double rx,double ry,
             // double rz, double s");（此句不应注释）坐标转换所需的7个参数  
             pCoordinateFrameTransformation.PutSpatialReferences(CreateCustomProjectedCoordinateSystem(), pProjectedCoordinateSystem as ISpatialReference);
             //投影转换      
             IGeometry2 pGeometry = pFromPoint as IGeometry2;
             pGeometry.ProjectEx(pProjectedCoordinateSystem as
             ISpatialReference, esriTransformDirection.esriTransformForward, pCoordinateFrameTransformation, false, 0, 0);
         }

         private IProjectedCoordinateSystem CreateCustomProjectedCoordinateSystem()
         {
             ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
             //高斯克吕格投影
             IProjectionGEN pProjection =
             pSpatialReferenceFactory.CreateProjection((int)esriSRProjectionType.esriSRProjection_GaussKruger) as IProjectionGEN;

             IGeographicCoordinateSystem pGeographicCoordinateSystem
             = pSpatialReferenceFactory.CreateGeographicCoordinateSystem((int)esriSRGeoCSType.esriSRGeoCS_WGS1984);
             //定义投影坐标系的来源
             ILinearUnit pUnit =
             pSpatialReferenceFactory.CreateUnit((int)esriSRUnitType.esriSRUnit_Meter) as ILinearUnit;
             //从投影获得默认的参数
             IParameter[] pParameters = pProjection.GetDefaultParameters();
             // 用Define方法创建投影坐标系
             IProjectedCoordinateSystemEdit pProjectedCoordinateSystemEdit = new ProjectedCoordinateSystemClass();
             object pName = "WGS-BeiJing1954";
             object pAlias = "WGS-BeiJing1954";
             object pAbbreviation = "WGS-BeiJing1954";
             object pRemarks = "WGS-BeiJing1954";
             object pUsage = "Calculate Meter From lat and lon";
             object pGeographicCoordinateSystemObject = pGeographicCoordinateSystem as object;
             object pUnitObject = pUnit as object;
             object pProjectionObject = pProjection as object;
             object pParametersObject = pParameters as object;
             pProjectedCoordinateSystemEdit.Define(ref pName, ref pAlias, ref pAbbreviation, ref pRemarks, ref pUsage,
             ref pGeographicCoordinateSystemObject, ref pUnitObject, ref pProjectionObject, ref pParametersObject);
             IProjectedCoordinateSystem5 pProjectedCoordinateSystem = pProjectedCoordinateSystemEdit as IProjectedCoordinateSystem5;
             pProjectedCoordinateSystem.FalseEasting = 500000;//坐标横轴向西移动500KM
             pProjectedCoordinateSystem.FalseNorthing = 0;//坐标纵轴不变
             pProjectedCoordinateSystem.LatitudeOfOrigin = 0;
             pProjectedCoordinateSystem.set_CentralMeridian(true, 111);// 设置中央子午线度数为111度
             pProjectedCoordinateSystem.ScaleFactor = 1;//投影坐标系的比例因子

             return pProjectedCoordinateSystem;
         }
         #endregion
         #region 同一基准面下的坐标转换
         private IPoint GetpProjectPoint(IPoint pPoint, bool pBool)
         {
             ISpatialReferenceFactory pSpatialReferenceEnvironemnt =
             new SpatialReferenceEnvironment();
             ISpatialReference pFromSpatialReference =
             pSpatialReferenceEnvironemnt.CreateGeographicCoordinateSystem((int)esriSRGeoCS3Type.esriSRGeoCS_Xian1980);//1980西安          
             ISpatialReference pToSpatialReference =
             pSpatialReferenceEnvironemnt.CreateProjectedCoordinateSystem((int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_34);//1980西安          
             if (pBool == true)//地理坐标转投影坐标   
             {
                 IGeometry pGeo = (IGeometry)pPoint;
                 pGeo.SpatialReference = pFromSpatialReference;
                 pGeo.Project(pToSpatialReference);
                 return pPoint;
             }
             else //投影坐标转地理坐标 
             {
                 IGeometry pGeo = (IGeometry)pPoint;
                 pGeo.SpatialReference = pToSpatialReference;
                 pGeo.Project(pFromSpatialReference);
                 return pPoint;
             }
         }
         #endregion                       
        #endregion
        #region 符号化
         #region 单一符号化
         private void SingleSymbol_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             try
             {
                 if (frmSimRender == null || frmSimRender.IsDisposed)
                 {
                     frmSimRender = new frmSimpleRender();
                     frmSimRender.SimpleRender += new frmSimpleRender.SimpleRenderEventHandler(frmSimRender_SimpleRender);
                 }
                 frmSimRender.PMap = axMapControl1.Map;
                 frmSimRender.InitUI();
                 frmSimRender.ShowDialog();
             }
             catch (Exception ex)
             {

             }
         }
         void frmSimRender_SimpleRender(string sFeatClsName, IRgbColor pRgbColr)
         {
             IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);
             SimpleRenderer(pFeatLyr, pRgbColr);
         }
         /// <summary>
         /// 单一符号化
         /// </summary>
         /// <param name="pFeatLyr">渲染图层</param>
         /// <param name="pRgbColor">渲染颜色</param>
         private void SimpleRenderer(IFeatureLayer pFeatLyr, IRgbColor pRgbColor)
         {
             try
             {
                 esriGeometryType types = pFeatLyr.FeatureClass.ShapeType;
                 ISimpleRenderer pSimRender = new SimpleRendererClass();
                 if (types == esriGeometryType.esriGeometryPolygon)
                 {
                     ISimpleFillSymbol pSimFillSym = new SimpleFillSymbolClass();
                     pSimFillSym.Color = pRgbColor;
                     pSimRender.Symbol = pSimFillSym as ISymbol; // 设置渲染的样式 
                 }
                 else if (types == esriGeometryType.esriGeometryPoint)
                 {
                     ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                     pSimpleMarkerSymbol.Color = pRgbColor;
                     pSimRender.Symbol = pSimpleMarkerSymbol as ISymbol;
                 }
                 else if (types == esriGeometryType.esriGeometryPolyline)
                 {
                     ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                     pSimpleLineSymbol.Color = pRgbColor;
                     pSimRender.Symbol = pSimpleLineSymbol as ISymbol;
                 }
                 IGeoFeatureLayer pGeoFeatLyr = pFeatLyr as IGeoFeatureLayer;
                 pGeoFeatLyr.Renderer = pSimRender as IFeatureRenderer;
                 (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
                 axTOCControl1.Update();
             }
             catch (Exception ex)
             {

             }
         }
         #endregion
         #region 唯一值符号化
         private void UniqueValue_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             try
             {
                 if (frmUniqueValueRen == null || frmUniqueValueRen.IsDisposed)
                 {
                     frmUniqueValueRen = new frmUniqueValueRender();
                     frmUniqueValueRen.UniqueValueRender += new frmUniqueValueRender.UniqueValueRenderEventHandler(frmUniqueValueRen_UniqueValueRender);
                 }
                 frmUniqueValueRen.Map = axMapControl1.Map;
                 frmUniqueValueRen.InitUI();
                 frmUniqueValueRen.ShowDialog();
             }
             catch (Exception ex)
             {

             }
         }
         void frmUniqueValueRen_UniqueValueRender(string sFeatClsName, string sFieldName)
         {
             IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);
             UniqueValueRenderer(pFeatLyr, sFieldName);

         }
         /// <summary>
         /// 唯一值符号化
         /// </summary>
         /// <param name="pFeatLyr">渲染图层</param>
         /// <param name="sFieldName">渲染字段</param>
         private void UniqueValueRenderer(IFeatureLayer pFeatLyr, string sFieldName)
         {
             try
             {
                 IGeoFeatureLayer pGeoFeatLyr = pFeatLyr as IGeoFeatureLayer;
                 ITable pTable = pFeatLyr as ITable;
                 IUniqueValueRenderer pUniqueValueRender = new UniqueValueRendererClass();

                 int intFieldNumber = pTable.FindField(sFieldName);
                 pUniqueValueRender.FieldCount = 1;//设置唯一值符号化的关键字段为一个
                 pUniqueValueRender.set_Field(0, sFieldName);//设置唯一值符号化的第一个关键字段

                 IRandomColorRamp pRandColorRamp = new RandomColorRampClass();
                 pRandColorRamp.StartHue = 0;
                 pRandColorRamp.MinValue = 0;
                 pRandColorRamp.MinSaturation = 15;
                 pRandColorRamp.EndHue = 360;
                 pRandColorRamp.MaxValue = 100;
                 pRandColorRamp.MaxSaturation = 30;
                 //根据渲染字段的值的个数，设置一组随机颜色，如某一字段有5个值，则创建5个随机颜色与之匹配
                 IQueryFilter pQueryFilter = new QueryFilterClass();
                 pRandColorRamp.Size = pFeatLyr.FeatureClass.FeatureCount(pQueryFilter);
                 bool bSuccess = false;
                 pRandColorRamp.CreateRamp(out bSuccess);

                 IEnumColors pEnumRamp = pRandColorRamp.Colors;
                 IColor pNextUniqueColor = null;
                 //查询字段的值
                 pQueryFilter = new QueryFilterClass();
                 pQueryFilter.AddField(sFieldName);
                 ICursor pCursor = pTable.Search(pQueryFilter, true);
                 IRow pNextRow = pCursor.NextRow();
                 object codeValue = null;
                 IRowBuffer pNextRowBuffer = null;


                 while (pNextRow != null)
                 {
                     pNextRowBuffer = pNextRow as IRowBuffer;
                     codeValue = pNextRowBuffer.get_Value(intFieldNumber);//获取渲染字段的每一个值

                     pNextUniqueColor = pEnumRamp.Next();
                     if (pNextUniqueColor == null)
                     {
                         pEnumRamp.Reset();
                         pNextUniqueColor = pEnumRamp.Next();
                     }
                     IFillSymbol pFillSymbol = null;
                     ILineSymbol pLineSymbol;
                     IMarkerSymbol pMarkerSymbol;
                     switch (pGeoFeatLyr.FeatureClass.ShapeType)
                     {
                         case esriGeometryType.esriGeometryPolygon:
                             {
                                 pFillSymbol = new SimpleFillSymbolClass();
                                 pFillSymbol.Color = pNextUniqueColor;
                                 pUniqueValueRender.AddValue(codeValue.ToString(), "", pFillSymbol as ISymbol);//添加渲染字段的值和渲染样式
                                 pNextRow = pCursor.NextRow();
                                 break;
                             }
                         case esriGeometryType.esriGeometryPolyline:
                             {
                                 pLineSymbol = new SimpleLineSymbolClass();
                                 pLineSymbol.Color = pNextUniqueColor;
                                 pUniqueValueRender.AddValue(codeValue.ToString(), "", pLineSymbol as ISymbol);//添加渲染字段的值和渲染样式
                                 pNextRow = pCursor.NextRow();
                                 break;
                             }
                         case esriGeometryType.esriGeometryPoint:
                             {
                                 pMarkerSymbol = new SimpleMarkerSymbolClass();
                                 pMarkerSymbol.Color = pNextUniqueColor;
                                 pUniqueValueRender.AddValue(codeValue.ToString(), "", pMarkerSymbol as ISymbol);//添加渲染字段的值和渲染样式
                                 pNextRow = pCursor.NextRow();
                                 break;
                             }
                     }
                 }
                 pGeoFeatLyr.Renderer = pUniqueValueRender as IFeatureRenderer;
                 axMapControl1.Refresh();
                 axTOCControl1.Update();
             }
             catch (Exception ex)
             {

             }

         }
         #endregion
         #region 唯一值字段
         private void UniqueValuesManyFileds_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             try
             {
                 if (frmUniqueValueMany_fields == null || frmUniqueValueMany_fields.IsDisposed)
                 {
                     frmUniqueValueMany_fields = new frmUniqueValueMany_fields();
                     frmUniqueValueMany_fields.UniqueValueRender += new frmUniqueValueMany_fields.UniqueValueRenderEventHandler(frmUniqueValueMany_fields_UniqueValueRender);
                 }

                 frmUniqueValueMany_fields.Map = axMapControl1.Map;
                 frmUniqueValueMany_fields.InitUI();
                 frmUniqueValueMany_fields.ShowDialog();
             }
             catch (Exception ex)
             {

             }
         }
         void frmUniqueValueMany_fields_UniqueValueRender(string sFeatClsName, string[] sFieldName)
         {
             IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);
             UniqueValueMany_fieldsRenderer(pFeatLyr, sFieldName);
         }
         /// <summary>
         /// 唯一值多字段
         /// </summary>
         /// <param name="pFeatLyr">渲染图层</param>
         /// <param name="sFieldName">渲染字段</param>
         private void UniqueValueMany_fieldsRenderer(IFeatureLayer pFeatLyr, string[] sFieldName)
         {
             IUniqueValueRenderer pUniqueValueRender;
             IColor pNextUniqueColor;
             IEnumColors pEnumRamp;
             ITable pTable;
             IRow pNextRow;
             ICursor pCursor;
             IQueryFilter pQueryFilter;
             IRandomColorRamp pRandColorRamp = new RandomColorRampClass();
             pRandColorRamp.StartHue = 0;
             pRandColorRamp.MinValue = 0;
             pRandColorRamp.MinSaturation = 15;
             pRandColorRamp.EndHue = 360;
             pRandColorRamp.MaxValue = 100;
             pRandColorRamp.MaxSaturation = 30;
             //根据渲染字段值的个数，设置一组随即颜色，如某一字段有5个不同值，则创建5个随机颜色与之匹配
             IQueryFilter pQueryFilter1 = new QueryFilterClass();
             pRandColorRamp.Size = pFeatLyr.FeatureClass.FeatureCount(pQueryFilter1);
             bool bSuccess = false;
             pRandColorRamp.CreateRamp(out bSuccess);
             //所选字段数为两个时
             if (sFieldName.Length == 2)
             {
                 string sFieldName1 = sFieldName[0];
                 string sFieldName2 = sFieldName[1];
                 IGeoFeatureLayer pGeoFeatureL = (IGeoFeatureLayer)pFeatLyr;
                 pUniqueValueRender = new UniqueValueRendererClass();
                 pTable = (ITable)pGeoFeatureL;
                 int pFieldNumber = pTable.FindField(sFieldName1);
                 int pFieldNumber2 = pTable.FindField(sFieldName2);
                 pUniqueValueRender.FieldCount = 2;//设置渲染字段的个数
                 pUniqueValueRender.set_Field(0, sFieldName1);//设置渲染的第一个字段
                 pUniqueValueRender.set_Field(1, sFieldName2);//设置渲染的第二个字段
                 pEnumRamp = pRandColorRamp.Colors;
                 pNextUniqueColor = null;
                 //获取渲染字段的每个属性值
                 pQueryFilter = new QueryFilterClass();
                 pQueryFilter.AddField(sFieldName1);
                 pQueryFilter.AddField(sFieldName2);
                 pCursor = pTable.Search(pQueryFilter, true);
                 pNextRow = pCursor.NextRow();
                 string codeValue;//这里的codeValue还可以定义成object类型
                 while (pNextRow != null)
                 {
                     codeValue = pNextRow.get_Value(pFieldNumber).ToString() + pUniqueValueRender.FieldDelimiter + pNextRow.get_Value(pFieldNumber2).ToString();
                     pNextUniqueColor = pEnumRamp.Next();
                     if (pNextUniqueColor == null)
                     {
                         pEnumRamp.Reset();
                         pNextUniqueColor = pEnumRamp.Next();
                     }
                     IFillSymbol pFillSymbol;
                     ILineSymbol pLineSymbol;
                     IMarkerSymbol pMarkerSymbol;
                     switch (pGeoFeatureL.FeatureClass.ShapeType)
                     {
                         case esriGeometryType.esriGeometryPolygon:
                             {
                                 pFillSymbol = new SimpleFillSymbolClass();
                                 pFillSymbol.Color = pNextUniqueColor;
                                 //设置渲染字段组合值对应的符号
                                 pUniqueValueRender.AddValue(codeValue, sFieldName1 + " " + sFieldName2, (ISymbol)pFillSymbol);
                                 break;
                             }
                         case esriGeometryType.esriGeometryPolyline:
                             {
                                 pLineSymbol = new SimpleLineSymbolClass();
                                 pLineSymbol.Color = pNextUniqueColor;
                                 //设置渲染字段组合值对应的符号
                                 pUniqueValueRender.AddValue(codeValue, sFieldName1 + " " + sFieldName2, (ISymbol)pLineSymbol);
                                 break;
                             }
                         case esriGeometryType.esriGeometryPoint:
                             {
                                 pMarkerSymbol = new SimpleMarkerSymbolClass();
                                 pMarkerSymbol.Color = pNextUniqueColor;
                                 //设置渲染字段组合值对应的符号
                                 pUniqueValueRender.AddValue(codeValue, sFieldName1 + " " + sFieldName2, (ISymbol)pMarkerSymbol);
                                 break;
                             }
                     }
                     pNextRow = pCursor.NextRow();
                 }
                 pGeoFeatureL.Renderer = (IFeatureRenderer)pUniqueValueRender;
                 axMapControl1.Refresh();
                 axTOCControl1.Update();
             }
             else if (sFieldName.Length == 3)
             {
                 string sFieldName1 = sFieldName[0];
                 string sFieldName2 = sFieldName[1];
                 string sFieldName3 = sFieldName[2];
                 IGeoFeatureLayer pGeoFeatureL = (IGeoFeatureLayer)pFeatLyr;
                 pUniqueValueRender = new UniqueValueRendererClass();
                 pTable = (ITable)pGeoFeatureL;
                 int pFieldNumber = pTable.FindField(sFieldName1);
                 int pFieldNumber2 = pTable.FindField(sFieldName2);
                 int pFieldNumber3 = pTable.FindField(sFieldName3);
                 pUniqueValueRender.FieldCount = 3;
                 pUniqueValueRender.set_Field(0, sFieldName1);
                 pUniqueValueRender.set_Field(1, sFieldName2);
                 pUniqueValueRender.set_Field(2, sFieldName3);
                 pEnumRamp = pRandColorRamp.Colors;
                 pNextUniqueColor = null;
                 pQueryFilter = new QueryFilterClass();
                 pQueryFilter.AddField(sFieldName1);
                 pQueryFilter.AddField(sFieldName2);
                 pQueryFilter.AddField(sFieldName3);
                 pCursor = pTable.Search(pQueryFilter, true);
                 pNextRow = pCursor.NextRow();
                 string codeValue;
                 while (pNextRow != null)
                 {
                     codeValue = pNextRow.get_Value(pFieldNumber).ToString() + pUniqueValueRender.FieldDelimiter + pNextRow.get_Value(pFieldNumber2).ToString() + pUniqueValueRender.FieldDelimiter + pNextRow.get_Value(pFieldNumber3).ToString();
                     pNextUniqueColor = pEnumRamp.Next();
                     if (pNextUniqueColor == null)
                     {
                         pEnumRamp.Reset();
                         pNextUniqueColor = pEnumRamp.Next();
                     }
                     IFillSymbol pFillSymbol;
                     ILineSymbol pLineSymbol;
                     IMarkerSymbol pMarkerSymbol;
                     switch (pGeoFeatureL.FeatureClass.ShapeType)
                     {
                         case esriGeometryType.esriGeometryPolygon:
                             {
                                 pFillSymbol = new SimpleFillSymbolClass();
                                 pFillSymbol.Color = pNextUniqueColor;
                                 pUniqueValueRender.AddValue(codeValue, sFieldName1 + " " + sFieldName2 + "" + sFieldName3, (ISymbol)pFillSymbol);
                                 break;
                             }
                         case esriGeometryType.esriGeometryPolyline:
                             {
                                 pLineSymbol = new SimpleLineSymbolClass();
                                 pLineSymbol.Color = pNextUniqueColor;
                                 pUniqueValueRender.AddValue(codeValue, sFieldName1 + " " + sFieldName2 + "" + sFieldName3, (ISymbol)pLineSymbol);
                                 break;
                             }
                         case esriGeometryType.esriGeometryPoint:
                             {
                                 pMarkerSymbol = new SimpleMarkerSymbolClass();
                                 pMarkerSymbol.Color = pNextUniqueColor;
                                 pUniqueValueRender.AddValue(codeValue, sFieldName1 + " " + sFieldName2 + "" + sFieldName3, (ISymbol)pMarkerSymbol);
                                 break;
                             }
                     }
                     pNextRow = pCursor.NextRow();
                 }
                 pGeoFeatureL.Renderer = (IFeatureRenderer)pUniqueValueRender;
                 axMapControl1.Refresh();
                 axTOCControl1.Update();
             }
         }
         #endregion
         #region 分级色彩
         private void Graduatedcolor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             try
             {
                 if (frmGraduatedcolors == null || frmGraduatedcolors.IsDisposed)
                 {
                     frmGraduatedcolors = new frmGraduatedcolors();
                     frmGraduatedcolors.Graduatedcolors += new frmGraduatedcolors.GraduatedcolorsEventHandler(frmGraduatedcolors_Graduatedcolors);
                 }
                 frmGraduatedcolors.Map = axMapControl1.Map;
                 frmGraduatedcolors.InitUI();
                 frmGraduatedcolors.ShowDialog();
             }
             catch (Exception ex)
             {

             }
         }
         void frmGraduatedcolors_Graduatedcolors(string sFeatClsName, string sFieldName, int numclasses)
         {
             IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);
             GraduatedColors(pFeatLyr, sFieldName, numclasses);
         }
         /// <summary>
         /// 分级色彩
         /// </summary>
         /// <param name="pFeatLyr">渲染图层</param>
         /// <param name="sFieldName">渲染字段</param>
         /// <param name="numclasses">分级数目</param>
         public void GraduatedColors(IFeatureLayer pFeatLyr, string sFieldName, int numclasses)
         {
             IGeoFeatureLayer pGeoFeatureL = pFeatLyr as IGeoFeatureLayer;
             object dataFrequency;
             object dataValues;
             bool ok;
             int breakIndex;

             ITable pTable = pGeoFeatureL.FeatureClass as ITable;
             ITableHistogram pTableHistogram = new BasicTableHistogramClass();
             IBasicHistogram pBasicHistogram = (IBasicHistogram)pTableHistogram;
             pTableHistogram.Field = sFieldName;
             pTableHistogram.Table = pTable;
             pBasicHistogram.GetHistogram(out dataValues, out dataFrequency);     //获取渲染字段的值及其出现的频率
             IClassifyGEN pClassify = new EqualIntervalClass();
             try
             {
                 pClassify.Classify(dataValues, dataFrequency, ref  numclasses);  //根据获取字段的值和出现的频率对其进行等级划分 
             }
             catch (Exception ex)
             {

             }
             //返回一个数组
             double[] Classes = pClassify.ClassBreaks as double[];
             int ClassesCount = Classes.GetUpperBound(0);
             IClassBreaksRenderer pClassBreaksRenderer = new ClassBreaksRendererClass();
             pClassBreaksRenderer.Field = sFieldName; //设置分级字段
             pClassBreaksRenderer.BreakCount = ClassesCount; //设置分级数目
             pClassBreaksRenderer.SortClassesAscending = true;//分级后的图例是否按升级顺序排列
             //设置分级着色所需颜色带的起止颜色
             IHsvColor pFromColor = new HsvColorClass();
             pFromColor.Hue = 0;//黄色
             pFromColor.Saturation = 50;
             pFromColor.Value = 96;
             IHsvColor pToColor = new HsvColorClass();
             pToColor.Hue = 80;
             pToColor.Saturation = 100;
             pToColor.Value = 96;
             //产生颜色带对象
             IAlgorithmicColorRamp pAlgorithmicCR = new AlgorithmicColorRampClass();
             pAlgorithmicCR.Algorithm = esriColorRampAlgorithm.esriHSVAlgorithm;
             pAlgorithmicCR.FromColor = pFromColor;
             pAlgorithmicCR.ToColor = pToColor;
             pAlgorithmicCR.Size = ClassesCount;
             pAlgorithmicCR.CreateRamp(out ok);
             //获得颜色
             IEnumColors pEnumColors = pAlgorithmicCR.Colors;
             //需要注意的是分级着色对象中的symbol和break的下标都是从0开始
             for (breakIndex = 0; breakIndex <= ClassesCount - 1; breakIndex++)
             {
                 IColor pColor = pEnumColors.Next();
                 switch (pGeoFeatureL.FeatureClass.ShapeType)
                 {
                     case esriGeometryType.esriGeometryPolygon:
                         {
                             ISimpleFillSymbol pSimpleFillS = new SimpleFillSymbolClass();
                             pSimpleFillS.Color = pColor;
                             pSimpleFillS.Style = esriSimpleFillStyle.esriSFSSolid;
                             pClassBreaksRenderer.set_Symbol(breakIndex, (ISymbol)pSimpleFillS);//设置填充符号
                             pClassBreaksRenderer.set_Break(breakIndex, Classes[breakIndex + 1]);//设定每一分级的分级断点
                             break;
                         }
                     case esriGeometryType.esriGeometryPolyline:
                         {
                             ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
                             pSimpleLineSymbol.Color = pColor;
                             pClassBreaksRenderer.set_Symbol(breakIndex, (ISymbol)pSimpleLineSymbol);
                             pClassBreaksRenderer.set_Break(breakIndex, Classes[breakIndex + 1]);
                             break;
                         }
                     case esriGeometryType.esriGeometryPoint:
                         {
                             ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                             pSimpleMarkerSymbol.Color = pColor;
                             pClassBreaksRenderer.set_Symbol(breakIndex, (ISymbol)pSimpleMarkerSymbol);//设置填充符号
                             pClassBreaksRenderer.set_Break(breakIndex, Classes[breakIndex + 1]);//设定每一分级的分级断点
                             break;
                         }
                 }
             }
             pGeoFeatureL.Renderer = (IFeatureRenderer)pClassBreaksRenderer;
             axMapControl1.Refresh();
             axTOCControl1.Update();
         }
         #endregion
         #region 分级符号
         private void Graduatedsymbol_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             try
             {
                 if (frmGraduatedSymbols == null || frmGraduatedSymbols.IsDisposed)
                 {
                     frmGraduatedSymbols = new frmGraduatedSymbols();
                     frmGraduatedSymbols.GraduatedSymbols += new frmGraduatedSymbols.GraduatedSymbolsEventHandler(frmGraduatedSymbols_GraduatedSymbols);
                 }
                 frmGraduatedSymbols.Map = axMapControl1.Map;
                 frmGraduatedSymbols.InitUI();
                 frmGraduatedSymbols.ShowDialog();
             }
             catch (Exception ex)
             {

             }
         }
         // 分级符号 
         void frmGraduatedSymbols_GraduatedSymbols(string sFeatClsName, string sFieldName, int numclasses)
         {
             IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);
             GraduatedSymbols(pFeatLyr, sFieldName, numclasses);
         }
         /// <summary>
         /// 分级符号
         /// </summary>
         /// <param name="pFeatLyr">渲染图层</param>
         /// <param name="sFieldName">渲染字段</param>
         /// <param name="numclasses">分级数目</param>
         public void GraduatedSymbols(IFeatureLayer pFeatLyr, string sFieldName, int numclasses)
         {
             ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
             pSimpleMarkerSymbol.Color = m_OperateMap.GetRgbColor(255, 100, 100);
             ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbolClass();
             pSimpleLineSymbol.Color = m_OperateMap.GetRgbColor(255, 100, 100);
             int IbreakIndex;
             object dataFrequency;
             object dataValues;

             //获得要着色的图层
             IGeoFeatureLayer pGeoFeatureL = pFeatLyr as IGeoFeatureLayer;
             ITable pTable = pGeoFeatureL.FeatureClass as ITable;
             ITableHistogram pTableHistogram = new BasicTableHistogramClass();
             IBasicHistogram pBasicHistogram = (IBasicHistogram)pTableHistogram;
             pTableHistogram.Field = sFieldName;
             pTableHistogram.Table = pTable;
             pBasicHistogram.GetHistogram(out dataValues, out dataFrequency);//获取渲染字段的值及其出现的频率                    
             IClassifyGEN pClassify = new EqualIntervalClass();
             try
             {
                 pClassify.Classify(dataValues, dataFrequency, ref numclasses);//根据获取字段的值和出现的频率对其进行等级划分 
             }
             catch (Exception ex)
             {
             }
             //返回一个数组
             double[] Classes = (double[])pClassify.ClassBreaks;
             int ClassesCount = Classes.GetUpperBound(0);
             IClassBreaksRenderer pClassBreakRenderer = new ClassBreaksRendererClass();
             pClassBreakRenderer.Field = sFieldName;// 设置分级字段
             //设置着色对象的分级数目
             pClassBreakRenderer.BreakCount = ClassesCount;//设置分级数目
             pClassBreakRenderer.SortClassesAscending = true;//升序排列
             //需要注意的是分级着色对象中的symbol和break的下标都是从0开始
             double symbolSizeOrigin = 5.0;
             if (ClassesCount <= 5)
             {
                 symbolSizeOrigin = 8;
             }
             if (ClassesCount < 10 && ClassesCount > 5)
             {
                 symbolSizeOrigin = 7;
             }
             IFillSymbol pBackgroundSymbol = new SimpleFillSymbolClass();
             pBackgroundSymbol.Color = m_OperateMap.GetRgbColor(255, 255, 100);
             //不同的要素类型，生成不同的分级符号
             switch (pGeoFeatureL.FeatureClass.ShapeType)
             {
                 case esriGeometryType.esriGeometryPolygon:
                     {
                         for (IbreakIndex = 0; IbreakIndex <= ClassesCount - 1; IbreakIndex++)
                         {
                             pClassBreakRenderer.set_Break(IbreakIndex, Classes[IbreakIndex + 1]);
                             pClassBreakRenderer.BackgroundSymbol = pBackgroundSymbol;
                             pSimpleMarkerSymbol.Size = symbolSizeOrigin + IbreakIndex * symbolSizeOrigin / 3.0d;
                             pClassBreakRenderer.set_Symbol(IbreakIndex, (ISymbol)pSimpleMarkerSymbol);
                         }
                         break;
                     }
                 case esriGeometryType.esriGeometryPolyline:
                     {
                         for (IbreakIndex = 0; IbreakIndex <= ClassesCount - 1; IbreakIndex++)
                         {
                             pClassBreakRenderer.set_Break(IbreakIndex, Classes[IbreakIndex + 1]);
                             pSimpleLineSymbol.Width = symbolSizeOrigin / 5 + IbreakIndex * (symbolSizeOrigin / 5) / 5.0d;
                             pClassBreakRenderer.set_Symbol(IbreakIndex, (ISymbol)pSimpleLineSymbol);
                         }
                         break;
                     }
                 case esriGeometryType.esriGeometryPoint:
                     {
                         for (IbreakIndex = 0; IbreakIndex <= ClassesCount - 1; IbreakIndex++)
                         {
                             pClassBreakRenderer.set_Break(IbreakIndex, Classes[IbreakIndex + 1]);
                             pSimpleMarkerSymbol.Size = symbolSizeOrigin + IbreakIndex * symbolSizeOrigin / 3.0d;
                             pClassBreakRenderer.set_Symbol(IbreakIndex, (ISymbol)pSimpleMarkerSymbol);
                         }
                         break;
                     }
             }
             pGeoFeatureL.Renderer = pClassBreakRenderer as IFeatureRenderer;
             axMapControl1.ActiveView.Refresh();
             axTOCControl1.Update();
         }
         #endregion
         #region 比例符号化
         private void Proportionalsymbol_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             try
             {
                 if (frmProportional == null || frmProportional.IsDisposed)
                 {
                     frmProportional = new frmProportional();
                     frmProportional.Proportional += new frmProportional.ProportionalEventHandler(frmProportional_Proportional);
                 }
                 frmProportional.Map = axMapControl1.Map;
                 frmProportional.InitUI();
                 frmProportional.ShowDialog();
             }
             catch (Exception ex)
             {

             }
         }
         // 比例符号 
         void frmProportional_Proportional(string sFeatClsName, string sFieldName)
         {
             IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);
             Proportional(pFeatLyr, sFieldName);
         }
         /// <summary>
         /// 比例符号化
         /// </summary>
         /// <param name="sender">渲染图层</param>
         /// <param name="e">渲染字段</param>        
         private void Proportional(IFeatureLayer pFeatLyr, string sFieldName)
         {
             try
             {
                 IGeoFeatureLayer pGeoFeatureLayer = pFeatLyr as IGeoFeatureLayer;
                 ITable pTable = pFeatLyr as ITable;
                 ICursor pCursor = pTable.Search(null, true);
                 //利用IDataStatistics和IStatisticsResults获取渲染字段的统计值，最主要是或得最大值和最小值
                 IDataStatistics pDataStatistics = new DataStatisticsClass();
                 pDataStatistics.Cursor = pCursor;
                 pDataStatistics.Field = sFieldName;
                 IStatisticsResults pStatisticsResult = pDataStatistics.Statistics;
                 if (pStatisticsResult != null)
                 {
                     //设置渲染背景色
                     IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
                     pFillSymbol.Color = m_OperateMap.GetRgbColor(155, 255, 0);
                     //设置比例符号的样式                 
                     ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                     pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;
                     pSimpleMarkerSymbol.Size = 3;
                     pSimpleMarkerSymbol.Color = m_OperateMap.GetRgbColor(255, 90, 0);
                     IProportionalSymbolRenderer pProportionalSymbolRenderer = new ProportionalSymbolRendererClass();
                     pProportionalSymbolRenderer.ValueUnit = esriUnits.esriUnknownUnits;//设置渲染单位
                     pProportionalSymbolRenderer.Field = sFieldName; //设置渲染字段   
                     pProportionalSymbolRenderer.FlanneryCompensation = false;//是否使用Flannery补偿
                     pProportionalSymbolRenderer.MinDataValue = pStatisticsResult.Minimum;//获取渲染字段的最大值
                     pProportionalSymbolRenderer.MaxDataValue = pStatisticsResult.Maximum;//获取渲染字段的最小值
                     pProportionalSymbolRenderer.BackgroundSymbol = pFillSymbol;
                     pProportionalSymbolRenderer.MinSymbol = pSimpleMarkerSymbol as ISymbol;//向设置渲染字段最小值的渲染符号，其余值的符号根据此符号产生
                     pProportionalSymbolRenderer.LegendSymbolCount = 5;// 设置TOC控件中显示的数目
                     pProportionalSymbolRenderer.CreateLegendSymbols();//生成图例
                     pGeoFeatureLayer.Renderer = pProportionalSymbolRenderer as IFeatureRenderer;
                 }
                 axMapControl1.Refresh();
                 axTOCControl1.Update();
             }
             catch (Exception ex)
             {
             }
         }
         #endregion
         #region 点密度
         private void Dotdensitys_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             try
             {
                 if (frmDotDensity == null || frmDotDensity.IsDisposed)
                 {
                     frmDotDensity = new frmDotDensity();
                     frmDotDensity.DotDensity += new frmDotDensity.DotDensityEventHandler(frmDotDensity_DotDensity);
                 }
                 frmDotDensity.Map = axMapControl1.Map;
                 frmDotDensity.InitUI();
                 frmDotDensity.ShowDialog();
             }
             catch (Exception ex)
             {

             }

         }
         // 点密度
         void frmDotDensity_DotDensity(string sFeatClsName, string sFieldName, int intRendererDensity)
         {
             IFeatureLayer pFeatLyr = m_OperateMap.GetFeatLyrByName(axMapControl1.Map, sFeatClsName);
             DotDensity(pFeatLyr, sFieldName, intRendererDensity);
         }
         /// <summary>
         /// 点密度图
         /// </summary>
         /// <param name="pFeatLyr">渲染图层</param>
         /// <param name="sFieldName">渲染字段</param>
         /// <param name="intRendererDensity">每个点多代表的值</param>           
         private void DotDensity(IFeatureLayer pFeatLyr, string sFieldName, int intRendererDensity)
         {
             try
             {
                 IGeoFeatureLayer pGeoFeatureLayer = pFeatLyr as IGeoFeatureLayer;
                 IDotDensityRenderer pDotDensityRenderer = new DotDensityRendererClass();
                 IRendererFields pRendererFields = pDotDensityRenderer as IRendererFields;
                 //设置渲染字段               
                 pRendererFields.AddField(sFieldName);
                 //设置填充背景色
                 IDotDensityFillSymbol pDotDensityFillSymbol = new DotDensityFillSymbolClass();
                 pDotDensityFillSymbol.DotSize = 3;
                 pDotDensityFillSymbol.BackgroundColor = m_OperateMap.GetRgbColor(0, 255, 0);
                 //设置渲染符号
                 ISymbolArray pSymbolArray = pDotDensityFillSymbol as ISymbolArray;
                 ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                 pSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                 pSimpleMarkerSymbol.Color = m_OperateMap.GetRgbColor(0, 0, 255);
                 pSymbolArray.AddSymbol(pSimpleMarkerSymbol as ISymbol);
                 pDotDensityRenderer.DotDensitySymbol = pDotDensityFillSymbol;
                 //设置渲染密度，即每个点符号所代表的数值大小
                 pDotDensityRenderer.DotValue = intRendererDensity;
                 //创建图例
                 pDotDensityRenderer.CreateLegend();
                 pGeoFeatureLayer.Renderer = pDotDensityRenderer as IFeatureRenderer;
                 axMapControl1.Refresh();
                 axTOCControl1.Update();
             }
             catch (Exception ex)
             {

             }
         }
         #endregion       
        #endregion
         #region 帮助
         private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
         {
             Help.ShowHelp(this, "https://resources.arcgis.com/en/help/arcobjects-net/componentHelp/index.html#//004300000067000000");
         }
         #endregion

         private void Mainform_FormClosed(object sender, FormClosedEventArgs e)
         {
             xitongjieshao xt = new xitongjieshao();
             xt.ShowDialog();
         }

    }
}                


