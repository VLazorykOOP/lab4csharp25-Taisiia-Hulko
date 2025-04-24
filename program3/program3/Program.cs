using System;

class MatrixUlong
{
    // Захищені поля
    protected ulong[,] ULArray;
    protected uint n, m;
    protected int codeError;
    protected static int num_m = 0;

    public MatrixUlong()
    {
        n = m = 1;
        ULArray = new ulong[1, 1];
        ULArray[0, 0] = 0;
        num_m++;
    }

    public MatrixUlong(uint rows, uint cols)
    {
        n = rows;
        m = cols;
        ULArray = new ulong[n, m];
        codeError = 0;
        num_m++;
    }

    public MatrixUlong(uint rows, uint cols, ulong initValue)
    {
        n = rows;
        m = cols;
        ULArray = new ulong[n, m];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                ULArray[i, j] = initValue;
        codeError = 0;
        num_m++;
    }

    ~MatrixUlong()
    {
        Console.WriteLine("Matrix destroyed");
    }

    public void Input()
    {
        Console.WriteLine("Enter matrix elements:");
        for (int i = 0; i < n; i++)
            for (int j = 0; j < m; j++)
                ULArray[i, j] = ulong.Parse(Console.ReadLine());
    }

    public void Output()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
                Console.Write(ULArray[i, j] + " ");
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

    public uint Rows => n;
    public uint Columns => m;

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
            codeError = -1;
            return 0;
        }
        set
        {
            if (i >= 0 && i < n && j >= 0 && j < m)
                ULArray[i, j] = value;
            else
                codeError = -1;
        }
    }

    public ulong this[int k]
    {
        get
        {
            int i = k / (int)m;
            int j = k % (int)m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                return ULArray[i, j];
            codeError = -1;
            return 0;
        }
        set
        {
            int i = k / (int)m;
            int j = k % (int)m;
            if (i >= 0 && i < n && j >= 0 && j < m)
                ULArray[i, j] = value;
            else
                codeError = -1;
        }
    }

    public static MatrixUlong operator ++(MatrixUlong mat)
    {
        for (int i = 0; i < mat.n; i++)
            for (int j = 0; j < mat.m; j++)
                mat.ULArray[i, j]++;
        return mat;
    }

    public static MatrixUlong operator --(MatrixUlong mat)
    {
        for (int i = 0; i < mat.n; i++)
            for (int j = 0; j < mat.m; j++)
                mat.ULArray[i, j]--;
        return mat;
    }

    public static bool operator true(MatrixUlong mat)
    {
        if (mat.n == 0 || mat.m == 0) return false;
        for (int i = 0; i < mat.n; i++)
            for (int j = 0; j < mat.m; j++)
                if (mat.ULArray[i, j] != 0)
                    return true;
        return false;
    }

    public static bool operator false(MatrixUlong mat) => !(mat);

    public static bool operator !(MatrixUlong mat)
    {
        return mat.n == 0 || mat.m == 0;
    }

    public static MatrixUlong operator ~(MatrixUlong mat)
    {
        MatrixUlong result = new MatrixUlong(mat.n, mat.m);
        for (int i = 0; i < mat.n; i++)
            for (int j = 0; j < mat.m; j++)
                result.ULArray[i, j] = ~mat.ULArray[i, j];
        return result;
    }

    // Бінарні арифметичні оператори
    public static MatrixUlong operator +(MatrixUlong a, MatrixUlong b)
    {
        MatrixUlong result = new MatrixUlong(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                result[i, j] = a[i, j] + b[i, j];
        return result;
    }

    public static MatrixUlong operator -(MatrixUlong a, MatrixUlong b)
    {
        MatrixUlong result = new MatrixUlong(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                result[i, j] = a[i, j] - b[i, j];
        return result;
    }

    public static MatrixUlong operator *(MatrixUlong a, MatrixUlong b)
    {
        MatrixUlong result = new MatrixUlong(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                result[i, j] = a[i, j] * b[i, j];
        return result;
    }

    public static MatrixUlong operator /(MatrixUlong a, MatrixUlong b)
    {
        MatrixUlong result = new MatrixUlong(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                result[i, j] = b[i, j] != 0 ? a[i, j] / b[i, j] : 0;
        return result;
    }

    public static MatrixUlong operator %(MatrixUlong a, MatrixUlong b)
    {
        MatrixUlong result = new MatrixUlong(a.n, a.m);
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                result[i, j] = b[i, j] != 0 ? a[i, j] % b[i, j] : 0;
        return result;
    }

    // Порівняння
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
        ulong sumA = 0, sumB = 0;
        for (int i = 0; i < a.n; i++)
            for (int j = 0; j < a.m; j++)
                sumA += a[i, j];
        for (int i = 0; i < b.n; i++)
            for (int j = 0; j < b.m; j++)
                sumB += b[i, j];
        return sumA > sumB;
    }

    public static bool operator <(MatrixUlong a, MatrixUlong b) => b > a;

    public static bool operator >=(MatrixUlong a, MatrixUlong b) => !(a < b);

    public static bool operator <=(MatrixUlong a, MatrixUlong b) => !(a > b);

    public override bool Equals(object obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();
}

class Program
{
    static void Main()
    {
        MatrixUlong m1 = new MatrixUlong(2, 2, 5);
        m1.Output();

        Console.WriteLine("After ++:");
        m1++;
        m1.Output();

        Console.WriteLine("Matrix Count: " + MatrixUlong.CountMatrices());
    }
}
