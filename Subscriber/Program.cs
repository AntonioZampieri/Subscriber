

Console.WriteLine("Press escape to stop");
Console.WriteLine("Please insert subscriber name");

string subscriberName = Console.ReadLine();

HttpClient client = new HttpClient();


Console.WriteLine("Please selec one of the following operations or remain waiting for messages");
Console.WriteLine("1 - Subscribe");
Console.WriteLine("2 - Publish");
string command = "";
do
{
    if(command == "1" || command == "Subscribe")
    {
        subscribe(subscriberName);
    }
    if(command == "2" || command == "Publish")
    {
        Console.WriteLine("Please write the channel you wish to publish on");
        string channel = Console.ReadLine();
        
        Console.WriteLine("Please write the message you wish to publish");

        publish(channel, Console.ReadLine());
    }
    else
    {
        getMessages(subscriberName);
    }
    command = Console.ReadLine();

} while (Console.ReadKey(true).Key != ConsoleKey.Escape);


static void subscribe(string subscriberName)
{
    throw new NotImplementedException();
}

static void publish(string channel, string messageText)
{
    throw new NotImplementedException();
}

static void getMessages(string subscriberName)
{
    throw new NotImplementedException();
}