using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Second
{
    class Program
    {
        static void Main(string[] args)
        {

            const int maxConsumers = 10;
            var sqQueue = new SpecialQueue<int>();
            int index = 1;
            Task taskWriter = new Task(() => 
            {
                for (int i = 0; i < maxConsumers; i++)
                {
                    sqQueue.Push(index++);
                    Console.WriteLine("****************write to queue done *********************");
                    Thread.Sleep(1000);
                }
                
            });

            Action action =
                () =>
                {
                    Console.WriteLine("task id = {0} read from queue value {1}",
                        Thread.CurrentThread.ManagedThreadId, sqQueue.Pop());
                    /*
                     * В зависимости от того, какая задержка тут будет стоять, можно увидеть как реально создадутся 10 разных потоков.
                     * Если задержку поставить небольшую, то TP будет выдавать потоки из пула, число потоков будет равных количеству ядер.
                     * Thread.Sleep(300); 
                     */


                };
                    

            var readTasks = Enumerable.Range(1, maxConsumers).Select(t => action).ToArray();

            taskWriter.Start();

            Parallel.Invoke(/*new ParallelOptions(){ MaxDegreeOfParallelism = 10},*/ readTasks);

            Console.WriteLine("All done!");
        }



    }
}
