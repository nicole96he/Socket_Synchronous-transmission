using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Client
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Socket clientt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress IP = IPAddress.Parse("127.0.0.1");
            IPEndPoint IPE = new IPEndPoint(IP, 4321);
            Console.WriteLine("Start to connect to server....");
            try
            {

                clientt.Connect(IPE);                                                                                    
                string first_string = "Let's begin";
                byte[] first = new byte[1024];
                first = Encoding.ASCII.GetBytes(first_string);
                clientt.Send(first);
                byte[] recc = new byte[1024];
                while (true)
                    {
                        //Console.WriteLine("receive successful.");
                        recc = new byte[1024];
                        int recc_len = clientt.Receive(recc);
                        string rec_string = Encoding.ASCII.GetString(recc, 0, recc_len);
                        Console.WriteLine("Server says:{0}", rec_string);
                        byte[] send_mes = Encoding.ASCII.GetBytes(Console.ReadLine());
                        clientt.Send(send_mes,send_mes.Length,0);
                    }              
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException:{0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException:{0}", e);

            }
        }
    }
}

