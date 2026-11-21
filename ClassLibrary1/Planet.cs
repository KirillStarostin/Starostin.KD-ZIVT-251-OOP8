namespace ClassLibrary1
{
    // Интерфейс
    interface IPlanet
    {
        void Info();
    }
    // Класс планета
    public class Planet : IPlanet
    {
        // Поля
        public string Name;
        private int Radius;
        protected int Weight;

        public string Name2
        {
            get { return Name; }
            set { Name = value; }
        }

        // Свойства
        protected int Radius2
        {
            get { return Radius; }
            set { Radius = value; }
        }

        private int Weight2
        {
            get { return Weight; }
            set { Weight = value; }
        }

        //Методы
        public void Info()
        {
            Console.WriteLine($"Наименование {Name} | Радиус = {Radius} | Вес = {Weight}");
        }

        private bool IsValidMassAndRadius(int weight, int radius)
        {
            return weight > 0 && radius > 0;
        }

        protected void Explosion()
        {
            Weight = 0;
            Radius = 0;
            Console.WriteLine($"Планета {Name} уничтожена");
            Name += "*destroyed";
        }


        // Конструктор без параметров
        public Planet()
        {
            Name = "Плутоний";
            Radius = 453246215;
            Weight = 323533214;
        }

        // Конструктор с параметрами
        public Planet(string name, int radius, int weight)
        {
            if (IsValidMassAndRadius(weight, radius))
            {
                Name = name;
                Radius = radius;
                Weight = weight;
            }
            else { Console.WriteLine("Ошибка: Некорректные значения радиуса и/или веса"); }
        }
    }

    // Singleton классы всех планет солнечной системы
    public sealed class Earth : Planet
    {
        private static readonly Earth _instance = new Earth();
        public static Earth Instance => _instance;
        public Earth() : base("Земля", 54573768, 85685648) { }
    }

    public sealed class Jupiter : Planet
    {
        private static readonly Jupiter _instance = new Jupiter();
        public static Jupiter Instance => _instance;
        public Jupiter() : base("Юпитер", 6843877, 79458638) { }
    }

    public sealed class Venera : Planet
    {
        private static readonly Venera _instance = new Venera();
        public static Venera Instance => _instance;
        public Venera() : base("Венера", 568386, 4586990) { }
    }

    public sealed class Mars : Planet
    {
        private static readonly Mars _instance = new Mars();
        public static Mars Instance => _instance;
        public Mars() : base("Марс", 7946800, 57805686) { }
    }

    public sealed class Mercury : Planet
    {
        private static readonly Mercury _instance = new Mercury();
        public static Mercury Instance => _instance;
        public Mercury() : base("Меркурий", 75997, 798085975) { }
    }

    public sealed class Saturn : Planet
    {
        private static readonly Saturn _instance = new Saturn();
        public static Saturn Instance => _instance;
        public Saturn() : base("Сатурн", 80709797, 79576859) { }
    }

    public sealed class Uran : Planet
    {
        private static readonly Uran _instance = new Uran();
        public static Uran Instance => _instance;
        public Uran() : base("Уран", 7074979, 5879476) { }
    }

    public sealed class Neptun : Planet
    {
        private static readonly Neptun _instance = new Neptun();
        public static Neptun Instance => _instance;
        public Neptun() : base("Нептун", 7946864, 68468436) { }
    }
}
