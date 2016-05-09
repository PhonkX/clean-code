using System;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace ControlDigit
{
	public static class ControlDigitExtensions
	{
		public static int ControlDigit(this long number)
		{
			int sum = 0;
			int factor = 1;
			do
			{
				int digit = (int)(number % 10);
				sum += factor * digit;
				factor = 4 - factor;
				number /= 10;

			}
			while (number > 0);

			int result = sum % 11;
			if (result == 10)
				result = 1;
			return result;
		}

		public static int ControlDigit2(this long number)
		{
		    int checkSum = GetCheckSum(number);
		    return GetControlDigitFromSum(checkSum);
		}

	    private static int GetLastDigit(long number)
	    {
	        return (int)(number % 10);
	    }

	    private static int GetCheckSum(long number)
	    {
	      //  var factorSequence = GetFactorSequence();
	        int checkSum = 0;
	        int position = 0;
	        do
	        {
	            int digit = GetLastDigit(number);
	            checkSum += GetDigitWithFactor(position, digit);
	            number = MoveToNextDigit(number);
	            position++;
	        } while (number > 0);

	        return checkSum;
	    }


	    private static int[] GetFactorSequence()
	    {
	        return new int[] {1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3};
	    }

	    private static int GetDigitWithFactor(int position, int digit)
	    {
	        return GetFactorSequence()[position] * digit;
	    }

	    private static long MoveToNextDigit(long number)
	    {
	        return number / 10;
	    }

	    private static int GetControlDigitFromSum(int sum)
	    {
            int digit = sum % 11;
            if (digit == 10)
                digit = 1;
            return digit;
        }
	}

	[TestFixture]
	public class ControlDigitExtensions_Tests
	{
		[TestCase(0, Result = 0)]
		[TestCase(1, Result = 1)]
		[TestCase(2, Result = 2)]
		[TestCase(9, Result = 9)]
		[TestCase(10, Result = 3)]
		[TestCase(15, Result = 8)]
		[TestCase(17, Result = 1)]
		[TestCase(18, Result = 0)]
		public int TestControlDigit(long x)
		{
			return x.ControlDigit();
		}

		[Test]
		public void TestControlDigitSpeed()
		{
			for (int i = 0; i < 10000000; i++)
				12345678L.ControlDigit();
		}
		[Test]
		public void TestControlDigit2Speed()
		{
			for (int i = 0; i < 10000000; i++)
				12345678L.ControlDigit2();
		}

		[Test]
		public void CompareImplementations()
		{
			for (long i = 0; i < 100000; i++)
				Assert.AreEqual(i.ControlDigit(), i.ControlDigit2());
		}
	}
}
