using System;

class Program
{
    static void Main(string[] args)
    {
        VectorULong.Test();
    }
}

class VectorULong
{
    protected ulong[] IntArray;
    protected uint size;
    protected int codeError;
    protected static uint num_vec = 0;

    public uint Size => size;

    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

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

    public VectorULong()
    {
        size = 1;
        IntArray = new ulong[size];
        IntArray[0] = 0;
        num_vec++;
    }

    public VectorULong(uint s)
    {
        size = s;
        IntArray = new ulong[size];
        for (int i = 0; i < size; i++) IntArray[i] = 0;
        num_vec++;
    }

    public VectorULong(uint s, ulong initVal)
    {
        size = s;
        IntArray = new ulong[size];
        for (int i = 0; i < size; i++) IntArray[i] = initVal;
        num_vec++;
    }

    ~VectorULong()
    {
        Console.WriteLine("Вектор знищено.");
    }

    public void Input()
    {
        Console.WriteLine("Введіть елементи вектора:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"[{i}] = ");
            IntArray[i] = Convert.ToUInt64(Console.ReadLine());
        }
    }

    public void Output()
    {
        Console.Write("Вектор: ");
        for (int i = 0; i < size; i++)
        {
            Console.Write(IntArray[i] + " ");
        }
        Console.WriteLine();
    }

    public void Assign(ulong value)
    {
        for (int i = 0; i < size; i++)
        {
            IntArray[i] = value;
        }
    }

    public static uint GetVectorCount()
    {
        return num_vec;
    }

    public static VectorULong operator ++(VectorULong v)
    {
        for (int i = 0; i < v.size; i++)
        {
            v.IntArray[i]++;
        }
        return v;
    }

    public static VectorULong operator --(VectorULong v)
    {
        for (int i = 0; i < v.size; i++)
        {
            v.IntArray[i]--;
        }
        return v;
    }

    public static bool operator true(VectorULong v)
    {
        if (v.size == 0) return false;
        foreach (var val in v.IntArray)
            if (val != 0) return true;
        return false;
    }

    public static bool operator false(VectorULong v)
    {
        return !(v);
    }

    public static bool operator !(VectorULong v)
    {
        return v.size == 0;
    }

    public static VectorULong operator ~(VectorULong v)
    {
        VectorULong result = new VectorULong(v.size);
        for (int i = 0; i < v.size; i++)
        {
            result.IntArray[i] = ~v.IntArray[i];
        }
        return result;
    }

    private static VectorULong ElementwiseOperation(VectorULong v, ulong scalar, Func<ulong, ulong, ulong> op)
    {
        VectorULong result = new VectorULong(v.size);
        for (int i = 0; i < v.size; i++)
        {
            result.IntArray[i] = op(v.IntArray[i], scalar);
        }
        return result;
    }

    public static VectorULong operator +(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v1.IntArray[i] + v2.IntArray[i];
        }
        return result;
    }

    public static VectorULong operator +(VectorULong v, ulong scalar) => ElementwiseOperation(v, scalar, (a, b) => a + b);

    public static VectorULong operator -(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v1.IntArray[i] - v2.IntArray[i];
        }
        return result;
    }

    public static VectorULong operator -(VectorULong v, ulong scalar) => ElementwiseOperation(v, scalar, (a, b) => a - b);

    public static VectorULong operator *(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v1.IntArray[i] * v2.IntArray[i];
        }
        return result;
    }

    public static VectorULong operator *(VectorULong v, ulong scalar) => ElementwiseOperation(v, scalar, (a, b) => a * b);

    public static VectorULong operator /(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v2.IntArray[i] != 0 ? v1.IntArray[i] / v2.IntArray[i] : 0;
        }
        return result;
    }

    public static VectorULong operator /(VectorULong v, ulong scalar) => ElementwiseOperation(v, scalar, (a, b) => b != 0 ? a / b : 0);

    public static VectorULong operator %(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v2.IntArray[i] != 0 ? v1.IntArray[i] % v2.IntArray[i] : 0;
        }
        return result;
    }

    public static VectorULong operator %(VectorULong v, ulong scalar) => ElementwiseOperation(v, scalar, (a, b) => b != 0 ? a % b : 0);

    public static VectorULong operator |(VectorULong v1, VectorULong v2)
    {
        uint minSize = Math.Min(v1.size, v2.size);
        uint maxSize = Math.Max(v1.size, v2.size);
        VectorULong result = new VectorULong(maxSize);
        for (int i = 0; i < minSize; i++)
        {
            result.IntArray[i] = v1.IntArray[i] | v2.IntArray[i];
        }
        return result;
    }

    public static bool operator ==(VectorULong v1, VectorULong v2)
    {
        if (v1.size != v2.size) return false;
        for (int i = 0; i < v1.size; i++)
        {
            if (v1.IntArray[i] != v2.IntArray[i]) return false;
        }
        return true;
    }

    public static bool operator !=(VectorULong v1, VectorULong v2)
    {
        return !(v1 == v2);
    }

    public override bool Equals(object obj)
    {
        return obj is VectorULong other && this == other;
    }

    public override int GetHashCode()
    {
        return IntArray.GetHashCode();
    }

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

        VectorULong v3 = v1 + v2;
        v3.Output();

        VectorULong v4 = v1 - v2;
        v4.Output();

        VectorULong v5 = v1 * v2;
        v5.Output();

        VectorULong v6 = v1 / v2;
        v6.Output();

        VectorULong v7 = v1 % v2;
        v7.Output();

        VectorULong v8 = v1 | v2;
        v8.Output();

        VectorULong v9 = v1 + 10;
        v9.Output();

        VectorULong v10 = v1 * 3;
        v10.Output();

        Console.WriteLine("v1 == v2: " + (v1 == v2));
    }
}
