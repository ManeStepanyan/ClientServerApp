using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerUDP
{
    class Program
    {
        private static MathService instance = new MathService();
        public static double parseProtocol(string line)
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

        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public static void Main()
        {
            byte[] buffer = new byte[100];
            int recieved = 0;
            socket.Bind(new IPEndPoint(IPAddress.Parse("192.168.2.252"), 3003));
            Console.WriteLine("Waiting for a client");
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 3003);
            EndPoint endPoint = (EndPoint)sender;
            do
            {
                recieved = socket.ReceiveFrom(buffer, ref endPoint);
                Console.WriteLine("received");
                string line = Encoding.ASCII.GetString(buffer);
                Console.WriteLine(line);
                double result = parseProtocol(line);
                socket.SendTo(Encoding.ASCII.GetBytes(result.ToString()), endPoint);
            } while (recieved > 0);
            socket.Close();
        }
    }
}
