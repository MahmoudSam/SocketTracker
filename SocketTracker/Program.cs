using System;
using System.IO;
using System.Threading.Tasks;
using Controls;
using System.Collections.Generic;

namespace SocketTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear(); 

            ListBox menu = new ListBox(0, 0, "SocketTracker, made by JkFrcss", Console.BackgroundColor, ConsoleColor.Blue);
            menu.Source = new List<string>{"Scan ports(127.0.0.1)", "Scan whole network", "Scan specific ip", "Spam(HTTP)", "Spam(Socket)" ,"Changelog"};
            menu.NumericPosition = true;
            menu.SeletionChanged += SelectionChanged;
            menu.Draw();
            menu.Activate();
        }

        static void SelectionChanged(object sender, EventArgs e)
        {
            ListBox s = (ListBox)sender;
            s.DeActivate();
            ParseSelection(s.Index);
            Main(null);
        }

        public static string Text{get;} = "1)Scan ports(127.0.0.1) \n2)Scan whole network \n3)Scan specific ip \n4)Spam(HTTP) \n5)Spam(Socket) \n6)Changelog\n\nInfo:\nSocketTracker V1.2\nUsing SimpleControls V 0.2(JkFrcss)";

        private static void ParseSelection(int num)
        {
            //Need to rewrite this...

            Console.SetCursorPosition(0, 9);

            switch(num)
            {
                case 0:
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
                case 1:
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
                case 2:
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
                case 3:
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
                case 4:
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
                case 5: 
                {
                    Box box = new Box(0, 0, Console.WindowWidth, Console.WindowHeight);
                    box.Text = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "ChangelogAndAbout.txt"));
                    box.Draw();

                    while(true)
                    {
                        if(Console.ReadLine() == "")
                        break;
                    }

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
