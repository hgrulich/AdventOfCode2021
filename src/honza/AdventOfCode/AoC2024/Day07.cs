namespace AoC2024;

public class Day07 : IDay
{
    public long Solve1()
    {
        var equationStrings = File.ReadAllLines("Input\\Input07.txt");
        long sum = 0;
        foreach (var equationString in equationStrings)
        {
            var parts = equationString.Split(":");
            var numStrings = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var equation = (long.Parse(parts[0]), numStrings.Select(long.Parse).ToArray());

            if (IsEquationCorrect(equation.Item1, 0, equation.Item2))
                sum += equation.Item1;
        }

        return sum;
    }
    
    bool IsEquationCorrect(long realResult, long subresult, long[] valuesToBeProcessed)
    {
        if (valuesToBeProcessed.Length == 0)
            return subresult == realResult;

        if (IsEquationCorrect(
                realResult,
                subresult * (int)Math.Pow(10, Math.Floor(Math.Log10(valuesToBeProcessed[0])) + 1) + valuesToBeProcessed[0],
                valuesToBeProcessed.Skip(1).ToArray()))
            return true;
        
        if(IsEquationCorrect(realResult, subresult + valuesToBeProcessed[0], valuesToBeProcessed.Skip(1).ToArray()))
            return true;

        if(IsEquationCorrect(realResult, subresult * valuesToBeProcessed[0], valuesToBeProcessed.Skip(1).ToArray()))
            return true;

        return false;
    }
    public long Solve2()
    {
        throw new NotImplementedException();
    }
}