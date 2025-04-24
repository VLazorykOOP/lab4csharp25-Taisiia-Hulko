using System;

public class MatrixUlong
{
    private ulong[,] ULArray;
    private int n, m;
    private int codeError;
    private static int num_m = 0;

    public MatrixUlong()
    {
        n = 0;
        m = 0;
        ULArray = new ulong[0, 0];
        codeError = 0;
        num_m++;
    }
    //метод для введення елемента матриці
    public void InputElement(int row, int col)
    {
        if (row >= 0 && row < n && col >= 0 && col < m)
        {
            Console.Write($"Введіть елемент [{row},{col}]: ");
            ULArray[row, col] = ulong.Parse(Console.ReadLine());
            codeError = 0;
        }
        else
        {
            Console.WriteLine("Помилка: індекси поза межами матриці.");
            codeError = 1;
        }
    }

    public void InputElement(int index)
    {
        int row = index / m;
        int col = index % m;
        if (row >= 0 && row < n && col >= 0 && col < m)
        {
            Console.Write($"Введіть елемент за індексом {index} (тобто [{row},{col}]): ");
            ULArray[row, col] = ulong.Parse(Console.ReadLine());
            codeError = 0;
        }
        else
        {
            Console.WriteLine("Помилка: індекс поза межами матриці.");
            codeError = 1;
        }
    }


    public MatrixUlong(int rows, int cols)
    {
        n = rows;
        m = cols;
        ULArray = new ulong[n, m];
        codeError = 0;
        num_m++;
    }

    public MatrixUlong(int rows, int cols, ulong defaultValue)
    {
        n = rows;
        m = cols;
        ULArray = new ulong[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                ULArray[i, j] = defaultValue;
        codeError = 0;
        num_m++;
    }

    ~MatrixUlong() { }

    public void Input()
    {
        Console.Write("Введіть кількість рядків: ");
        n = int.Parse(Console.ReadLine());
        Console.Write("Введіть кількість стовпців: ");
        m = int.Parse(Console.ReadLine());
        ULArray = new ulong[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
            {
                Console.Write($"ULArray[{i},{j}] = ");
                ULArray[i, j] = ulong.Parse(Console.ReadLine());
            }
    }

    public void Output()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
                Console.Write($"{ULArray[i, j],6}");
            Console.WriteLine();
        }
    }

    public void Assign(ulong value)
    {
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                ULArray[i, j] = value;
    }

    public static int CountMatrices() => num_m;

    public int Rows => n;
    public int Columns => m;

    public int CodeError
    {
        get => codeError;
        set => codeError = value;
    }

    public ulong this[int i, int j]
    {
        get
        {
            if (i >= 0 && i < n && j >= 0 && j < m)
                return ULArray[i, j];
            codeError = 1;
            return 0;
        }
        set
        {
            if (i >= 0 && i < n && j >= 0 && j < m)
                ULArray[i, j] = value;
            else codeError = 1;
        }
    }

    public ulong this[int k]
    {
        get
        {
            int i = k / m;
            int j = k % m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                return ULArray[i, j];
            codeError = 1;
            return 0;
        }
        set
        {
            int i = k / m;
            int j = k % m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                ULArray[i, j] = value;
            else codeError = 1;
        }
    }

    // ++ --
    public static MatrixUlong operator ++(MatrixUlong a)
    {
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                a.ULArray[i, j]++;
        return a;
    }

    public static MatrixUlong operator --(MatrixUlong a)
    {
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                a.ULArray[i, j]--;
        return a;
    }

    // true / false
    public static bool operator true(MatrixUlong a)
    {
        foreach (ulong el in a.ULArray)
            if (el != 0) return true;
        return false;
    }

    public static bool operator false(MatrixUlong a)
    {
        foreach (ulong el in a.ULArray)
            if (el != 0) return false;
        return true;
    }

    // !
    public static MatrixUlong operator !(MatrixUlong a)
    {
        MatrixUlong result = new MatrixUlong(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                result[i, j] = a[i, j] == 0 ? 1u : 0u;
        return result;
    }

    // ~
    public static MatrixUlong operator ~(MatrixUlong a)
    {
        MatrixUlong result = new MatrixUlong(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                result[i, j] = ~a[i, j];
        return result;
    }

    // арифметика між матрицями
    private static MatrixUlong BinOp(MatrixUlong a, MatrixUlong b, Func<ulong, ulong, ulong> op)
    {
        if (a.n != b.n || a.m != b.m) return a;
        MatrixUlong res = new MatrixUlong(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                res[i, j] = op(a[i, j], b[i, j]);
        return res;
    }

    public static MatrixUlong operator +(MatrixUlong a, MatrixUlong b) => BinOp(a, b, (x, y) => x + y);
    public static MatrixUlong operator -(MatrixUlong a, MatrixUlong b) => BinOp(a, b, (x, y) => x - y);
    public static MatrixUlong operator *(MatrixUlong a, MatrixUlong b) => BinOp(a, b, (x, y) => x * y);
    public static MatrixUlong operator /(MatrixUlong a, MatrixUlong b) => BinOp(a, b, (x, y) => y != 0 ? x / y : 0);
    public static MatrixUlong operator %(MatrixUlong a, MatrixUlong b) => BinOp(a, b, (x, y) => y != 0 ? x % y : 0);

    // з ulong
    public static MatrixUlong operator +(MatrixUlong a, ulong b) => BinOp(a, new MatrixUlong(a.n, a.m, b), (x, y) => x + y);
    public static MatrixUlong operator +(ulong b, MatrixUlong a) => a + b;

    public static MatrixUlong operator -(MatrixUlong a, ulong b) => BinOp(a, new MatrixUlong(a.n, a.m, b), (x, y) => x - y);
    public static MatrixUlong operator -(ulong b, MatrixUlong a) => BinOp(a, new MatrixUlong(a.n, a.m, b), (y, x) => x - y);

    public static MatrixUlong operator *(MatrixUlong a, ulong b) => BinOp(a, new MatrixUlong(a.n, a.m, b), (x, y) => x * y);
    public static MatrixUlong operator /(MatrixUlong a, ulong b) => BinOp(a, new MatrixUlong(a.n, a.m, b), (x, y) => y != 0 ? x / y : 0);

    // порівняння
    public static bool operator ==(MatrixUlong a, MatrixUlong b)
    {
        if (a.n != b.n || a.m != b.m) return false;
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                if (a[i, j] != b[i, j]) return false;
        return true;
    }

    public static bool operator !=(MatrixUlong a, MatrixUlong b) => !(a == b);

    public static bool operator >(MatrixUlong a, MatrixUlong b)
    {
        for (int i = 0; i < Math.Min(a.n, b.n); i++)
            for (int j = 0; j < Math.Min(a.m, b.m); j++)
                if (!(a[i, j] > b[i, j])) return false;
        return true;
    }

    public static bool operator <(MatrixUlong a, MatrixUlong b)
    {
        for (int i = 0; i < Math.Min(a.n, b.n); i++)
            for (int j = 0; j < Math.Min(a.m, b.m); j++)
                if (!(a[i, j] < b[i, j])) return false;
        return true;
    }

    public static bool operator >=(MatrixUlong a, MatrixUlong b) => (a > b) || (a == b);
    public static bool operator <=(MatrixUlong a, MatrixUlong b) => (a < b) || (a == b);

    // побітові
    public static MatrixUlong operator |(MatrixUlong a, MatrixUlong b) => BinOp(a, b, (x, y) => x | y);
    public static MatrixUlong operator ^(MatrixUlong a, MatrixUlong b) => BinOp(a, b, (x, y) => x ^ y);
    public static MatrixUlong operator >>(MatrixUlong a, int shift) => BinOp(a, new MatrixUlong(a.n, a.m, (ulong)shift), (x, y) => x >> (int)y);
    public static MatrixUlong operator <<(MatrixUlong a, int shift) => BinOp(a, new MatrixUlong(a.n, a.m, (ulong)shift), (x, y) => x << (int)y);

    public override bool Equals(object obj) => obj is MatrixUlong other && this == other;
    public override int GetHashCode() => ULArray.GetHashCode();
}

// Тестова програма
class Program
{
    static void Main()
    {
        MatrixUlong A = new MatrixUlong(2, 2, 10);
        MatrixUlong B = new MatrixUlong(2, 2, 5);

        Console.WriteLine("Matrix A:");
        A.Output();

        Console.WriteLine("Matrix B:");
        B.Output();

        Console.WriteLine("A + B:");
        (A + B).Output();

        Console.WriteLine("A * 2:");
        (A * 2).Output();

        Console.WriteLine("A == B: " + (A == B));
        Console.WriteLine("A > B: " + (A > B));

        Console.WriteLine("~A:");
        (~A).Output();

        Console.WriteLine("A | B:");
        (A | B).Output();

        Console.WriteLine("!A:");
        (!A).Output();

        Console.WriteLine("A after ++:");
        (++A).Output();

        Console.WriteLine($"Матриць створено: {MatrixUlong.CountMatrices()}");
    }
}
