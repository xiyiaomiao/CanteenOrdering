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
        FlowLayoutPanel[] flow;
        PictureBox[] pict;
        Label[] lab;
        Boolean flag = false;
        String user_id;
        int id = 0;
        
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


            
            lab = new Label[num];
            flow = new FlowLayoutPanel[num];
            pict = new PictureBox[num];

            for (int i = 0; i < num; i++)
            {
                pict[i] = new System.Windows.Forms.PictureBox();
                pict[i].SizeMode = PictureBoxSizeMode.Zoom;
                pict[i].Image = System.Drawing.Image.FromFile(@"..\\..\\Resources\\"+myTable.Rows[i]["店铺名称"].ToString()+".jpg");
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

                pict[i].Name = "pict" + i.ToString();
                pict[i].Click += new System.EventHandler(picti_Click);

                lab[i].Name = "lab" + i.ToString();
                lab[i].Click += new System.EventHandler(labi_Click);

                flow[i].Name = "flow"+i.ToString();
                flow[i].Click += new System.EventHandler(flowi_Click);
                flowLayoutPanel1.Controls.Add(flow[i]);
            }
            SqlCon.Close();
        }

        //判断点击的是哪一个
        private void flowi_Click(object sender, EventArgs e)
        {
            
            var index= (sender as FlowLayoutPanel).Name.Replace("flow","");
            id = int.Parse(index);
            enter_order(id);
            
        }

        private void picti_Click(object sender, EventArgs e)
        {

            var index = (sender as PictureBox).Name.Replace("pict", "");
            id = int.Parse(index);
            enter_order(id);
        }

        private void labi_Click(object sender, EventArgs e)
        {

            var index = (sender as Label).Name.Replace("lab", "");
            id = int.Parse(index);
            enter_order(id);
        }

        public void enter_order(int i)
        {
            id = i;
            if (MessageBox.Show("是否进入"+lab[id].Text.Replace(" ","")+"点餐？", "提示",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
             //   MessageBox.Show(id.ToString());
                商品点餐界面 f1 = new 商品点餐界面(lab[id].Text.Replace(" ", ""),user_id);
                f1.Show();
            }
            
        }


        public SqlConnection login_database(){
            string ConnectionString = CommonDate.ConnectionString;
            SqlConnection SqlCon = new SqlConnection(ConnectionString);
            SqlCon.Open(); //打开数据库
            return SqlCon;

        }

        public void tab2_generatorFlow()
        {
            //列表头创建
            this.listView1.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加

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
            
            //添加数据项
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {
                //添加前先检查有无重复项
                if (i != 0)
                {
                    int j;
                    for(j = 0; j < listView1.Items.Count; j++)
                    {
                        if (listView1.Items[j].Text.Equals(myTable.Rows[i]["订单编号"].ToString()))
                        {
                            listView1.Items[j].SubItems[3].Text = listView1.Items[j].SubItems[3].Text + "等";
                            
                        }
                        
                    }
                    
                }
                
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

            //移除重复数据项
            int j1;
            for (j1 = 0; j1 < listView1.Items.Count; j1++)
            {
                if (j1 > 0)
                {
                    if (listView1.Items[j1].Text.Equals(listView1.Items[j1 - 1].Text))
                    {
                        listView1.Items[j1].Remove();

                    }
                }
                

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

            textBox3.Visible = false;//密码框
            textBox4.Visible = false;
            textBox5.Visible = false;

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
                label3.Text = "unknowm";
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
                    SqlConnection SqlCon = login_database();
                    String sql = "insert into 用户 " +
                        "values('" + user_id + "','" + textBox1.Text + "'," +
                        "'" + textBox2.Text + "')";
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
            }          
        }

        private void button4_Click(object sender, EventArgs e)//修改密码
        {
            if (button4.Text.Equals("修改密码"))
            {
                button4.Text = "修改完成";
                label11.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                
            }
            else
            {
                button4.Text = "修改密码";
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;

                //读取数据与输入密码做匹配
                SqlConnection SqlCon = login_database();

                String sql = "select * from 登录注册 where 用户名='" + user_id + "'";
                SqlCommand cmd = new SqlCommand(sql, SqlCon);

                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();//返回一个数据流

                if (sdr.Read())//查找到该用户登录注册信息
                {
                    if (sdr["密码"].ToString().Equals(textBox3.Text))//首先对原密码进行判断
                    {
                        if (textBox4.Text.Equals(textBox5.Text))//进行两次新密码一致性判定
                        {
                            cmd.Cancel();
                            sdr.Close();
                            String sql1 = "update 登录注册 set 密码='" + textBox4.Text + "'" +
                            "where 用户名='" + user_id + "' ";
                            SqlCommand cmd1 = new SqlCommand(sql1, SqlCon);
                            cmd1.CommandType = CommandType.Text;
                            if (cmd1.ExecuteNonQuery() != 0)
                            {
                                MessageBox.Show("修改成功！");

                            }

                        }
                        else//两次输入的密码不一致请重新输入
                        {
                            MessageBox.Show("请保持两次输入的新密码一致！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请输入正确的修改前原密码！");
                    }
                }

                
                SqlCon.Close();
                //修改完成后清空密码框
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("您确认要注销账号吗？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("开始注销，请稍候！");
                
                //登录注册表一定需要删
                SqlConnection SqlCon = login_database();

                String sql = "delete from 登录注册 where 用户名='" + user_id + "'";
                SqlCommand cmd = new SqlCommand(sql, SqlCon);
                cmd.CommandType = CommandType.Text;
            
            
                if (cmd.ExecuteNonQuery() != 0)
                {
                    //MessageBox.Show("删除登录注册表成功！");
                    cmd.Cancel();
                    if (flag)//用户表也要删除
                    {
                        String sql1= "delete from 用户 where 用户手机号='" + user_id + "'";
                        SqlCommand cmd1 = new SqlCommand(sql1, SqlCon);
                        cmd1.CommandType = CommandType.Text;
                        if(cmd1.ExecuteNonQuery() != 0)
                        {
                            MessageBox.Show("注销成功！");
                            //   MessageBox.Show("注销用户表成功！");
                        }
                    
                    }
                    else
                    {
                        MessageBox.Show("注销成功！");
                        //   MessageBox.Show("只需要注销登录注册表！");
                    }
                

                }

            
                SqlCon.Close();

                //返回登录界面
                this.Close();
                登录 f1 = new 登录();
                f1.Show();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
