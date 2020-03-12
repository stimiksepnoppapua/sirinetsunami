//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.AspNet.SignalR.Client;
//using Microsoft.AspNet.SignalR.Client.Hubs;
//using MobileBMKG.Models;

//namespace MobileBMKG
//{

//    public class SignalRClientConnection
//    {
//        private readonly string Url = Helper.URL + "/signalr";
//        private HubConnection _connection;
//        private IHubProxy _proxy;
//        private CancellationTokenSource reconnectTokenSource;
//        private Queue<HubConnection> invokeQueue;
//        public ConnectionState ConnectionState { get; private set; }
//        public EventHandler<StateChange> OnConnectionStateChanged;

//        public event EventHandler<Gempa> NewReceived;

//        public async Task StartListening()
//        {
//            _connection = new HubConnection(Url);
//            // _connection.TraceWriter = tracer;
//            _connection.TraceLevel = TraceLevels.All;
//            _connection.StateChanged += OnConnectionStateChangedHandler;
//            _connection.Reconnected += OnReconnectedHandler;

//            _proxy = _connection.CreateHubProxy("infoHub");

//            _proxy.On<Gempa>("updateShape", urls =>
//            {
//                if (NewReceived != null)
//                    NewReceived.Invoke(this, urls);
//            });

//            if (_connection.State == ConnectionState.Disconnected)
//            {
//                try
//                {
//                    await _connection.Start();
//                    invokeQueue.Enqueue(_connection);
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.Message);
//                }
//            }
//        }

//        private void OnReconnectedHandler()
//        {
//            //  Task.Factory.StartNew(async () => await invokeQueue.ProcessQueue());
//        }

//        private void OnConnectionStateChangedHandler(StateChange change)
//        {
//            this.ConnectionState = change.NewState;
//            OnConnectionStateChanged?.Invoke(this, change);

//            switch (change.NewState)
//            {
//                case ConnectionState.Disconnected:
//                    // SignalR doesn´t do anything after disconnected state, so we need to manually reconnect
//                    reconnectTokenSource = new CancellationTokenSource();
//                    Task.Factory.StartNew(async () =>
//                    {
//                        await Task.Delay(TimeSpan.FromSeconds(Helper.RECONNECT_PERIOD_SECONDS), reconnectTokenSource.Token);
//                        await StartListening();
//                    }, reconnectTokenSource.Token);

//                    break;
//            }
//        }
//    }
//}
