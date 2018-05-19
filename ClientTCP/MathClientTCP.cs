using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTCP
{
  public class MathClientTCP
    {
        private Socket socket;
        private byte[] buffer;
        public MathClientTCP()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect(string IP, int port)
        {
            socket.BeginConnect(new IPEndPoint(IPAddress.Parse(IP),port),ConnectCallBack, null);
        }
        public void ConnectCallBack(IAsyncResult result)
        {
            if (socket.Connected)
            {
                Console.WriteLine("Waiting for input");
                string operation = Console.ReadLine();
                socket.Send(Encoding.ASCII.GetBytes(operation));    
                buffer = new byte[100];
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallBack, null);
            }

        }
        public void ReceiveCallBack(IAsyncResult result)
        {
                int size = socket.EndReceive(result);
                byte[] temp = new byte[1024];
                Array.Copy(buffer, temp, size);
                // handle
                Console.WriteLine("Result is" + " " + Encoding.ASCII.GetString(buffer));
                buffer = new byte[1024];
                ConnectCallBack(result);
            }

        }
    }

