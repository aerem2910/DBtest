using DBtest.Abstraction;

namespace DBtest
{
    public class Program
    {
        static void Main(string[] args)
        {
            IMessageSource messageSource = new MessageSource(5430);
            // Создаем экземпляр сервера и запускаем его
            IServer server = new Server(messageSource);
            server.Work();

            // Тестовые данные
            TestRegisterMessage(server);
            TestConfirmationMessage(server);
            TestRelayMessage(server);
            TestGetUnreadMessages(server);

            // Ожидаем, пока сервер продолжит работу
            //Console.ReadLine();
        }

        static void TestRegisterMessage(IServer server)
        {
            // Тестовое сообщение для регистрации
            var registerMessage = new MessageUDP
            {
                Command = Command.Register,
                FromName = "User1"
            };

            // Отправляем тестовое сообщение на сервер для регистрации
            server.ProcessMessage(registerMessage, new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 5430));
        }

        static void TestConfirmationMessage(IServer server)
        {
            // Тестовое сообщение подтверждения
            var confirmationMessage = new MessageUDP
            {
                Command = Command.Confirmation,
                Id = 1 // Предполагается, что это идентификатор сообщения, которое нужно подтвердить
            };

            // Отправляем тестовое сообщение на сервер для подтверждения
            server.ProcessMessage(confirmationMessage, new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 5430));
        }

        static void TestRelayMessage(IServer server)
        {
            // Тестовое сообщение для пересылки
            var relayMessage = new MessageUDP
            {
                Command = Command.Message,
                FromName = "User1",
                ToName = "User2",
                Text = "Привет, как дела?"
            };

            // Отправляем тестовое сообщение на сервер для пересылки
            server.ProcessMessage(relayMessage, new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 5430));
        }

        static void TestGetUnreadMessages(IServer server)
        {
            // Тестовое сообщение для получения непрочитанных сообщений
            var getUnreadMessages = new MessageUDP
            {
                Command = Command.GetUnreadMessages,
                FromName = "User2"
            };

            // Отправляем тестовое сообщение на сервер для получения непрочитанных сообщений
            server.ProcessMessage(getUnreadMessages, new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 5430));
        }
    }
}

