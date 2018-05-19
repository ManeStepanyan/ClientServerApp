using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTCP
{
    class Program
    {

        static void Main(string[] args)
        {
            MathClientTCP client = new MathClientTCP();
            client.Connect("192.168.2.252", 3002);
            while (true)
            {

            }
        }
    }
}

