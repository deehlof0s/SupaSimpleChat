using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

using TcpChatService.Models;

namespace TcpChatService.Service {
    /// <summary>
    /// Контрукт службы
    /// </summary>
    [ServiceContract(CallbackContract=typeof(ITcpServiceCallback))]
    public interface ITcpService {
        /// <summary>
        /// Оперция подключения пользователя
        /// </summary>
        /// <param name="user">Объект типа UserInfo</param>
        [OperationContract]
        void Connect(UserInfo user);

        /// <summary>
        /// Операция отключения пользователя
        /// </summary>
        /// <param name="user">Объект типа UserInfo</param>
        [OperationContract]
        void Disconnect(UserInfo user);

        /// <summary>
        /// Операция отправки сообщения всем подключенным пользователям
        /// </summary>
        /// <param name="message">Объект типа MessageInfo</param>
        [OperationContract]
        void SendMessage(MessageInfo message);
    }
}
