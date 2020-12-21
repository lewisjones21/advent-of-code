using UnityEngine;

public class Calculator : CalculatorBase<long, long>
{
    protected override long ParseInputLine(string inputLine)
    {
        return long.Parse(inputLine);
    }

    protected override long CalculateOutput()
    {
        int searchRange = 25;
        // Determine which number cannot be created by summing two numbers in the previous range
        bool hasSolution;
        for (int lastIndex = searchRange; lastIndex < _input.Count; lastIndex++)
        {
            hasSolution = false;
            for (int index1 = lastIndex - searchRange; index1 < lastIndex - 1; index1++)
            {
                for (int index2 = index1 + 1; index2 < lastIndex; index2++)
                {
                    if (_input[index1] + _input[index2] == _input[lastIndex])
                    {
                        hasSolution = true;
                        break;
                    }
                }
                if (hasSolution)
                {
                    break;
                }
            }
            if (!hasSolution)
            {
                return _input[lastIndex];
            }
        }

        Debug.LogError("No solution was found");
        return 0;
    }
}
