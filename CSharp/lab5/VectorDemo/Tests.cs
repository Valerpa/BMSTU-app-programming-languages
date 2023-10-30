using LinearAlgebra;
namespace VectorDemo;

public static class Test
{
    public static void ConstructorTest()
    {
        Console.WriteLine("1. Testing Constructor...");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        Console.WriteLine($"Dimensions = {vectorOne.Dimensions}");

        try
        {
            Console.WriteLine("1.1 Checking vector with different number of dimensions and coordinates");
            IMathVector vectorTwo = new MathVector(4, new double[] { 1, 2, 3 });
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }

        try
        {
            Console.WriteLine("1.2 Checking vector with incorrect number of dimensions");
            IMathVector vectorTwo = new MathVector(0, new double[] { 1, 2, 3 });
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Constructor tests passed!");
    }

    public static void DimensionsTest()
    {
        Console.WriteLine("\n2. Testing Dimensions...");
        int expected = 3;
        IMathVector vectorOne = new MathVector(expected, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        Console.WriteLine($"Dimensions number = {vectorOne.Dimensions}, expected = {expected}");
        if (vectorOne.Dimensions == expected)
        {
            Console.WriteLine("Dimensions test passed!");
        }
        else
        {
            Console.WriteLine("Dimensions test failed!");
        }
    }

    public static void IndexatorTest()
    {
        Console.WriteLine("\n3. Testing Indexator...");
        Console.WriteLine("3.1 Testing getter");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        int expectedIndex = 0;
        Console.WriteLine($"Index of element 1 = {expectedIndex}");

        try
        {
            int incorrectIndex = -1;
            Console.WriteLine($"Testing incorrect index = {incorrectIndex}");
            Console.WriteLine(vectorOne[incorrectIndex]);
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("3.2 Testing setter");
        vectorOne[0] += 5;
        if (vectorOne[0] == 6)
        {
            Console.WriteLine("Indexator test passed!");
        }
        else
        {
            Console.WriteLine("Indexator test failed!");
        }
        
    }

    public static void LengthTest()
    {
        Console.WriteLine("\n4. Testing Length...");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        double expected = Math.Sqrt(14);
        double len = vectorOne.Length;
        Console.WriteLine($"Length of vector = {len}, expected {expected}");
        if (len == expected)
        {
            Console.WriteLine("Length test passed!");
        }
        else
        {
            Console.WriteLine("Length test failed!");
        }

    }

    public static void VectorsEqualTest()
    {
        Console.WriteLine("\n5. Testing Comparing Two Vectors...");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        IMathVector vectorTwo = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorTwo);
        Output.OutputTestResult(vectorOne, vectorTwo);
    }

    public static void SumNumberTest()
    {
        Console.WriteLine("\n6. Testing Sum Of Vector With Number ...");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        double num = 2;
        Console.WriteLine($"number = {num}");
        IMathVector vectorTwo = vectorOne.SumNumber(num);
        Output.OutputVector(vectorTwo);
        IMathVector expectedVector = new MathVector(3, new double[] { 3, 4, 5 });
        Output.OutputTestResult(vectorTwo, expectedVector);
    }

    public static void MultiplyNumberTest()
    {
        Console.WriteLine("\n7. Testing Mult Of Vector With Number...");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        double num = 2;
        Console.WriteLine($"number = {num}");
        IMathVector vectorTwo = vectorOne.MultiplyNumber(num);
        Output.OutputVector(vectorTwo);
        IMathVector expectedVector = new MathVector(3, new double[] { 2, 4, 6 });
        Output.OutputTestResult(vectorTwo, expectedVector);
    }

    public static void SumVectorsTest()
    {
        Console.WriteLine("\n8. Testing Sum Of Two Vectors...");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        IMathVector vectorTwo = new MathVector(3, new double[] { 2, 4, 6 });
        Output.OutputVector(vectorTwo);
        IMathVector expectedVector = new MathVector(3, new double[] { 3, 6, 9 });
        IMathVector vectorThree = vectorOne.Sum(vectorTwo);
        Output.OutputVector(vectorThree);
        Output.OutputTestResult(vectorThree, expectedVector);

        try
        {
            Console.WriteLine("8.1 Testing Vectors with different dimensions");
            IMathVector vectorFour = new MathVector(4, new double[] { 2, 4, 7, 9 });
            Output.OutputVector(vectorFour);
            IMathVector vectorFive = vectorFour.Sum(vectorOne);
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Sum tests completed!");
    }

    public static void MultVectorsTest()
    {
        Console.WriteLine("\n9. Testing Mult Of Two Vectors...");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        IMathVector vectorTwo = new MathVector(3, new double[] { 2, 4, 6 });
        Output.OutputVector(vectorTwo);
        IMathVector expectedVector = new MathVector(3, new double[] { 2, 8, 18 });
        IMathVector vectorThree = vectorOne.Multiply(vectorTwo);
        Output.OutputVector(vectorThree);
        Output.OutputTestResult(vectorThree, expectedVector);

        try
        {
            Console.WriteLine("9.1 Testing Vectors with different dimensions");
            IMathVector incorrectVector = new MathVector(4, new double[] { 2, 4, 7, 9 });
            Output.OutputVector(incorrectVector);
            IMathVector vectorFour = incorrectVector.Multiply(vectorOne);
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Mult tests completed!");
    }

    public static void ScalarMultTest()
    {
        Console.WriteLine("\n10. Testing Scalar Mult Of Two Vectors...");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        IMathVector vectorTwo = new MathVector(3, new double[] { 2, 4, 6 });
        Output.OutputVector(vectorTwo);
        double expectedScalarMult = 28;
        double scalarMult = vectorOne.ScalarMultiply(vectorTwo);
        Console.WriteLine($"Scalar mult = {scalarMult}, expected = {expectedScalarMult}");
        if (scalarMult == expectedScalarMult)
        {
            Console.WriteLine("Scalar mupltiply of correct vectors test passed!");
        }
        else
        {
            Console.WriteLine("Scalar mupltiply of correct vectors test failed!");
        }

        try
        {
            Console.WriteLine("10.1 Testing Vectors with different dimensions");
            IMathVector incorrectVector = new MathVector(4, new double[] { 2, 4, 7, 9 });
            Output.OutputVector(incorrectVector);
            double mult = incorrectVector.ScalarMultiply(vectorOne);
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Scalar mult tests completed!");

    }

    public static void CalcDistanceTest()
    {
        Console.WriteLine("\n11. Testing Calculating Distance Between Two Vectors...");
        IMathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        IMathVector vectorTwo = new MathVector(3, new double[] { 2, 4, 6 });
        Output.OutputVector(vectorTwo);
        double expectedDistance = Math.Sqrt(14);
        double distance = vectorOne.CalcDistance(vectorTwo);
        Console.WriteLine($"Distance = {distance}, expected = {expectedDistance}");
        if (distance == expectedDistance)
        {
            Console.WriteLine("Calculate distance between correct vectors test passed!");
        }
        else
        {
            Console.WriteLine("Calculate distance between correct vectors test failed!");
        }
        try
        {
            Console.WriteLine("11.1 Testing Vectors with different dimensions");
            IMathVector incorrectVector = new MathVector(4, new double[] { 2, 4, 7, 9 });
            Output.OutputVector(incorrectVector);
            double mult = incorrectVector.ScalarMultiply(vectorOne);
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Calculate distance tests completed!");
    }


    public static void OperatorPlusNumberTest()
    {
        Console.WriteLine("\n12. Testing Operator + With Number...");
        MathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);

        IMathVector vectorTwo = vectorOne + 6;
        Output.OutputVector(vectorTwo);
        IMathVector expectedVector = new MathVector(3, new double[] { 7, 8, 9 });
        Output.OutputTestResult(vectorTwo, expectedVector);
    }

    public static void OperatorPlusVectorTest()
    {
        Console.WriteLine("\n13. Testing Opearator + With Vectors...");
        MathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        MathVector vectorTwo = new MathVector(3, new double[] { 4, 5, 6 });
        Output.OutputVector(vectorTwo);

        IMathVector vectorThree = vectorOne + vectorTwo;
        Output.OutputVector(vectorThree);
        IMathVector expectedVector = new MathVector(3, new double[] { 5, 7, 9 });
        Output.OutputTestResult(vectorThree, expectedVector);
        try
        {
            Console.WriteLine("13.1 Testing vectors with different dimensions");
            MathVector incorrectVector = new MathVector(4, new double[] { 1, 2, 3, 4 });
            IMathVector vec = vectorOne + incorrectVector;
;       }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Operator + tests completed!");

    }

    public static void OperatorMinusNumberTest()
    {
        Console.WriteLine("\n14. Testing Operator - With Number...");
        MathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);

        IMathVector vectorTwo = vectorOne - 1;
        Output.OutputVector(vectorTwo);
        IMathVector expectedVector = new MathVector(3, new double[] { 0, 1, 2 });
        Output.OutputTestResult(vectorTwo, expectedVector);
    }

    public static void OperatorMinusVectorTest()
    {
        Console.WriteLine("\n15. Testing Operator - With Vectors...");
        MathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        MathVector vectorTwo = new MathVector(3, new double[] { 4, 5, 6 });
        Output.OutputVector(vectorTwo);

        IMathVector vectorThree = vectorOne - vectorTwo;
        Output.OutputVector(vectorThree);
        IMathVector expectedVector = new MathVector(3, new double[] { -3, -3, 3 });
        Output.OutputTestResult(vectorThree, expectedVector);
        try
        {
            Console.WriteLine("15.1 Testing vectors with different dimensions");
            MathVector incorrectVector = new MathVector(4, new double[] { 1, 2, 3, 4 });
            IMathVector vec = vectorOne + incorrectVector;
            ;
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Operator - tests completed!");
    }

    public static void OperatorMultiplyNumberTest()
    {
        Console.WriteLine("\n16. Testing Operator * With Number...");
        MathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        double num = 3;
        Console.WriteLine($"number = {num}");
        IMathVector vectorTwo = vectorOne * num;
        Output.OutputVector(vectorTwo);
        IMathVector expectedVector = new MathVector(3, new double[] { 3, 6, 9 });
        Output.OutputTestResult(vectorTwo, expectedVector);
    }

    public static void OperatorMultiplyVectorTest()
    {
        Console.WriteLine("\n17. Testing Operator * With Vectors...");
        MathVector vectorOne = new MathVector(3, new double[] { 1, 2, 3 });
        Output.OutputVector(vectorOne);
        MathVector vectorTwo = new MathVector(3, new double[] { 4, 5, 6 });
        Output.OutputVector(vectorTwo);

        IMathVector vectorThree = vectorOne * vectorTwo;
        Output.OutputVector(vectorThree);
        IMathVector expectedVector = new MathVector(3, new double[] { 4, 10, 18 });
        Output.OutputTestResult(vectorThree, expectedVector);
        try
        {
            Console.WriteLine("17.1 Testing vectors with different dimensions");
            MathVector incorrectVector = new MathVector(4, new double[] { 1, 2, 3, 4 });
            Output.OutputVector(incorrectVector);
            IMathVector vec = vectorOne + incorrectVector;
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Operator * test completed!");
    }

    public static void OperatorDivideNumberTest()
    {
        Console.WriteLine("\n18. Testing Operator / With Number...");
        MathVector vectorOne = new MathVector(3, new double[] { 4, 6, 10 });
        Output.OutputVector(vectorOne);
        double num = 2;
        Console.WriteLine($"number = {num}");
        IMathVector vectorTwo = vectorOne / num;
        Output.OutputVector(vectorTwo);
        IMathVector expectedVector = new MathVector(3, new double[] { 2, 3, 5 });
        Output.OutputTestResult(vectorTwo, expectedVector);
        try
        {
            Console.WriteLine("18.1 Testing division by 0");
            num = 0;
            IMathVector vec = vectorOne / num;
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void OperatorDivideVectorTest()
    {
        Console.WriteLine("\n19. Testing Operator / With Vectors...");

        MathVector vectorOne = new MathVector(3, new double[] { 4, 6, 10 });
        Output.OutputVector(vectorOne);

        MathVector vectorTwo = new MathVector(3, new double[] { 2, 3, 5 });
        Output.OutputVector(vectorTwo);

        IMathVector vectorThree = vectorOne / vectorTwo;
        Output.OutputVector(vectorThree);

        IMathVector expectedVector = new MathVector(3, new double[] { 2, 2, 2 });
        Output.OutputTestResult(vectorThree, expectedVector);
        try
        {
            Console.WriteLine("19.1 Testing division by 0");
            MathVector incorrectVector = new MathVector(3, new double[] { 2, 0, 8 });
            Output.OutputVector(incorrectVector);
            IMathVector vec = vectorOne / incorrectVector;
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Operator / tests completed!");
    }

    public static void OperatorScalTest()
    {
        Console.WriteLine("\n20. Testing Operator %...");
        MathVector vectorOne = new MathVector(3, new double[] { 4, 6, 10 });
        Output.OutputVector(vectorOne);
        MathVector vectorTwo = new MathVector(3, new double[] { 2, 3, 5 });
        Output.OutputVector(vectorTwo);
        double expectedScal = 76;
        double scal = vectorOne % vectorTwo;
        Console.WriteLine($"Scalar mult = {scal}, expected = {expectedScal}");
        if (scal == expectedScal)
        {
            Console.WriteLine("Operator / for two correct vectors test passed!");
        }
        else
        {
            Console.WriteLine("Operator / for two correct vectors test failed!");
        }
        try
        {
            Console.WriteLine("20.1 Testing vectors with different dimensions");
            MathVector incorrectVector = new MathVector(4, new double[] { 2, 0, 8 });
            scal = vectorOne % incorrectVector;
        }
        catch (MyException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Operator % tests completed!");
    }
}