using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;



namespace 抽奖
{
    
    public partial class Form1 : Form
    {
        
        public Form1()
        {
                       
            InitializeComponent();
           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.BackgroundImage = System.Drawing.Image.FromFile("back.jpg");//读取背景图片
            System.Windows.Forms.OpenFileDialog fg = new OpenFileDialog();//初始化文件选择方法
            MessageBox.Show("请选择抽奖文件");
            fg.ShowDialog();//调用文件选择窗口
            come.listname = fg.FileName;//把选择的文件名导入全局变量
            come.winners.AddRange( File.ReadAllLines(come.listname, System.Text.Encoding.Default));
            //读取LIST文件所有内容赋值给全局数组winners并关闭文件,编码方式为系统默认


        }

        private void button1_Click(object sender, EventArgs e)
        {
            come.j++;//全局变理加1
            Random random = new Random();
            if (come.listname=="")
            { 
                MessageBox.Show("请选择抽奖文件");
                System.Windows.Forms.OpenFileDialog fg = new OpenFileDialog();//初始化文件选择方法
                fg.ShowDialog();//调用文件选择窗口
                come.listname = fg.FileName;//把选择的文件名导入全局变量
                come.winners.AddRange(File.ReadAllLines(come.listname, System.Text.Encoding.Default));
                //读取LIST文件所有内容赋值给全局数组winners并关闭文件,编码方式为系统默认
                
            }
            

            if (come.j % 2 == 0 && come.listname != "")
            //当全局变量是偶数时执行下面操作

            {
                button1.Text = "停止";
                
                //遍历数组内容,并循环显示
                for (int i = 0; come.j % 2 == 0; )
                {   
                    
                    i = random.Next(come.winners.Count);
                    if (come.winners[i] == "已中奖")
                    {
                        continue;
                    }
                    
                    label1.Text = come.winners[i];
                    come.k = i;
                    Application.DoEvents();//另开进程.
                }

            }
            else
            {
                button1.Text = "滚动";
                using (StreamWriter w = File.AppendText(Application.StartupPath + "\\name.txt"))
                //定义方法W,此方法打开中奖名单.
                //下面是把向文件内写入内容.
                {
                    label2.Text = (come.j / 2).ToString();
                    w.Write("抽取的第");
                    w.Write(label2.Text);
                    w.Write("个 抽奖时间:");
                    w.WriteLine(DateTime.Now.ToString());
                    w.WriteLine( label1.Text);
                    come.winners[come.k] = "已中奖";
                }
                
            }
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", Application.StartupPath + "\\name.txt");
            //用记事本打开获奖名单
        }

        private void label4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", come.listname);//用记事本打开抽奖文件
            
           
        }
    }
           

}
       
        
       
    

