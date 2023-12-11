namespace lab7;

public static class FileManager
{
    public static void WriteToFile(string message)
    {
        var filePath = "/Users/valerikanasha228/Desktop/labs_3sem/CSharp/lab7/lab7/Data/events.txt";
        File.AppendAllText(filePath, $"{message}\n");
    }
}