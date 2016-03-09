using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Threading;




namespace Roboarm
{
    public partial class Form1 : Form
    {
        private Queue myq = new Queue();
        private string command = null;
        private SerialPort serial = new SerialPort("COM3", 9600);
        public bool arm_state = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Arduino.RunWorkerAsync();
            UdpServer.RunWorkerAsync();
            ProcessData.RunWorkerAsync();
        }

        private void UdpServer_DoWork(object sender, DoWorkEventArgs e)
        {
            UdpClient udpServer = new UdpClient(1000);
            writelog("UDP Start Receiving!");

            while (true)
            {
                try
                {
                    var RemoteEP = new IPEndPoint(IPAddress.Any, 1000);
                    var data = System.Text.Encoding.Default.GetString(udpServer.Receive(ref RemoteEP));
                    command = data;
                    
                }
                catch (Exception a)
                {
                    writelog(a.Message);
                }

            }
        }

        private void Arduino_DoWork(object sender, DoWorkEventArgs e)
        {
            arm_state = false;
            try
            {
                serial.Open();
                writelog(serial.ReadLine());
                Thread.Sleep(2000);

                writelog("Serial Found!! Arduino on " + serial.PortName + "!");
                writelog("Sending Servo data!");
                releaseArm(true);
                Thread.Sleep(2000);
                
               

                while (true)
                {
                    if (command != null)
                    {

                        writelog(command);
                        switch (command[0])
                        {
                            case '0':
                                resetArm();

                                break;
                            case '5':
                                myq.Enqueue(command);
                                break;
                            default:
                                serial.WriteLine("2 " + command[0] + " " + command.Remove(0, 2));
                                break;
                        }

                        command = null;
                    }
                }

            }
            catch (Exception n)
            {
                writelog(n.Message);
                arm_state = false;
                Arduino.CancelAsync();
            }
        }

        private void ProcessData_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (myq.Count != 0)
                {
                    string[] XY = myq.Dequeue().ToString().Remove(0, 2).Split(' ');
                    gerak2(Convert.ToDouble(XY[0]), 16 + Convert.ToDouble(XY[1]));
                }
                Thread.Sleep(50);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!Arduino.IsBusy)
            {
                Arduino.RunWorkerAsync();
            }
        }

        private void btnTgl_Click(object sender, EventArgs e)
        {
            if (arm_state)
            {
                releaseArm(false);
                arm_state = false;
            }
            else
            {
                releaseArm(true);
                arm_state = true;
            }
        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLog.Text = null;
        }

        public void gerak(double y, double z, double x)
        {
            double jari2 = pytagoras(y, z);
            //double sudut1 = cosinusLaw(25, 16, jari2);
            //double sudut2 = cosinusLaw(jari2, 16, 25);
            //double atan = cosinusLaw(y, z, false);
            //sudut1 += atan;

            double haha1 = ((256) + (jari2 * jari2) - (625)) / (2 * 16 * jari2);
            double haha2 = ((16 * 16) + (625) - (jari2 * jari2)) / (800);

            double sudut2 = Math.Acos(haha2) * (180 / Math.PI);

            double sudut1 = Math.Acos(haha1) * (180 / Math.PI);
            double hasbi = (y / z);
            double atan = Math.Atan(hasbi) * (180 / Math.PI);

            //writelog("Atan :" + atan);

            sudut1 += atan;
            

            try
            {
                sudut1 = 180 - sudut1;
                sudut2 = 180 - sudut2;
                //x = 180 - x;

                int u1 = Convert.ToInt32(sudut1);
                int u2 = Convert.ToInt32(sudut2);
                int u3 = Convert.ToInt32(x);

                serial.WriteLine("2 1 " + u1);
                serial.WriteLine("2 3 " + u1);
                serial.WriteLine("2 4 " + u2);
                serial.WriteLine("2 2 " + u3);
            }
            catch
            {

            }
        }

        public void gerak2(double x, double y)
        {
            double jari3 = Math.Sqrt(((x * x) + (25 * 25)));
            double atan = Math.Acos((25 / jari3));
            atan = atan * (180 / Math.PI);

            if (x < 0)
                atan *= -1;

            atan = atan + 90;

            writelog("x : " + x);
            writelog("atan : " + atan);





            gerak(y, jari3, atan);

/*
            double jari3 = pytagoras(x, 25);
            double atan = cosinusLaw(25, jari3, true);

            if (x < 0)
                atan *= -1;

            atan = atan + 90;

            writelog("x : " + x);
            writelog("atan : " + atan);

            gerak(y, jari3, atan);
 */

        }

        private double pytagoras(double a, double b)
        {
            return Math.Sqrt((a * a) + (b * b));
        }

        private double cosinusLaw(double a, double b, double c)
        {
            double data = ((a*a)-((b*b)+(c*c)))/(2*b*c);
            data = Math.Acos(data) * (180 / Math.PI);
            return data;
        }

        private double cosinusLaw(double a, double b, bool acos)
        {
            if (acos)
            {
                return Math.Acos(a / b) * (180 / Math.PI);
            }
            else
            {
                return Math.Atan(a / b) * (180 / Math.PI);
            }
        }

        private void resetArm()
        {
            serial.WriteLine("2 1 90 2 2 90 2 4 90 2 5 90");

            writelog("arm position reseted");
        }

        private void releaseArm(bool status)
        {
            if (!status)
            {
                serial.WriteLine("0 1 0 2 0 3 0 4 0 5");

                writelog("servo released");
            }
            else
            {
                serial.WriteLine("1 1 3 1 2 9 1 3 10 1 4 5 1 5 6");
                writelog("servo attached");

                resetArm();
            }
        }

        private void writelog(string text)
        {
            txtLog.AppendText(System.DateTime.Now + ": " + text + "\r\n");
        }

    }
}
