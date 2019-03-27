using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TcpChatService.Models;

namespace TcpChatService.Common {
    /// <summary>
    /// Класс для аргумента события подключения/отключения пользователей
    /// </summary>
    public class ConnectionEventArgs : EventArgs {
        /// <summary>
        /// Массив объктов типа UserInfo
        /// </summary>
        public readonly UserInfo[] Users;
        /// <summary>
        /// Коснтруктор
        /// </summary>
        /// <param name="users">Массив объктов типа UserInfo</param>
        public ConnectionEventArgs(UserInfo[] users) {
            Users = users;
        }
    }
}
