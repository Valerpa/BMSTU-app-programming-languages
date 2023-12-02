namespace Demographic.FileOperations;

public struct InitialAgeTable
{
    public int Year;
    public double AmountForK;

    public InitialAgeTable(int year, double amount)
    {
        Year = year;
        AmountForK = amount;
    }
}

public struct DeathRulesTable
{
    public int StartAge;
    public int EndAge;
    public double MaleDeathProbability;
    public double FemaleDeathProbability;

    public DeathRulesTable(int startAge, int endAge, double maleDeathProbability, double femaleDeathProbability)
    {
        StartAge = startAge;
        EndAge = endAge;
        MaleDeathProbability = maleDeathProbability;
        FemaleDeathProbability = femaleDeathProbability;
    }
}
public interface IFileWorker
{
    public string InitialAgeFilepath { get; set; }
    public string DeathFilepath { get; set; }
    
    public bool CheckFileExistance(string fileOne, string fileTwo);

    public List<InitialAgeTable> GetInitialAges();

    public List<DeathRulesTable> GetDeathRules();
}