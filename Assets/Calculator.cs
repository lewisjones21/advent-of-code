using UnityEngine;

public class Calculator : CalculatorBase<int, int>
{
    protected override int ParseInput(string inputLine)
    {
        return int.Parse(inputLine);
    }

    protected override int CalculateOutput()
    {
        return 0;
    }
}
