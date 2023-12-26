namespace DatabaseModels;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public List<Student> Students { get; set; }
    public Curator Curator { get; set; }
}