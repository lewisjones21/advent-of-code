using System;
using UnityEngine;

public class Calculator : CalculatorBase<Tuple<int, int, char, string>, int>
{
    protected override Tuple<int, int, char, string> ParseInput(string inputLine)
    {
        string[] splitInput = inputLine.Split('-', ' ', ':');
        return new Tuple<int, int, char, string>(int.Parse(splitInput[0]),
                                                 int.Parse(splitInput[1]),
                                                 splitInput[2][0],
                                                 splitInput[4]);
    }

    protected override int CalculateOutput()
    {
        int validInputs = 0;
        int charCount;
        for (int i = 0; i < _input.Count; i++)
        {
            charCount = _input[i].Item4.Split(_input[i].Item3).Length - 1;
            if (charCount >= _input[i].Item1 && charCount <= _input[i].Item2)
            {
                validInputs++;
            }
        }
        return validInputs;
    }
}
