object manager = new Manager();
// Object.GetType() - это невиртуальный метод, который возвращает тип,
// содержащийся в переменной
Console.WriteLine(manager.GetType().Name);
PromoteEmployee(manager);

object dateTime = new DateTime(2022, 1, 1);
Console.WriteLine(dateTime.GetType().Name);
// Исключение InvalidCastException
PromoteEmployee(dateTime);

// Если object поменяем на Employee, то получим ошибку на этапе компиляции
static void PromoteEmployee(object o)
{
    // Можем использовать оператор is или as, чтобы избежать исключения, но
    // as в результате будет производительнее, чем is
    Employee e = (Employee)o;
}

internal class Employee
{

}
internal class Manager : Employee
{

}