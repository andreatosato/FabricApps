using CounterService.Interfaces;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CounterService.Runner
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            ICounterService actor = ActorProxy.Create<ICounterService>(ActorId.CreateRandom(), new Uri("fabric:/CounterActorApp/CounterServiceActorService"));

            Stopwatch diagnostic = Stopwatch.StartNew();
            int i = 0;
            while(i <= 200)
            {
                try
                {
                    int retval = await actor.GetCountAsync(CancellationToken.None);
                    Console.WriteLine($"value: {retval}");
                }
                catch (Exception)
                {
                }
                Console.WriteLine($"event {i} ... from last event: {diagnostic.ElapsedMilliseconds} milli");
                
                diagnostic.Restart();
                i += 1;
            }
            Console.Read();
        }
    }
}
