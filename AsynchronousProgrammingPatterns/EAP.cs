using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Threading.Thread;
using static System.Console;

namespace AsynchronousProgrammingPatterns
{
    /// <summary>
    /// A class that supports the Event-based Asynchronous Pattern will have 
    /// one or more methods named MethodNameAsync. 
    /// These methods may mirror synchronous versions, 
    /// which perform the same operation on the current thread. 
    /// The class may also have a MethodNameCompleted event and 
    /// it may have a MethodNameAsyncCancel (or simply CancelAsync) method.
    /// 
    /// </summary>
    public class EAP
    {
        WebClient _client;

        public EAP()
        {
            _client = new WebClient();
        }

        public void WebClientSample(string webPageAddr)
        {            
            _client.DownloadStringCompleted += Client_DownloadStringCompleted;
            _client.DownloadProgressChanged += Client_DownloadProgressChanged;
            _client.DownloadStringAsync(new Uri(webPageAddr, UriKind.Absolute));
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            
            Console.WriteLine(e.BytesReceived + " bytes received.");
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Console.WriteLine("Download completed.." + e.Result.Substring(0,100));
        }
        
    }

    public class EAP2TestEv
    {
        public delegate void MyDelegate(string msg);
        public event MyDelegate MyEvent;

        public void RaiseEvent(string message)
        {
            MyEvent(message);
        }

    }

    public class EAP2Test
    {
        EAP2TestEv _eap2TestEv;

        public void StartTest()
        {
            _eap2TestEv = new EAP2TestEv();
            _eap2TestEv.MyEvent += Test_Delegate;
            DoWorkAsync();

            for (int i = 1; i < 7; i++)
            {
                WriteLine(i);
                Sleep(1000);
            }
        }

        private async void DoWorkAsync()
        {
            await Task.Delay(3000);
            _eap2TestEv.RaiseEvent("engin test");            
        }

        private void Test_Delegate(string msg)
        {
            Console.WriteLine("Your Message is: {0}", msg);
        }

    }


}
