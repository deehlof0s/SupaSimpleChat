using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

using TcpChatService.Models;
using TcpChatService.Common;

namespace TcpChatService.Service {
    /// <summary>
    /// Реализация контракта службы
    /// </summary>
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class TcpService : ITcpService {
        /// <summary>
        /// Событие оповещения о подключении нового пользователя (локальное оповещение)
        /// </summary>
        public event EventHandler<ConnectionEventArgs> ConnectionEvent = delegate { };

        /// <summary>
        /// Коллекция (словарь) для хранения информации о подключенных пользователях и каналов обратного вызова
        /// </summary>
        static Dictionary<UserInfo, ITcpServiceCallback> callbackStorage = 
            new Dictionary<UserInfo, ITcpServiceCallback>();

        /// <summary>
        /// Реализация операции подключения нового пользователя
        /// </summary>
        /// <param name="user">Объект типа UserInfo</param>
        public void Connect(UserInfo user) {
            if (!callbackStorage.ContainsKey(user)) {
                ITcpServiceCallback callback = OperationContext.Current.GetCallbackChannel<ITcpServiceCallback>();
                callbackStorage.Add(user, callback);

                foreach (ITcpServiceCallback cb in callbackStorage.Values) {
                    cb.GetConnectedUsers(callbackStorage.Keys.ToArray());
                }
                
                ConnectionEventArgs args = new ConnectionEventArgs(callbackStorage.Keys.ToArray());
                OnConnectionEvent(args);
            }
        }

        /// <summary>
        /// Реализация отключения пользователя
        /// </summary>
        /// <param name="user">Объект типа UserInfo</param>
        public void Disconnect(UserInfo user) {
            if (callbackStorage.ContainsKey(user)) {
                ITcpServiceCallback callback = OperationContext.Current.GetCallbackChannel<ITcpServiceCallback>();
                callbackStorage.Remove(user);

                foreach (ITcpServiceCallback cb in callbackStorage.Values) {
                    cb.GetConnectedUsers(callbackStorage.Keys.ToArray());
                }

                ConnectionEventArgs args = new ConnectionEventArgs(callbackStorage.Keys.ToArray());
                OnConnectionEvent(args);
            }
        }

        /// <summary>
        /// Реализация передачи сообщения всем пользователям
        /// </summary>
        /// <param name="message">Объект типа MessageInfo</param>
        public void SendMessage(MessageInfo message) {
            foreach (ITcpServiceCallback cb in callbackStorage.Values) {
                cb.SendToAll(message);
            }
        }

        /// <summary>
        /// Метод для вызова события оповещения о подключении нового пользователя (локальное оповещение)
        /// </summary>
        /// <param name="e">Объект типа ConnectionEventArgs</param>
        protected virtual void OnConnectionEvent(ConnectionEventArgs e) {
            if (ConnectionEvent != null) {
                ConnectionEvent(this, e);
            }
        }
    }
}
