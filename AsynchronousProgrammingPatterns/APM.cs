using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProgrammingPatterns
{
    /// <summary>
    /// After calling BeginOperationName, an application can 
    /// continue executing instructions on the calling thread while 
    /// the asynchronous operation takes place on a different thread. 
    /// For each call to BeginOperationName, the application should also 
    /// call EndOperationName to get the results of the operation.
    /// 
    /// EndOperationName blocks the calling thread until the asynchronous operation is complete.
    /// </summary>
    class APM
    {
        /// <summary>
        /// FileStream class provides the BeginRead and EndRead methods 
        /// to asynchronously read bytes from a file. 
        /// These methods implement the asynchronous version of the Read method.
        /// </summary>
        public void readFileAsync(string filePath)
        {
            byte[] buffer = new byte[50];
            string filename = String.Concat(filePath);

            FileStream fs = new FileStream(filename, FileMode.Open, 
                FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous);

            IAsyncResult result = fs.BeginRead(buffer, 0, buffer.Length, null, null);
            // do some work here while you wait
            //Calling EndRead will block until the Async work is complete
            int numBytes = fs.EndRead(result);

            fs.Close();

            Console.WriteLine("Read {0}  Bytes:", numBytes);
            Console.WriteLine(BitConverter.ToString(buffer));
        }

    }
}
