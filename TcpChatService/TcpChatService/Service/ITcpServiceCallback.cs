using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

using TcpChatService.Models;

namespace TcpChatService.Service {
    /// <summary>
    /// Контракт обратного вызова
    /// </summary>
    [ServiceContract]
    public interface ITcpServiceCallback {
        /// <summary>
        /// Операция оповещения о подключении нового пользователя
        /// </summary>
        /// <param name="users">Массив объектов типа UserInfo</param>
        [OperationContract(IsOneWay=true)]
        void GetConnectedUsers(UserInfo[] users);

        /// <summary>
        /// Операция рассылки сообщений всем подключенным пользователям
        /// </summary>
        /// <param name="message">Объект типа MessageInfo</param>
        [OperationContract(IsOneWay=true)]
        void SendToAll(MessageInfo message);
    }
}
