using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using Controls;

namespace SocketTracker
{
       
    public class Scanner
    {
        public List<ScannResult> Results{get;set;}
        public string IpAddress{get;set;} = "127.0.0.1";
        public int MinPort{get;set;}
        public int MaxPort{get;set;}

        #region Constructor

        public Scanner(int maxport, int minport)
        {
            if(minport > maxport)
               throw new ArgumentException("MinPort can't be higher than MinPort");
            if(maxport > 65535)
               throw new ArgumentException("Port can't be higher than 65535");
            if(minport < 0)
               throw new ArgumentException("MinPort can't be lower than 0");

            MaxPort = maxport;
            MinPort = minport;

            Results = new List<ScannResult>();
        }
        
        public Scanner(int maxport, int minport, string ip)
        {
            if(minport > maxport)
               throw new ArgumentException("MinPort can't be higher than MinPort");
            if(maxport > 65535)
               throw new ArgumentException("Port can't be higher than 65535");
            if(minport < 0)
               throw new ArgumentException("MinPort can't be lower than 0");

            MaxPort = maxport;
            MinPort = minport;
            IpAddress = ip;

            Results = new List<ScannResult>();
        }

        public Scanner()
        {
            MaxPort = 65535;
            MinPort = 1;

            Results = new List<ScannResult>();
        }
        #endregion

        public async Task ScanWholeNetworkAsync()
        {
            for(int i = 1; i <= 255; i++)
            {
                IpAddress = "192.168." + i.ToString() + ".";

                for(int b = 1; b <= 255; b++)
                {
                    Console.Clear();
                    Console.WriteLine("This will take very, very long time...");
              
                    IpAddress += b.ToString();
                    Console.WriteLine("Scanning ip: {0}", IpAddress);

                    ProgressBar progress = new ProgressBar(MaxPort - MinPort);
                    progress.Draw();

                    for (int port = MinPort; port <= MaxPort; port++)
                    {
                        await CheckIsOpenAsync(port);
                        progress.Value = port - MinPort;
                    }

                    await AddResultsToFileAsync();
                }
            }
        }        

        public async Task ScanAsync()
        {
            ProgressBar progress = new ProgressBar(MaxPort - MinPort);
            progress.Draw();

            for (int port = MinPort; port <= MaxPort; port++)
            {
                await CheckIsOpenAsync(port);
                progress.Value = port - MinPort;
            }

            Console.WriteLine("Done!");

            ShowResults();
            await AddResultsToFileAsync();
        }

        private async Task CheckIsOpenAsync(int port)
        {
            if(await TryConnectAsync(port))
                Results.Add(new ScannResult{Port = port, IsOpen = true});
            else
                Results.Add(new ScannResult{Port = port, IsOpen = false});
            

        }
        private async Task<bool> TryConnectAsync(int port)
        {
            TcpClient client = new TcpClient();

            try
            {
                await client.ConnectAsync(IPAddress.Parse(IpAddress), port);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ShowResults()
        {
            Console.WriteLine("Click enter to see Open ports");
            while(true)
            {
                if(Console.ReadLine() == "")
                  break;
            }

            Console.WriteLine("\nOpen ports:");
            Console.WriteLine("------------------------");

            foreach(ScannResult details in Results)
            {
                if(details.IsOpen == true)
                   Console.WriteLine(details.Port + "   " + details.IsOpen);
            }



            Console.WriteLine("Click enter to see closed ports, or no to cancel");
            while(true)
            {
                if(Console.ReadLine() == "")
                  break;
            }

            Console.WriteLine("\nClosed ports:");
            Console.WriteLine("------------------------");

            foreach(ScannResult details in Results)
            {
                if(details.IsOpen == false)
                   Console.WriteLine(details.Port + "   " + details.IsOpen);
            }
        }

        private async Task AddResultsToFileAsync()
        {
            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);   
            string mainFolder = Path.Combine(myDocuments, "SocketTracker");
            //string folderDate = Path.Combine(mainFolder, DateTime.Today.Date.ToString("MM/dd/yyyy").Replace('/', ' '));
            string file = Path.Combine(mainFolder, IpAddress);

            if(Directory.Exists(mainFolder) != true)
                Directory.CreateDirectory(mainFolder);

            StreamWriter writer = new StreamWriter(file);

            foreach(ScannResult result in Results)
                await writer.WriteLineAsync(result.Port + "     " + result.IsOpen);
            
            
            await writer.FlushAsync();
        }
    }
}