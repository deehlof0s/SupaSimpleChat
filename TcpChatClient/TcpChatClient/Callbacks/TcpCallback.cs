using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

using TcpChatClient.Common;
using TcpChatClient.TcpServiceReference;

namespace TcpChatClient.Callbacks {
    /// <summary>
    /// Класс реализации контракта обратного вызова
    /// </summary>
    [CallbackBehavior(UseSynchronizationContext=false,ConcurrencyMode=ConcurrencyMode.Reentrant)]
    public class TcpCallback : ITcpServiceCallback {
        /// <summary>
        /// Событие оповещения о подключении/отключении пользователя
        /// </summary>
        public event EventHandler<CallbackEventArgs> UsersEven = delegate { };
        /// <summary>
        /// Событие о получении новго сообщения
        /// </summary>
        public event EventHandler<CallbackEventArgs> MessageEvent = delegate { };

        public void GetConnectedUsers(UserInfo[] users) {
            CallbackEventArgs args = new CallbackEventArgs(users);
            OnUsersEvent(args);
        }

        /// <summary>
        /// Реализация метода получения нового сообщения
        /// </summary>
        /// <param name="message"></param>
        public void SendToAll(MessageInfo message) {
            CallbackEventArgs args = new CallbackEventArgs(message);
            OnMessageEvent(args);
        }

        /// <summary>
        /// Метод вызова события UsersEven
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUsersEvent(CallbackEventArgs e) {
            if (UsersEven != null) {
                UsersEven(this, e);
            }
        }

        /// <summary>
        /// Метод вызова события MessageEvent
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMessageEvent(CallbackEventArgs e) {
            if (MessageEvent != null) {
                MessageEvent(this, e);
            }
        }
    }
}
