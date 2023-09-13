

using System.Net.Http.Json;
using Subscriber.Models;

HttpClient client = new HttpClient();
const string basicPublisherUrl = "https://localhost:7262";
List<string> receivedMessages = new List<string>();

Console.WriteLine("Press escape to stop");
Console.WriteLine("Please insert subscriber name");

string subscriberName = Console.ReadLine();
Console.WriteLine("");


Console.WriteLine("Please select one of the following operations or remain waiting for messages");
Console.WriteLine("1 - Subscribe");
Console.WriteLine("2 - Publish");
Console.WriteLine("");

string command = "";

/// <summary>
/// do-while cycle to use the functions to contact the server
/// </summary>
do
{
    if(command == "1" || command == "Subscribe")
    {
        Console.WriteLine("Please write the channel you wish to subscribe to");
        subscribe(client, subscriberName, Console.ReadLine());
        Console.WriteLine("Subscription completed");
        Console.WriteLine("");
    }
    if(command == "2" || command == "Publish")
    {
        Console.WriteLine("Please write the channel you wish to publish on");
        string channel = Console.ReadLine();
        
        Console.WriteLine("Please write the message you wish to publish");

        publish(client, channel, Console.ReadLine());
        Console.WriteLine("Publish completed");
        Console.WriteLine("");
    }
    else
    {
        getMessages(client, subscriberName, receivedMessages);
    }
    command = Console.ReadLine();

} while (Console.ReadKey(true).Key != ConsoleKey.Escape);


/// <summary>
/// function to subscribe to a channel
/// </summary>
/// <param name="client"> http client used to call the server </param>
/// <param name="subscriberName"> name of the subscriber to add </param>
/// <param name="channelName"> name of the channel to subscribe to </param>
static void subscribe(HttpClient client, string subscriberName, string channelName)
{
    SubscriberModel subscriber = new SubscriberModel { SubscriberName = subscriberName };

    var response = client.PostAsJsonAsync($"{basicPublisherUrl}/api/channels/{channelName}/subscribe", subscriber).Result;

    var responseContent = response.Content.ReadAsStringAsync().Result;
}

/// <summary>
/// function to publish a message on a channel
/// </summary>
/// <param name="client"> http client used to call the server </param>
/// <param name="channelName"> name of the channel to publish the message on </param>
/// <param name="messageText"> message to publish on the channel </param>
static void publish(HttpClient client, string channelName, string messageText)
{
    Message message = new Message { MessageText = messageText };

    var response = client.PostAsJsonAsync($"{basicPublisherUrl}/api/channels/{channelName}/publish", message).Result;

    var responseContent = response.Content.ReadAsStringAsync().Result;
}

/// <summary>
/// function to get the messages for a selected subscriber and write them to console
/// </summary>
/// <param name="client"> http client used to call the server </param>
/// <param name="subscriberName"> name of the subscriber to get the messages for </param>
/// <param name="receivedMessages"> list of already received messages </param>
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