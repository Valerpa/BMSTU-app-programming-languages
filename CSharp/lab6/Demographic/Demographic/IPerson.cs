namespace Demographic;

public enum Gender
{
    Male,
    Female
}

public delegate void ChildBirth();
public delegate void Death(Person person);
public interface IPerson
{
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public bool IsAlive { get; set; }
    public event ChildBirth? BornChild;
    public event Death? OccureDeath;
}