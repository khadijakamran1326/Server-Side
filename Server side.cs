﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
class Server
{
    static void Main()
    {
        var listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Server started on  port 5000");
        while (true)
        {
            new Thread(() => Handle(listener.AcceptTcpClient())).Start();
        }
        static void Handle(TcpClient Client)
        {
            using (Client)
            {
                for (int i = 1; i <= 10; i++)
                {
                    var ns = Client.GetStream();
                    var buf = new byte[1024];
                    int n = ns.Read(buf, 0, buf.Length);
                    string msg = Encoding.UTF8.GetString(buf, 0, n);
                    Console.WriteLine($"Client request:{msg}");
                    Console.Write("Respond");
                    msg = Console.ReadLine();
                    ns.Write(Encoding.UTF8.GetBytes(msg));
                }
            }
        }
    }
}