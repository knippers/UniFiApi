using System;
using System.Configuration;
using System.Threading.Tasks;

namespace KoenZomers.UniFi.Api.ConsoleApp
{
    class Program
    {
        /// <summary>
        /// Default synchronous application main
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args)
        {
            // Create a new Api instance to connect with the UniFi Controller
            Console.WriteLine("Connecting to UniFi Controller");
            var uniFiApi = new Api(new Uri(ConfigurationManager.AppSettings["UniFiControllerUrl"]), ConfigurationManager.AppSettings["UniFiControllerSiteId"], true);

            // Disable SSL validation as UniFi uses a self signed certificate
            Console.WriteLine("- Disabling SSL validation");
            uniFiApi.DisableSslValidation();

            // Authenticate to UniFi
            Console.WriteLine("- Authenticating");
            var authenticationSuccessful = await uniFiApi.Authenticate(ConfigurationManager.AppSettings["UniFiControllerUsername"], ConfigurationManager.AppSettings["UniFiControllerPassword"]);

            if (!authenticationSuccessful)
            {
                Console.WriteLine("- Authentication failed");
                return;
            }

            Console.WriteLine("- Authentication successful");
            
            // Retrieve the UniFi devices
            Console.WriteLine("- Getting devices");
            var devices = await uniFiApi.GetDevices();

            foreach (var device in devices)
            {
                Console.WriteLine($"  - {device.Name} (MAC {device.MacAddress})");
            }

            // Retrieve the active clients
            Console.WriteLine("- Getting active clients");
            var activeClients = await uniFiApi.GetActiveClients();

            foreach (var activeClient in activeClients)
            {
                Console.WriteLine($"  - {activeClient.FriendlyName} (MAC {activeClient.MacAddress}, Channel {activeClient.Channel})");
            }

            // Retrieve wireless networkd
            Console.WriteLine("- Getting wireless networks");
            var wirelessNetworks = await uniFiApi.GetWirelessNetworks();

            Responses.WirelessNetwork networkToUpdate = null;

            foreach (var wifiNetwork in wirelessNetworks)
            {
                Console.WriteLine($"  - {wifiNetwork.Name} (ID: {wifiNetwork.Id}, MAC Filter: {wifiNetwork.IsMACFilterEnabled})");
                if( wifiNetwork.IsMACFilterEnabled == true)
                {
                    foreach( var mac in wifiNetwork.MACFilterList)
                    {
                        Console.WriteLine($"    - {mac}");
                    }
                }

                if( wifiNetwork.Name == "TimeTell" )
                {
                    Console.WriteLine($"    - Saving {wifiNetwork.Name} for update");
                    networkToUpdate = wifiNetwork;
                }
            }

            if( networkToUpdate != null ) 
            {
                Console.WriteLine($"- Update wireless network: {networkToUpdate.Name}");
                var data = await uniFiApi.UpdateWirelessNetwork(networkToUpdate.Id, networkToUpdate.MACFilterList);
                //var data = await uniFiApi.SetWirelessNetworkStatus(networkToUpdate.Id, true);
                Console.WriteLine(data.Count);

            }




            // Logout
            await uniFiApi.Logout();
        }
    }
}
