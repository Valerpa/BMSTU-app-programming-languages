using Core;
using DatabaseContext;
public class Program
{
    public static async Task Main()
    {
        using (var dbContext = new UniversityContext())
        {
            var repository = new UniversityRepository(dbContext);
            var facade = new Facade(repository);
            await facade.Run();
        }
    }
}