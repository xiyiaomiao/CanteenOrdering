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
    public partial class 商品点餐界面 : Form
    {
        String shop_name = "";
        public 商品点餐界面(String shop)
        {
            InitializeComponent();
            shop_name = shop;
            lab_ini();
        }

        public void lab_ini()
        {
            label1.Text = shop_name;
        }
    }
}
