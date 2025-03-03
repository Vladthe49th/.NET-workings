class Matrix
{
    private int[,] data;

    public int Rows { get; }
    public int Cols { get; }

    public Matrix(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
        data = new int[rows, cols];
    }

    public int this[int i, int j]
    {
        get => data[i, j];
        set => data[i, j] = value;
    }

    public void FillMatrix(int[,] values)
    {
        if (values.GetLength(0) != Rows || values.GetLength(1) != Cols)
            throw new ArgumentException("Matrix sizes don`t match");

        data = (int[,])values.Clone();
    }

    public void DisplayMatrix()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
                Console.Write(data[i, j] + " ");
            Console.WriteLine();
        }
    }

    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            throw new ArgumentException("Matrixes should have the same size");

        Matrix result = new Matrix(a.Rows, a.Cols);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Cols; j++)
                result[i, j] = a[i, j] + b[i, j];

        return result;
    }

    public static Matrix operator -(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            throw new ArgumentException("Matrixes must be of the same size.");

        Matrix result = new Matrix(a.Rows, a.Cols);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Cols; j++)
                result[i, j] = a[i, j] - b[i, j];

        return result;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.Cols != b.Rows)
            throw new ArgumentException("First matrix cols don`t match second`s rows.");

        Matrix result = new Matrix(a.Rows, b.Cols);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < b.Cols; j++)
                for (int k = 0; k < a.Cols; k++)
                    result[i, j] += a[i, k] * b[k, j];

        return result;
    }

    public static Matrix operator *(Matrix a, int scalar)
    {
        Matrix result = new Matrix(a.Rows, a.Cols);
        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Cols; j++)
                result[i, j] = a[i, j] * scalar;

        return result;
    }

    public static bool operator ==(Matrix a, Matrix b)
    {
        if (a.Rows != b.Rows || a.Cols != b.Cols)
            return false;

        for (int i = 0; i < a.Rows; i++)
            for (int j = 0; j < a.Cols; j++)
                if (a[i, j] != b[i, j])
                    return false;

        return true;
    }

    public static bool operator !=(Matrix a, Matrix b) => !(a == b);

    public override bool Equals(object? obj)
    {
        if (obj is Matrix other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        foreach (var item in data)
            hash = hash * 31 + item.GetHashCode();
        return hash;
    }
}

