using ClassLibrary1;
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

// Работа метода Info
Earth earth = new Earth();
earth.Info();