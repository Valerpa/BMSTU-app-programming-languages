namespace UnitTests;
using System.Collections;
using LinearAlgebra;

[TestFixture]
public class Tests
{
    [Test]
    public void ConstructorTest()
    {
        IMathVector actualVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 1, 2, 3 });
        Assert.That(expectedVector, Is.EqualTo(actualVector));
    }

    [Test]
    public void ConstructorZeroCoordsTest()
    {
        Assert.Throws<EmptyCoordsException>(() => new MathVector(3, new double[] { }));
    }
    
    [Test]
    public void ConstructorZeroDimensionsTest()
    {
        Assert.Throws<EmptyCoordsException>(() => new MathVector(0, new double[] {1, 2, 3 }));
    }

    [Test]
    public void ConstructorDifferentLengthsTest()
    {
        Assert.Throws<DifferentDimensionsException>(() => new MathVector(3, new double[] { 1, 2 }));
    }
    
    [Test]
    public void CopyConstructorTest()
    {
        MathVector expectedVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector actualVector = new MathVector(expectedVector);
        Assert.That(expectedVector, Is.EqualTo(actualVector));
    }

    [Test]
    public void IndexatorGetCorrectTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        double expectedElement = 1;
        Assert.That(expectedElement, Is.EqualTo(tempVector[0]));
    }

    [Test]
    public void IndexatorGetNegativeTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        Assert.Throws<IndexOutOfRangeException>(() => Console.WriteLine(tempVector[-1]));
    }
    
    [Test]
    public void IndexatorGetMoreSizeTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        Assert.Throws<IndexOutOfRangeException>(() => Console.WriteLine(tempVector[5]));
    }
    
    [Test]
    public void IndexatorSetCorrectTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        double expectedElement = 4;
        Assert.That(expectedElement, Is.EqualTo(tempVector[0] = 4));
    }

    [Test]
    public void IndexatorSetNegativeTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        Assert.Throws<IndexOutOfRangeException>(() => Console.WriteLine(tempVector[-1] = 3));
    }
    
    [Test]
    public void IndexatorSetMoreSizeTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        Assert.Throws<IndexOutOfRangeException>(() => Console.WriteLine(tempVector[5] = 8));
    }

    [Test]
    public void LengthTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        double expectedLength = Math.Sqrt(14);
        Assert.That(expectedLength, Is.EqualTo(tempVector.Length));
    }

    [Test]
    public void SumNumberTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 3, 4, 5 });
        Assert.That(expectedVector, Is.EqualTo(tempVector.SumNumber(2)));
    }

    [Test]
    public void SumVectorsCorrectTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector tempVectorTwo = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 2, 4, 6 });
        Assert.That(expectedVector, Is.EqualTo(tempVector.Sum(tempVectorTwo))); 
    }
    
    [Test]
    public void SumVectorsDiffDimensionsTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector tempVectorTwo = new MathVector(4, new double[] { 1, 2, 3, 4 });
        Assert.Throws<DifferentDimensionsException>(() => tempVector.Sum(tempVectorTwo));
    }
    
    [Test]
    public void OperatorPlusNumberTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 3, 4, 5 });
        Assert.That(expectedVector, Is.EqualTo(tempVector + 2));
    }
    

    [Test]
    public void OperatorPlusVectorsCorrectTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 2, 4, 6 });
        Assert.That(expectedVector, Is.EqualTo(tempVector + tempVectorTwo)); 
    }

    [Test]
    public void OperatorPlusVectorsDiffDimensionsTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(4, new double[] { 1, 2, 3, 4 });
        Assert.Throws<DifferentDimensionsException>(() => Console.WriteLine(tempVector + tempVectorTwo));
    }

    [Test]
    public void OperatorMinusNumberTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { -1, 0, 1 });
        Assert.That(expectedVector, Is.EqualTo(tempVector - 2));
    }
    

    [Test]
    public void OperatorMinusVectorsCorrectTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 0, 0, 0 });
        Assert.That(expectedVector, Is.EqualTo(tempVector - tempVectorTwo)); 
    }
    
    [Test]
    public void OperatorMinusVectorsDiffDimensionsTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(4, new double[] { 1, 2, 3, 4 });
        Assert.Throws<DifferentDimensionsException>(() => Console.WriteLine(tempVector - tempVectorTwo));
    }
    
    [Test]
    public void MultiplyNumberTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 2, 4, 6 });
        Assert.That(expectedVector, Is.EqualTo(tempVector.MultiplyNumber(2)));
    }

    [Test]
    public void MultiplyVectorsCorrectTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector tempVectorTwo = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 1, 4, 9 });
        Assert.That(expectedVector, Is.EqualTo(tempVector.Multiply(tempVectorTwo))); 
    }
    
    [Test]
    public void MultiplyVectorsDiffDimensionsTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector tempVectorTwo = new MathVector(4, new double[] { 1, 2, 3, 4 });
        Assert.Throws<DifferentDimensionsException>(() => tempVector.Multiply(tempVectorTwo));
    }
    
    [Test]
    public void OperatorMultNumberTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 2, 4, 6 });
        Assert.That(expectedVector, Is.EqualTo(tempVector * 2));
    }
    

    [Test]
    public void OperatorMultVectorsCorrectTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 1, 4, 9 });
        Assert.That(expectedVector, Is.EqualTo(tempVector * tempVectorTwo)); 
    }

    [Test]
    public void OperatorMultVectorsDiffDimensionsTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(4, new double[] { 1, 2, 3, 4 });
        Assert.Throws<DifferentDimensionsException>(() => Console.WriteLine(tempVector * tempVectorTwo));
    }

    [Test]
    public void OperatorDivideNumberCorrectTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 0.5, 1, 1.5 });
        Assert.That(expectedVector, Is.EqualTo(tempVector / 2));
    }
    
    [Test]
    public void OperatorDivideByZeroTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        Assert.Throws<ZeroDivisionException>(() => Console.WriteLine(tempVector / 0));
    }
    
    [Test]
    public void OperatorDivideVectorsCorrectTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector expectedVector = new MathVector(3, new double[] { 1, 1, 1 });
        Assert.That(expectedVector, Is.EqualTo(tempVector / tempVectorTwo)); 
    }
    
    [Test]
    public void OperatorDivideByVectorWithZeroCoordTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(3, new double[] { 1, 0, 3 });
        Assert.Throws<ZeroDivisionException>(() => Console.WriteLine(tempVector / tempVectorTwo));
    }
    
    [Test]
    public void OperatorDivideByVectorWithDiffDimensionsTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(4, new double[] { 2, 4, 6, 8 });
        Assert.Throws<DifferentDimensionsException>(() => Console.WriteLine(tempVector / tempVectorTwo));
    }
    
    [Test]
    public void ScalarMultiplyCorrectTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector tempVectorTwo = new MathVector(3, new double[] { 2, 4, 6 });
        double expected = 28;
        Assert.That(expected, Is.EqualTo(tempVector.ScalarMultiply(tempVectorTwo)));
    }

    [Test]
    public void ScalarMultiplyWithDiffDimensionsTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector tempVectorTwo = new MathVector(4, new double[] { 2, 4, 6, 8 });
        Assert.Throws<DifferentDimensionsException>(() => tempVector.ScalarMultiply(tempVectorTwo));
    }
    
    [Test]
    public void OperatorScalarMultCorrectTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(3, new double[] { 2, 4, 6 });
        double expected = 28;
        Assert.That(expected, Is.EqualTo(tempVector % tempVectorTwo));
    }

    [Test]
    public void OperatorScalarMultWithDiffDimensionsTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(4, new double[] { 2, 4, 6, 8 });
        Assert.Throws<DifferentDimensionsException>(() => Console.WriteLine(tempVector % tempVectorTwo));
    }

    [Test]
    public void CalcDistanceCorrectTest()
    {
        IMathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        IMathVector tempVectorTwo = new MathVector(3, new double[] { 2, 4, 6 });
        double expected =Math.Sqrt(14);
        Assert.That(expected, Is.EqualTo(tempVector.CalcDistance(tempVectorTwo)));
    }
    
    [Test]
    public void CalcDistanceDiffDimensionsTest()
    {
        MathVector tempVector = new MathVector(3, new double[] { 1, 2, 3 });
        MathVector tempVectorTwo = new MathVector(4, new double[] { 2, 4, 6, 8 });
        Assert.Throws<DifferentDimensionsException>(() => tempVector.CalcDistance(tempVectorTwo));
    }

    [Test]
    public void GetEnumeratorTest()
    {
        double[] coords = new double[] { 1, 2, 3 };
        IMathVector mathVector = new MathVector(3, coords);
        IEnumerator enumerator = mathVector.GetEnumerator();
        Assert.IsNotNull(enumerator);
        int index = 0;
        while (enumerator.MoveNext())
        {
            Assert.That(coords[index], Is.EqualTo(enumerator.Current));
            index++;
        }
    }
}