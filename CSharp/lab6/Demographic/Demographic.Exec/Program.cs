namespace Demographic.Exec;
using Demographic.FileOperations;

static class Program
{
    static void Main(string[] args)
    {
        try
        {
            var ageFilepath = args[0];
            var deathFilepath = args[1];
            var startYear = int.Parse(args[2]);
            var endYear = int.Parse(args[3]);
            var population = int.Parse(args[4]);
            var populationByYearFilepath = args[5];
            var endSimPopulationFilepath = args[6];
            FileWorker fileWorker = new FileWorker(ageFilepath, deathFilepath);
            var ages = fileWorker.GetInitialAges();
            var deaths = fileWorker.GetDeathRules();
            Engine engine = new Engine(ages, deaths, startYear, endYear, population);
            engine.RunSimulation();
            var resultPopulation = engine.ConvertSimResultToString();
            fileWorker.WriteResultToNewFile(populationByYearFilepath, engine.PopulationData);
            fileWorker.WriteResultToNewFile(endSimPopulationFilepath, resultPopulation);
        }
        catch (FileWorkerExceptions ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}