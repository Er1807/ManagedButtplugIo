using System;
using System.Threading.Tasks;
using Buttplug;
using System.ComponentModel;

namespace ButtplugCSharpFFITest
{
    public static class Program
    {
        private static async Task WaitForKey()
        {
            Console.WriteLine("Press any key to continue.");
            while (!Console.KeyAvailable)
            {
                await Task.Delay(10).ConfigureAwait(false);
            }
            Console.ReadKey(true);
        }


        private static async Task RunExample()
        {
            
            await WaitForKey().ConfigureAwait(false);
        }

        public static void Main(string[] args)
        {
            // ButtplugUtils.ActivateEnvLogger();
             RunExample().Wait();
        }
    }
}
