using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CanteenOrdering
{
    public partial class 商家 : Form
    {
        String id;
        public 商家(String userid)
        {
            InitializeComponent();
            id = userid;
            tab5_generatorFlow();
            tab4_generatorFlow();
            tab7_generatorFlow();
            tab3_generatorFlow();
            tab2_generatorFlow();
            tab6_generatorFlow();
            tab1_generatorFlow();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {
            
        }
        public void tab5_generatorFlow()
        {
            SqlConnection SqlCon = login_database();
            String sql2 = "Select * from 店铺 where 店铺电话='" + id + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, SqlCon);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader sdr2;
            sdr2 = cmd2.ExecuteReader();//返回一个数据流
            if (sdr2.Read())
            {
                label1.Text = "您好！店铺：" + sdr2["店铺名称"];
                textBox1.Text = Convert.ToString(sdr2["店铺名称"]);
                comboBox1.Text = Convert.ToString(sdr2["配送费"]);
                textBox2.Text = Convert.ToString(sdr2["营业时间"]);
                textBox5.Text = Convert.ToString(sdr2["老板"]);
                textBox4.Text = Convert.ToString(sdr2["店铺电话"]);
                textBox3.Text = Convert.ToString(sdr2["店铺地址"]);
            }
            else
            {
                label1.Text = "您好！店铺" + id;
            }

            cmd2.Cancel();
            sdr2.Close();
            SqlCon.Close();
        }
            public SqlConnection login_database()
        {
            string ConnectionString = CommonDate.ConnectionString;
            SqlConnection SqlCon = new SqlConnection(ConnectionString);
            SqlCon.Open(); //打开数据库
            return SqlCon;

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //获取商家输入信息
            string name = this.textBox1.Text;
            decimal money = Convert.ToDecimal(this.comboBox1.Text.Trim());
            string time = this.textBox2.Text;
            string boss = this.textBox5.Text;
            string phone = this.textBox4.Text;
            string address = this.textBox3.Text;
            if (name.Equals("") || money.Equals("") || time.Equals("") || boss.Equals("") || phone.Equals("") || address.Equals("")) 
            {
                MessageBox.Show("请完善信息！");
            }
            else
            {
                SqlConnection SqlCon1 = login_database();
                String sql3 = "Select * from 店铺 where 店铺电话='" + id + "'";
                SqlCommand cmd3 = new SqlCommand(sql3, SqlCon1);
                cmd3.CommandType = CommandType.Text;
                SqlDataReader sdr5;
                sdr5 = cmd3.ExecuteReader();//返回一个数据流
                if (sdr5.Read())
                {

                    cmd3.Cancel();
                    sdr5.Close();
                    
                    SqlConnection SqlCon3 = login_database();
                    string sql = "update 店铺 set 店铺名称='" + name + "',配送费='" + money + "',营业时间='" + time + "',老板='" + boss + "',店铺电话= '" + phone + "',店铺地址='" + address + "' where 店铺电话= '" + id + "'  ";
                    SqlCommand cmd = new SqlCommand(sql, SqlCon3);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader sdr3;
                    sdr3 = cmd.ExecuteReader();
                   
                    MessageBox.Show("更新成功！");
         
                    cmd.Cancel();
                    sdr3.Close();
                    SqlCon3.Close();
                }
                else
                {

                    cmd3.Cancel();
                    sdr5.Close();
                    SqlConnection SqlCon2 = login_database();
                    string sql = "insert into 店铺(店铺名称,配送费,营业时间,老板,店铺电话,店铺地址)values('" + name + "', '" + money + "', '" + time + "', '" + boss + "', '" + phone + "', '" + address + "')";
                    SqlCommand cmd = new SqlCommand(sql, SqlCon2);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader sdr4;
                    sdr4 = cmd.ExecuteReader();
                    MessageBox.Show("修改成功！");
                   
                    cmd.Cancel();
                    sdr4.Close();
                    SqlCon2.Close();
                }

                SqlCon1.Close();
            }
            SqlConnection SqlCon = login_database();
            String sql2 = "Select * from 店铺 where 店铺电话='" + id + "'";
            SqlCommand cmd2 = new SqlCommand(sql2, SqlCon);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader sdr2;
            sdr2 = cmd2.ExecuteReader();//返回一个数据流
            if (sdr2.Read())
            {
                label1.Text = "您好！店铺：" + sdr2["店铺名称"];
            }
            else
            {
                label1.Text = "您好！店铺" + id;
            }

            cmd2.Cancel();
            sdr2.Close();
            SqlCon.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string dt1 = dateTimePicker1.Text; //获取当前日期
            DateTime dt = this.dateTimePicker1.Value;//获取DataTimePicker控件的值
            string name = this.textBox1.Text;
            SqlConnection SqlCon = login_database();
            String sql1 = "Select * from 订单 where 店铺名称='" + name + "' " +
                "and convert(varchar,下单时间,21) like'%" + dt.ToString("yyyy-MM-dd") + "%' " 
               ;
            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
           
            decimal sum = 0;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流

            while (sdr.Read())
            {
                decimal money = Convert.ToDecimal(sdr["总额"]);
                sum=sum+ money;
            }
            label12.Text = sum + "元";
            cmd.Cancel();
            sdr.Close();
            SqlCon.Close();
        }
         public void tab4_generatorFlow()
         {
             dateTimePicker2.Value = DateTime.Now;
             //显示所有订单
             this.listView1.Items.Clear();  //只移除所有的项
             SqlConnection SqlCon = login_database();
             DateTime dt = this.dateTimePicker2.Value;//获取DataTimePicker控件的值
                                                      //    MessageBox.Show(dt.ToString("yyyy年MM月dd日dddd"));

             string name = this.textBox1.Text;

             String sql1 = "Select * from 订单,购买 where 订单.店铺名称='" + name + "' and 订单.配送状态 in( '已取餐', '已变质')" +
                 "and 订单.订单编号=购买.订单编号";

             SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

             SqlDataAdapter myDataAdapter = new SqlDataAdapter(sql1, SqlCon);
             DataSet myDataSet = new DataSet();      // 创建DataSet
             myDataAdapter.Fill(myDataSet, "已完成订单信息");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
             DataTable myTable = myDataSet.Tables["已完成订单信息"];
             if (num > 0)
             {
                 DataRow row = myTable.Rows[0];
             }


             this.listView1.SmallImageList = this.imageList1;
             //列表头创建
             this.listView1.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
             this.listView1.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
             this.listView1.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
             this.listView1.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
             this.listView1.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
             this.listView1.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
             this.listView1.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
             this.listView1.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
             this.listView1.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);
             //添加数据项
             this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
             int i = 0;
             while (num != 0)
             {

                 ListViewItem lvi = new ListViewItem();

                 // lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                 lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                 lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                 lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                 lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                 lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                 lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));
                 //Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00");
                 lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                 lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                 lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
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
        public void listview1_selectdate()//选择显示
        {
           

            this.listView1.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();
            DateTime dt = this.dateTimePicker2.Value;//获取DataTimePicker控件的值
                                                     //    MessageBox.Show(dt.ToString("yyyy年MM月dd日dddd"));

            string name = this.textBox1.Text;
          
            String sql1 = "Select * from 订单,购买 where 店铺名称='" + name+ "'and 订单.配送状态 in( '已取餐', '已变质') " +
                "and convert(varchar,下单时间,21) like'%" + dt.ToString("yyyy-MM-dd") + "%' " +
                "and 订单.订单编号=购买.订单编号" ;

            //convert(varchar,下单时间,21) LIKE '%2021-11-25%'

            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

            SqlDataAdapter myDataAdapter = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet = new DataSet();      // 创建DataSet
            myDataAdapter.Fill(myDataSet, "已完成订单信息");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet.Tables["已完成订单信息"];
            if (num > 0)
            {
                DataRow row = myTable.Rows[0];
            }


            this.listView1.SmallImageList = this.imageList1;
            //列表头创建
            this.listView1.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
            this.listView1.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
            this.listView1.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
            this.listView1.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);


            //添加数据项
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();

                // lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));
                //Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00");
                lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
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



        private void button3_Click(object sender, EventArgs e)
        {
            string dt1 = dateTimePicker2.Text; //获取当前日期
                                               //    label2.Text = dateTimePicker1.Value.ToShortDateString(); //选择成短类型，方便数据库语句
                listview1_selectdate();
            
        }

        public void tab7_generatorFlow()
        {
           
            //显示所有订单
            this.listView2.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();

            //string name = this.textBox1.Text;
            string name = this.textBox1.Text;
            
            String sql1 = "Select * from 订单,购买 where 订单.店铺名称='" + name + "' and 订单.配送状态 = '待取餐'" +
                "and 订单.订单编号=购买.订单编号";

            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

            SqlDataAdapter myDataAdapter1 = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet1 = new DataSet();      // 创建DataSet
            myDataAdapter1.Fill(myDataSet1, "待取餐订单信息");   // 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet1.Tables["待取餐订单信息"];
            if (num > 0)
            {
                DataRow row1 = myTable.Rows[0];
            }


            this.listView2.SmallImageList = this.imageList2;
            //列表头创建
            this.listView2.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
            this.listView2.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
            this.listView2.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
            this.listView2.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);
            //添加数据项
            this.listView2.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();

               

                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));
               
                lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
                this.listView2.Items.Add(lvi);

                num--;
                i++;
            }


            this.listView2.EndUpdate();  //结束数据处理，UI界面一次性绘制。


            ImageList imgList2 = new ImageList();

            imgList2.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高

            listView2.SmallImageList = imgList2; //这里设置listView的SmallImageList ,用imgList将其撑大

            SqlCon.Close();

        }


        public void listview2_selectdate()//选择显示
        {
            this.listView2.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();
            

            string name = this.textBox1.Text;
           
           
            String sql1 = "Select * from 订单,购买 where 店铺名称='" + name + "'and 订单.配送状态 ='待取餐' "  +
                "and 订单.订单编号=购买.订单编号";

           

            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

            SqlDataAdapter myDataAdapter1 = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet1 = new DataSet();      // 创建DataSet
            myDataAdapter1.Fill(myDataSet1, "待取餐订单信息");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet1.Tables["待取餐订单信息"];
            if (num > 0)
            {
                DataRow row1 = myTable.Rows[0];
            }


            this.listView2.SmallImageList = this.imageList2;
            //列表头创建
            this.listView2.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
            this.listView2.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
            this.listView2.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
            this.listView2.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
            this.listView2.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);


            //添加数据项
            this.listView2.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();

                // lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));
                //Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00");
                lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
                this.listView2.Items.Add(lvi);

                num--;
                i++;
            }


            this.listView2.EndUpdate();  //结束数据处理，UI界面一次性绘制。


            ImageList imgList2 = new ImageList();

            imgList2.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高

            listView2.SmallImageList = imgList2; //这里设置listView的SmallImageList ,用imgList将其撑大


            SqlCon.Close();
        }



        private void button4_Click(object sender, EventArgs e)
        {
           
            listview2_selectdate();

        }


        public void tab3_generatorFlow()
        {

            //显示所有订单
            this.listView3.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();

            //string name = this.textBox1.Text;
            string name = this.textBox1.Text;

            String sql1 = "Select * from 订单,购买 where 订单.店铺名称='" + name + "' and 订单.配送状态 = '待送达'" +
                "and 订单.订单编号=购买.订单编号";

            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

            SqlDataAdapter myDataAdapter1 = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet1 = new DataSet();      // 创建DataSet
            myDataAdapter1.Fill(myDataSet1, "待取餐订单信息");   // 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet1.Tables["待取餐订单信息"];
            if (num > 0)
            {
                DataRow row1 = myTable.Rows[0];
            }


            this.listView3.SmallImageList = this.imageList2;
            //列表头创建
            this.listView3.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
            this.listView3.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
            this.listView3.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
            this.listView3.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);
            //添加数据项
            this.listView3.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();



                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));

                lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
                this.listView3.Items.Add(lvi);

                num--;
                i++;
            }


            this.listView3.EndUpdate();  //结束数据处理，UI界面一次性绘制。


            ImageList imgList2 = new ImageList();

            imgList2.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高

            listView3.SmallImageList = imgList2; //这里设置listView的SmallImageList ,用imgList将其撑大

            SqlCon.Close();

        }


        public void listview3_selectdate()//选择显示
        {
            this.listView3.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();


            string name = this.textBox1.Text;


            String sql1 = "Select * from 订单,购买 where 店铺名称='" + name + "'and 订单.配送状态 ='待送达' " +
                "and 订单.订单编号=购买.订单编号";



            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

            SqlDataAdapter myDataAdapter1 = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet1 = new DataSet();      // 创建DataSet
            myDataAdapter1.Fill(myDataSet1, "待取餐订单信息");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet1.Tables["待取餐订单信息"];
            if (num > 0)
            {
                DataRow row1 = myTable.Rows[0];
            }


            this.listView3.SmallImageList = this.imageList2;
            //列表头创建
            this.listView3.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
            this.listView3.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
            this.listView3.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
            this.listView3.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
            this.listView3.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);


            //添加数据项
            this.listView3.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();

                // lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));
                //Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00");
                lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
                this.listView3.Items.Add(lvi);

                num--;
                i++;
            }


            this.listView3.EndUpdate();  //结束数据处理，UI界面一次性绘制。


            ImageList imgList2 = new ImageList();

            imgList2.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高

            listView3.SmallImageList = imgList2; //这里设置listView的SmallImageList ,用imgList将其撑大


            SqlCon.Close();
        }



        private void button5_Click(object sender, EventArgs e)
        {

            listview3_selectdate();

        }


        public void tab2_generatorFlow()
        {

            //显示所有订单
            this.listView4.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();

            //string name = this.textBox1.Text;
            string name = this.textBox1.Text;

            String sql1 = "Select * from 订单,购买 where 订单.店铺名称='" + name + "' and 订单.配送状态 = '待配送'" +
                "and 订单.订单编号=购买.订单编号";

            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

            SqlDataAdapter myDataAdapter1 = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet1 = new DataSet();      // 创建DataSet
            myDataAdapter1.Fill(myDataSet1, "待取餐订单信息");   // 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet1.Tables["待取餐订单信息"];
            if (num > 0)
            {
                DataRow row1 = myTable.Rows[0];
            }


            this.listView4.SmallImageList = this.imageList2;
            //列表头创建
            this.listView4.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
            this.listView4.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
            this.listView4.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
            this.listView4.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);
            //添加数据项
            this.listView4.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();



                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));

                lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
                this.listView4.Items.Add(lvi);

                num--;
                i++;
            }


            this.listView4.EndUpdate();  //结束数据处理，UI界面一次性绘制。


            ImageList imgList2 = new ImageList();

            imgList2.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高

            listView4.SmallImageList = imgList2; //这里设置listView的SmallImageList ,用imgList将其撑大

            SqlCon.Close();

        }


        public void listview4_selectdate()//选择显示
        {
            this.listView4.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();


            string name = this.textBox1.Text;


            String sql1 = "Select * from 订单,购买 where 店铺名称='" + name + "'and 订单.配送状态 ='待配送' " +
                "and 订单.订单编号=购买.订单编号";



            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

            SqlDataAdapter myDataAdapter1 = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet1 = new DataSet();      // 创建DataSet
            myDataAdapter1.Fill(myDataSet1, "待取餐订单信息");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet1.Tables["待取餐订单信息"];
            if (num > 0)
            {
                DataRow row1 = myTable.Rows[0];
            }


            this.listView4.SmallImageList = this.imageList2;
            //列表头创建
            this.listView4.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
            this.listView4.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
            this.listView4.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
            this.listView4.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
            this.listView4.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);


            //添加数据项
            this.listView4.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();

                // lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));
                //Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00");
                lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
                this.listView4.Items.Add(lvi);

                num--;
                i++;
            }


            this.listView4.EndUpdate();  //结束数据处理，UI界面一次性绘制。


            ImageList imgList2 = new ImageList();

            imgList2.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高

            listView4.SmallImageList = imgList2; //这里设置listView的SmallImageList ,用imgList将其撑大


            SqlCon.Close();
        }



        private void button6_Click(object sender, EventArgs e)
        {

            listview4_selectdate();

        }


        public void tab6_generatorFlow()
        {

            //显示所有订单
            this.listView5.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();

            //string name = this.textBox1.Text;
            string name = this.textBox1.Text;

            String sql1 = "Select * from 订单,购买 where 订单.店铺名称='" + name + "' and 订单.配送状态 = '待配货'" +
                "and 订单.订单编号=购买.订单编号";

            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

            SqlDataAdapter myDataAdapter1 = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet1 = new DataSet();      // 创建DataSet
            myDataAdapter1.Fill(myDataSet1, "待取餐订单信息");   // 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet1.Tables["待取餐订单信息"];
            if (num > 0)
            {
                DataRow row1 = myTable.Rows[0];
            }


            this.listView5.SmallImageList = this.imageList2;
            //列表头创建
            this.listView5.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
            this.listView5.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
            this.listView5.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
            this.listView5.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);
            //添加数据项
            this.listView5.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();



                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));

                lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
                this.listView5.Items.Add(lvi);

                num--;
                i++;
            }


            this.listView5.EndUpdate();  //结束数据处理，UI界面一次性绘制。


            ImageList imgList2 = new ImageList();

            imgList2.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高

            listView5.SmallImageList = imgList2; //这里设置listView的SmallImageList ,用imgList将其撑大

            SqlCon.Close();

        }


        public void listview5_selectdate()//选择显示
        {
            this.listView5.Items.Clear();  //只移除所有的项
            SqlConnection SqlCon = login_database();


            string name = this.textBox1.Text;


            String sql1 = "Select * from 订单,购买 where 店铺名称='" + name + "'and 订单.配送状态 ='待配货' " +
                "and 订单.订单编号=购买.订单编号";



            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
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

            SqlDataAdapter myDataAdapter1 = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet1 = new DataSet();      // 创建DataSet
            myDataAdapter1.Fill(myDataSet1, "待配送订单信息");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet1.Tables["待配送订单信息"];
            if (num > 0)
            {
                DataRow row1 = myTable.Rows[0];
            }


            this.listView5.SmallImageList = this.imageList2;
            //列表头创建
            this.listView5.Columns.Add("订单编号", 160, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("用户手机号", 160, HorizontalAlignment.Center);
            this.listView5.Columns.Add("下单时间", 240, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("配送状态", 120, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("订单细节", 140, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("价格", 120, HorizontalAlignment.Center); //一步添加
            this.listView5.Columns.Add("工作人员编号", 160, HorizontalAlignment.Center);
            this.listView5.Columns.Add("地址名称", 120, HorizontalAlignment.Center);
            this.listView5.Columns.Add("快递员手机号", 160, HorizontalAlignment.Center);


            //添加数据项
            this.listView5.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            int i = 0;
            while (num != 0)
            {

                ListViewItem lvi = new ListViewItem();

                // lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = myTable.Rows[i]["订单编号"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["用户手机号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["下单时间"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["配送状态"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["商品名称"].ToString() + "*" + myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add(Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00"));
                //Convert.ToDecimal(myTable.Rows[i]["总额"]).ToString("0.00");
                lvi.SubItems.Add(myTable.Rows[i]["工作人员编号"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["地址名称"].ToString());
                lvi.SubItems.Add(myTable.Rows[i]["快递员手机号"].ToString());
                this.listView5.Items.Add(lvi);

                num--;
                i++;
            }


            this.listView5.EndUpdate();  //结束数据处理，UI界面一次性绘制。


            ImageList imgList2 = new ImageList();

            imgList2.ImageSize = new Size(1, 20);// 设置行高 20 //分别是宽和高

            listView5.SmallImageList = imgList2; //这里设置listView的SmallImageList ,用imgList将其撑大


            SqlCon.Close();
        }



        private void button7_Click(object sender, EventArgs e)
        {

            listview5_selectdate();

        }

        public void tab1_generatorFlow()
        {
            string name = this.textBox1.Text;
            if (name == "一食堂")
            {
                panel1.Show();
                panel2.Hide();

            }
            else if(name == "二食堂")
            {
                panel2.Show();
                panel1.Hide();
            }
            else
            {
                panel1.Hide();
                panel2.Hide();

            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlConnection SqlCon = login_database();
            string name = this.textBox1.Text;
            String sql1 = "update 商品 set 状态='无' where 店铺名称='" + name + "'"; 
            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流
            sdr.Close();
            cmd.Cancel();
            SqlCon.Close();
            if (checkBox1.Checked)
            {
               
                if (this.textBox6.Text.Equals(""))
                {
                    MessageBox.Show("请输入牛肉面价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox6.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='牛肉面'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("牛肉面上架成功！");
                }
            }
            if (checkBox2.Checked)
            {

                if (this.textBox7.Text.Equals(""))
                {
                    MessageBox.Show("请输入鸡丝面价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox7.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='鸡丝面'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("鸡丝面上架成功！");
                }
            }

            if (checkBox3.Checked)
            {

                if (this.textBox8.Text.Equals(""))
                {
                    MessageBox.Show("请输入番茄鸡蛋面价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox8.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='番茄鸡蛋面'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("番茄鸡蛋面上架成功！");
                }
            }


            if (checkBox4.Checked)
            {

                if (this.textBox9.Text.Equals(""))
                {
                    MessageBox.Show("请输入葱油拌面价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox9.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='葱油拌面'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("葱油拌面上架成功！");
                }
            }
           
            if (checkBox5.Checked)
            {

                if (this.textBox10.Text.Equals(""))
                {
                    MessageBox.Show("请输入炸酱面价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox10.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='炸酱面'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("炸酱面上架成功！");
                }
            }
            if (checkBox6.Checked)
            {

                if (this.textBox11.Text.Equals(""))
                {
                    MessageBox.Show("请输入红烧牛肉饭价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox11.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='红烧牛肉饭'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("红烧牛肉饭上架成功！");
                }
            }
            if (checkBox7.Checked)
            {

                if (this.textBox12.Text.Equals(""))
                {
                    MessageBox.Show("请输入黄焖鸡米饭价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox12.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='黄焖鸡米饭'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("黄焖鸡米饭上架成功！");
                }
            }
            if (checkBox8.Checked)
            {

                if (this.textBox13.Text.Equals(""))
                {
                    MessageBox.Show("请输入黄焖排骨饭价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox13.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='黄焖排骨饭'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("黄焖排骨饭上架成功！");
                }
            }
            if (checkBox9.Checked)
            {

                if (this.textBox14.Text.Equals(""))
                {
                    MessageBox.Show("请输入泡菜五花肉饭价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox14.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='泡菜五花肉饭'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("泡菜五花肉饭上架成功！");
                }
            }
            if (checkBox10.Checked)
            {

                if (this.textBox15.Text.Equals(""))
                {
                    MessageBox.Show("请输入香菇卤肉饭价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox15.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='香菇卤肉饭'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("香菇卤肉饭上架成功！");
                }
            }
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlConnection SqlCon = login_database();
            string name = this.textBox1.Text;
            String sql1 = "update 商品 set 状态='无' where 店铺名称='" + name + "'";
            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流
            sdr.Close();
            cmd.Cancel();
            SqlCon.Close();
            if (checkBox11.Checked)
            {

                if (this.textBox16.Text.Equals(""))
                {
                    MessageBox.Show("请输入盖饭价格！");
                }
                else
                {
                    decimal price2 = Convert.ToDecimal(this.textBox16.Text.Trim());
                    SqlConnection SqlCon2 = login_database();
                    String sql2 = "update 商品 set 状态='有',价格='" + price2 + "' where 店铺名称='" + name + "'and 商品名称='盖饭'";
                    SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
                    cmd2.CommandType = CommandType.Text;
                    SqlDataReader sdr2;
                    sdr2 = cmd2.ExecuteReader();//返回一个数据流
                    sdr2.Close();
                    cmd2.Cancel();
                    SqlCon2.Close();
                    MessageBox.Show("盖饭上架成功！");
                }
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            SqlConnection SqlCon2 = login_database();
            String sql2 = "update 订单 set 配送状态='待配送'where 店铺名称='" + name + "'and 配送状态='待配货'";
            SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader sdr2;
            sdr2 = cmd2.ExecuteReader();//返回一个数据流
            sdr2.Close();
            cmd2.Cancel();
            SqlCon2.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            SqlConnection SqlCon2 = login_database();
            String sql2 = "update 订单 set 配送状态='待送达'where 店铺名称='" + name + "'and 配送状态='待配送'";
            SqlCommand cmd2 = new SqlCommand(sql2, SqlCon2);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader sdr2;
            sdr2 = cmd2.ExecuteReader();//返回一个数据流
            sdr2.Close();
            cmd2.Cancel();
            SqlCon2.Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            tab2_generatorFlow();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            tab7_generatorFlow();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            tab6_generatorFlow();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            tab3_generatorFlow();
        }

       /* private void button12_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                pictureBox12.Image = Image.FromFile(op.FileName);
            }
        }*/

        private void 商家_Load(object sender, EventArgs e)
        {

        }
    }
}
