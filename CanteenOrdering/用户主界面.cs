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
        String user_id;
        
        public 用户主界面(String userid)
        {
            user_id = userid;
            InitializeComponent();
            tab1_generatorFlow();
        }

        public void tab1_generatorFlow()
        {
            SqlConnection SqlCon = login_database();
            String sql2 = "Select * from 用户 where 用户手机号='" + user_id+ "'";            
            SqlCommand cmd2 = new SqlCommand(sql2, SqlCon);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader sdr2;
            sdr2 = cmd2.ExecuteReader();//返回一个数据流
            if (sdr2.Read())
            {
                label1.Text = "欢迎！用户" + sdr2["昵称"] + "进行点餐！";
            }
            else
            {
                label1.Text = "欢迎！用户"+user_id+"进行点餐！";
            }

            cmd2.Cancel();
            sdr2.Close();


            string sql = "Select * from 店铺";//查找用户sql语句
            SqlCommand cmd = new SqlCommand(sql, SqlCon);
            int num = 0;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流

            
            while (sdr.Read())
            {
                num++;
            }
            cmd.Cancel();
            sdr.Close();

            SqlDataAdapter myDataAdapter = new SqlDataAdapter(sql, SqlCon);
            DataSet myDataSet = new DataSet();      // 创建DataSet
            myDataAdapter.Fill(myDataSet, "店铺");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet.Tables["店铺"];
            DataRow row = myTable.Rows[0];


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
                pict[i].Image = System.Drawing.Image.FromFile(@"..\\..\\Resources\\"+(i+1)+".jpg");
                pict[i].Size = new Size(200, 100);//设置图片大小
                pict[i].BorderStyle = BorderStyle.None;//取消边框
                pict[i].Image.Tag = i;
                lab[i] = new System.Windows.Forms.Label();
                lab[i].Text = "     "+myTable.Rows[i]["店铺名称"].ToString();
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
            string ConnectionString = CommonDate.ConnectionString;
            SqlConnection SqlCon = new SqlConnection(ConnectionString);
            SqlCon.Open(); //打开数据库
            return SqlCon;

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void 用户主界面_Load(object sender, EventArgs e)
        {

        }
    }
}
