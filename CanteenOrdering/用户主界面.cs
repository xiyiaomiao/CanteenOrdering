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
        Boolean flag = false;
        String user_id;
        
        public 用户主界面(String userid)
        {
            user_id = userid;
            InitializeComponent();
            tab1_generatorFlow();
            tab2_generatorFlow();
            tab3_ini();
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

        public void tab2_generatorFlow()
        {
            dateTimePicker1.Value = DateTime.Now;
            //显示所有订单
            DateTime dt = this.dateTimePicker1.Value;//获取DataTimePicker控件的值
            //    MessageBox.Show(dt.ToString("yyyy年MM月dd日dddd"));

            String sql = "Select * from 订单,购买 where 用户手机号='" + user_id + "' " +
                "and 订单.订单编号=购买.订单编号";

            listview1_update(dt, sql);
            //convert(varchar,下单时间,21) LIKE '%2021-11-25%'

        }

        



        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//点击进行订单筛选
        {
            
            string dt1 = dateTimePicker1.Text; //获取当前日期
                                               //    label2.Text = dateTimePicker1.Value.ToShortDateString(); //选择成短类型，方便数据库语句

            DateTime dt = this.dateTimePicker1.Value;//获取DataTimePicker控件的值
                                                     //    MessageBox.Show(dt.ToString("yyyy年MM月dd日dddd"));


            String sql = "Select * from 订单,购买 where 用户手机号='" + user_id + "' " +
                "and convert(varchar,下单时间,21) like'%" + dt.ToString("yyyy-MM-dd") + "%' " +
                "and 订单.订单编号=购买.订单编号";
            listview1_update(dt, sql);


        }

       

        private void listView1_Click(object sender, MouseEventArgs e)//选择行事件
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string x="";
                try
                {
                    x = listView1.SelectedItems[0].SubItems[0].Text.ToString();//选中行的第一列的值
                    
                    
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
                

                if (!x.Equals(""))
                {
             //       MessageBox.Show("你选择了" + x + "行！");
                    订单细节 order_inf = new 订单细节(x);
                    order_inf.Show();
                }


            }
            else
            {
                MessageBox.Show("你选择了" + listView1.SelectedItems.Count.ToString() + "行！");
            }

        }

        private void 用户主界面_Load(object sender, EventArgs e)
        {

        }

        public void listview1_update(DateTime dt,String sql)
        {
            this.listView1.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();
            
            //convert(varchar,下单时间,21) LIKE '%2021-11-25%'

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
            myDataAdapter.Fill(myDataSet, "订单信息");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet.Tables["订单信息"];
            if (num > 0)
            {
                DataRow row = myTable.Rows[0];
            }


            this.listView1.SmallImageList = this.imageList1;
            //列表头创建
            this.listView1.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加


            //添加数据项
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();

                // lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));
                //Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00");

                this.listView1.Items.Add(lvi);

                num--;
                i++;
            }


            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。


            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高
            listView1.SmallImageList = imgList; //这里设置listView的SmallImageList ,用imgList将其撑大
            SqlCon.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;//获取DataTimePicker控件的值
                                       //    MessageBox.Show(dt.ToString("yyyy年MM月dd日dddd"));


            String sql = "Select * from 订单,购买 where 用户手机号='" + user_id + "' " +
                "and convert(varchar,下单时间,21) like'%" + dt.ToString("yyyy-MM") + "%' " +
                "and 订单.订单编号=购买.订单编号";
            listview1_update(dt, sql);
            
        }
        
        public void tab3_ini()
        {
            textBox1.Visible = false;//昵称
            textBox2.Visible = false;//地址
            
            label5.Text = user_id;//昵称

            SqlConnection SqlCon = login_database();
            String sql = "select * from 用户 where 用户手机号='" + user_id + "'";
            SqlCommand cmd = new SqlCommand(sql, SqlCon);
            int num = 0;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流


            while (sdr.Read())
            {
                label3.Text = sdr["昵称"].ToString();
                label7.Text = sdr["地址名称"].ToString();
                num++;
            }
            if (num == 0)//说明只是刚刚注册还未登记进入用户表
            {
                label5.Text = "unknowm";
                label7.Text = "unknown";
                MessageBox.Show("请点击修改完善个人信息\n否则点餐将出现问题！");
            }
            else
            {
                flag = true;//说明是注册过的登记用户
            }
            cmd.Cancel();
            sdr.Close();




            SqlCon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text.Equals("修改个人信息"))
            {
                //此时为修改状态
                button3.Text = "修改完成";
                textBox1.Visible = true;
                textBox2.Visible = true;
                label3.Visible = false;
                label7.Visible = false;
                //重新读取数据
                SqlConnection SqlCon = login_database();

                String sql = "select * from 用户 where 用户手机号='" + user_id + "'";
                SqlCommand cmd = new SqlCommand(sql, SqlCon);
                int num = 0;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();//返回一个数据流


                while (sdr.Read())
                {
                    textBox1.Text = sdr["昵称"].ToString();
                    textBox2.Text = sdr["地址名称"].ToString();
                    num++;
                }
                SqlCon.Close();
            }
            else
            {
                //此时为修改完成状态
                button3.Text = "修改个人信息";
                textBox1.Visible = false;
                textBox2.Visible = false;
                label3.Visible = true;
                label7.Visible = true;

            }

            if (!button3.Text.Equals("修改个人信息"))
            {
                //重新读取数据
                SqlConnection SqlCon = login_database();
                
                String sql = "select * from 用户 where 用户手机号='" + user_id + "'";
                SqlCommand cmd = new SqlCommand(sql, SqlCon);
                int num = 0;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();//返回一个数据流


                while (sdr.Read())
                {
                    label3.Text = sdr["昵称"].ToString();
                    label7.Text = sdr["地址名称"].ToString();
                    num++;
                }
                SqlCon.Close();
            }
            else
            {
                //修改或插入数据
                if (flag)//登记用户，修改
                {
                    SqlConnection SqlCon = login_database();
                    String sql = "update 用户 set 昵称='" + textBox1.Text + "'" +
                        ",地址名称='" + textBox2.Text + "' " +
                        "where 用户手机号='" + user_id + "' ";
                    SqlCommand cmd = new SqlCommand(sql, SqlCon);
                    cmd.CommandType = CommandType.Text;
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        MessageBox.Show("修改成功！");
                        label3.Text = textBox1.Text;
                        label7.Text = textBox2.Text;
                    }
                    SqlCon.Close();


                }
                else//未登记用户，插入
                {

                }
            }

            
        }
    }
}
