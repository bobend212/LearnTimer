using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DevTime
{
    public partial class Form1 : Form
    {
        System.Timers.Timer t;
        int h, m, s;
        bool timerIsOff = true;

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (timerIsOff)
            {
                t.Start();
                btnStartStop.BackColor = Color.Red;
                btnStartStop.Text = "STOP";
                timerIsOff = false;
            }
            else
            {
                t.Stop();
                btnStartStop.BackColor = Color.Lime;
                btnStartStop.Text = "RESUME";
                timerIsOff = true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;
        }

        private void btnSaveTime_Click(object sender, EventArgs e)
        {
            string path = @"M:\c# stuff\HowMuchICode.csv";
            var lineCount = File.ReadLines(path).Count() + 1;

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(lineCount + "," + DateTime.Now.ToString("MM/dd/yyyy") + "," + txtTime.Text.ToString());
            }

            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OnTimeEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
                {
                s += 1;
                if(s == 60)
                {
                    s = 0;
                    m += 1;
                }
                if(m == 60)
                {
                    m = 0;
                    h += 1;
                }
                txtTime.Text = string.Format("{0},{1},{2}", h.ToString().PadLeft(2, '0')
                    ,m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            }));
        }

        public Form1()
        {
            InitializeComponent();
        }

    }
}
