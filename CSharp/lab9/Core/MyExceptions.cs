namespace Core;

public class MyExceptions : Exception
{
    public override string Message { get; }

    protected MyExceptions(string message)
    {
        Message = message;
    }
}

public class DatabaseUpdateException : MyExceptions
{
    public DatabaseUpdateException() : base("Error while updating database!") { }
}
public class IncorrectInputException : MyExceptions
{
    public IncorrectInputException(string message) : base($"Incorrect {message}! Enter again.") { }
}

public class IncorrectEmailException : MyExceptions
{
    public IncorrectEmailException() : base("Incorrect email format (must include @ and .)!") { }
}

public class NonExistentEntityException : MyExceptions
{
    public NonExistentEntityException(string sender) : base($"{sender} not found!") { }
}

public class AlreadyExistException : MyExceptions
{
    public AlreadyExistException() : base("This group already has curator!") { }
}
