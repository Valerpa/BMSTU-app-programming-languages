namespace Demographic.FileOperations;

public class FileWorker : IFileWorker
{
    public string InitialAgeFilepath { get; set; }
    public string DeathFilepath { get; set; }
    
    public bool CheckFileExistance(string fileOne, string fileTwo)
    {
        return File.Exists(fileOne) && File.Exists(fileTwo);
    }
    public FileWorker(string initialAgeFilepath, string deathFilepath)
    {
        if (!CheckFileExistance(initialAgeFilepath, deathFilepath))
        {
            throw new FileNotExistsException();
        }
        InitialAgeFilepath = initialAgeFilepath;
        DeathFilepath = deathFilepath;
    }
    
    public List<InitialAgeTable> GetInitialAges()
    {
        List <InitialAgeTable> data = new List<InitialAgeTable>();

        using (var streamReader = new StreamReader(InitialAgeFilepath))
        {
            var headerLine = streamReader.ReadLine();
            if (headerLine != "Age, Amount_for_k")
            {
                Console.WriteLine(headerLine);
                throw new IncorrectHeaderException();
            }

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                var values = line
                    .Replace(" ", "")
                    .Replace("\n", "")
                    .Replace("\r", "")
                    .Split((','));

                if (!int.TryParse(values[0], out int year) || 
                    !double.TryParse(values[1].Replace(".",","), out double amount))
                {
                    throw new IncorrectDataException();
                }

                InitialAgeTable initialAge = new InitialAgeTable(year, amount);
                data.Add(initialAge);
            }
        }
        return data;
    }

    public List<DeathRulesTable> GetDeathRules()
    {
        List <DeathRulesTable> data = new List<DeathRulesTable>();

        using (var streamReader = new StreamReader(DeathFilepath))
        {
            var headerLine = streamReader.ReadLine();
            if (headerLine != "Start_age, End_age, Male_death_probability, Female_death_probability")
            {
                throw new IncorrectHeaderException();
            }

            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                var values = line
                    .Replace(" ", "")
                    .Replace("\n", "")
                    .Replace("\r", "")
                    .Split((','));

                if (!int.TryParse(values[0], out int startAge) || 
                    !int.TryParse(values[1], out int endAge) ||
                    !double.TryParse(values[2].Replace(".",","), out double maleDeathProbability) || 
                    !double.TryParse(values[3].Replace(".",","), out double femaleDeathProbability))
                {
                    throw new IncorrectDataException();
                }
                DeathRulesTable deathRule = new DeathRulesTable(startAge, 
                                                                endAge, 
                                                                maleDeathProbability, 
                                                                femaleDeathProbability);
                data.Add(deathRule);
            }
        }
        return data;
    }

    public void WriteResultToNewFile(string filepath, List<string> population)
    {
        File.WriteAllLines(filepath, population);
    }
}