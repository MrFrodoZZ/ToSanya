using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace February
{
    public partial class Form2 : Form
    {
        Button skip = new Button();
        string[] traits = { "1", "2", "3", "4" };

        
        public Form2()
        {
            InitializeComponent();

            skip.Location = new Point(570, 10);
            skip.AutoSize = true;
            skip.Text = "SKIP";
            skip.Click += new EventHandler(Evhend);
            skip.Visible = false;
            Controls.Add(skip);

            DataGridViewColumn[] columns = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn() { Name = "Step", HeaderText = "Шаг" },
                new DataGridViewTextBoxColumn() { Name = "Choice", HeaderText = "Выбор" }
            };
            dataGridView1.Columns.AddRange(columns);

            groupBox1.Text = "";
        }

        private System.Windows.Forms.Timer timer, skiptimer;
        private int currentIndex = 0;
        private string textToDisplay = "ASDLKFJAOSLJKDRSDSAHDKJASHDKJASHDKJASHDKJAHSDKJAHSKDJAHSJKDHAKJSDHKAJSHDKJASHDKJASHDKAJSHDKJAHSDKJASHDKJAHSKJDDSDSDDSDSDSDSDSDSDSDMNFOALIWMDFPOKSFDOPAKJISFG";

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentIndex < textToDisplay.Length)
            {
                richTextBox1.AppendText(textToDisplay[currentIndex].ToString());
                currentIndex++;
            }
            else
            {
                timer.Stop();
                skiptimer.Stop();
                showButtons(4, traits);
                skip.Visible = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer = new System.Windows.Forms.Timer();
            skiptimer = new System.Windows.Forms.Timer();
            timer.Interval = 50;
            skiptimer.Interval = 5000;
            timer.Tick += Timer_Tick;
            skiptimer.Tick += Skiptimer_Tick;

            timer.Start();
            skiptimer.Start();

            ReadFromFile();

            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.White;

            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void Skiptimer_Tick(object sender, EventArgs e)
        {
            skip.Visible = true;
        }

        private void Evhend(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "1":
                    richTextBox1.Text = "абоба";
                    groupBox1.Controls.Clear();
                    break;
                case "2":
                    richTextBox1.Text = "або";
                    groupBox1.Controls.Clear();
                    break;
                case "3":
                    richTextBox1.Text = "аб";
                    groupBox1.Controls.Clear();
                    break;
                case "4":
                    richTextBox1.Text = "а";
                    groupBox1.Controls.Clear();
                    break;
                case "SKIP":
                    timer.Stop();
                    skiptimer.Stop();
                    richTextBox1.Text = textToDisplay;
                    skip.Visible = false;
                    showButtons(4, traits);
                    break;
            }
        }

        public void showButtons(int n, string[] t)
        {
            Button btn1 = new Button();
            Button btn2 = new Button();
            Button btn3 = new Button();
            Button btn4 = new Button();

            btn1.AutoSize = true;
            btn2.AutoSize = true;
            btn3.AutoSize = true;
            btn4.AutoSize = true;

            btn1.Location = new Point(10, 10);
            btn2.Location = new Point(10, 50);
            btn3.Location = new Point(10, 90);
            btn4.Location = new Point(10, 130);

            btn1.Click += new EventHandler(Evhend);
            btn2.Click += new EventHandler(Evhend);
            btn3.Click += new EventHandler(Evhend);
            btn4.Click += new EventHandler(Evhend);

            btn1.Text = t[0];
            btn2.Text = t[1];
            btn3.Text = t[2];
            btn4.Text = t[3];
            switch (n)
            {
                case 2:
                    groupBox1.Controls.Add(btn1);
                    groupBox1.Controls.Add(btn2);
                    groupBox1.Controls.Remove(btn3);
                    groupBox1.Controls.Remove(btn4);
                    break;
                case 3:
                    groupBox1.Controls.Add(btn1);
                    groupBox1.Controls.Add(btn2);
                    groupBox1.Controls.Add(btn3);
                    groupBox1.Controls.Remove(btn4);
                    break;
                case 4:
                    groupBox1.Controls.Add(btn1);
                    groupBox1.Controls.Add(btn2);
                    groupBox1.Controls.Add(btn3);
                    groupBox1.Controls.Add(btn4);
                    break;
                default:
                    groupBox1.Controls.Clear();
                    break;
            }
        }

        private void WriteToFile(int s, string c)
        {
            using (StreamWriter sw = new StreamWriter("save.txt"))
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int step = Convert.ToInt32(dataGridView1[0, i].Value);
                    string choice = Convert.ToString(dataGridView1[1, i].Value);
                    sw.WriteLine(step + " " + choice);
                }
                sw.WriteLine(s + " " + c);
            }
        }

        private void ReadFromFile()
        {
            using (StreamReader sr = new StreamReader("save.txt"))
            {
                string data = sr.ReadLine();
                string[] payload;
                while (data != "EOF")
                {
                    payload = data.Split(' ');
                    dataGridView1.Rows.Add(payload);
                    data = sr.ReadLine();
                }
            }
        }
    }
}
