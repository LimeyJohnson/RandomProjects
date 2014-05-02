using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DLLViewer
{
    public class Program
    {
        static void Main(string[] args)
        {
            encryption9.Encryption enc = new encryption9.Encryption();
            Console.Write(enc.Encrypt("CTF"));

        }
    }
}
