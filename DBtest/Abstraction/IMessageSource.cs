using System.Net;

namespace DBtest.Abstraction
{
    public interface IMessageSource
    {
        void SendMessage(MessageUDP message, IPEndPoint endPoint);
        MessageUDP ReceiveMessage(ref IPEndPoint endPoint);
    }
}
