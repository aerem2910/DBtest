# DBtest

Программа представляет собой простой сервер обмена сообщениями по протоколу UDP с использованием базы данных для хранения информации о пользователях и сообщениях.

1. **Server** - класс, представляющий сервер. Взаимодействует с клиентами через протокол UDP.

    - **Register(MessageUDP message, IPEndPoint fromep)** - метод регистрации нового пользователя. При получении сообщения с командой `Command.Register`, добавляет пользователя в словарь `clients` и сохраняет информацию о пользователе в базу данных.

    - **ConfirmMessageReceived(int? id)** - метод подтверждения получения сообщения. При получении сообщения с командой `Command.Confirmation`, устанавливает флаг `Received` в базе данных для соответствующего сообщения.

    - **RelayMessage(MessageUDP message)** - метод пересылки сообщения. При получении сообщения с командой `Command.Message`, добавляет сообщение в базу данных и отправляет его получателю через протокол UDP.

    - **ProcessMessage(MessageUDP message, IPEndPoint fromep)** - метод обработки полученных сообщений. В зависимости от команды сообщения вызывает соответствующий метод (регистрация, подтверждение или пересылка).

    - **GetUnreadMessages(string userName)** - метод отправки непрочитанных сообщений пользователю. Используется для получения непрочитанных сообщений из базы данных и отправки их через UDP.

    - **Work()** - основной метод, запускающий сервер, ожидающий сообщения через протокол UDP. Бесконечный цикл, который обрабатывает входящие сообщения.

2. **MessageUDP** - класс, представляющий структуру сообщения для обмена между сервером и клиентами.

    - **Command Command** - команда сообщения (регистрация, подтверждение, пересылка и т.д.).

    - **int? Id** - идентификатор сообщения.

    - **string FromName** - имя отправителя.

    - **string ToName** - имя получателя.

    - **string Text** - текст сообщения.

    - **List<string> UnreadMessages** - список непрочитанных сообщений.

    - **string ToJson()** - метод для сериализации объекта в JSON.

    - **static MessageUDP FromJson(string json)** - статический метод для десериализации JSON в объект MessageUDP.

3. **Context** - класс для взаимодействия с базой данных с использованием Entity Framework.

    - **DbSet<User> Users** - набор данных для пользователей.

    - **DbSet<Message> Messages** - набор данных для сообщений.

    - **OnConfiguring(DbContextOptionsBuilder optionsBuilder)** - метод для настройки подключения к базе данных.

    - **OnModelCreating(ModelBuilder modelBuilder)** - метод для настройки отображения классов на таблицы базы данных.
