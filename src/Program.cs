using System;
using EasyHue;

namespace EasyHueExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new HueClient object with the IP address of your Hue Bridge
            HueClient client = new HueClient("192.168.1.123");

            // Get a reference to the first light in the system
            Light light = client.GetLight(1);

            // Turn the light on
            light.On();

            // Wait for 5 seconds
            System.Threading.Thread.Sleep(5000);

            // Turn the light off
            light.Off();
        }
    }
}
