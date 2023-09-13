

using System.Net.Http.Json;
using Subscriber.Models;

HttpClient client = new HttpClient();
const string basicPublisherUrl = "https://localhost:7262";
List<string> receivedMessages = new List<string>();

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
        Console.WriteLine("Subscription completed");
    }
    if(command == "2" || command == "Publish")
    {
        Console.WriteLine("Please write the channel you wish to publish on");
        string channel = Console.ReadLine();
        
        Console.WriteLine("Please write the message you wish to publish");

        publish(client, channel, Console.ReadLine());
        Console.WriteLine("Publish completed");
    }
    else
    {
        getMessages(client, subscriberName, receivedMessages);
    }
    command = Console.ReadLine();

} while (Console.ReadKey(true).Key != ConsoleKey.Escape);


static void subscribe(HttpClient client, string subscriberName, string channelName)
{
    SubscriberModel subscriber = new SubscriberModel { SubscriberName = subscriberName };

    var response = client.PostAsJsonAsync($"{basicPublisherUrl}/api/channels/{channelName}/subscribe", subscriber).Result;

    var responseContent = response.Content.ReadAsStringAsync().Result;
}

static void publish(HttpClient client, string channelName, string messageText)
{
    Message message = new Message { MessageText = messageText };

    var response = client.PostAsJsonAsync($"{basicPublisherUrl}/api/channels/{channelName}/publish", message).Result;

    var responseContent = response.Content.ReadAsStringAsync().Result;
}

static async void getMessages(HttpClient client, string subscriberName, List<string> receivedMessages)
{
    while (!Console.KeyAvailable)
    {
        var messages = await client.GetFromJsonAsync<List<string>>($"{basicPublisherUrl}/api/subscribers/{subscriberName}/messages");

        var newMessages = messages.Where(m => !receivedMessages.Contains(m));

        if (newMessages.Any()) 
        {
            newMessages.ToList().ForEach(message =>
            {
                Console.WriteLine(message);
                receivedMessages.Add(message);
            });
        }

        Thread.Sleep(2000);
    }
}