using UnityEngine;

public class Calculator : CalculatorBase<int, int>
{
    protected override int ParseInput(string inputLine)
    {
        return int.Parse(inputLine);
    }

    protected override int CalculateOutput()
    {
        for (int i = 0; i < _input.Count; i++)
        {
            for (int j = i + 1; j < _input.Count; j++)
            {
                if (_input[i] + _input[j] == 2020)
                {
                    return _input[i] * _input[j];
                }
            }
        }
        Debug.LogWarning("Could not find a valid output");
        return 0;
    }
}
