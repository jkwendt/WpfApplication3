﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public delegate void messageDelegate(String s);
    public partial class MainWindow : Window
    {
        public static IDictionary<string, Delegate> dict =
          new Dictionary<string, Delegate>();
        bool IsRemotePeerOpen;
        TcpClient client;
      //  IPAddress ip;
       // int port = 50000;
        bool init = true;
       //  TcpListener listener;
        //NetworkStream netStream;
        public MainWindow()
        {
           
           
            InitializeComponent();
            //System.Windows.MessageBox.Show(ip.ToString());
            /* TcpListener listener = new TcpListener(ip, port);
             listener.Start();
             while (true)
             {

                 tcpClient = listener.AcceptTcpClient();

                 //console write connection excepted
                 Console.WriteLine("connection accepted");
                 IsRemotePeerOpen = true;

                 Thread th = new Thread(netReaderMonitor);

                 th.Start();
                 Console.WriteLine("new thread started");
                 //console write new thread started

             }*/
        }
        /*public void AppendTextBox(string value)
        {
            if(InvokeRequired)
            {
                this.in
            }
        }*/

        private async void InitialSetup(object sender, EventArgs e)
        {

            //dict.Add("tBox", new messageDelegate();
            if (init)
            {
                init = false;
                ClientServerDialog dialogBox = new ClientServerDialog();
                Nullable<bool> dialogResult = dialogBox.ShowDialog();
                if (Application.Current.Properties["IsServer"].Equals(false))
                {
                    this.Title = "Client";
                    IPAddressDialog ipDialog = new IPAddressDialog();
                    Nullable<bool> ipDialogResult = ipDialog.ShowDialog();


                }
               await Task.Run(()=> netSetUp());
               
            }
            




          
           
                           

        }

        private void imageOnClick(object sender, RoutedEventArgs e)
        {
            // Pop up a file open dialog
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = //"All supported graphics|*.jpg;*.jpeg;*.png|" +
               // "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if(dlg.ShowDialog() == true)
            {
                String filetype = System.IO.Path.GetExtension(dlg.FileName);

                byte[] byteImg;
                BitmapImage img = new BitmapImage(new Uri(dlg.FileName));
                sentImage.Source = img;
                System.Windows.MessageBox.Show(filetype);
               /* if(filetype.Equals(".jpg") || filetype.Equals(".Jpeg"))
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(img));
                    using(MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);
                        byteImg = ms.ToArray();
                    }
                     System.Windows.MessageBox.Show(filetype);
                }*/
                //else if(filetype.Equals(".png") )
               // {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(img));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);
                        byteImg = ms.ToArray();
                    }
                    System.Windows.MessageBox.Show(filetype);
               // }
                 //   NetworkStream netStream = tcpClient.GetStream();  //        NetworkStream is a binary stream
                   // BinaryWriter netWriter = new BinaryWriter(netStream);

                    
                //    netWriter.Write("Pic");
                 //   netWriter.Write(byteImg.LongLength);
                //    netWriter.Write(byteImg);
                 //   netWriter.Flush();
                   
                
            }
            

        }
        // Thread function for monitoring the net reader for input
        // from the remote side
        private void netReaderMonitor(Object arg)
        {

           // AppendTextBox("");
            TcpClient tcpClient = arg as TcpClient;
            //TcpClient tcpClient = arg as TcpClient;
            NetworkStream netStream = tcpClient.GetStream();  //        NetworkStream is a binary stream
            BinaryReader netReader = new BinaryReader(netStream);
            //Application.Current.Dispatcher.Invoke(() => Application.Current.MainWindow.Activate());
            //MessageTextBox.Dispatcher.Invoke(new Update);
            //e.Cancel = true;
            while (IsRemotePeerOpen)
            {
                String netInput = null;
                try
                {
                    netInput = netReader.ReadString();
                }
                catch (IOException)
                {
                    // Remote peer has closed while we are waiting, just return
                    return;
                }

                switch (netInput)
                {
                    case "Chat":
                        ChatHistoryTextBox.Text += "Remote: " + netInput + "\n";


                        break;
                    case "Pic":


                        break;
                    case "Bye":
                        this.Dispatcher.Invoke(
                                                 () =>
                                                 {

                                                     MessageBox.Show("Remote peer has closed.");
                                                     IsRemotePeerOpen = false;
                                                     Application.Current.Shutdown(0);
                                                 }
                                             );
                        return;
                }

            }
        }
        private void sendMessage(object sender, RoutedEventArgs e)
        {
            ChatHistoryTextBox.Dispatcher.Invoke(() => ChatHistoryTextBox.AppendText("Me: " + MessageTextBox.Text + "\n"));

            //NetworkStream netStream = client.GetStream();  //        NetworkStream is a binary stream
          //  BinaryWriter netWriter = new BinaryWriter(netStream);

            
            //System.Windows.MessageBox.Show(MessageTextBox.Text);
           // netWriter.Write("Chat");
          //  netWriter.Write(MessageTextBox.Text);
         //   netWriter.Flush();
           MessageTextBox.Clear();

        }

        private async void netSetUp()
        {
            if (init == false)
            {
                //System.Windows.MessageBox.Show("in Net setUP");

              /*  if (Application.Current.Properties["IsServer"].Equals(false))
                {
                    ip = IPAddress.Parse(Application.Current.Properties["ServerIP"].ToString());
                    listener = new TcpListener(ip, port);
                    listener.Start();
                }
                else
                {
                    ip = IPAddress.Any;
                    listener = new TcpListener(ip, port);
                    listener.Start();
                }*/
             //   listener = new TcpListener(IPAddress.Any, port);
              //  listener.Start();
                int port = 50000;
                TcpListener listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                while (true)
                {
                
                    TcpClient tcpClient = listener.AcceptTcpClient();
                    client = tcpClient; 
                    //console write connection excepted
                    Console.WriteLine("connection accepted");
                    IsRemotePeerOpen = true;

                    Thread th = new Thread(netReaderMonitor);

                    th.Start();
                    Console.WriteLine("new thread started");
                    //console write new thread started

                }
            }
        }





    }
}
