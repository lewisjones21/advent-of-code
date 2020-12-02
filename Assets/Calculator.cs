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
                for (int k = i + 2; k < _input.Count; k++)
                {
                    if (_input[i] + _input[j] + _input[k] == 2020)
                    {
                        return _input[i] * _input[j] * _input[k];
                    }
                }
            }
        }
        Debug.LogWarning("Could not find a valid output");
        return 0;
    }
}
