using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace dnget
{
    internal static class Program
    {
        private const int BufferSize = 256;
        
        private static void Main()
        {
            Console.WriteLine("Enter source url: ");
            string remoteUri = Console.ReadLine();
            Console.WriteLine("Enter file.name: ");
            string fileName = Console.ReadLine();
            Download(remoteUri, fileName).Wait();
        }

        private static async Task Download(string url, string fileName)
        {
            using (var client = new HttpClient()) {
                Console.WriteLine($"\nDownloading File \"{fileName}\" to {Environment.CurrentDirectory}\nfrom \"{url}\"\n");
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                using (var response = await client.GetAsync(url))
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var size = response.Content.Headers.ContentLength;                   
                    
                    var buffer = new byte[BufferSize];
                    var read = 0;
                    while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, read);
                        Console.Write($"\r{fileStream.Position} bytes read");
                        if (size.HasValue)
                        {
                            Console.Write($" of {size}");
                        }
                        
                        Thread.Sleep(1000);
                    }
                }
                
                Console.WriteLine($"\nDownload completed");
            }
        }

        private static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num) + suf[place];
        }
    }
}