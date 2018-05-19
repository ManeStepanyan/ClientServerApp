using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Connect(new IPEndPoint(IPAddress.Parse("192.168.2.252"), 3003));
            EndPoint endpoint = (EndPoint)new IPEndPoint(IPAddress.Parse("192.168.2.252"), 3003);
            string inputs = Console.ReadLine();
            while (inputs!="close")
            {       client.SendTo(Encoding.ASCII.GetBytes(inputs), endpoint);
                    byte[] buffer = new byte[100];
                    client.ReceiveFrom(buffer, ref endpoint);
                    Console.WriteLine("Result is" + " "+ Encoding.ASCII.GetString(buffer));                    
                    inputs = Console.ReadLine();
                } client.Close();
            }
        }
    }

