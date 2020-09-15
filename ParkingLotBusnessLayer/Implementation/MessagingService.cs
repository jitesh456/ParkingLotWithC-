using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParkingLotBusnessLayer
{
    public class MessagingService :IMessagingService
    {
        private MessageQueue parkinLotQueue = new MessageQueue();

        public MessagingService()
        {
           this.parkinLotQueue.Path = @".\private$\parkInfo";

            if (!MessageQueue.Exists(parkinLotQueue.Path))
            {
                this.parkinLotQueue=MessageQueue.Create(parkinLotQueue.Path);
            }
        }

        public void Send(string message)
        {
            
            parkinLotQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            parkinLotQueue.Send(message);
        }

        public void Receive()
        {
            using (StreamWriter writer = new StreamWriter(@"D:\parking.txt"))
            {
                Message[] messages = parkinLotQueue.GetAllMessages();
                foreach (Message message in messages)
                {
                    message.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                    writer.WriteLine(message.Body.ToString());
                }
            }                                     
        }
    }
}
