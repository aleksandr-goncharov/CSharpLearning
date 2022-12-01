// Версии до C# 8.0: интерфейс может содержать методы, свойства, события, индексаторы.
// По сути подобен абстрактному базовому классу, содержащему только абстрактные элементы.
interface IInterface
{
    void Method();

    int Property { get; set; }

    event Action<int>? Event;

    int this[int index] { get; set; }

}

// Версия C# 8.0 и новее: интерфейс может содержать: методы, свойства, события, индексаторы
// + константы, операторы, статический конструктор, вложенные типы, статические члены.
// Каждый член интерфейса может содержать реализацию.
// При реализации члена к нему можно получить доступ только через экземпляр интерфейса.
// Поля с модификатором private и (или) static обязательно должны содержать реализацию.
// Наиболее распространенным сценарием использования членов с реализацией является безопасное добавление членов в интерфейс,
// который уже выпущен и используется многочисленными клиентами (классами, структурами).

interface INewInterface
{
    public const int ConstNumber = 100;

    public static int StaticNumber = 50;

    void Method()
    {
        Console.WriteLine("Hello!");
    }

    static void StaticMethod()
    {
        Console.WriteLine("Hello from static method!");
    }

    private void PrivateMethod()
    {
        Console.WriteLine("Hello from private method!");
    }

}
