using System;
using System.Timers;
using TwitchLib;
using TwitchLib.Models.Client;
using TwitchLib.Events.Client;
using TwitchLib.Models.API.v5.Users;

namespace ChatBotWithLib
{
    internal class TwitchChatBot
    {
        private Timer timer1;
        private ConnectionCredentials cred;
        private string UserName;
        private string TwitchToken;
        private string ClientID;
        private string ChannelName;
        private TwitchClient Client;
        private float Time = 60f;
        private float LPM;
        private int lulCounter = 0;
        public bool lulCountEnabled = false;

        public TwitchChatBot(string UserName, string TwitchToken, string ClientID ,string ChannelName)
        {
            this.UserName = UserName;
            this.TwitchToken = TwitchToken;
            this.ClientID = ClientID;
            this.ChannelName = ChannelName;
            this.cred = CreateCred();
        }
        private ConnectionCredentials CreateCred()
        {
            cred = new ConnectionCredentials(this.UserName , this.TwitchToken);
            return cred;
        }
        
        internal void Connect()
        {
                Client = new TwitchClient(cred, this.ChannelName, logging: false);
                Console.WriteLine("Client created");

                Client.OnConnectionError += Client_OnConnectionError;
                Client.OnConnected += Client_OnConnected;
                
                Console.WriteLine("Connecting");
                
                Client.Connect();
        }
        private void MainFunc()
        {
            
            Client.OnMessageReceived += Client_OnMessageReceived;
            Console.WriteLine("Starting timer");
            StartCounter();
        }
        internal void Disconnect()
        {
            Console.WriteLine("Stopping timer");

            StopCounter();

            Console.WriteLine("Disconnecting");

            Client.Disconnect();
        }
        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if(e.ChatMessage.Message.StartsWith("lul", StringComparison.InvariantCultureIgnoreCase))
            {
                lulCounter++;
                goto End;
            }
            else if(e.ChatMessage.Message.EndsWith("lul", StringComparison.InvariantCultureIgnoreCase))
            {
                lulCounter++;
                goto End;
            }
            End:;
        }
        private void Client_OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            Console.WriteLine($"Error!! {e.Error}");
        }
        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected as {e.Username}");
            Program.Connected = true;
            MainFunc();
        }
        public void CounterEnable() //switches on/off counter output
        {
            if(lulCountEnabled)
            {
                lulCountEnabled = false;
                Console.WriteLine("Counter display disabled");
            }
            else
            {
                lulCountEnabled = true;
                Console.WriteLine("Counter display enabled");
            }
        }
        private void StartCounter() //starts timer
        {
            timer1 = new Timer();
            timer1.Interval = Time * 1000;
            timer1.Elapsed += OntimedEvent;
            timer1.AutoReset = true;
            timer1.Enabled = true;
        }
        private void StopCounter()
        {
            timer1.Enabled = false;   
        }        
        private void OntimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            if(lulCountEnabled)
            {
                Console.Write($"\nLuLCount: {lulCounter} and {string.Format("LPS : {0:0.00##}",CalculateLPM())}\nInput> ");
                lulCounter = 0;
            }
        }
        private float CalculateLPM()
        {
            LPM = lulCounter/Time;
            return LPM;
        }
    }
}