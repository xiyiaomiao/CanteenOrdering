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

namespace CanteenOrdering
{
    public partial class 登录 : Form
    {
        public 登录()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            注册 f2 = new 注册();
            f2.ShowDialog();
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            //获取文本框中的值
            string userid = this.textBox1.Text;
            string password = this.textBox2.Text;
            string classifi = this.comboBox1.Text;
            if (userid.Equals("") || password.Equals("") || classifi.Equals(""))//用户名或密码为空
            {
                MessageBox.Show("用户名、密码或登录类型不能为空");
            }
            else//用户名或密码不为空
            {
                // 设置连接字符串
                string ConnectionString = CommonDate.ConnectionString;
                DataSet dataset = new DataSet(); // 创建数据集
                                             // 创建一个新连接
                SqlConnection SqlCon = new SqlConnection(ConnectionString);
                SqlCon.Open(); //打开数据库
                string sql = "Select * from 登录注册 where 用户名='" + userid + "' and 密码='" + password + "'and 类别='" + classifi + "'";//查找用户sql语句
                SqlCommand cmd = new SqlCommand(sql, SqlCon);
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();
                if (sdr.Read())         //从结果中找到
                {
                    MessageBox.Show("登录成功", "提示");
                 
                    switch (classifi)
                    {
                        case "用户":
                            this.Hide();
                            用户主界面 useruse = new 用户主界面(userid);
                            useruse.Show();
                            break;
                    }
                    

                }
                else
                {
                    MessageBox.Show("用户名或密码错误", "提示");
                    return;
                }
            }
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.PasswordChar = '*';
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
