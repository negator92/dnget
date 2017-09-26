using System;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace dnget
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadTask();
            Console.ReadKey();
        }

        async static Task DownloadTask()
        {
            Console.WriteLine("Enter source url: ");
            string remoteUri = Console.ReadLine();
            Console.WriteLine("Enter file.name: ");
            string fileName = Console.ReadLine();
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            // Concatenate the domain with the Web resource filename.
            //Console.WriteLine($"{remoteUri.Substring(remoteUri.LastIndexOf('/'), (remoteUri.Length-3 - remoteUri.LastIndexOf('/')))}{DateTime.Now.ToString()}{remoteUri.Substring(remoteUri.Length-4)}");
            Console.WriteLine($"\nDownloading File \"{fileName}\" to {Environment.CurrentDirectory}\nfrom \"{remoteUri}\"\n");
            // Download the Web resource and save it into the current filesystem folder.
            // myWebClient.DownloadFile(remoteUri, fileName);
            await myWebClient.DownloadFileTaskAsync(new Uri(remoteUri), fileName);
            Console.WriteLine($"\"{fileName}\" Successfully downloaded to {Environment.CurrentDirectory}");
        }
    }
    //class DownloadGamefile
    //{
    //    private volatile bool _completed;

    //    public void DownloadFile(string address, string location)
    //    {
    //        WebClient client = new WebClient();
    //        Uri Uri = new Uri(address);
    //        _completed = false;

    //        client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);

    //        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress);
    //        client.DownloadFileTaskAsync(Uri, location);

    //    }

    //    public bool DownloadCompleted { get { return _completed; } }

    //    private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
    //    {
    //        // Displays the operation identifier, and the transfer progress.
    //        Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
    //            (string)e.UserState,
    //            e.BytesReceived,
    //            e.TotalBytesToReceive,
    //            e.ProgressPercentage);
    //    }

    //    private void Completed(object sender, AsyncCompletedEventArgs e)
    //    {
    //        if (e.Cancelled == true)
    //        {
    //            Console.WriteLine("Download has been canceled.");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Download completed!");
    //        }

    //        _completed = true;
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Enter source url: ");
    //        string remoteUri = Console.ReadLine();
    //        Console.WriteLine("Enter file.name: ");
    //        string fileName = Console.ReadLine();
    //        DownloadGamefile DGF = new DownloadGamefile();

    //        DGF.DownloadFile(remoteUri, fileName);

    //        while (!DGF.DownloadCompleted)
    //            Thread.Sleep(1000);
    //    }
    //}
}