using System.Collections.Generic;
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
        int lastIndex;
        for (lastIndex = searchRange; lastIndex < _input.Count; lastIndex++)
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
                break;
            }
        }
        // Determine the range of contiguous numbers that sum to the number discovered above
        for (int startIndex = 0; startIndex < lastIndex; startIndex++)
        {
            int rangeWidth = 0;
            long sum = 0;
            while (sum < _input[lastIndex])
            {
                sum += _input[startIndex + rangeWidth];
                rangeWidth++;
            }
            if (sum == _input[lastIndex])
            {
                List<long> range = _input.GetRange(startIndex, rangeWidth);
                range.Sort();
                return range[0] + range[range.Count - 1];
            }
        }

        Debug.LogError("No solution was found");
        return 0;
    }
}
