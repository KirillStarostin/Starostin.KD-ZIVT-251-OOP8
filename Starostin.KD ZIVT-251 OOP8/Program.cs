using ClassLibrary1;
using System;
using System.Linq;
using System.Numerics;
using System.Reflection;

// Метод вывода информации о полях, свойствах и методах класса
static void PrintMembers(Type t)
{
    Console.WriteLine($"\nКласс {t.Name.ToUpper()}");
    // Поля
    Console.WriteLine("ПОЛЯ");
    foreach (FieldInfo f in t.GetFields(
        BindingFlags.Public | BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.Static))
    {
        string mods = f.IsPublic ? "public" :
                      f.IsFamily ? "protected" :
                      f.IsPrivate ? "private" : "internal";
        Console.WriteLine($"{mods,-9} {f.FieldType.Name,-10} {f.Name}");
    }

    // Свойства
    Console.WriteLine("СВОЙСТВА");
    foreach (PropertyInfo p in t.GetProperties(
        BindingFlags.Public | BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.Static))
    {
        string get = p.GetMethod != null ? "get" : "   ";
        string set = p.SetMethod != null ? "set" : "   ";
        Console.WriteLine($"{p.PropertyType.Name,-10} {p.Name,-12} [{get} {set}]");
    }

    // Методы
    Console.WriteLine("МЕТОДЫ");
    foreach (MethodInfo m in t.GetMethods(
        BindingFlags.Public | BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.Static |
        BindingFlags.DeclaredOnly))
    {
        string mods = m.IsPublic ? "public" :
                      m.IsFamily ? "protected" :
                      m.IsPrivate ? "private" : "internal";
        string pars = string.Join(", ",
            m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
        Console.WriteLine($"{mods,-9} {m.ReturnType.Name,-8} {m.Name}({pars})");
    }
}

// Метод чтения полей и свойств экземпляра
static void ReadFieldsAndProps(object obj)
{
    Type t = obj.GetType();
    Console.WriteLine($"\nЧТЕНИЕ {t.Name}");

    // Чтение полей
    Console.WriteLine("ПОЛЯ");
    foreach (FieldInfo f in t.GetFields(
        BindingFlags.Public | BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.Static))
    {
        object value = f.GetValue(obj);
        Console.WriteLine($"{f.Name,-10} = {value}");
    }

    // Чтение свойств
    Console.WriteLine("СВОЙСТВА");
    foreach (PropertyInfo p in t.GetProperties(
        BindingFlags.Public | BindingFlags.NonPublic |
        BindingFlags.Instance | BindingFlags.Static))
    {
        if (p.GetMethod == null) continue;
        if (p.GetIndexParameters().Length > 0) continue;

        object value = p.GetValue(obj);
        Console.WriteLine($"{p.Name,-10} = {value}");
    }
}

// Метод вывода информации о классе
static void LoadAssemblyAndInspect(Type t)
{
    Console.WriteLine("\nЗАГРУЗКА DLL");
    Assembly asm = t.Assembly;
    Console.WriteLine($"Сборка: {asm.FullName}");

    // Получить конкретный класс
    Console.WriteLine($"\nИнформация о {t.FullName}\n");
    Console.WriteLine($"Базовый класс: {t.BaseType?.Name}");
    Console.WriteLine($"Абстрактный:   {t.IsAbstract}");
    Console.WriteLine($"Запечатан:     {t.IsSealed}");
    Console.WriteLine($"Интерфейсы:    {string.Join(", ", t.GetInterfaces().Select(i => i.Name))}");

    // Найти всех наследников Planet
    Console.WriteLine("\nНаследники");
    var subclasses = asm.GetTypes()
        .Where(x => x.IsSubclassOf(t) && !x.IsAbstract)
        .OrderBy(x => x.Name);

    if (!subclasses.Any())
    {
        Console.WriteLine("Наследников нет");
    }
    else
    {
        foreach (Type s in subclasses)
            Console.WriteLine($"  {s.Name}");
    }
}

// Вывод информации о полях, свойствах и методах всех классов
PrintMembers(typeof(Planet));
PrintMembers(typeof(Earth));
PrintMembers(typeof(Mercury));
PrintMembers(typeof(Venera));
PrintMembers(typeof(Mars));
PrintMembers(typeof(Jupiter));
PrintMembers(typeof(Saturn));
PrintMembers(typeof(Uran));
PrintMembers(typeof(Neptun));

Console.ReadKey();

// Чтение полей и свойств экземпляра earth
Earth earth = new Earth();
Console.WriteLine($"\n\n\nЧтение полей и свойств {earth.Name}");
ReadFieldsAndProps(earth);
Console.WriteLine();

Console.ReadKey();

// Изменение поля
Planet planet = new Planet();
Console.WriteLine($"\nИзменение поля {planet.Name}");
Type t = typeof(Planet);
FieldInfo nameField = t.GetField("Name");
nameField.SetValue(planet, "Pluton");
planet.Info();

Console.ReadKey();

// Изменение свойства
Console.WriteLine($"\nИзменение свойства {planet.Name}");
PropertyInfo nameProp = t.GetProperty("Name2");
nameProp.SetValue(planet, "Pluton-2");
Console.WriteLine($"Новое имя = {planet.Name2}");

Console.ReadKey();

// Вызов методов
Console.WriteLine($"\nВызов метода уничтожения {t.Name}");
MethodInfo explosion = t.GetMethod("Explosion",
        BindingFlags.NonPublic | BindingFlags.Instance);
explosion.Invoke(planet, null);
planet.Info();

Console.ReadKey();

// Вызов конструкторов
Console.WriteLine("\nВызов конструкторов Planet");

// Конструктор без параметров
ConstructorInfo ctorDefault = t.GetConstructor(Type.EmptyTypes);
Planet p1 = (Planet)ctorDefault.Invoke(null);
Console.WriteLine("Без параметров:");
p1.Info();

// Конструктор с параметрами
ConstructorInfo ctorArgs = t.GetConstructor(new[]
{
        typeof(string), typeof(int), typeof(int)
    });
Planet p2 = (Planet)ctorArgs.Invoke(new object[]
{
        "Кириллий", 185, 60
});
Console.WriteLine("С параметрами:");
p2.Info();

Console.ReadKey();

LoadAssemblyAndInspect(typeof(Planet));
LoadAssemblyAndInspect(typeof(Earth));

Console.ReadKey();