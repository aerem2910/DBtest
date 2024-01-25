using Moq;
using System.Net;
using System.Text;
using DBtest;
using DBtest.Abstraction;

namespace DBtest
{
    public class Tests
    {
        /*
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        */
        [Test]
        public void TestClientSender()
        {

            var messageSourceMock = new Mock<IMessageSource>();
            var peerEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5430);
            var client = new Client(messageSourceMock.Object, peerEndPoint, "User1");


            client.ClientSendler();


            messageSourceMock.Verify(m => m.SendMessage(It.IsAny<MessageUDP>(), peerEndPoint), Times.Once);
        }

        [Test]
        public void TestClientListener_RegisterCommand()
        {

            var messageSourceMock = new Mock<IMessageSource>();
            var peerEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5430);
            var client = new Client(messageSourceMock.Object, peerEndPoint, "User1");
            var registerMessage = new MessageUDP { Command = Command.Register, FromName = "User2" };
            var registerMessageJson = registerMessage.ToJson();
            var registerBytes = Encoding.UTF8.GetBytes(registerMessageJson);

            messageSourceMock.Setup(m => m.ReceiveMessage(ref peerEndPoint)).Returns(registerMessage);


            client.ClientListener();


            Console.WriteLine("Check the console output for registration message.");
        }

        [Test]
        public void TestClientListener_MessageCommand()
        {
            var messageSourceMock = new Mock<IMessageSource>();
            var peerEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5430);
            var client = new Client(messageSourceMock.Object, peerEndPoint, "User1");
            var messageText = "Hello from User2!";
            var message = new MessageUDP { Command = Command.Message, Text = messageText };
            var messageJson = message.ToJson();
            var messageBytes = Encoding.UTF8.GetBytes(messageJson);

            messageSourceMock.Setup(m => m.ReceiveMessage(ref peerEndPoint)).Returns(message);

            
            client.ClientListener();

         
            Console.WriteLine($"Check the console output for the received message: {messageText}");
        }
    }
}
