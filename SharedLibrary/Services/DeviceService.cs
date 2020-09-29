using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace SharedLibrary.Services
{
    public static class DeviceService
    {
        private static readonly Random rnd = new Random();

        public static async Task<string> SendMessageAsync(DeviceClient deviceClient)
        {
                var data = new Weather()
                {
                   Temperature = rnd.Next(20, 30).ToString(),
                   Humidity = rnd.Next(30, 40).ToString()
                };

                var json = JsonConvert.SerializeObject(data);

                //formatera meddlandet till UTF8. Omvandla till bytes så att datorn förstår. SKickas på nätet genom 1or och 0or
                var payload = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(json));

                await deviceClient.SendEventAsync(payload);
                Console.WriteLine($"Message sent: {json}");

            return json;    
        }
        
        public static async Task RecieveMessageAsync(DeviceClient deviceClient)
        {
            while (true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                {
                    continue;
                }
                Console.WriteLine($"Message recieved {Encoding.UTF8.GetString(payload.GetBytes())}");

                //tar bort meddelandet från hubben
                await deviceClient.CompleteAsync(payload);

            }
            
        }
        
        public static async Task SendMessageToDeviceAsync(ServiceClient serviceClient, string targetDevice, string message)
        {
            var payload = new Microsoft.Azure.Devices.Message(Encoding.UTF8.GetBytes(message));

            await serviceClient.SendAsync(targetDevice, payload);
        }

    }
}
