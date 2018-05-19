using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathServerTCP
{
    class Program
    {
        static ServerSocketTCP server = new ServerSocketTCP();
        static void Main(string[] args)
        {
            server.Bind("192.168.2.252", 3002);
            server.Listen(15);
            server.Accept();
            while (true)
            {

            } 
        }
    }
}
