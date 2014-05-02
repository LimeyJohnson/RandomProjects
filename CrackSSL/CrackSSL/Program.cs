using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackSSL
{
    class Program
    {
        static void Main(string[] args)
        {
            double e = DoubleFromHexString("ffffffa95256a837568a41c265f4fe27110814aae19f144762d5cc0bcb931807");
            double d = DoubleFromHexString("c3c3c3817b3335577e69b9d0e48e2bc1fdf71f1f4f73a38a7d628d39739bbaf1");
            Console.Write(d);
            double n = 0x11;

        }
        public static double DoubleFromHexString(string hex)
        {
            double d = 0;

            for (int n = hex.Length - 1; n >= 0; n--)
            {
                d += System.Uri.FromHex(hex[n]) * Math.Pow(16, hex.Length - 1 - n);
            }
            return d;
        }
    }

}
