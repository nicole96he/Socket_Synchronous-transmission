using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class MainClass
    {
        public static void Main(string[] args)
        { 
            Socket spylisten = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress IP = IPAddress.Parse("127.0.0.1");
            IPEndPoint IPE = new IPEndPoint(IP, 4321);
            spylisten.Bind(IPE);
            spylisten.Listen(0);
            Console.WriteLine("Wait for connect...");
            Socket temp = spylisten.Accept();
            Console.WriteLine("Get connected");
            byte[] bytes = new byte[1024];
            while (true)
            {
                try
                {
                    //Console.WriteLine("receive successful.");
                    bytes = new byte[1024];
                    int recc_len = temp.Receive(bytes);
                    string rec = Encoding.ASCII.GetString(bytes, 0, recc_len);
                    Console.WriteLine("Client says: {0}",rec);
                    byte[] send_bytes = Encoding.ASCII.GetBytes(Console.ReadLine());
                    //Console.WriteLine("send successfully!");
                    temp.Send(send_bytes,send_bytes.Length,0);
                }
                catch(ArgumentNullException e)
                {
                    Console.WriteLine("ArgumentNullException:{0}", e);
                }
                catch(SocketException e)
                {
                    Console.WriteLine("SocketException:{0}", e);
                }
            }
        }
    }
}



