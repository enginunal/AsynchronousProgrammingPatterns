using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Thread;

namespace AsynchronousProgrammingPatterns
{

    class Program
    {
       

        static void Main(string[] args)
        {
            //In this sample project I would like to give information about 
            //performing asynchronous operations 
            // We have three patterns:

            //1-APM (Asynchronous Programming Model) Pattern
            //"This pattern is no longer recommended for new development."
            //Asynchronous operations require BeginXX and EndXX methods.


            //2-EAP (Event-based Asynchronous Pattern)
            //"It is no longer recommended for new development."
            //Requires a method that has the Async suffix, and also requires 
            //one or more events, event handler delegate types, and EventArg-derived types. 


            //3-TAP (Task-based Asynchronous Pattern)
            //Uses a single method to represent the initiation and completion of an asynchronous operation.
            //The async and await keywords add language support for TAP


            WriteLine("1 : APM pattern test");
            WriteLine("2 : EAP pattern test");
            WriteLine("3 : TAP pattern test");
            Write("Enter your sample no:");
            char key = ReadKey().KeyChar;
            WriteLine("\nNo " + key + " is starting.");

            switch (key)
            {
                case '1':
                    APM apm = new APM();
                    apm.readFileAsync(@"C:\TEMP\ldac4.rar");
                    break;
                case '2':
                    //first sample of EAP
                    //EAP eap = new EAP();
                    //eap.WebClientSample("http://www.google.com");
                    //another sample 
                    EAP2Test eap2Test = new EAP2Test();
                    eap2Test.StartTest();
                    break;
                case '3':
                    TAP tap = new TAP();
                    Task<int> read1 = tap.ReadFileAsync(@"C:\YEDEK\infragistics\NetAdvantage_20071_CLR2x_NET\NetAdvantage_20071_CLR2x_NET.msi");
                    WriteLine("1.File read opr has started");
                    Task<int> read2 = tap.ReadFileAsync(@"C:\YEDEK\Telerik\2011\Asp.Net\2011-Q3\Telerik.Web.UI_2011_3_1305_Dev.msi");
                    WriteLine("2.File read opr has started");
                    Task<int> read3 = tap.ReadFileAsync(@"C:\TEMP\tst.rar");
                    WriteLine("3.File read opr has started");

                    for (int i = 1; i < 20; i++)
                    {
                        WriteLine("counter {0}", i);
                        Sleep(100);
                    }

                    Task.WhenAll(read1, read2, read3)
                        .ContinueWith(task => Console.WriteLine("All files have been read successfully."));

                    break;
            }


            WriteLine("Any key to continue...");
            ReadKey();
        }
    }
}
