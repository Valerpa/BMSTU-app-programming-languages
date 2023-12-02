using Demographic.FileOperations;

namespace Demographic;

public delegate void YearTick(List<DeathRulesTable> deaths);
public interface IEngine
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public event YearTick? YearPassed;
    public List<Person> Population { get; }
    public void RunSimulation();
}