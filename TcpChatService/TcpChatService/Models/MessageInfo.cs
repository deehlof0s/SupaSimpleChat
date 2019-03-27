using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace TcpChatService.Models {
    /// <summary>
    /// Класс модели сообщения
    /// </summary>
    [DataContract]
    public class MessageInfo {
        /// <summary>
        /// Отправитель сообщения
        /// </summary>
        [DataMember]
        public UserInfo User { get; set; }
        /// <summary>
        /// Текст сообщения
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }
}
