using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

using TcpChatClient.Common;
using TcpChatClient.Callbacks;
using TcpChatClient.TcpServiceReference;

namespace TcpChatClient.Forms {
    public partial class MainForm : Form {
        /// <summary>
        /// Поле класса посредника
        /// </summary>
        TcpServiceClient proxy = null;

        /// <summary>
        /// Поле текущего пользователя
        /// </summary>
        UserInfo user = null;


        /// <summary>
        /// Конструктор основной формы
        /// </summary>
        public MainForm() {
            InitializeComponent();
            this.Load += new EventHandler(MainForm_Load);
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            btnSend.Click += new EventHandler(btnSend_Click);
        }

        /// <summary>
        /// Обработчик кнопки передачи сообщения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSend_Click(object sender, EventArgs e) {
            if (proxy != null) {
                MessageInfo message = new MessageInfo {
                    User=user,
                    Message=txtMessage.Text
                };
                proxy.SendMessage(message);
                txtMessage.Clear();
            }
        }

        /// <summary>
        /// Обработчик закрытия основной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (proxy != null && user!=null) {
                proxy.Disconnect(user);
                (proxy as IDisposable).Dispose();
            }
        }

        /// <summary>
        /// Обработчик загрузки основной формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_Load(object sender, EventArgs e) {
            TcpCallback callback = new TcpCallback();
            callback.UsersEven += new EventHandler<CallbackEventArgs>(callback_UsersEven);
            callback.MessageEvent += new EventHandler<CallbackEventArgs>(callback_MessageEvent);

            InstanceContext ctx = new InstanceContext(callback);
            proxy = new TcpServiceClient(ctx);

            user = new UserInfo {
                Name = Guid.NewGuid().ToString().Substring(0, 10)
            };

            proxy.Connect(user);
        }

        /// <summary>
        /// Обработчик события MessageEvent объекта обратного вызова
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void callback_MessageEvent(object sender, CallbackEventArgs e) {
            lbMessages.Invoke((Action)(() => {
                string info = string.Format("User: {0}; Message: {1}",
                    e.Message.User.Name, e.Message.Message);
                lbMessages.Items.Add(info);
            }));
        }

        /// <summary>
        /// Обработчик события UsersEven объекта обратного вызова
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void callback_UsersEven(object sender, CallbackEventArgs e) {
            lbOnline.Invoke((Action)(() => {
                lbOnline.Items.Clear();
                lbOnline.Items.AddRange(e.Users.Select(u => u.Name).ToArray());
            }));
        }
    }
}
