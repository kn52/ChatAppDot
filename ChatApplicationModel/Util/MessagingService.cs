namespace ChatApplicationModel.Util
{
    using ChatApplicationModel.Model;
    using ChatApplicationModel.Util.Infc;
    using Experimental.System.Messaging;
    using System;
    public class MessagingService : IMessagingService
    {
        private MessageQueue messagQueue = new MessageQueue();
        public MessagingService()
        {
            this.messagQueue.Path = @".\private$\emailsender";
            if (!MessageQueue.Exists(this.messagQueue.Path))
            {
                this.messagQueue = MessageQueue.Create(this.messagQueue.Path);
            }
        }

        public void Send(string sub,string msg,string to)
        {
            this.messagQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Email) });
            this.messagQueue.Send(new Email
            {
                subject = sub,
                message = msg,
                email = to,
            });
            SendEmail.Email();
        }

        public Message[] ReceiveMsg()
        {
            return messagQueue.GetAllMessages();
        }
    } 
}
