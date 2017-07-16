using System;

namespace ChatBotWithLib
{
    class Program
    {
        private static string UserName = "HydraSoftBot";
        private static string TwitchToken = "qne6na6a9mpflovjqbr9pxcryeza5d";
        private static string ClientID = "yf1hibe5pgrt68xt8ehzphejct7zbu";
        private static string ChannelName;
        public static bool Running;
        private static string UInput ="";
        private static Graph graph;
        public static bool Connected = false;
        private static int c = 0;
        private static string ConnectingText = "Connecting";
        
        static void Main(string[] args)
        {
            graph = new Graph();
            Running = true;
            ConnectBot();            
        }
        private static string GetChannelName()
        {
            Console.Write("ChannelName: ");
            ChannelName = Console.ReadLine();
            return ChannelName;
        }
        private static void ConnectBot()
        {
            TwitchChatBot Bot = new TwitchChatBot(UserName,TwitchToken,ClientID,GetChannelName());
            Bot.Connect();
            string Loading = ConnectingText;
            while(!Connected)
            {
                if(c<3)
                {
                    Loading+=".";
                    Console.WriteLine(Loading);
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    Loading = ConnectingText;
                }
            }
            while(Running)
            {
                Console.Write("Input> ");
                UInput = Console.ReadLine();
                if(UInput == "DC" || UInput == "Disconnect" || UInput == "dc")
                {
                    Bot.Disconnect();
                    Running = false;
                }
                else if(UInput == "Counter.Enable")
                {
                    Bot.CounterEnable();
                }
                else if(UInput == "help" || UInput == "?")
                {
                    Console.Write("DC,Disconnect,dc - Disconnects bot and exits program.\nCounter.Enable - Switches on/off Counter visibility (default off).\nhelp,? - Prints out all avaible commands.\nGraph.Draw - Shows graph\nGraph.Close - Closes graph\n");
                }
                else if(UInput == "Graph.Draw")
                {
                    graph.DrawGraph();
                }
                else if(UInput == "Graph.Close")
                {
                    graph.CloseGraph();
                }
                else
                {
                    Console.WriteLine("Unknown command "+'"'+UInput+'"');
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}