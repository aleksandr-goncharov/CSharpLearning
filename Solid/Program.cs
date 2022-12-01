using System.Data.SqlClient;
using System.Net.Mail;

// SRP - Single-responsibility principle (принцип единственной ответственности)
// Для каждого класса должно быть определено единственное назначение.
// Все ресурсы, необходимые для его осуществления, должны быть инкапсулированы в этот класс и подчинены только этой задаче.

public class BadRegisterService
{
    // SRP не соблюдается: данный метод и регистрирует пользователя, и добавляет его в БД, и отправляет эл. письмо
    public void RegisterUser(string username)
    {
        if (username == "admin")
        {
            throw new InvalidOperationException();
        }
        
        var connection = new SqlConnection();
        connection.Open();
        // Вставить в БД запись о пользователе
        var command = new SqlCommand("INSERT INTO [...]");

        SmtpClient client = new SmtpClient("smtp.myhost.com");
        // Отправить пользователю письмо
        client.Send(new MailMessage());
    }
}

public class GoodRegisterService
{
    // SRP соблюдается: предыдущая реализация разбита на три отдельных класса, каждый из которых имеет единственное назначение
    public void RegisterUser(string username)
    {
        if (username == "admin")
            throw new InvalidOperationException();

        //_userRepository.Insert(...);

        //_emailService.Send(...);
    }
}