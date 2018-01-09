using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousProgrammingPatterns
{
    /// <summary>
    ///  An asynchronous method should return either a System.Threading.Tasks.Task or a 
    ///  System.Threading.Tasks.Task<TResult> object. 
    ///  For the latter, the body of the function should return a TResult, and the compiler 
    ///  ensures that this result is made available through the resulting task object.
    ///  
    /// </summary>
    class TAP
    {
        public async Task<int> ReadFileAsync(string filePath)
        {            
            FileStream fs = File.OpenRead(filePath);
            var readBuffer = new byte[fs.Length];
            Task<int> readTask = fs.ReadAsync(readBuffer, 0, (int)fs.Length);

            int res = await readTask;

            if (readTask.IsCompleted)
            {
                Console.WriteLine("Read {0} bytes successfully from file {2}", res, filePath);
            }

            return res;
            //readTask.ContinueWith(task =>
            //{
            //    if (task.Status == TaskStatus.RanToCompletion)
            //        Console.WriteLine("Read {0} bytes successfully from file {1}", task.Result, filePath);
            //    else
            //        Console.WriteLine("Exception occurred while reading file {0}.", filePath);

            //    fs.Dispose();
            //});


            //return readTask;
        }

    }
}
