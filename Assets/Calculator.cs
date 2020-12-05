using System;
using System.Collections.Generic;

public class Calculator : CalculatorBase<List<Tuple<int, string>>, long>
{
    private readonly List<string> _requiredFieldNames = new List<string> {
        "byr",
        "iyr",
        "eyr",
        "hgt",
        "hcl",
        "ecl",
        "pid"
    };

    private readonly List<string> _possibleFieldNames = new List<string> {
        "byr",
        "iyr",
        "eyr",
        "hgt",
        "hcl",
        "ecl",
        "pid",
        "cid"
    };

    protected override List<Tuple<int, string>> ParseInputLine(string inputLine)
    {
        List<string> splitValues = new List<string>(inputLine.Split(':', ' '));
        List<Tuple<int, string>> returnValues = new List<Tuple<int, string>>();
        if (splitValues.Count > 1)
        {
            for (int i = 0; i < splitValues.Count; i += 2)
            {
                returnValues.Add(new Tuple<int, string>(_possibleFieldNames.IndexOf(splitValues[i]),
                                                        splitValues[i + 1]));
            }
        }
        return returnValues;
    }

    protected override long CalculateOutput()
    {
        // Determine the bitmask required for a valid input
        int validInputMask = 0;
        for (int i = 0; i < _requiredFieldNames.Count; i++)
        {
            validInputMask += 1 << _possibleFieldNames.IndexOf(_requiredFieldNames[i]);
        }

        // Calculate the bitmasks of each input paragraph
        List<int> inputValidations = new List<int>();
        inputValidations.Add(0);
        for (int i = 0; i < _input.Count; i++)
        {
            if (_input[i].Count == 0)
            {
                inputValidations.Add(0);
            }
            else
            {
                for (int j = 0; j < _input[i].Count; j++)
                {
                    inputValidations[inputValidations.Count - 1] += 1 << _input[i][j].Item1;
                }
            }
        }

        // Count how many inputs have valid bitmasks
        int validInputCount = 0;
        for (int v = 0; v < inputValidations.Count; v++)
        {
            if ((inputValidations[v] & validInputMask) == validInputMask)
            {
                validInputCount++;
            }
        }

        return validInputCount;
    }
}
