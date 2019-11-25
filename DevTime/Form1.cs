using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DevTime
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer;
        int hours, minutes, seconds;
        bool timerIsOff = true;
        string pathToSourceFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\DevTime.csv";


        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (timerIsOff)
            {
                timer.Start();
                btnStartStop.BackColor = Color.Red;
                btnStartStop.Text = "STOP";
                timerIsOff = false;
            }
            else
            {
                timer.Stop();
                btnStartStop.BackColor = Color.Lime;
                btnStartStop.Text = "RESUME";
                timerIsOff = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamWriter sw;

            if (!File.Exists(pathToSourceFile))
            {
                sw = File.CreateText(pathToSourceFile);
            }

            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += OnTimeEvent;
        }

        private void btnSaveTime_Click(object sender, EventArgs e)
        {
            var lineCount = File.ReadLines(pathToSourceFile).Count() + 1;

            using (StreamWriter sw = File.AppendText(pathToSourceFile))
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
                    seconds += 1;
                    if (seconds == 60)
                    {
                        seconds = 0;
                        minutes += 1;
                    }
                    if (minutes == 60)
                    {
                        minutes = 0;
                        hours += 1;
                    }
                    txtTime.Text = string.Format("{0},{1},{2}", hours.ToString().PadLeft(2, '0')
                        , minutes.ToString().PadLeft(2, '0'), seconds.ToString().PadLeft(2, '0'));
                }));
        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
