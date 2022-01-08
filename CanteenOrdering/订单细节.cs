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
    public partial class 订单细节 : Form
    {
        string ordernum;
        public 订单细节(String order_num)
        {
            ordernum = order_num;
            InitializeComponent();
            label_ini();
        }

        public void label_ini()
        {
            label2.Text = ordernum;
            SqlConnection SqlCon = login_database();
            String sql1 = "Select * from 订单,购买 where 订单.订单编号='" + ordernum + "' " +
                "and 订单.订单编号=购买.订单编号";


            SqlCommand cmd = new SqlCommand(sql1, SqlCon);
            int num = 0;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流

            //label_ini
            while (sdr.Read())
            {
                //取出数据流中的值
                label22.Text = sdr["下单时间"].ToString();
                label3.Text = sdr["店铺名称"].ToString();
                label10.Text = Convert.ToDecimal(sdr["总额"]).ToString("0.00");
                //Convert.ToDecimal(sdr["总额"]).ToString("0.00")
                label13.Text = sdr["配送状态"].ToString();
                label15.Text = sdr["配送时间"].ToString();
                label17.Text = sdr["地址名称"].ToString();
                num++;
            }
            if (label13.Text.Equals("已取餐")) {
                label19.Text = "订单已完成！";
            }
            else if (label13.Text.Equals("待取餐"))
            {
                label19.Text = "订单已送达，请尽快来领取！";
            }
            else if (label13.Text.Equals("已变质"))
            {
                label19.Text = "很遗憾，您的订单已变质，请勿食用！";
            }
            else
            {
                label19.Text = "订单还在路上，请稍候！";
            }
            cmd.Cancel();
            sdr.Close();

            //配送费
            String sql2 = "Select 配送费 from 店铺 where 店铺名称='" + label3.Text + "' " ;

            SqlCommand cmd2 = new SqlCommand(sql2, SqlCon);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader sdr2;
            sdr2 = cmd2.ExecuteReader();//返回一个数据流

            if (sdr2.Read())
            {
                label7.Text = Convert.ToDecimal(sdr2["配送费"]).ToString("0.00");
                //Convert.ToDecimal(sdr2["配送费"]).ToString("0.00")
            }
            cmd2.Cancel();
            sdr2.Close();

            

            //构建多个商品信息

            SqlDataAdapter myDataAdapter = new SqlDataAdapter(sql1, SqlCon);
            DataSet myDataSet = new DataSet();      // 创建DataSet
            myDataAdapter.Fill(myDataSet, "订单细节");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet.Tables["订单细节"];
            if (num > 0)
            {
                DataRow row = myTable.Rows[0];
            }
            int i = 0;

            this.listView1.Columns.Add("商品名称", 120, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("数量", 60, HorizontalAlignment.Center); //一步添加
            this.listView1.Columns.Add("总价", 80, HorizontalAlignment.Center); //一步添加


            while (num != 0)
            {
                ListViewItem lvi = new ListViewItem();

                // lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = myTable.Rows[i]["商品名称"].ToString();
                lvi.SubItems.Add(myTable.Rows[i]["购买数量"].ToString());
                lvi.SubItems.Add((Convert.ToDecimal(myTable.Rows[i]["购买商品价格"])
                    * int.Parse(myTable.Rows[i]["购买数量"].ToString())).ToString("0.00"));
                //Convert.ToDecimal(myTable.Rows[i]["购买商品价格"])
                //Convert.ToDecimal(sdr["总额"]).ToString("0.00")

                this.listView1.Items.Add(lvi);


                i++;
                num--;
            }

            //箱格编号
            if (label13.Text.Equals("待取餐"))
            {
                String sql3 = "Select * from 格子 where 订单编号='" + label2.Text + "' ";

                SqlCommand cmd3 = new SqlCommand(sql3, SqlCon);
                cmd3.CommandType = CommandType.Text;
                SqlDataReader sdr3;
                sdr3 = cmd3.ExecuteReader();//返回一个数据流

                if (sdr3.Read())
                {
                    label21.Text = sdr3["快递柜名称"].ToString() + sdr3["格子编号"].ToString();
                    //Convert.ToDecimal(sdr2["配送费"]).ToString("0.00")
                }
                cmd3.Cancel();
                sdr3.Close();

            }
            else
            {
                label20.Visible = false;
                label21.Visible = false;
            }

            SqlCon.Close();

        }

        private void button3_Click(object sender, EventArgs e)//联系商家
        {
            SqlConnection SqlCon = login_database();
            String sql2 = "Select 店铺电话 from 店铺 where 店铺名称='" + label3.Text + "' ";

            SqlCommand cmd2 = new SqlCommand(sql2, SqlCon);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader sdr2;
            sdr2 = cmd2.ExecuteReader();//返回一个数据流

            if (sdr2.Read())
            {
                MessageBox.Show("请致电商家：" + sdr2["店铺电话"].ToString());
            }
            cmd2.Cancel();
            sdr2.Close();
            SqlCon.Close();
        }

        private void button2_Click(object sender, EventArgs e)//联系骑手
        {
            SqlConnection SqlCon = login_database();
            String sql2 = "Select 快递员手机号 from 订单 where 订单编号='" + ordernum + "' ";

            SqlCommand cmd2 = new SqlCommand(sql2, SqlCon);
            cmd2.CommandType = CommandType.Text;
            SqlDataReader sdr2;
            sdr2 = cmd2.ExecuteReader();//返回一个数据流

            if (sdr2.Read())
            {
                if (!sdr2["快递员手机号"].ToString().Equals(""))
                {
                    MessageBox.Show("请致电快递员：" + sdr2["快递员手机号"].ToString());
                }
                else
                {
                    MessageBox.Show("很抱歉，您的订单当前状态为" + label13.Text 
                        + "\n还未分配快递员，请稍候");
                }
                
            }
            cmd2.Cancel();
            sdr2.Close();
            SqlCon.Close();
        }

        private void button1_Click(object sender, EventArgs e)//再来一单
        {

        }
        public SqlConnection login_database()
        {
            string ConnectionString = CommonDate.ConnectionString;
            SqlConnection SqlCon = new SqlConnection(ConnectionString);
            SqlCon.Open(); //打开数据库
            return SqlCon;

        }
    }
}
