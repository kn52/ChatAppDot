namespace ChatApplicationModel.Util.Infc
{
    using Experimental.System.Messaging;
    public interface IMessagingService
    {
        void Send(string sub,string msg, string to);
        Message[] ReceiveMsg();
    }
}
