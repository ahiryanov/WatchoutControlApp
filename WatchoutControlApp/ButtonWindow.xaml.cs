using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace WatchoutControlApp
{
    /// <summary>
    /// Логика взаимодействия для ButtonWindow.xaml
    /// </summary>
    public partial class ButtonWindow : Window
    {
        Socket socket;
        bool touch = false;
        public ButtonWindow()
        {
            
            InitializeComponent();
            //media.Play();
            try
            {
                IPAddress ipAddr = IPAddress.Parse(Properties.Settings.Default.ipaddress);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 3040);
                socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
                return;
            }
                
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!touch)
            {
                var controller = ImageBehavior.GetAnimationController(backgroung);
                controller.Play();
                controller.CurrentFrameChanged += Controller_CurrentFrameChanged;
                byte[] msg = Encoding.UTF8.GetBytes("run \"" + Properties.Settings.Default.timeline + "\"\n");
                try
                {
                    int bytesSent = socket.Send(msg);
                }
                catch
                { }
                touch = true;
            }
        }

        private void button_TouchDown(object sender, TouchEventArgs e)
        {
            if (!touch)
            {
                var controller = ImageBehavior.GetAnimationController(backgroung);
                controller.Play();
                controller.CurrentFrameChanged += Controller_CurrentFrameChanged;
                byte[] msg = Encoding.UTF8.GetBytes("run \"" + Properties.Settings.Default.timeline + "\"\n");
                try
                {
                    int bytesSent = socket.Send(msg);
                }
                catch
                { }
                touch = true;
            }
        }

        private void Controller_CurrentFrameChanged(object sender, EventArgs e)
        {
            var controller = sender as ImageAnimationController;
            if (controller.CurrentFrame == controller.FrameCount - 1)
                controller.Pause();
        }
    }
}
