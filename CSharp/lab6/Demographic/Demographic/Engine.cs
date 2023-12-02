namespace Demographic;
using FileOperations;
public class Engine : IEngine
{
    public int StartYear { get; set; } 
    public int EndYear { get; set; }
    public event YearTick? YearPassed;
    private int PeopleAmount { get; } 
    public List<Person> Population { get; } 
    
    private List<InitialAgeTable> _ageData; 
    private List<DeathRulesTable> _deathData; 
    
    public List<string> PopulationData;
    
    public Engine(List<InitialAgeTable> ages, List<DeathRulesTable> deaths, int startYear = 1970, int endYear = 2021, 
        int peopleAmount = 130)
    {
        StartYear = startYear;
        EndYear = endYear;
        _ageData = ages;
        _deathData = deaths;
        PeopleAmount = peopleAmount * 1000;
        PopulationData = new List<string> { "Year,Male,Female" };
        Population = new List<Person>(PeopleAmount);
        GeneratePopulation();
        
    }

    private void GeneratePopulation()
    {
        for (int i = 0; i < PeopleAmount; i++)
        {
            Gender gender = i % 2 == 0 ? Gender.Male : Gender.Female;
            Person person = new(_ageData, gender);
            WeakReference<Person> weakPerson = new WeakReference<Person>(person);
            if (weakPerson.TryGetTarget(out Person? targetPerson))
            {
                YearPassed += targetPerson.YearTickHandler;
                
                targetPerson.OccureDeath += HandleDeath;
                if (targetPerson.Gender == Gender.Female)
                {
                    targetPerson.BornChild += ChildBirthHandler;
                }
            }
            Population.Add(person);
        }
        
    }

    private void RecordPopulationByGender(int year)
    {
        int maleCount = Population.Count(person => person.Gender == Gender.Male && person.IsAlive);
        int femaleCount = Population.Count(person => person.Gender == Gender.Female && person.IsAlive);
        PopulationData.Add($"{year}, {maleCount}, {femaleCount}");
    }
    public void RunSimulation()
    {
        for (int year = StartYear; year <= EndYear; year++)
        {
            RecordPopulationByGender(year);
            YearPassed?.Invoke(_deathData);
        }
    }

    private void ChildBirthHandler()
    {
        Gender childGender = ProbabilityCalculator.IsEventHappened(0.45) ? Gender.Male : Gender.Female;
        Person person = new Person(childGender);
        WeakReference<Person> weakPerson = new WeakReference<Person>(person); 

        if (weakPerson.TryGetTarget(out Person? targetPerson)) 
        {
            YearPassed += targetPerson.YearTickHandler;
            targetPerson.OccureDeath += HandleDeath;
            if (targetPerson.Gender == Gender.Female)
            {
                targetPerson.BornChild += ChildBirthHandler;
            }
        }
        Population.Add(person);
    }

    private void HandleDeath(Person person)
    {
        WeakReference<Person> weakPerson = new(person); 

        if (weakPerson.TryGetTarget(out Person? targetPerson)) 
        {
            YearPassed -= targetPerson.YearTickHandler; 
            targetPerson.OccureDeath -= HandleDeath;
            if (targetPerson.Gender == Gender.Female)
            {
                targetPerson.BornChild -= ChildBirthHandler;
            }
        }
        Population.Remove(person);
    }
    
    public List<string> ConvertSimResultToString()
    { 
        List<string> resultList = new List<string>(Population.Count) { "Gender,Age" };
        resultList.AddRange(Population.Select(person => $"{person.Gender}, {person.Age}"));
        return resultList;
    }
}