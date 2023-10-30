using System.Collections;
namespace LinearAlgebra;

public class MathVector : IMathVector
{
    private double[] coords;

    public int Dimensions { get; }
    public IEnumerator GetEnumerator() => coords.GetEnumerator();
    public MathVector(int Dimensions, double[] vector)
    {
        if (vector.Length == 0)
        {
            throw new EmptyCoordsException();

        }

        if (vector.Length != Dimensions)
        {
            throw new DifferentDimensionsException();
        }

        this.Dimensions = Dimensions;
        coords = new double[Dimensions];

        for (int i = 0; i < Dimensions; i++)
        {
            coords[i] = vector[i];
        }
    }

    public MathVector(MathVector vector)
    {
        Dimensions = vector.Dimensions;
        coords = new double[vector.Dimensions];

        for (int i = 0; i < Dimensions; i++)
        {
            coords[i] = vector.coords[i];
        }
    }

    public static IMathVector operator +(MathVector vector, double number) => vector.SumNumber(number);
    public static IMathVector operator +(MathVector vectorA, MathVector vectorB) => vectorA.Sum(vectorB);

    public static IMathVector operator -(MathVector vector, double number) => vector.SumNumber(-number);
    public static IMathVector operator -(MathVector vectorA, MathVector vectorB)
    {
        IMathVector newVector = new MathVector(vectorA);

        for (int i = 0; i < newVector.Dimensions; i++)
        {
            newVector[i] = vectorA[i] - vectorB[i];
        }

        return newVector;
    }

    public static IMathVector operator *(MathVector vector, double number) => vector.MultiplyNumber(number);
    public static IMathVector operator *(MathVector vectorA, MathVector vectorB) => vectorA.Multiply(vectorB);

    public static IMathVector operator /(MathVector vector, double number)
    {
        if (number == 0)
        {
            throw new ZeroDivisionException();
        }

        IMathVector newVector = new MathVector(vector);

        for (int i = 0; i < newVector.Dimensions; i++)
        {
            newVector[i] = vector[i] / number;
        }

        return newVector;
    }
    public static IMathVector operator /(MathVector vectorA, MathVector vectorB)
    {
        IMathVector newVector = new MathVector(vectorA);

        for (int i = 0; i < newVector.Dimensions; i++)
        {
            if (vectorB[i] == 0)
            {
                throw new ZeroDivisionException("ERROR: there is coordinates with value 0 in second vector!");
            }

            newVector[i] = vectorA[i] / vectorB[i];
        }

        return newVector;
    }

    public static double operator %(MathVector vectorA, MathVector vectorB) => vectorA.ScalarMultiply(vectorB);

    public double this[int i]
    {
        get
        {
            double ind;
            if (i < 0 || i >= Dimensions)
            {
                throw new IndexOutOfRangeException();
            }

            else
            {
                ind = coords[i];
            }

            return ind;
        }
        set
        {

            if (i < 0 || i >= Dimensions)
            {
                throw new IndexOutOfRangeException();
            }

            else
            {
                double[] tempCoords = coords;
                tempCoords[i] = value;
                coords = tempCoords;
            }
        }
    }

    public double Length
    {
        get
        {
            double len = 0;
            for (int i = 0; i < Dimensions; i++)
            {
                len += Math.Pow(coords[i], 2);
            }
            len = Math.Sqrt(len);
            return len;
        }
    }

    public bool AreEqual(IMathVector vec)
    {
        IMathVector tempVector = new MathVector(this);

        if (tempVector.Dimensions != vec.Dimensions)
        {
            return false;
        }

        for (int i = 0; i < tempVector.Dimensions; i++)
        {
            if (tempVector[i] != vec[i])
            {
                return false;
            }
        }

        return true;
    }

    public IMathVector SumNumber(double number)
    {
        IMathVector tempVector = new MathVector(this);

        for (int i = 0; i < tempVector.Dimensions; i++)
        {
            tempVector[i] += number;
        }

        return tempVector;
    }

    public IMathVector MultiplyNumber(double number)
    {
        IMathVector tempVector = new MathVector(this);

        for (int i = 0; i < tempVector.Dimensions; i++)
        {
            tempVector[i] *= number;
        }

        return tempVector;
    }

    public IMathVector Sum(IMathVector vector)
    {
        if (Dimensions != vector.Dimensions)
        {
            throw new DifferentDimensionsException("ERROR: vectors must be of the same dimension!");
        }

        IMathVector tempVector = new MathVector(this);

        for (int i = 0; i < tempVector.Dimensions; i++)
        {
            tempVector[i] += vector[i];
        }

        return tempVector;

    }

    public IMathVector Multiply(IMathVector vector)
    {
        if (Dimensions != vector.Dimensions)
        {
            throw new DifferentDimensionsException("ERROR: vectors must be of the same dimension!");
        }

        IMathVector tempVector = new MathVector(this);

        for (int i = 0; i < tempVector.Dimensions; i++)
        {
            tempVector[i] *= vector[i];
        }

        return tempVector;
    }
    
    public double ScalarMultiply(IMathVector vector)
    {
        if (Dimensions != vector.Dimensions)
        {
            throw new DifferentDimensionsException("ERROR: vectors must be of the same dimension!");
        }

        IMathVector tempVector = new MathVector(this);

        double scalMult = 0;

        for (int i = 0; i < tempVector.Dimensions; i++)
        {
            scalMult += tempVector[i] * vector[i];
        }

        return scalMult;
    }

    public double CalcDistance(IMathVector vector)
    {
        if (Dimensions != vector.Dimensions)
        {
            throw new DifferentDimensionsException("ERROR: vectors must be of the same dimension!");
        }

        IMathVector tempVector = new MathVector(this);

        double distance = 0;

        for (int i = 0; i < tempVector.Dimensions; i++)
        {
            distance += Math.Pow(tempVector[i] - vector[i], 2);
        }

        distance = Math.Sqrt(distance);

        return distance;
    }
}


