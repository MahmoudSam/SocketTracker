using System;
using Controls;

namespace SocketTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear(); 

            while(true)
            {
                //Console.WriteLine("Using SimpleControls V0.1(by JkFrcss)\n");

                Console.WriteLine("1)Scan ports(127.0.0.1) \n2)Scan whole network \n3)Scan specific ip \n4)Spam(HTTP) \n5)Spam(Socket)");

                int option;
                int.TryParse(Console.ReadLine(), out option);
                
                switch(option)
                {
                    case 1:
                    {
                        int max;
                        int min;

                        Console.WriteLine("Enter maximum port number:");
                        int.TryParse(Console.ReadLine(), out max);
                        Console.WriteLine("Enter minimum port number:");
                        int.TryParse(Console.ReadLine(), out min);


                        Scanner scanner = new Scanner(max, min);
                        scanner.ScanAsync().Wait();

                        break;
                    }
                    case 2:
                    {
                        Console.WriteLine("You sure, this will take very long time!");

                        while(true)
                        {
                            Console.WriteLine("Type y to continue...");
                            if(Console.ReadLine() == "y")
                            {
                                break;
                            }
                        }

                        Scanner scanner = new Scanner();

                        scanner.ScanWholeNetworkAsync().Wait();
                        
                        break;
                    }
                    case 3:
                    {
                        string ip;
                        int max;
                        int min;

                        Console.WriteLine("Enter Ip address");
                        ip = Console.ReadLine();
                        Console.WriteLine("Enter maximum port number:");
                        int.TryParse(Console.ReadLine(), out max);
                        Console.WriteLine("Enter minimum port number:");
                        int.TryParse(Console.ReadLine(), out min);


                        Scanner scanner = new Scanner(max, min, ip);
                        scanner.ScanAsync().Wait();


                        break;
                    }
                    case 4:
                    {
                        string address;
                        long loop;
                        Console.WriteLine("Enter address:");
                        address = Console.ReadLine();
                        Console.WriteLine("Enter number of loops(1 loop = 999999999 new web clients)");
                        long.TryParse(Console.ReadLine(), out loop);


                        Spammer.Http attack = new Spammer.Http(address, loop);
                        attack.StartSpammingAsync().Wait();

                        break;
                    }
                    case 5:
                    {
                        
                        string ip;
                        int port;
                        long loop;

                        Console.WriteLine("Enter Ip address");
                        ip = Console.ReadLine();
                        Console.WriteLine("Enter port");
                        port = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter number of loops(1 loop = 999999999 new tcp clients)");
                        long.TryParse(Console.ReadLine(), out loop);

                        Spammer.Sockets attack = new Spammer.Sockets(ip, port, loop);
                        attack.StartSpammingAsync().Wait();

                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Wrong input");
                        break;
                    }
                }
            }
        }
    }
}
