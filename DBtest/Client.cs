using DBtest.Abstraction;
using System.Net;

namespace DBtest
{
    public class Client 
    {
        private readonly string  _name;
        private readonly IMessageSource _messageSource;
        private readonly IPEndPoint _peerEndPoint;
        public Client(IMessageSource messageSource, IPEndPoint peerEndPoint, string name)
        {
            _messageSource = messageSource;
            _peerEndPoint = peerEndPoint;
            _name = name;
        }


        private void Regestred() 
        {
            var messageJson = new MessageUDP()
            {
                Command = Command.Register,
                FromName = _name,

            };
           
            _messageSource.SendMessage(messageJson, _peerEndPoint);
        }


         public void ClientSendler() 
         { 
            Regestred();

            while (true) 
            {
                Console.WriteLine("Enter message");
                string text = Console.ReadLine();
                Console.WriteLine("Enter name");
                string name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                continue;
                            
                 
               
                var messageJson = new MessageUDP()
                {
                    Text = text,
                    FromName = _name,
                    ToName = name,

                };

                _messageSource.SendMessage(messageJson, _peerEndPoint);
            }
        
        
        }

        public void ClientListener() 
        {   

            Regestred();
            IPEndPoint ep = new IPEndPoint(_peerEndPoint.Address, _peerEndPoint.Port);
            while (true) 
            {
               
                MessageUDP message = _messageSource.ReceiveMessage(ref ep);
                Console.WriteLine(message.ToString());

            }
        
        
        }

    }
}
