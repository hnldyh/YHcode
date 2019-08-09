using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace 图片滚动抽奖
{
    public partial class Form1 : Form
    {
        public int j = 1, k = 1, totel = 1, win0 = 1, win1 = 1, win2 = 1, win3 = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = System.Drawing.Image.FromFile("back.jpg");
            PHOTO.BackgroundImage = System.Drawing.Image.FromFile("./image/3.jpg");
            button1.Text = "开始";
            


    }
        
        private void button1_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();//声明XML方法
            doc.Load("list.xml");//加载XML文件
            totel = int.Parse(doc.GetElementsByTagName("totel").Item(0).InnerText);
            win0 = int.Parse(doc.GetElementsByTagName("win0").Item(0).InnerText);
            win1 = int.Parse(doc.GetElementsByTagName("win1").Item(0).InnerText);
            win2 = int.Parse(doc.GetElementsByTagName("win2").Item(0).InnerText);
            win3 = int.Parse(doc.GetElementsByTagName("win3").Item(0).InnerText);

            
            
            j++;//全局变理加1


            if (j % 2 == 0)
            //当全局变量是偶数时执行下面操作
            {
                button1.Text = "停止";
                for (int i = 1; j % 2 == 0; i++, k++)
                {
                    PHOTO.BackgroundImage = System.Drawing.Image.FromFile("./image/" + i.ToString() + ".jpg");
                    Thread.Sleep(50);
                    Application.DoEvents();
                    if (i == 5)
                        i = 0;
                    if (k == totel) k = 1;

                }

            }
            else
            {
                string a = "";
                button1.Text = "滚动";

                //中奖概率
                if (k <= totel - win0 - win1 - win2-win3)
                {
                    PHOTO.BackgroundImage = System.Drawing.Image.FromFile("./image/1.jpg");
                    a = "参与奖";
                }
                if (k <= totel - win0 - win1 - win2 && k > totel - win0 - win1 - win2-win3)
                {
                    PHOTO.BackgroundImage = System.Drawing.Image.FromFile("./image/2.jpg");
                    a = "三等奖";win3--;
                }

                if (k <= totel - win0 - win1 && k > totel - win0 - win1-win2)
                {
                    PHOTO.BackgroundImage = System.Drawing.Image.FromFile("./image/3.jpg");
                    a = "二等奖";win2--;
                }
                if (k<= totel - win0 && k >totel - win0-win1)
                {
                    PHOTO.BackgroundImage = System.Drawing.Image.FromFile("./image/4.jpg");
                    a = "一等奖";win1--;
                }
                if (k <=totel+1&&k>totel-win0)
                {
                    PHOTO.BackgroundImage = System.Drawing.Image.FromFile("./image/5.jpg");
                    a = "特等奖";win0--;
                }
                MessageBox.Show(a);
                using (StreamWriter w = File.AppendText(Application.StartupPath + "\\name.txt"))
                //定义方法W,此方法打开中奖名单.
                //下面是把向文件内写入内容.
                {
                    
                    w.Write(a);
                    w.WriteLine(DateTime.Now.ToString());
                    w.WriteLine(k.ToString());
                }

            }
            if (j == 100) j = 1;

            doc.GetElementsByTagName("totel").Item(0).InnerText = totel.ToString();//修改TOTEL的值
            doc.GetElementsByTagName("win0").Item(0).InnerText = win0.ToString();//修改win0的值
            doc.GetElementsByTagName("win1").Item(0).InnerText = win1.ToString();//修改win1的值
            doc.GetElementsByTagName("win2").Item(0).InnerText = win2.ToString();//修改win2的值
            doc.GetElementsByTagName("win3").Item(0).InnerText = win3.ToString();//修改win3的值

            doc.Save("list.xml");//把修改的XML结果保存
        }
      


    }
   
}

