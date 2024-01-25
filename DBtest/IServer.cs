using System.Net;

namespace DBtest.Abstraction
{
    public interface IServer
    {
        void Register(MessageUDP message, IPEndPoint fromep);
        void ConfirmMessageReceived(int? id);
        void RelayMessage(MessageUDP message);
        void ProcessMessage(MessageUDP message, IPEndPoint fromep);
        void GetUnreadMessages(string userName);
        void Work();
    }
}
