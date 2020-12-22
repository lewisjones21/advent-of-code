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
        // Sort the inputs into ascending order
        _input.Sort();

        // Add the starting input of 0
        _input.Insert(0, 0);
        // Add the final input of [largest input + 3]
        _input.Add(_input[_input.Count - 1] + 3);

        // Determine how many of each step size is present in the inputs (plus an extra 3)
        int diffsOf1 = 0, diffsOf2 = 0, diffsOf3 = 0;
        for (int i = 1; i < _input.Count; i++)
        {
            switch (_input[i] - _input[i - 1])
            {
                case 1:
                    diffsOf1++;
                    break;
                case 2:
                    diffsOf2++;
                    break;
                case 3:
                    diffsOf3++;
                    break;
                default:
                    Debug.LogErrorFormat("Unexpected input delta of {0} at index {1}",
                                         _input[i] - _input[i - 1], i);
                    break;
            }
        }
        Debug.LogFormat("There were {0}, {1}, {2} diffs of 1, 2, 3",
                        diffsOf1, diffsOf2, diffsOf3);
        return diffsOf1 * diffsOf3;
    }
}
