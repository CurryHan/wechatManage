// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR;
using Sensing.Data;
using System.Linq;
using System.Diagnostics;

namespace SensingCloud.Hubs
{
    public class ActionMessage
    {
        public string Action { get; set; }
        public object Content { get; set; }
    }

    public class Message
    {
        public string Type { get; set; }
        public string ConnectionId { get; set; }
        public string Content { get; set; }
        public string GroupName { get; set; }
        public string Action { get; set; }
    }

    enum MessageType
    {
        ToMe,
        ToPrivate,
        ToGroup,
        ToGroupExceptMe,
        Broadcast,
        BroadcastExceptMe,
        JoinGroup,
        LeaveGroup,
    }

    public class SyncHub : Hub
    {

    }
    public class RawConnection : PersistentConnection
    {
        private static readonly ConcurrentDictionary<string, string> _users = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, string> _clients = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, int> _devices = new ConcurrentDictionary<string, int>();


        private SensingSiteDbContext db = new SensingSiteDbContext();
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Broadcast(new ActionMessage { Action = "Notification", Content = $"OnConnected?ConnectionId={connectionId}" });
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {

            //int deviceid = -1;
            //if (_devices.TryGetValue(connectionId, out deviceid))
            //{
            //    var device = db.Devices.FirstOrDefault(d => d.Id == deviceid);
            //    if (device != null)
            //    {
            //        device.DeviceStatus = DeviceStatus.Stopped;
            //        db.SaveChanges();
            //        _devices.TryRemove(connectionId, out deviceid);
            //        Debug.WriteLine("device stopped");
            //        return Connection.Broadcast(new ActionMessage { Action = "DeviceStopped", Content = device });

            //    }
            //}
            return Connection.Broadcast(new ActionMessage { Action = "Notification", Content = $"OnDisconnected?ConnectionId={connectionId}" });
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            var message = JsonConvert.DeserializeObject<Message>(data);

            switch (message.Type)
            {
                case "sendToMe":
                    Connection.Send(connectionId, new ActionMessage { Action = message.Action, Content = message.Content });
                    break;
                case "sendToConnectionId":
                    Connection.Send(message.ConnectionId, new ActionMessage { Action = message.Action, Content = message.Content });
                    break;
                case "sendBroadcast":
                    if (message.Action == "DeviceRunning")
                    {
                        //var deviceDto = JsonConvert.DeserializeObject<Device>(message.Content);
                        //var device = db.Devices.FirstOrDefault(d => d.Mac == deviceDto.Mac);
                        //if (device != null)
                        //{
                        //    device.DeviceStatus = DeviceStatus.Running;
                        //    db.SaveChanges();
                        //    _devices[connectionId] = device.Id;
                        //    Debug.WriteLine("device login");
                        //    Connection.Broadcast(new ActionMessage { Action = message.Action, Content = device });
                        //    break;
                        //}
                    }
                    Connection.Broadcast(new ActionMessage { Action = message.Action, Content = message.Content });
                    break;
                case "sendToGroup":
                    Groups.Send(message.GroupName, new ActionMessage { Action = message.Action, Content = message.Content });
                    break;
                case "joinGroup":
                    Groups.Add(message.ConnectionId, message.GroupName);
                    Connection.Broadcast(new ActionMessage { Action = "Notification", Content = $"JoinGroup?ConnectionId={connectionId} in {message.GroupName}" });
                    break;
                case "leaveGroup":
                    Groups.Remove(message.ConnectionId, message.GroupName);
                    Connection.Broadcast(new ActionMessage { Action = "Notification", Content = $"LeaveGroup?ConnectionId={connectionId} in {message.GroupName}" });
                    break;
                case "throw":
                    throw new InvalidOperationException("Client does not receive this error");
                    break;
            }

            return base.OnReceived(request, connectionId, data);
        }

        protected override Task OnReconnected(IRequest request, string connectionId)
        {
            return Connection.Send(connectionId, new ActionMessage { Action = "Notification", Content = $"OnReconnected?ConnectionId={connectionId}" });
        }

        protected override bool AuthorizeRequest(IRequest request)
        {
            return base.AuthorizeRequest(request);
        }
        protected override IList<string> GetSignals(string userId, string connectionId)
        {
            return base.GetSignals(userId, connectionId);
        }
        protected override IList<string> OnRejoiningGroups(IRequest request, IList<string> groups, string connectionId)
        {
            return base.OnRejoiningGroups(request, groups, connectionId);
        }


        //protected override Task OnConnected(IRequest request, string connectionId)
        //{
        //    Cookie userNameCookie;
        //    if (request.Cookies.TryGetValue("user", out userNameCookie) &&
        //        userNameCookie != null)
        //    {
        //        _clients[connectionId] = userNameCookie.Value;
        //        _users[userNameCookie.Value] = connectionId;
        //    }

        //    string clientIp = GetClientIP(request);

        //    string user = GetUser(connectionId);

        //    return Groups.Add(connectionId, "foo").ContinueWith(_ =>
        //           Connection.Broadcast(DateTime.Now + ": " + user + " joined from " + clientIp)).Unwrap();
        //}

        //protected override Task OnReconnected(IRequest request, string connectionId)
        //{
        //    string user = GetUser(connectionId);

        //    return Connection.Broadcast(DateTime.Now + ": " + user + " reconnected");
        //}

        //protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        //{
        //    string ignored;
        //    _users.TryRemove(connectionId, out ignored);

        //    string suffix = stopCalled ? "cleanly" : "uncleanly";
        //    return Connection.Broadcast(DateTime.Now + ": " + GetUser(connectionId) + " disconnected " + suffix);
        //}

        //protected override Task OnReceived(IRequest request, string connectionId, string data)
        //{
        //    var message = JsonConvert.DeserializeObject<Message>(data);

        //    switch (message.Type) 
        //    {
        //        case MessageType.Broadcast:
        //            Connection.Broadcast(new
        //            {
        //                type = MessageType.Broadcast.ToString(),
        //                from = GetUser(connectionId),
        //                data = message.Value
        //            });
        //            break;
        //        case MessageType.BroadcastExceptMe:
        //            Connection.Broadcast(new
        //            {
        //                type = MessageType.Broadcast.ToString(),
        //                from = GetUser(connectionId),
        //                data = message.Value
        //            },
        //            connectionId);
        //            break;
        //        case MessageType.Send:
        //            Connection.Send(connectionId, new
        //            {
        //                type = MessageType.Send.ToString(),
        //                from = GetUser(connectionId),
        //                data = message.Value
        //            });
        //            break;
        //        case MessageType.Join:
        //            string name = message.Value;
        //            _clients[connectionId] = name;
        //            _users[name] = connectionId;
        //            Connection.Send(connectionId, new
        //            {
        //                type = MessageType.Join.ToString(),
        //                data = message.Value
        //            });
        //            break;
        //        case MessageType.PrivateMessage:
        //            var parts = message.Value.Split('|');
        //            string user = parts[0];
        //            string msg = parts[1];
        //            string id = GetClient(user);
        //            Connection.Send(id, new
        //            {
        //                from = GetUser(connectionId),
        //                data = msg
        //            });
        //            break;
        //        case MessageType.AddToGroup:
        //            Groups.Add(connectionId, message.Value);
        //            break;
        //        case MessageType.RemoveFromGroup:
        //            Groups.Remove(connectionId, message.Value);
        //            break;
        //        case MessageType.SendToGroup:
        //            var parts2 = message.Value.Split('|');
        //            string groupName = parts2[0];
        //            string val = parts2[1];
        //            Groups.Send(groupName, val);
        //            break;
        //        default:
        //            break;
        //    }

        //    return base.OnReceived(request, connectionId, data);
        //}

        //private string GetUser(string connectionId)
        //{
        //    string user;
        //    if (!_clients.TryGetValue(connectionId, out user))
        //    {
        //        return connectionId;
        //    }
        //    return user;
        //}

        private string GetClient(string user)
        {
            string connectionId;
            if (_users.TryGetValue(user, out connectionId))
            {
                return connectionId;
            }
            return null;
        }

        //class Message
        //{
        //    public MessageType Type { get; set; }
        //    public string Value { get; set; }
        //}

        private static string GetClientIP(IRequest request)
        {
            return Get<string>(request.Environment, "server.RemoteIpAddress");
        }

        private static T Get<T>(IDictionary<string, object> env, string key)
        {
            object value;
            return env.TryGetValue(key, out value) ? (T)value : default(T);
        }
    }
}