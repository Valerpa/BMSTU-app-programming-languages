using Demographic.FileOperations;
namespace Demographic;
public class Person : IPerson
{
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public bool IsAlive { get; set; }
    public event ChildBirth? BornChild;
    public event Death? OccureDeath;
    
    public Person(Gender gender)
    {
        Gender = gender;
        Age = 0;
        IsAlive = true;
    }

    public Person(List<InitialAgeTable> ages, Gender gender)
    {
        Gender = gender;
        
        double sum = ages.Sum(age => age.AmountForK);
        
        double ageChance = ProbabilityCalculator.GetRandomDoubleValue();
        double probabilitySum = 0;
        Age = -1;
        int len = ages.Count;
        for (int i = 0; i < len; i++)
        {
            probabilitySum += ages[i].AmountForK / sum;
            if (ageChance < probabilitySum)
            {
                Age = ages[i].Year;
                break;
            }
        }
        if (Age == -1)
        {
            Age = ProbabilityCalculator.GetRandomIntValue();
        }

        IsAlive = true;
    }
    private void Death()
    {
        IsAlive = false;
        OccureDeath?.Invoke(this);
    }
    public void YearTickHandler(List<DeathRulesTable> deaths)
    {
        Age++;
        if (Age == 100)
        {
            Death();
        }
        int len = deaths.Count;
        if (Gender == Gender.Male)
        {
            for (int i = 0; i < len; i++)
            {
                {
                    if (Age >= deaths[i].StartAge && Age <= deaths[i].EndAge
                        && ProbabilityCalculator.IsEventHappened(deaths[i].MaleDeathProbability))
                    {
                        Death();
                    }
                }
                
            }
        }

        else
        {
            
            for (int i = 0; i < len; i++)
            {
                {
                    if (Age >= deaths[i].StartAge && Age <= deaths[i].EndAge
                       && ProbabilityCalculator.IsEventHappened(deaths[i].FemaleDeathProbability))
                    {
                        Death();
                    }
                }
                
            }

            if (IsAlive && Age is >= 18 and <= 45 && ProbabilityCalculator.IsEventHappened(0.151))
            {
                BornChild?.Invoke();
            }
        }
    }
}
