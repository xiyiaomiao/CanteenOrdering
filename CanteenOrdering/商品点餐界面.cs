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
    public partial class 商品点餐界面 : Form
    {
        FlowLayoutPanel[] flow;
        PictureBox[] pict;
        Label[] lab;
        Button[] but;
        int id = 0;
        String shop_name = "";
        String user = "";
        String old_order = "";
        public 商品点餐界面(String shop,String user_id,String old)
        {
            InitializeComponent();
            shop_name = shop;
            user = user_id;
            old_order = old;
            tab1_ini();
            tab2_ini();
            tab3_ini();
            if (!old_order.Equals(""))
            {
                order_again();
            }
        }

        public void order_again()
        {
            SqlConnection SqlCon = login_database();
            string sql = "Select * from 购买 where 订单编号='" + old_order + "'";
            SqlCommand cmd = new SqlCommand(sql, SqlCon);
            int num = 0;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流

            

            while (sdr.Read())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = sdr["商品名称"].ToString();
                lvi.SubItems.Add(Convert.ToDecimal(sdr["购买商品价格"]).ToString("0.00"));
                lvi.SubItems.Add(sdr["购买数量"].ToString());
                lvi.SubItems.Add("删除");
                lvi.Tag = num.ToString();
                this.listView1.Items.Add(lvi);
                num++;
            }


            cmd.Cancel();
            sdr.Close();
            SqlCon.Close();

            int nRow = 0;
            ListViewItem select1_lvi = new ListViewItem();
            decimal money = Convert.ToDecimal(label11.Text.Replace("¥", ""));
            for (nRow = 0; nRow < listView1.Items.Count; nRow++)
            {
                select1_lvi = listView1.Items[nRow];
                money += Convert.ToDecimal(select1_lvi.SubItems[1].Text) * int.Parse(select1_lvi.SubItems[2].Text);
            }
            label15.Text = money.ToString("0.00");

        }
        public void tab1_ini()//商品列表
        {
            SqlConnection SqlCon = login_database();
            string sql = "Select * from 商品 " +
                "where 店铺名称='" + shop_name + "'" +
                "and 状态='有'";//查找用户sql语句
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
            myDataAdapter.Fill(myDataSet, "商品");	// 将返回的数据集作为“表”填入DataSet中，表名可以与数据库真实的表名不同，并不影响后续的增、删、改等操作
            DataTable myTable = myDataSet.Tables["商品"];
            if (myTable.Rows.Count == 0)
            {
                return;
            }
            DataRow row = myTable.Rows[0];


            lab = new Label[num];
            flow = new FlowLayoutPanel[num];
            pict = new PictureBox[num];
            but = new Button[num];

            for (int i = 0; i < num; i++)
            {
                pict[i] = new System.Windows.Forms.PictureBox();
                pict[i].SizeMode = PictureBoxSizeMode.Zoom;

                pict[i].Image = System.Drawing.Image.FromFile(@"..\\..\\Resources\\" + myTable.Rows[i]["商品名称"].ToString() + ".png");

                pict[i].Size = new Size(200, 100);//设置图片大小
                pict[i].BorderStyle = BorderStyle.None;//取消边框
                pict[i].Image.Tag = i;

                lab[i] = new System.Windows.Forms.Label();
                lab[i].Text = "    " + myTable.Rows[i]["商品名称"].ToString();
                lab[i].Visible = true;
                lab[i].AutoSize = true;
                lab[i].Font = new System.Drawing.Font("宋体", 20F,
                    System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                but[i] = new System.Windows.Forms.Button();
                but[i].Size = new Size(150, 30);
                but[i].Cursor = Cursors.Hand;
                but[i].Text = "加入购物车";
                but[i].Margin = new Padding(25,5,5,5);
                but[i].BackColor = Color.FromArgb(210, 115, 115);
                but[i].Font = new System.Drawing.Font("宋体", 15F,
                    System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));


                flow[i] = new System.Windows.Forms.FlowLayoutPanel();
                flow[i].FlowDirection = FlowDirection.TopDown;
                flow[i].BorderStyle = BorderStyle.FixedSingle;
                flow[i].WrapContents = false;
                flow[i].AutoScroll = true;
                flow[i].AutoSize = true;
                flow[i].Controls.Add(pict[i]);
                flow[i].Controls.Add(lab[i]);
                flow[i].Controls.Add(but[i]);
                flow[i].Visible = true;


                but[i].Name = "but" + i.ToString();
                but[i].Click += new System.EventHandler(buti_Click);

                pict[i].Name = "pict" + i.ToString();
            //    pict[i].Click += new System.EventHandler(picti_Click);

                lab[i].Name = "lab" + i.ToString();
             //   lab[i].Click += new System.EventHandler(labi_Click);


                flow[i].Name = "flow" + i.ToString();
             //   flow[i].Click += new System.EventHandler(flowi_Click);
                flowLayoutPanel1.Controls.Add(flow[i]);
            }




            SqlCon.Close();
        }

        private void buti_Click(object sender, EventArgs e)
        {

            var index = (sender as Button).Name.Replace("but", "");
            id = int.Parse(index);
            String select_good = lab[id].Text.Replace(" ", "");
            if (MessageBox.Show("是否将" + select_good + "加入购物车？", "提示",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                //填入购物车页面的listview，之后可以再读取加入订单表和细节表

                //添加数据项
                this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

                //查找购物车中有无这样项
                ListViewItem select_lvi = new ListViewItem();
                int nRow;
                for (nRow = 0; nRow < listView1.Items.Count; nRow++)
                {
                    select_lvi = listView1.Items[nRow];
                    if(select_lvi.Text.Equals(select_good))
                    {
                        //取数量加一
                        select_lvi.SubItems[2].Text = (int.Parse(select_lvi.SubItems[2].Text) + 1).ToString();
                        break;
                    }
                }
                if (nRow >= listView1.Items.Count)//说明遍历后没有这一项
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = select_good;
                    //查找商品表
                    SqlConnection SqlCon = login_database();
                    string sql = "Select * from 商品 " +
                        "where 店铺名称='" + shop_name + "'" +
                        "and 商品名称='"+select_good+"'";
                    SqlCommand cmd = new SqlCommand(sql, SqlCon);
                    
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader sdr;
                    sdr = cmd.ExecuteReader();//返回一个数据流
                    while (sdr.Read())
                    {
                        lvi.SubItems.Add(Convert.ToDecimal(sdr["价格"]).ToString("0.00"));
                        lvi.SubItems.Add("1");
                        lvi.SubItems.Add("删除");
                    }
                    lvi.Tag = listView1.Items.Count.ToString();
                    this.listView1.Items.Add(lvi);
                    cmd.Cancel();
                    sdr.Close();
                    SqlCon.Close();

                    

                }
                
               
                this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。

                MessageBox.Show("已成功加入购物车");

                decimal money = Convert.ToDecimal(label11.Text.Replace("¥", ""));
                for (nRow = 0; nRow < listView1.Items.Count; nRow++)
                {
                    select_lvi = listView1.Items[nRow];
                    money += Convert.ToDecimal(select_lvi.SubItems[1].Text)* int.Parse(select_lvi.SubItems[2].Text);
                }
                label15.Text = money.ToString("0.00");

            }

            
        }







        public void tab2_ini()//购物车
        {
            
        }

        public void tab3_ini()
        {
            label1.Text = shop_name;
            

            SqlConnection SqlCon = login_database();
            string sql = "Select * from 店铺 " +
                "where 店铺名称='"+shop_name+"'";//查找用户sql语句
            SqlCommand cmd = new SqlCommand(sql, SqlCon);
            int num = 0;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流


            while (sdr.Read())
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.BorderStyle = BorderStyle.None;
                pictureBox1.Image= System.Drawing.Image.FromFile(@"..\\..\\Resources\\" + shop_name + ".jpg");
                label3.Text = sdr["老板"].ToString();
                label5.Text = sdr["店铺电话"].ToString();
                label7.Text = sdr["店铺地址"].ToString();
                label9.Text = sdr["营业时间"].ToString();
                label11.Text = "¥"+ Convert.ToDecimal(sdr["配送费"]).ToString("0.00");
                //Convert.ToDecimal(sdr["配送费"]).ToString("0.00");
                num++;
            }
            label15.Text = label11.Text.Replace("¥", "");
            cmd.Cancel();
            sdr.Close();
            SqlCon.Close();
        }


        public SqlConnection login_database()
        {
            string ConnectionString = CommonDate.ConnectionString;
            SqlConnection SqlCon = new SqlConnection(ConnectionString);
            SqlCon.Open(); //打开数据库
            return SqlCon;

        }

        private void button2_Click(object sender, EventArgs e)//清空购物车
        {
            this.listView1.Items.Clear();  //只移除所有的项
            label15.Text = label11.Text.Replace("¥", "");
        }

        private void button1_Click(object sender, EventArgs e)//提交
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("您尚未选择购买商品，请先加入购物车！");
                return;
            }else if (comboBox1.Text.Equals(""))
            {
                MessageBox.Show("请选择送达时间！");
                return;
            }
            //生成订单编号
            SqlConnection SqlCon = login_database();
            String order_id = "";
            //找出最后一个最大的订单编号
            String sql = "select 订单编号 from 订单 order by 订单编号 DESC";
            SqlCommand cmd = new SqlCommand(sql, SqlCon);
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();//返回一个数据流
            if (sdr.Read())//第一个取到的就是最大值
            {
                order_id = (int.Parse(sdr["订单编号"].ToString()) + 1).ToString().PadLeft(10, '0');
            }
            sdr.Close();
            cmd.Cancel();

            //地址
            String sql1 = "select * from 用户 where 用户手机号='" + user + "'";
            SqlCommand cmd1 = new SqlCommand(sql1, SqlCon);
            cmd1.CommandType = CommandType.Text;
            SqlDataReader sdr1;
            sdr1 = cmd1.ExecuteReader();//返回一个数据流
            String addr = "";
            while (sdr1.Read())
            {
                addr = sdr1["地址名称"].ToString();
            }
            sdr1.Close();
            cmd1.Cancel();
            


            //提交订单表
            String sql2 = "insert into 订单(订单编号,用户手机号,配送状态," +
                "下单时间,配送时间,店铺名称,总额,地址名称)" +
                "values('"+order_id+"','"+user+"','待配货'," +
                "'"+DateTime.Now+"','"+comboBox1.Text+"','"+shop_name+"','"+label15.Text+"','"+addr+"')";
            SqlCommand cmd2 = new SqlCommand(sql2, SqlCon);
            cmd2.CommandType = CommandType.Text;

            if (cmd2.ExecuteNonQuery() != 0)
            {
             //   MessageBox.Show("插入订单表成功！");
                
            }
            //提交购买表
            int nRow = 0;
            ListViewItem select_lvi = new ListViewItem();
            for (nRow = 0; nRow < listView1.Items.Count; nRow++)
            {
                select_lvi = listView1.Items[nRow];
                String sql3 = "insert into 购买 values('"+order_id+"','"+select_lvi.Text+"'," +
                    "'"+select_lvi.SubItems[2].Text+"','"+select_lvi.SubItems[1].Text+"')";
                SqlCommand cmd3 = new SqlCommand(sql3, SqlCon);
                cmd3.CommandType = CommandType.Text;

                if (cmd3.ExecuteNonQuery() != 0)
                {
                 //   MessageBox.Show("插入购买成功！");

                }

            }
            

            //提交后清空
            this.listView1.Items.Clear();
            comboBox1.Text = "";
            label15.Text = label11.Text;
            SqlCon.Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0 || this.listView1.SelectedItems.Count > 1)
            {
                return;
            }

            string path = this.listView1.SelectedItems[0].Tag.ToString();
         //   MessageBox.Show(path);
            ListViewItem select_lv = listView1.Items[int.Parse(path)];
            int nRow = 0;
            
            if (int.Parse(select_lv.SubItems[2].Text) > 1)
            {
                select_lv.SubItems[2].Text = (int.Parse(select_lv.SubItems[2].Text) - 1).ToString();
            }
            else
            {
                select_lv.Remove();
                //重新修改tag
                
                ListViewItem select_lvi = new ListViewItem();
                for (nRow = 0; nRow < listView1.Items.Count; nRow++)
                {
                    select_lvi = listView1.Items[nRow];
                    select_lvi.Tag = nRow.ToString();
                }
            }
            

            ListViewItem select1_lvi = new ListViewItem();
            decimal money = Convert.ToDecimal(label11.Text.Replace("¥", ""));
            for (nRow = 0; nRow < listView1.Items.Count; nRow++)
            {
                select1_lvi = listView1.Items[nRow];
                money += Convert.ToDecimal(select1_lvi.SubItems[1].Text) * int.Parse(select1_lvi.SubItems[2].Text);
            }
            label15.Text = money.ToString("0.00");

        }
    }
}
