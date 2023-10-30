namespace VectorDemo;

public static class Facade
{
    public static void RunTests()
    {
        Test.ConstructorTest();
        Test.DimensionsTest();
        Test.IndexatorTest();
        Test.LengthTest();
        Test.VectorsEqualTest();
        Test.SumNumberTest();
        Test.MultiplyNumberTest();
        Test.SumVectorsTest();
        Test.MultVectorsTest();
        Test.ScalarMultTest();
        Test.CalcDistanceTest();
        Test.OperatorPlusNumberTest();
        Test.OperatorPlusVectorTest();
        Test.OperatorMinusNumberTest();
        Test.OperatorMinusVectorTest();
        Test.OperatorMultiplyNumberTest();
        Test.OperatorMultiplyVectorTest();
        Test.OperatorDivideNumberTest();
        Test.OperatorDivideVectorTest();
        Test.OperatorScalTest();
    }
}

