namespace Demographic.FileOperations;

public class FileWorkerExceptions : Exception
{
    public override string Message { get; }

    protected FileWorkerExceptions(string message)
    {
        Message = message;
    }
}

public class FileNotExistsException : FileWorkerExceptions
{
    public FileNotExistsException() : base("ERROR: Files don't exists!") { }
}

public class IncorrectHeaderException : FileWorkerExceptions
{
    public IncorrectHeaderException () : base("ERROR: incorrect header line in file!") { }
}

public class IncorrectDataException : FileWorkerExceptions
{
    public IncorrectDataException () : base("ERROR: incorrect data in file!") { }
}

