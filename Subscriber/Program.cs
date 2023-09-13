

using System.Net.Http.Json;
using Subscriber.Models;

HttpClient client = new HttpClient();
const string basicPublisherUrl = "https://localhost:7262";

Console.WriteLine("Press escape to stop");
Console.WriteLine("Please insert subscriber name");

string subscriberName = Console.ReadLine();


Console.WriteLine("Please select one of the following operations or remain waiting for messages");
Console.WriteLine("1 - Subscribe");
Console.WriteLine("2 - Publish");
string command = "";

do
{
    if(command == "1" || command == "Subscribe")
    {
        Console.WriteLine("Please write the channel you wish to subscribe to");
        subscribe(client, subscriberName, Console.ReadLine());
    }
    if(command == "2" || command == "Publish")
    {
        Console.WriteLine("Please write the channel you wish to publish on");
        string channel = Console.ReadLine();
        
        Console.WriteLine("Please write the message you wish to publish");

        publish(client, channel, Console.ReadLine());
    }
    else
    {
        getMessages(client, subscriberName);
    }
    command = Console.ReadLine();

} while (Console.ReadKey(true).Key != ConsoleKey.Escape);


static void subscribe(HttpClient client, string subscriberName, string channelName)
{
    SubscriberModel subscriber = new SubscriberModel { SubscriberName = subscriberName };

    var response = client.PostAsJsonAsync($"{basicPublisherUrl}/api/channels/{channelName}/subscribe", subscriber).Result;

    var responseContent = response.Content.ReadAsStringAsync().Result;
}

static void publish(HttpClient client, string channel, string messageText)
{
    throw new NotImplementedException();
}

static void getMessages(HttpClient client, string subscriberName)
{
    throw new NotImplementedException();
    while (!Console.KeyAvailable)
    {
        Thread.Sleep(2000);
        getMessages(client, subscriberName);
    }
}