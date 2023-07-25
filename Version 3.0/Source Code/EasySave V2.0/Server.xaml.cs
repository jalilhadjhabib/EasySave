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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasySave_V2._0
{
    /// <summary>
    /// Logique d'interaction pour Socket.xaml
    /// </summary>
    public partial class Server : Page
    {
        public Server()
        {
            InitializeComponent();
        }
        private async void EVENT_LOAD(object sender, RoutedEventArgs e)
        {
        linkload:
#pragma warning disable CS0618 
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
#pragma warning restore CS0618 
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipPoint = new IPEndPoint(ipAddress, 8888);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Bind(ipPoint);

                socket.Listen(10);
                IP4CONNECT_LABEL.Content = "Server started on address: " + ipAddress.ToString() + ":8888";
                await Task.Run(async () =>
                {
                    while (true)
                    {
                        Socket handler = socket.Accept();
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        byte[] data = new byte[256];

                        do
                        {
                            bytes = handler.Receive(data);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (handler.Available > 0);
                        Action action = () =>
                        {
                            MESSAGES.Text += DateTime.Now.ToShortTimeString() + ": " + builder.ToString() + "\n";
                        };
                        await MESSAGES.Dispatcher.BeginInvoke(action);
                        string message = "Your message sended";
                        data = Encoding.Unicode.GetBytes(message);
                        handler.Send(data);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SocketServer.BeginInit();
            }
        }


    }
}
