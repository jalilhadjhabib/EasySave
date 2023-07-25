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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //Button to send the message
        private void SEND_CLICK(object sender, RoutedEventArgs e)
        {
            string ipAdress = IP_BOX.Text;//IP address 
            int port = Convert.ToInt32(PORT_BOX.Text);//Port
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAdress), port);
                //Create the socket
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);//Connect the socket
                string message = MESSAGE_TEXTBOX.Text;//Save the message on the message variable
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);//Send the data
                MESSAGES.Text += DateTime.Now.ToShortTimeString() + ": " + message + "\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Client.BeginInit();
            }
        }
    }
}
