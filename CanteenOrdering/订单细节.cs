using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
