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

// OCP - Open-closed principle (принцип открытости-закрытости)
// Программные сущности должны быть открыты для расширения, но закрыты для модификации.

public class BadRectangle
{
    public double Width { get; set; }

    public double Height { get; set; }
}

public class BadCircle
{
    public double Radius { get; set; }
}

public class BadAreaCalculator
{
    // OCP не соблюдается: данный метод закрыт для расширения, так как может обрабатывать
    // только классы Rectangle и Circle, а также открыт для модификации, так как если потребуется добавить поддержку Triangle,
    // то придется модифицировать этот метод.
    public double Area(object[] shapes)
    {
        double area = 0;

        foreach (var shape in shapes)
        {
            if (shape is BadRectangle)
            {
                var rectangle = (BadRectangle)shape;

                area += rectangle.Width * rectangle.Height;
            }
            else
            {
                var circle = (BadCircle)shape;

                area += circle.Radius * circle.Radius * Math.PI;
            }
        }

        return area;
    }
}

public abstract class Shape
{
    public abstract double Area();
}

public class GoodRectangle : Shape
{
    public double Width { get; set; }

    public double Height { get; set; }

    public override double Area() => Width * Height;
}

public class GoodCircle : Shape
{
    public double Radius { get; set; }

    public override double Area() => Radius * Radius * Math.PI;
}

public class GoodAreaCalculator
{
    // OCP соблюдается: теперь каждый подтип класса Shape обрабатывает рассчет площади, используя полиморфизм,
    // что открывает метод Area для расширения и избавляет от необходимости его модификации.
    public double Area(Shape[] shapes)
    {
        double area = 0;

        foreach (var shape in shapes)
        {
            area += shape.Area();
        }

        return area;
    }
}

// LSP - Liskov substitution priciple (принцип подстановки Лисков)
// Объекты в программе могут заменяться экземплярами их подтипов без изменения правильности работы этой программы. Другая формулировка:
// функции, которые используют базовый тип, должны иметь возможность использовать подтипы базового типа не зная об этом.

[Flags]
public enum Color
{
    Red = 1,
    Green = 2,
    Yellow = 4,
    Orange = 16,
}

public class BadApple
{
    public virtual Color GetAvailableColor() => Color.Red | Color.Green | Color.Yellow;
}

public class BadOrange : BadApple
{
    public override Color GetAvailableColor() => Color.Orange;
}

class BadFruitProgram
{
    // LSP не соблюдается: если в метод WriteAppleColorToConsole в качестве параметра передать объект класса BadApple, то
    // на консоль выведется строка "Red, Green, Yellow". Если в этот же метод в качестве параметра передать объект
    // класса BadOrange (дочерний по отношению к BadApple), то на консоль выведется стркоа "Orange". Таким образом, объекты в программе НЕ могут
    // заменяться экземплярами их подтипов без изменения правильности работы этой программы.
    public void WriteAppleColorToConsole(BadApple apple) => Console.Write(apple.GetAvailableColor());
}

public abstract class Fruit
{
    public abstract Color GetAvailableColor();
}

public class GoodApple : Fruit
{
    public override Color GetAvailableColor() => Color.Red | Color.Green | Color.Yellow;
}

public class GoodOrange : Fruit
{
    public override Color GetAvailableColor() => Color.Orange;
}

class GoodFruitProgram
{
    // LSP соблюдается: в метод WriteAppleColorToConsole в качестве параметра можно передать объект любого дочернего класса Fruit и на консоль
    // будет выведена корректная строка.
    public void WriteAppleColorToConsole(Fruit fruit) => Console.Write(fruit.GetAvailableColor());
}