using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RabbitProducer
{
    public partial class MessageInputView : Form
    {
        private int rabbitCount = 0;

        public MessageInputView()
        {
            InitializeComponent();
        }

        private void SendARabbitClick(object sender, System.EventArgs e)
        {
            Sender messageSender = new Sender();
            messageSender.SendMessage(string.Format("Rabbit {0} from Farm {1}", rabbitCount, FarmName.Text));
            rabbitCount++;
        }
    }
}
