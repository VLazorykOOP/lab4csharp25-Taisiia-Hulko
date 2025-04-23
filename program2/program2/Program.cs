using System;

class Program
{
    static void Main(string[] args)
    {
        // Викликаємо метод тестування класу VectorULong
        VectorULong.Test();
    }
}

class VectorULong
{
    // Поля
    protected ulong[] IntArray; // масив елементів
    protected uint size; // розмір вектора
    protected int codeError; // код помилки(1 - помилка індексації, 0 - все ок)
    protected static uint num_vec = 0; // кількість векторів

    // Властивість для розміру (тільки читання)
    public uint Size => size;

    // Властивість для codeError (читання і запис помилки)
    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

    // Індексатор: доступ для елементів за індексом з перевіркою меж
    public ulong this[int index]
    {
        get
        {
            if (index >= 0 && index < size)
            {
                codeError = 0;
                return IntArray[index];
            }
            else
            {
                codeError = 1;
                return 0;
            }
        }
        set
        {
            if (index >= 0 && index < size)
            {
                codeError = 0;
                IntArray[index] = value;
            }
            else
            {
                codeError = 1;
            }
        }
    }

    // Конструктор без параметрів. створення вектора з одним елементом 0
    public VectorULong()
    {
        size = 1;
        IntArray = new ulong[size];
        IntArray[0] = 0;
        num_vec++;
    }

    // Конструктор з параметром (заданий розмір нулями)
    public VectorULong(uint s)
    {
        size = s;
        IntArray = new ulong[size];
        for (int i = 0; i < size; i++) IntArray[i] = 0;
        num_vec++;
    }

    // Конструктор з розміром і значенням
    public VectorULong(uint s, ulong initVal)
    {
        size = s;
        IntArray = new ulong[size];
        for (int i = 0; i < size; i++) IntArray[i] = initVal;
        num_vec++;
    }

    // Деструктор: повідомлення про знищення вектора
    ~VectorULong()
    {
        Console.WriteLine("Вектор знищено.");
    }

    // Метод введення елементів
    public void Input()
    {
        Console.WriteLine("Введіть елементи вектора:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"[{i}] = ");
            IntArray[i] = Convert.ToUInt64(Console.ReadLine());
        }
    }

    // Метод виведення елементів
    public void Output()
    {
        Console.Write("Вектор: ");
        for (int i = 0; i < size; i++)
        {
            Console.Write(IntArray[i] + " ");
        }
        Console.WriteLine();
    }

    // Метод присвоєння всім елементам значення
    public void Assign(ulong value)
    {
        for (int i = 0; i < size; i++)
        {
            IntArray[i] = value;
        }
    }

    // Статичний метод — скільки векторів створено
    public static uint GetVectorCount()
    {
        return num_vec;
    }

    // Перевантаження унарного ++ ( збільшує кожен елемент на 1)
    public static VectorULong operator ++(VectorULong v)
    {
        for (int i = 0; i < v.size; i++)
        {
            v.IntArray[i]++;
        }
        return v;
    }

    // Перевантаження унарного -- ( зменшує кожен елемент на 1)
    public static VectorULong operator --(VectorULong v)
    {
        for (int i = 0; i < v.size; i++)
        {
            v.IntArray[i]--;
        }
        return v;
    }

    // Перевантаження true(якщо size > 0 або елементи не 0)/false
    public static bool operator true(VectorULong v)
    {
        if (v.size == 0) return false;
        foreach (var val in v.IntArray)
            if (val != 0) return true;
        return false;
    }

    //Перевантаження false
    public static bool operator false(VectorULong v)
    {
        return !(v);
    }

    // Перевантаження логічного заперечння ! 
    public static bool operator !(VectorULong v)
    {
        return v.size == 0;
    }

    // Перевантаження побітового заперечення ~
    public static VectorULong operator ~(VectorULong v)
    {
        VectorULong result = new VectorULong(v.size);
        for (int i = 0; i < v.size; i++)
        {
            result.IntArray[i] = ~v.IntArray[i];
        }
        return result;
    }

    // Перевантаження арифметичних операцій
    // Додавання
    public static VectorULong operator +(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        VectorULong result = new VectorULong(minSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v1.IntArray[i] + v2.IntArray[i];
        }
        return result;
    }

    // Віднімання
    public static VectorULong operator -(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        VectorULong result = new VectorULong(minSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v1.IntArray[i] - v2.IntArray[i];
        }
        return result;
    }

    // Множення
    public static VectorULong operator *(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        VectorULong result = new VectorULong(minSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v1.IntArray[i] * v2.IntArray[i];
        }
        return result;
    }

    // Ділення
    public static VectorULong operator /(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        VectorULong result = new VectorULong(minSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v2.IntArray[i] != 0 ? v1.IntArray[i] / v2.IntArray[i] : 0;
        }
        return result;
    }

    // Операція побітового OR
    public static VectorULong operator |(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        VectorULong result = new VectorULong(minSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v1.IntArray[i] | v2.IntArray[i];
        }
        return result;
    }

    // Метод тестування
    public static void Test()
    {
        VectorULong v1 = new VectorULong(3, 5);
        v1.Output();

        VectorULong v2 = new VectorULong(3);
        v2.Input();
        v2.Output();

        v1.Assign(2);
        v1.Output();

        v1++;
        v1.Output();

        Console.WriteLine("Кількість векторів: " + VectorULong.GetVectorCount());

        // Тестування арифметичних операцій
        VectorULong v3 = v1 + v2;
        v3.Output();

        VectorULong v4 = v1 - v2;
        v4.Output();

        VectorULong v5 = v1 * v2;
        v5.Output();

        VectorULong v6 = v1 / v2;
        v6.Output();

        // Тестування побітових операцій
        VectorULong v7 = v1 | v2;
        v7.Output();
    }
}