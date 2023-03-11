using System;
using System.Net;
using System.Net.Security;

namespace TlsChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ServicePointManager.SecurityProtocol);
        }
    }
}
