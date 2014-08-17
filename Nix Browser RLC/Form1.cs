using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using mshtml;
using System.Diagnostics;
using System.Timers;

namespace Nix_Browser_RLC
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer aTimer;
        private static System.Timers.Timer bTimer;
        private string basePath = Path.GetDirectoryName(Application.ExecutablePath);
        private WebBrowser screens;
        private Panel panelsw;
        private Boolean camtimer = false;
        private Boolean roomtimer = false;

        public Form1()
        {
           InitializeComponent();
           this.browserCreater();
        }

        public void TimerCamCreate()
        {
            int timerTo;
            timerTo = Convert.ToInt32(textBox3.Text);

            textBox1.Text = ("Cam Rotate started " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
            aTimer = new System.Timers.Timer(timerTo * 1000);
            aTimer.Elapsed += new ElapsedEventHandler(rotateCamz);
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void TimerCamDestroy()
        {
            textBox1.Text = ("Cam Rotate stopped " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
            aTimer.Enabled = false;
        }

        public void TimerRoomCreate()
        {
            int timerTo;
            timerTo = Convert.ToInt32(textBox2.Text);

            textBox1.Text = ("Room Rotate started " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
            bTimer = new System.Timers.Timer(timerTo * 1000);
            bTimer.Elapsed += new ElapsedEventHandler(rotataRoomz);
            bTimer.AutoReset = true;
            bTimer.Enabled = true;
        }

        public void TimerRoomDestroy()
        {
            textBox1.Text = ("Room Rotate stopped " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
            bTimer.Enabled = false;
        }

        public void browserCreater()
        {
            this.screens = new WebBrowser();
            this.panelsw = new Panel();
            this.screens.ScrollBarsEnabled = false;
            this.screens.Dock = DockStyle.Fill;
            this.myPanel1.Controls.Add(this.screens);

        }

        public void makeCam()
        {
            nakeIndex();
            this.screens.Navigate(this.basePath + "/index.html");
        }

        public void screenCap()
        {
            int width = this.screens.Width;
            int height = this.screens.Height;
            String path;
            path = this.basePath + "\\Screenshots\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                textBox1.Text = ("Click again");
            }
            else
            {
                string str = this.basePath + "\\Screenshots\\" + DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss") + ".png";
                using (Graphics graphics = this.screens.CreateGraphics())
                {
                    using (Bitmap bitmap = new Bitmap(width, height, graphics))
                    {
                        Rectangle targetBounds = new Rectangle(0, 0, width, height);
                        this.screens.DrawToBitmap(bitmap, targetBounds);
                        try
                        {
                            bitmap.Save(str);
                            Image image = Image.FromFile(str);
                            Image thumb = image.GetThumbnailImage(209, 106, () => false, IntPtr.Zero);
                            thumb.Save(str + ".png");
                            thumbPanel.BackgroundImage = thumb;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        textBox1.Text = ("Last saved " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
                    }
                }
            }
        }

        public void rotateCamz(object source, ElapsedEventArgs e)
        {
            String js2String;
            js2String = "javascript:rlc.Controller.rotateCameras(1);";
            this.screens.Navigate(js2String);
        }

        public void rotataRoomz(object source, ElapsedEventArgs e)
        {
            String js2String;
            js2String = "javascript:rlc.Controller.rotateApartments(1);";
            this.screens.Navigate(js2String);
        }

        public void nakeIndex()
        {
            string[] lines = { @"<HEAD>", "<TITLE>nix</TITLE>", "<meta http-equiv='refresh' content='0; url=http://reallifecam.com/' />", "</head>" };
            String ihtml;
            ihtml = this.basePath + @"\index.html";
            if (!File.Exists(ihtml))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(ihtml))
                {
                    foreach (string line in lines)
                    {
                        file.WriteLine(line);

                    }
                }

            }
        }

        public void resetCam(object sender, EventArgs e)
        {
            this.screens.DocumentText = "";
        }

        public void nixCam(object sender, EventArgs e)
        {
            this.screens.Navigate("javascript:function foo(){$('#mode_1').click();$('.controls').remove();$('.els').remove();$('._info').remove();$('#descr').remove();$('.panel').remove();$('.header').remove();$('#faq').remove();$('#apartments').toggle();$('.footer-dumb').remove();$('#mode_1').click();$('#player-inner').height($(window).height()).width($(window).width()).css('position','absolute').css('top','0').css('left','0');$('#player').height($(window).height()).width($(window).width()).css('position','absolute').css('top','0').css('left','0').css('margin','0px 0px 0px');$('#player-border').height($(window).height()).width($(window).width()).css('position','absolute').css('top','0').css('left','0');$('#player-holderview16').height($(window).height()).width($(window).width()).css('position','absolute').css('top','0').css('left','0');$('.layout').css('max-width','100%');$('.select_cameras').remove();$('.controls').remove();$('.select_apartments').remove()}foo();");
            System.Console.WriteLine(Convert.ToInt32(((Control)sender).Tag) - 1);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            makeCam();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.screens.Navigate("javascript:function foo(){$('#mode_1').click();$('.controls').remove();$('.els').remove();$('._info').remove();$('#descr').remove();$('.panel').remove();$('.header').remove();$('#faq').remove();$('#apartments').toggle();$('.footer-dumb').remove();$('#mode_1').click();$('#player-inner').height($(window).height()).width($(window).width()).css('position','absolute').css('top','0').css('left','0');$('#player').height($(window).height()).width($(window).width()).css('position','absolute').css('top','0').css('left','0').css('margin','0px 0px 0px');$('#player-border').height($(window).height()).width($(window).width()).css('position','absolute').css('top','0').css('left','0');$('#player-holderview16').height($(window).height()).width($(window).width()).css('position','absolute').css('top','0').css('left','0');$('.layout').css('max-width','100%');$('.select_cameras').remove();$('.controls').remove();$('.select_apartments').remove()}foo();");
            System.Console.WriteLine(Convert.ToInt32(((Control)sender).Tag) - 1);
        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            String js2String;
            js2String = "javascript:rlc.Controller.rotateApartments(1);";
            this.screens.Navigate(js2String);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String js2String;
            js2String = "javascript:rlc.Controller.rotateCameras(1);";
            this.screens.Navigate(js2String);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.screens.Navigate(this.basePath + "/index.html");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.screenCap();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.screens.Navigate(textBox4.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.screens.GoBack();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.screens.GoForward();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.screens.Refresh();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.screens.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.screens.Navigate("http://reallifecam.com");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 255");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            TimerCamCreate();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            TimerRoomCreate();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            TimerCamDestroy();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            TimerRoomDestroy();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void thumbPanel_Paint(object sender, PaintEventArgs e)
        {

        }     

    }
}
