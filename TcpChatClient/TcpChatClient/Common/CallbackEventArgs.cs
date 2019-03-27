using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TcpChatClient.TcpServiceReference;

namespace TcpChatClient.Common {
    /// <summary>
    /// Класс аргумента для событий оповещения из контракта обратного вызова
    /// </summary>
    public class CallbackEventArgs : EventArgs {
        /// <summary>
        /// Массив объектов типа UserInfo
        /// </summary>
        public readonly UserInfo[] Users;

        /// <summary>
        /// Объект типа MessageInfo
        /// </summary>
        public readonly MessageInfo Message;

        /// <summary>
        /// Конструктор (сцепленный с основным)
        /// </summary>
        /// <param name="users"></param>
        public CallbackEventArgs(UserInfo[] users)
            : this(users, null) {
        }

        /// <summary>
        /// Конструктор (сцепленный с основным)
        /// </summary>
        /// <param name="message"></param>
        public CallbackEventArgs(MessageInfo message)
            : this(null, message) {
        }

        /// <summary>
        /// Конструктор (основной)
        /// </summary>
        /// <param name="users"></param>
        /// <param name="message"></param>
        public CallbackEventArgs(UserInfo[] users, MessageInfo message) {
            Users = users;
            Message = message;
        }
    }
}
