using UnityEngine;

public class Calculator : CalculatorBase<int, long>
{
    protected override int ParseInputLine(string inputLine)
    {
        int returnValue = 0;
        for (int i = 0; i < inputLine.Length; i++)
        {
            returnValue += (inputLine[i] == 'B' || inputLine[i] == 'R')
                ? (1 << (inputLine.Length - i - 1)) : 0;
        }
        return returnValue;
    }

    protected override long CalculateOutput()
    {
        return Mathf.Max(_input.ToArray());
    }
}
