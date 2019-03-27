using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

using TcpChatService.Service;
using TcpChatService.Common;

namespace TcpChatService.Forms {
    public partial class MainForm : Form {
        /// <summary>
        /// Поле хоста службы
        /// </summary>
        ServiceHost host = null;
        /// <summary>
        /// Поле объекта службы
        /// </summary>
        TcpService service = null;

        /// <summary>
        /// Конструктор основной формы
        /// </summary>
        public MainForm() {
            InitializeComponent();
            service = new TcpService();
            service.ConnectionEvent += new EventHandler<ConnectionEventArgs>(service_ConnectionEvent);
            btnStart.Click += new EventHandler(btnStart_Click);
            btnStop.Click += new EventHandler(btnStop_Click);
        }

        /// <summary>
        /// Обработчик события подключения/отключения очередного пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void service_ConnectionEvent(object sender, ConnectionEventArgs e) {
            lbConnected.Items.Clear();
            lbConnected.Items.AddRange(e.Users.Select(u => u.Name).ToArray());
        }

        /// <summary>
        /// Обработчик нажатия кнопки Stop (остановка хоста службы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnStop_Click(object sender, EventArgs e) {
            if (host != null) {
                host.Close();

                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки Start (запуск хоста службы)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnStart_Click(object sender, EventArgs e) {
            if (host != null) {
                host.Close();               
            }
            host = new ServiceHost(service);
            host.Opened += new EventHandler(host_Opened);
            host.Closed += new EventHandler(host_Closed);
            host.Open();

            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        /// <summary>
        /// Обработчик закрытия хоста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void host_Closed(object sender, EventArgs e) {
            lblHostStatus.Text = "Status: Stopped";
        }

        /// <summary>
        /// Обработчик открытия хоста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void host_Opened(object sender, EventArgs e) {
            lblHostStatus.Text = "Status: Working";
        }
    }
}
