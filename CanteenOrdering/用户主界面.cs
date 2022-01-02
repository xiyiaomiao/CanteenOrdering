using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanteenOrdering
{
    public partial class 用户主界面 : Form
    {
        
        public 用户主界面()
        {
            InitializeComponent();
            generatorFlow();
        }

        public void generatorFlow()
        {
            SqlConnection SqlCon = login_database();
            string sql = "Select * from 店铺";//查找用户sql语句
            SqlCommand cmd = new SqlCommand(sql, SqlCon);
            int num = 0;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流

            

            FlowLayoutPanel[] flow;
            flow = new FlowLayoutPanel[num];
            PictureBox[] pict;
            pict = new PictureBox[num];
            Label[] lab;
            lab = new Label[num];
            

            for (int i = 0; i < num; i++)
            {
                pict[i] = new System.Windows.Forms.PictureBox();
                pict[i].SizeMode = PictureBoxSizeMode.Zoom;
                pict[i].Image = Image.FromFile(@"C:\Users\木滋蔓\Pictures\test\" + (i + 1) + ".jpg");//导入图片
                pict[i].Size = new Size(200, 200);//设置图片大小
                pict[i].BorderStyle = BorderStyle.None;//取消边框
                pict[i].Image.Tag = i;
                lab[i] = new System.Windows.Forms.Label();
                //lab[j].Text = sdr["店铺名称"].ToString();
                lab[i].Text = sdr[i].ToString();
                lab[i].Visible = true;
                lab[i].AutoSize = true;
                lab[i].Font = new System.Drawing.Font("宋体", 15F,
                    System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                flow[i] = new System.Windows.Forms.FlowLayoutPanel();
                flow[i].FlowDirection = FlowDirection.TopDown;
                flow[i].BorderStyle = BorderStyle.FixedSingle;
                flow[i].WrapContents = false;
                flow[i].AutoScroll = true;
                flow[i].AutoSize = true;

                flow[i].Controls.Add(pict[i]);
                flow[i].Controls.Add(lab[i]);
                flow[i].Visible = true;

                flowLayoutPanel1.Controls.Add(flow[i]);
            }
            SqlCon.Close();
        }

        public SqlConnection login_database(){
            //    string ConnectionString = "server=.;" +
            //     "database=食堂送餐数据库;UID=sa;PWD=XIAOYAN99";   //修改为你的用户名和密码
            string ConnectionString = "Data Source=LAPTOP-3CGT6SVO;Initial Catalog=食堂;Integrated Security=True";
            // 创建一个新连接
            SqlConnection SqlCon = new SqlConnection(ConnectionString);
            SqlCon.Open(); //打开数据库
            return SqlCon;

        }

    }
}
