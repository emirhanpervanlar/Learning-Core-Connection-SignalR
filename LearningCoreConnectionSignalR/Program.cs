using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace LearningCoreConnectionSignalR
{
    class Program
    {
        static HubConnection connection = new HubConnectionBuilder()
                  .WithUrl("http://localhost:63845/chat")
                  .Build();

        static void Main(string[] args)
        {
            connect();
            Console.WriteLine("Hello World!");
            string getLine = "";
            while(getLine != "x")
            {
                getLine = Console.ReadLine();
                sendMessage(getLine);
            }
        }

        static async void connect() {
            try
            {
                connection.On<string, string>("Send", (user, message) =>
                {
                    Console.WriteLine(user+" : "+message);
                });

                await connection.StartAsync();
                Console.WriteLine("Connection started");
                await connection.InvokeAsync("setUser", "ConsoleApp");
                await connection.InvokeAsync("SendPublicRoom","Selamlar");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async void sendMessage(string message) {
            await connection.InvokeAsync("SendPublicRoom", message);
        }
    }
}
