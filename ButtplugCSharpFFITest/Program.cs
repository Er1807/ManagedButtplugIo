using System;
using System.Threading.Tasks;
using Buttplug;
using System.ComponentModel;
using System.Threading;

namespace ButtplugCSharpFFITest
{
    public static class Program
    {
        

        public static async Task Main(string[] args)
        {
            var client = new ButtplugClient("Managed Client");
            client.ErrorReceived += (a,b) =>
            {
                Console.WriteLine(b.Exception);
            };
            client.DeviceAdded += (a, b) =>
            {
                Console.WriteLine(b.Device.Name);
                b.Device.SendVibrateCmd(1);
                Thread.Sleep(200);
                b.Device.SendVibrateCmd(0.5);
                Thread.Sleep(200);
                b.Device.SendVibrateCmd(0.2);
                Thread.Sleep(200);
                b.Device.SendVibrateCmd(0.7);
                Thread.Sleep(200);
                b.Device.SendVibrateCmd(0.1);
                Thread.Sleep(200);
                b.Device.SendVibrateCmd(0);
            };
            await client.ConnectAsync(new ButtplugWebsocketConnectorOptions(new Uri("ws://localhost:12346")));

            
            while (true)
            {
                if (!client.IsScanning)
                {
                    await client.StartScanningAsync();
                    Console.WriteLine("Startin scanning");
                }

                Thread.Sleep(1000);
            }
            Console.ReadLine();

            Console.WriteLine(client.Devices.Length);

            Console.ReadLine();

            await client.DisconnectAsync();

            Console.ReadLine();

        }
    }
}
