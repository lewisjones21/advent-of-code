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
        bool pos1IsChar, pos2IsChar;
        for (int i = 0; i < _input.Count; i++)
        {
            pos1IsChar = _input[i].Item4.Length >= _input[i].Item1
                && _input[i].Item4[_input[i].Item1 - 1] == _input[i].Item3;
            pos2IsChar = _input[i].Item4.Length >= _input[i].Item2
                && _input[i].Item4[_input[i].Item2 - 1] == _input[i].Item3;
            if ((pos1IsChar && !pos2IsChar) || (!pos1IsChar && pos2IsChar))
            {
                validInputs++;
            }
        }
        return validInputs;
    }
}
