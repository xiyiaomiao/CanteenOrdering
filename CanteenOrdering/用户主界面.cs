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
    public partial class 用户主界面 : Form
    {
        public 用户主界面()
        {
            InitializeComponent();
            //  generatorPictureBox();
            generatorFlow();
        }

        public void generatorFlow()
        {
            int num = 4;
            FlowLayoutPanel[] flow;
            flow = new FlowLayoutPanel[num];
            PictureBox[] pict;
            pict = new PictureBox[num];
            Label[] lab;
            lab = new Label[num];

            for(int i = 0; i < num; i++)
            {
                pict[i] = new System.Windows.Forms.PictureBox();
                //    pict[i].Location = new Point(5, 10 + (i - 1) * 60);//设置图片位置  竖向排列
                pict[i].SizeMode = PictureBoxSizeMode.Zoom;
                pict[i].Image = Image.FromFile(@"C:\Users\木滋蔓\Pictures\test\" + (i + 1) + ".jpg");//导入图片
                pict[i].Size = new Size(200, 200);//设置图片大小
                pict[i].BorderStyle = BorderStyle.None;//取消边框
                pict[i].Image.Tag = i;
                lab[i] = new System.Windows.Forms.Label();
                lab[i].Text = "第"+i+"个";
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
        }

    }
}
