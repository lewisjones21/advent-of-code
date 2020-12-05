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
        _input.Sort();

        for (int i = 1; i < _input.Count; i++)
        {
            if (_input[i - 1] == _input[i] - 2)
            {
                return _input[i] - 1;
            }
        }

        Debug.LogError("Could not find valid output");
        return 0;
    }
}
