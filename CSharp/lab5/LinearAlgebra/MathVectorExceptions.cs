using System;
namespace LinearAlgebra;

public class MyException : Exception
{
    override public string Message { get; }

    public MyException(string message)
    {
        Message = message;
    }
}

public class EmptyCoordsException : MyException
{
    public EmptyCoordsException() : base("ERROR: empty length!") { } 
}

public class DifferentDimensionsException : MyException
{
    public DifferentDimensionsException() : base("ERROR: different number of coordinates!") { }
    public DifferentDimensionsException(string message) : base(message) { }
}


public class IndexOutOfRangeException : MyException
{
    public IndexOutOfRangeException() : base("ERROR: wrong index!") { }
}

public class ZeroDivisionException : MyException
{
    public ZeroDivisionException() : base("ERROR: division by zero!") { }
    public ZeroDivisionException(string message) : base(message) { }
}