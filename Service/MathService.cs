using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{

    public interface IMathService
    {
        double Add(double firstValue, double secondValue);
        double Sub(double firstValue, double secondValue);
        double Div(double firstValue, double secondValue);
        double Mult(double firstValue, double secondValue);
    }
    public class MathService : IMathService
    {
        public double Add(double firstValue, double secondValue)
        {
            return firstValue + secondValue;
        }

        public double Div(double firstValue, double secondValue)
        {
            if (secondValue != 0)
                return firstValue / secondValue;
            return double.MaxValue;
        }

        public double Mult(double firstValue, double secondValue)
        {
            return firstValue * secondValue;
        }

        public double Sub(double firstValue, double secondValue)
        {
            return firstValue - secondValue;
        }
    }
}

