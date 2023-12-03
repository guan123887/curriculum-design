using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Data.SqlClient;
using MySql.Data.MySqlClient;//SqlConnection Command DataReader

namespace test.登录界面
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        private void LoginOksimpleButton_Click(object sender, EventArgs e)
        {
            //检查是否已经存在
            string userID = userId.Text.Trim();  //取出账号

            /**
             * 连接数据库
             */
            string constr = "server=localhost;User Id=root;password=980319;Database=kechengsheji";
            MySqlConnection mycon = new MySqlConnection(constr);
            mycon.Open();


            //查询新注册的用户是否存在
            string sltStr = "select useId from login where useId='" + userId + "'";
            MySqlCommand checkCmd = new MySqlCommand(sltStr, mycon);       //创建SQL命令执行对象          
            checkCmd.CommandText = sltStr;
            MySqlDataAdapter check = new MySqlDataAdapter();       //实例化数据适配器
            check.SelectCommand = checkCmd;                    //让适配器执行SELECT命令
            DataSet checkData = new DataSet();                 //实例化结果数据集
            int n = check.Fill(checkData, "register");         //将结果放入数据适配器，返回元祖个数
            if (n != 0)
            {
                MessageBox.Show("用户名存在");
                userId.Text = ""; userPw.Text = "";
            }


            //确认密码
            if (ensurePw.Text != userPw.Text)
            {
                ensurePw.Text = "";
            }

            //验证码
            if (textCheck.Text != checkCode.Text)
            {
                textCheck.Text = "";
            }
            //插入数据SQL  逻辑
            string s1 = "insert into login(useId,usePw) values ('" + userId.Text + "','" + userPw.Text + "')";                            //编写SQL命令
            MySqlCommand mycom = new MySqlCommand(s1, mycon);          //初始化命令
            mycom.ExecuteNonQuery();             //执行语句
            mycon.Close();                       //关闭连接
            mycom = null;
            mycon.Dispose();                     //释放对象
            

            if (userId.Text == "" || userPw.Text.Length <= 6  || ensurePw.Text == ""|| textCheck.Text == "")
            {
                MessageBox.Show("请将信息填完整");
            }
            else
            {
                MessageBox.Show("注册成功");
                this.Close();
            }
        }

        private void textCheck_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int minV = 12345, maxV = 98765;
            checkCode.Text = random.Next(minV, maxV).ToString();
        }
    }
}
