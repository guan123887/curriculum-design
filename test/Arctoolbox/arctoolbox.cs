using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using test.Arctoolbox;

namespace test
{
    public partial class arctoolbox : Form
    {
        public arctoolbox()
        {
            InitializeComponent();
        }

        private void ArctoolB_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            #region 定义投影
            if (e.Node.Text.ToString() == "定义投影")
            {
                define_project def_p = new define_project();
                def_p.Show();
            }
            #endregion
            #region Spatial Analyst工具
            #region 水文分析
            if (e.Node.Text.ToString() == "填洼")
            {
                Fill fill = new Fill();
                fill.Show();
            }
            if (e.Node.Text.ToString() == "流向")
            {
                FlowDirection  flD= new FlowDirection();
                flD.Show();
            }
            if (e.Node.Text.ToString() == "流量")
            {
                Liuliang ll = new Liuliang();
                ll.Show();
            }
            if (e.Node.Text.ToString() == "分水岭")
            {
                FenShuiLing fsl = new FenShuiLing();
                fsl.Show();
            }
            if (e.Node.Text.ToString() == "汇")
            {
                Hui hui = new Hui();
                hui.Show();
            }
            #endregion
            #region 栅格计算器
            if (e.Node.Text.ToString() == "栅格计算器")
            {
                ShanGeJiSuanQi sgjsq = new ShanGeJiSuanQi();
                sgjsq.Show();
            }
            #endregion
            #region 叠加分析
            if (e.Node.Text.ToString() == "加权叠加")
            {
                JiaQuanDieJia jqdj = new JiaQuanDieJia();
                jqdj.Show();
            }
            if (e.Node.Text.ToString() == "加权总和")
            {
                jiaquanzonghe jqzh = new jiaquanzonghe();
                jqzh.Show();
            }
            if (e.Node.Text.ToString() == "模糊分类")
            {
                jiaquanzonghe jqzh = new jiaquanzonghe();
                jqzh.Show();
            }
            if (e.Node.Text.ToString() == "模糊叠加")
            {
                MoHuDieJia MHDJ = new MoHuDieJia();
                MHDJ.Show();
            }
            #endregion
            #region 插值分析
            if (e.Node.Text.ToString() == "克里金法")
            {
                KeLiJin klj = new KeLiJin();
                klj.Show();
            }
            if (e.Node.Text.ToString() == "反距离权重法")
            {
                FanJuLiQuanZhongFa FJLQZF = new FanJuLiQuanZhongFa();
                FJLQZF.Show();
            }
            if (e.Node.Text.ToString() == "样条函数法")
            {
                YangTiaoHanShuFa YTHSF = new YangTiaoHanShuFa();
               YTHSF.Show();
            }
            if (e.Node.Text.ToString() == "自然邻域法")
            {
                ZiRanLinYuFa ZRLYF = new ZiRanLinYuFa();
                ZRLYF.Show();
            }
            if (e.Node.Text.ToString() == "趋势面法")
            {
                QuShiMianFa QSMF = new QuShiMianFa();
                QSMF.Show();
            }
            #endregion
            #region 表面分析
            if (e.Node.Text.ToString() == "坡向")
            {
                PoXiang PX = new PoXiang();
                PX.Show();
            }
            if (e.Node.Text.ToString() == "坡度")
            {
                PoDu PD = new PoDu();
                PD.Show();
            }
            if (e.Node.Text.ToString() == "曲率")
            {
                QuLv QL = new QuLv();
                QL.Show();
            }
            if (e.Node.Text.ToString() == "视域")
            {
                ShiYu SY = new ShiYu();
                SY.Show();
            }
            if (e.Node.Text.ToString() == "填挖方")
            {
                TianWaFang TWF = new TianWaFang();
                TWF.Show();
            }
            #endregion
            #region 距离分析
            if (e.Node.Text.ToString() == "欧氏距离")
            {
                oushijuli OSJL = new oushijuli();
                OSJL.Show();
            }
            if (e.Node.Text.ToString() == "欧式方向")
            {
                oushifangxiang OSFX = new oushifangxiang();
                OSFX.Show();
            }
            if (e.Node.Text.ToString() == "成本距离")
            {
                chengbenjuli CBJL = new chengbenjuli();
                CBJL.Show();
            }
            if (e.Node.Text.ToString() == "路径距离")
            {
                lujingjuli LJJL = new lujingjuli();
                LJJL.Show();
            }
            #endregion
            #region 重分类
            if (e.Node.Text.ToString() == "重分类")
            {
                chongfenlei CFL = new chongfenlei();
                CFL.Show();
            }
            #endregion
            #region 邻域分析
            if (e.Node.Text.ToString() == "块统计")
            {
                kuaitongji KTJ = new kuaitongji();
                KTJ.Show();
            }
            if (e.Node.Text.ToString() == "焦点统计")
            {
                jiaodiantongji JDTJ = new jiaodiantongji();
                JDTJ.Show();
            }
            #endregion
            #endregion
            #region 分析工具
            #region 叠加分析
            if (e.Node.Text.ToString() == "擦除")
            {
                cachu cc = new cachu();
                cc.Show();
            }
            if (e.Node.Text.ToString() == "更新")
            {
                gengxin GX = new gengxin();
                GX.Show();
            }
            if (e.Node.Text.ToString() == "标识")
            {
                biaoshi bs = new biaoshi();
                bs.Show();
            }
            if (e.Node.Text.ToString() == "相交")
            {
                xiangjiao XJ = new xiangjiao();
                XJ.Show();
            }
            if (e.Node.Text.ToString() == "联合")
            {
                lianhe LH = new lianhe();
                LH.Show();
            }
            #endregion
            #region 提取分析
            if (e.Node.Text.ToString() == "分割")
            {
                fenge FG = new fenge();
                FG.Show();
            }
            if (e.Node.Text.ToString() == "裁剪")
            {
                caijian CJ = new caijian();
                CJ.Show();
            }
            if (e.Node.Text.ToString() == "筛选")
            {
                shaixuan SX = new shaixuan();
                SX.Show();
            }
            #endregion
            #region 统计分析
            if (e.Node.Text.ToString() == "频数")
            {
                pinshu PS= new pinshu();
                PS.Show();
            }
            if (e.Node.Text.ToString() == "交集制表")
            {
                jiaojizhibiao JJZB = new jiaojizhibiao();
                JJZB.Show();
            }
            #endregion
            #region 邻域分析
            if (e.Node.Text.ToString() == "缓冲区")
            {
                huanchongqu HCQ = new huanchongqu();
                HCQ.Show();
            }
            if (e.Node.Text.ToString() == "面邻域")
            {
                mianlinyu MLY = new mianlinyu();
                MLY.Show();
            }
            if (e.Node.Text.ToString() == "创建泰森多边形")
            {
                chuangjiantaisenduobianxing CJTSDBX = new chuangjiantaisenduobianxing();
                CJTSDBX.Show();
            }
            if (e.Node.Text.ToString() == "点距离")
            {
                dianjuli DJL = new dianjuli();
                DJL.Show();
            }
            if (e.Node.Text.ToString() == "近邻分析")
            {
                jinlinfenxi JLFX = new jinlinfenxi();
                JLFX.Show();
            }
            #endregion
            #endregion
            #region 制图工具
            #region 制图综合
            if (e.Node.Text.ToString() == "平滑线")
            {
                pinghuaxian PHX = new pinghuaxian();
                PHX.Show();
            }
            if (e.Node.Text.ToString() == "平滑面")
            {
                pinghuamian PHM = new pinghuamian();
                PHM.Show();
            }
            if (e.Node.Text.ToString() == "简化线")
            {
                jianhuaxian JHX = new jianhuaxian();
                JHX.Show();
            }
            if (e.Node.Text.ToString() == "简化面")
            {
                jianhuamian JHM = new jianhuamian();
                JHM.Show();
            }
            if (e.Node.Text.ToString() == "聚合点")
            {
                juhedian JHD = new juhedian();
                JHD.Show();
            }
            if (e.Node.Text.ToString() == "聚合面")
            {
                juhemian JHM = new juhemian();
                JHM.Show();
            }
            #endregion
            #region 掩膜工具
            if (e.Node.Text.ToString() == "死胡同掩膜")
            {
                sihutongyanmo SHTYM = new sihutongyanmo();
                SHTYM.Show();
            }
            if (e.Node.Text.ToString() == "要素轮廓线掩膜")
            {
                yaosulunkuoyanmo YSLKXYM= new yaosulunkuoyanmo();
                YSLKXYM.Show();
            }
            if (e.Node.Text.ToString() == "交叉图层掩膜")
            {
                jiaochatucengyanmo JCTCYM= new jiaochatucengyanmo();
                JCTCYM.Show();
            }
            #endregion
            #endregion
            #region 转换工具
            #region Excel
            if (e.Node.Text.ToString() == "Excel转表")
            {
                ExcelZhuanBiao EZB = new ExcelZhuanBiao();
                EZB.Show();
            }
            if (e.Node.Text.ToString() == "表转Excel")
            {
                BiaoZhuanExcel BZE= new BiaoZhuanExcel();
                BZE.Show();
            }
            #endregion
            #region 由栅格转出
            if (e.Node.Text.ToString() == "栅格转面")
            {
                shangezhuanmian SGZM = new shangezhuanmian();
                SGZM.Show();
            }
            if (e.Node.Text.ToString() == "栅格转点")
            {
                shangezhuandian SGZD = new shangezhuandian();
                SGZD.Show();
            }
            if (e.Node.Text.ToString() == "栅格转ASCII")
            {
                shangezhuanASCII SZAS = new shangezhuanASCII();
                SZAS.Show();
            }
            #endregion
            #region 转为栅格
            if (e.Node.Text.ToString() == "ASCII转栅格")
            {
                ASCIIzhuanshange ASCII = new ASCIIzhuanshange();
                ASCII.Show();
            }
            if (e.Node.Text.ToString() == "点转栅格")
            {
                dianzhuanshange DZSG = new dianzhuanshange();
                DZSG.Show();
            }
            if (e.Node.Text.ToString() == "面转栅格")
            {
                mianzhuanshange MZSG = new mianzhuanshange();
                MZSG.Show();
            }
            if (e.Node.Text.ToString() == "要素转栅格")
            {
                yaosuzhuanshange YSZSG = new yaosuzhuanshange();
                YSZSG.Show();
            }
            #endregion
            #region 转为shapefile
            if (e.Node.Text.ToString() == "要素转shapefile（批量)")
            {
                yaosuzhuanshapefile YSZSF = new yaosuzhuanshapefile();
                YSZSF.Show();
            }
            #endregion
            #endregion
        }

    }
}
