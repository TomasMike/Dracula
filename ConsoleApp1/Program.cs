using Dracula.Core;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

class MyTcpListener
{
    public static void Main()
    {
        CommServer s = new CommServer();
        //s.callEvent += ParseRequest();
        s.Start();

        


        Console.WriteLine("\nHit enter to continue...");
        Console.Read();
    }



   

    
}



public class TestObj
{
    public string ble;
    public bool bli;
}