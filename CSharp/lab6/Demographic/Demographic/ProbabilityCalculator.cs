namespace Demographic;

public static class ProbabilityCalculator
{
    private static readonly Random Random = new Random();

    public static bool IsEventHappened(double eventProbability)
    {
        return Random.NextDouble() <= eventProbability;
    }

    public static double GetRandomDoubleValue()
    {
        return Random.NextDouble();
    }

    public static int GetRandomIntValue()
    {
        return Random.Next(0, 101);
    }
}