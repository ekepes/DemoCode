using System.Windows.Forms;

namespace RabbitProducer
{
    public partial class MessageInputView : Form
    {
        private int _rabbitCount;

        public MessageInputView()
        {
            InitializeComponent();
        }

        private void SendARabbitClick(object sender, System.EventArgs e)
        {
            var messageSender = new Sender();
            messageSender.SendMessage(string.Format("Rabbit {0} from Farm {1}", _rabbitCount, FarmName.Text));
            _rabbitCount++;
        }
    }
}
