using System.Collections;
using LinearAlgebra;
namespace VectorDemo;

public static class Output
{
	public static void OutputVector(IMathVector vector)
	{
		Console.Write("Vector: ");
		foreach (var c in vector)
		{
			Console.Write(c + " ");
		}
		Console.WriteLine();
	}

	public static void OutputTestResult(IMathVector vectorOne, IMathVector expectedVector)
	{
		if (vectorOne.AreEqual(expectedVector))
		{
			Console.WriteLine("Passed!");
		}

		else
		{
			Console.WriteLine("Failed!");
		}
	}
}

