using System;

class DRomb
{
    protected int d1, d2;
    protected int color;

    public DRomb(int d1, int d2, int color)
    {
        this.d1 = d1;
        this.d2 = d2;
        this.color = color;
    }

    public int D1
    {
        get { return d1; }
        set { d1 = value; }
    }

    public int D2
    {
        get { return d2; }
        set { d2 = value; }
    }

    public int Color
    {
        get { return color; }
    }

    public void DisplayDimensions()
    {
        Console.WriteLine($"Діагоналі ромба: D1 = {d1}, D2 = {d2}");
    }

    public double CalculatePerimeter()
    {
        double sideLength = Math.Sqrt(Math.Pow(d1 / 2.0, 2) + Math.Pow(d2 / 2.0, 2));
        return 4 * sideLength;
    }

    public double CalculateArea()
    {
        return (d1 * d2) / 2.0;
    }

    public bool IsSquare()
    {
        return d1 == d2;
    }

    public void DisplayColor()
    {
        Console.WriteLine($"Колір ромба: {GetColorName()}");
    }

    private string GetColorName()
    {
        return color switch
        {
            1 => "Червоний",
            2 => "Синій",
            3 => "Зелений",
            4 => "Жовтий",
            _ => "Невідомий"
        };
    }

    // Індексатор для доступу до полів по індексу
    public int this[int index]  //індексатор. клас ромба тепер поводиться як масив
    {
        get
        {
            return index switch 
            {
                0 => d1,
                1 => d2,
                2 => color,
                _ => throw new IndexOutOfRangeException("Недійсний індекс для ромба")
            };
        }
        set
        {
            switch (index)
            {
                case 0: d1 = value; break;
                case 1: d2 = value; break;
                case 2: color = value; break;
                default: throw new IndexOutOfRangeException("Недійсний індекс для ромба");
            }
        }
    }

    // Перевантаження операції ++(інкремент)
    public static DRomb operator ++(DRomb r) 
    {
        r.d1++; // Збільшуємо d1 на 1
        r.d2++;
        return r;
    }

    // Перевантаження операції --(декремент)
    public static DRomb operator --(DRomb r)
    {
        r.d1--; // Зменшуємо d1 на 1
        r.d2--;
        return r;
    }

    // Перевантаження true
    public static bool operator true(DRomb r) => r.d1 == r.d2; 

    // Перевантаження false
    public static bool operator false(DRomb r) => r.d1 != r.d2;

    // Перевантаження операції +
    public static DRomb operator +(DRomb r, int scalar) //додавання скалярного значення
    {
        return new DRomb(r.d1 + scalar, r.d2 + scalar, r.color);
    }

    // Перетворення DRomb у string
    public static explicit operator string(DRomb r)
    {
        return $"{r.d1},{r.d2},{r.color}";
    }

    // Перетворення string у DRomb
    public static explicit operator DRomb(string s)
    {
        var parts = s.Split(',');
        if (parts.Length != 3)
            throw new FormatException("Неправильний формат для перетворення в DRomb");

        int d1 = int.Parse(parts[0]);
        int d2 = int.Parse(parts[1]);
        int color = int.Parse(parts[2]);

        return new DRomb(d1, d2, color);
    }
}

class Program
{
    static void Main(string[] args)
    {
        DRomb[] rombs = {
            new DRomb(10, 15, 1),
            new DRomb(5, 5, 2),
            new DRomb(8, 12, 3)
        };

        int squareCount = 0;

        foreach (var romb in rombs)
        {
            romb.DisplayDimensions();
            romb.DisplayColor();
            Console.WriteLine($"Периметр ромба: {romb.CalculatePerimeter():F2}");
            Console.WriteLine($"Площа ромба: {romb.CalculateArea():F2}");

            if (romb.IsSquare())
            {
                squareCount++;
                Console.WriteLine("Це квадрат.");
            }
            else
            {
                Console.WriteLine("Це не квадрат.");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Кількість квадратів у масиві: {squareCount}");
    }
}