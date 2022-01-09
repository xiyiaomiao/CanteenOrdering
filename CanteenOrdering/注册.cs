using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace CanteenOrdering
{
    public partial class 注册 : Form
    {
        登录 f2;
        public 注册(登录 f1)
        {
            InitializeComponent();
            f2 = f1;


        }

        private void 注册_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;            
            f2.Visible=true;
         //   this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取文本框中的值
            string userid = this.textBox1.Text;
            string password = this.textBox2.Text;
            string dopassword = this.textBox3.Text;
            string classifi = this.comboBox1.Text;
            if (userid.Equals("") || password.Equals("") || dopassword.Equals("") || classifi.Equals(""))//用户名或密码为空
            {
                MessageBox.Show("用户名、密码或注册类型不能为空");
            }
            else//用户名或密码不为空
            {
                if (password != dopassword)
                {
                    MessageBox.Show("密码不一致!", "提示");

                }
                else {
                    // 设置连接字符串
                    string ConnectionString = CommonDate.ConnectionString;
                    //DataSet dataset = new DataSet(); // 创建数据集
                    // 创建一个新连接
                    SqlConnection SqlCon = new SqlConnection(ConnectionString);
                SqlCon.Open(); //打开数据库
                string sql1 = "Select * from 登录注册 where 用户名='" + userid + "' and 类别='" + classifi + "'";//查找用户sql语句
                SqlCommand cmd1 = new SqlCommand(sql1, SqlCon);
                    cmd1.CommandType = CommandType.Text;
                    SqlDataReader sdr;
                    sdr = cmd1.ExecuteReader();
                    if (sdr.Read())         //从结果中找到
                    {
                        MessageBox.Show("用户名以及注册类型已存在", "提示");
                    }
                    else {
                        SqlConnection SqlCon1 = new SqlConnection(ConnectionString);
                        SqlCon1.Open(); //打开数据库
                    
                        string sql = "insert into 登录注册(用户名,密码,类别)values('" + userid + "', '" + password + "', '" + classifi + "')";
                        SqlCommand cmd = new SqlCommand(sql, SqlCon1);
                        SqlDataReader sdr1;
                        sdr1 = cmd.ExecuteReader();
                        MessageBox.Show("注册成功", "提示");


                    }
                }





            }

        }
    }
}
