using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathServerTCP
{
    public class ServerSocketTCP
    {
        private Socket serverSocket;
        MathService instance;
        byte[] buffer = new byte[1024];
        public ServerSocketTCP()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            instance = new MathService();
        }
        public void Bind(String IP, int port)
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(IP), port));
        }
        public void Listen(int backlog)
        {
            serverSocket.Listen(15);
        }
        public void Accept()
        {
            serverSocket.BeginAccept(AcceptCallBack, serverSocket);
        }
        public void AcceptCallBack(IAsyncResult result)
        {  // serverSocket = result.AsyncState as Socket;
            Console.WriteLine("Connected to a client");
            Socket clientSocket = serverSocket.EndAccept(result);
            if (clientSocket != null)
            {
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallBack, clientSocket);
                Accept();
            }
        }
        public void ReceiveCallBack(IAsyncResult result)
        {
            Socket clientSocket = result.AsyncState as Socket;
            int size = clientSocket.EndReceive(result);
            byte[] temp = new byte[size];
            Array.Copy(buffer, temp, size);
            string line = "";
            // handle the temp
            line += Encoding.ASCII.GetString(temp);
            Console.WriteLine("Received: {0}", line);
            double arithmeticResult = parseProtocol(line);
            if (arithmeticResult == double.MaxValue) { clientSocket.Send(Encoding.ASCII.GetBytes("Cannot divide by zero")); }
            else
            {
                clientSocket.Send(Encoding.ASCII.GetBytes(arithmeticResult.ToString()));
            }
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallBack, clientSocket);

        }
        public double parseProtocol(string line)
        {
            double firstValue = double.MaxValue;
            double secondValue = double.MaxValue;
            double result = double.MaxValue;
            string[] values = line.Split(':');
            if (values.Length != 3)
            {
                throw new Exception("No such a protocol exists");

            }
            try
            {
                firstValue = Convert.ToDouble(values[1]);
                secondValue = Convert.ToDouble(values[2]);
            }
            catch (Exception)
            {
                throw new ArgumentException("Wrong arguments!!");
            }
            switch (values[0])
            {
                case "+":
                    result = instance.Add(firstValue, secondValue);
                    break;
                case "-":
                    result = instance.Sub(firstValue, secondValue);
                    break;
                case "/":
                    result = instance.Div(firstValue, secondValue);
                    break;
                case "*":
                    result = instance.Mult(firstValue, secondValue);
                    break;
                default:
                    throw new ArgumentException("No such an operation");
            }
            return result;
        }

    }
}
