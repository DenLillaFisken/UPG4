using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json.Linq;
using SharedLibrary.Models;
using SharedLibrary.Services;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpApp
{
    public sealed partial class MainPage : Page
    {
       
        private static readonly string _conn = "HostName=EC-WEB20-AJ.azure-devices.net;DeviceId=consoleapp;SharedAccessKey=FQFt0u9cyO0SEDTU7+zDQHAogBVuOPeag2O49QiDaPw=";

        //Skapar en iot Device
        private static readonly DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);
        public MainPage()
        {
            this.InitializeComponent();
        }
        public WeatherList weatherlist = new WeatherList();
        public SendMessages humtemp = new SendMessages();
        private async void btnSendMessageAsync_Click(object sender, RoutedEventArgs e)
        {
            var result = await DeviceService.SendMessageAsync(deviceClient);
        
            dynamic data = JObject.Parse(result);

            var temp = Convert.ToString(data.Temperature);
            var hum = Convert.ToString(data.Humidity);

            //get.Awaiter är istället för att ändra om Main-funtionen till async
            DeviceService.RecieveMessageAsync(deviceClient).GetAwaiter();

            try
            {
                weatherlist.Add(new Weather($"Temperature: {temp}", $"Humidity: {hum}"));
            }
            catch { }

        }
    }
}