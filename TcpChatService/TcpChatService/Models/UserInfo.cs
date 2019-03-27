using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace TcpChatService.Models {
    /// <summary>
    /// Класс модели пользователя
    /// </summary>
    [DataContract]
    public class UserInfo {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Переопределенный метод Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) {
            return this.ToString() == obj.ToString();
        }

        /// <summary>
        /// Переопределенный метод GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Переопределенный метод ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return this.Name;
        }
    }
}
