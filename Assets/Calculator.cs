using System;
using System.Collections.Generic;
using UnityEngine;

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

    private readonly List<string> _possibleEyeColours = new List<string> {
        "amb",
        "blu",
        "brn",
        "gry",
        "grn",
        "hzl",
        "oth"
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
        // Combine the distributed inputs into comprehensive entries
        List<List<Tuple<int, string>>> combinedInputs = new List<List<Tuple<int, string>>>();
        combinedInputs.Add(new List<Tuple<int, string>>());
        for (int i = 0; i < _input.Count; i++)
        {
            if (_input[i].Count == 0)
            {
                combinedInputs.Add(new List<Tuple<int, string>>());
            }
            else
            {
                for (int j = 0; j < _input[i].Count; j++)
                {
                    combinedInputs[combinedInputs.Count - 1].Add(_input[i][j]);
                }
            }
        }

        // Validate the bitmasks of each input
        int currentMask;
        List<bool> inputValidations = new List<bool>();
        for (int i = 0; i < combinedInputs.Count; i++)
        {
            inputValidations.Add(true);
            currentMask = 0;
            for (int j = 0; j < combinedInputs[i].Count; j++)
            {
                currentMask += 1 << combinedInputs[i][j].Item1;
            }
            if ((currentMask & validInputMask) != validInputMask)
            {
                inputValidations[inputValidations.Count - 1] = false;
            }
        }

        if (inputValidations.Count != combinedInputs.Count)
        {
            Debug.LogError("Error combining inputs; cannot produce valid output");
            return 0;
        }

        // Validate each field on each input
        for (int i = 0; i < combinedInputs.Count; i++)
        {
            for (int f = 0; f < combinedInputs[i].Count; f++)
            {
                if (inputValidations[i] == false)
                {
                    break;
                }
                string str = combinedInputs[i][f].Item2;
                switch (combinedInputs[i][f].Item1)
                {
                    // Birth Year
                    case 0:
                        int byr = int.Parse(str);
                        if (byr < 1920 || byr > 2002)
                        {
                            Debug.LogFormat("Found invalid Birth Year ({0}) in entry {1}", str, i);
                            inputValidations[i] = false;
                        }
                        break;
                    // Issue Year
                    case 1:
                        int iyr = int.Parse(str);
                        if (iyr < 2010 || iyr > 2020)
                        {
                            Debug.LogFormat("Found invalid Issue Year ({0}) in entry {1}", str, i);
                            inputValidations[i] = false;
                        }
                        break;
                    // Expiration Year
                    case 2:
                        int eyr = int.Parse(str);
                        if (eyr < 2020 || eyr > 2030)
                        {
                            Debug.LogFormat("Found invalid Expiration Year ({0}) in entry {1}",
                                            str, i);
                            inputValidations[i] = false;
                        }
                        break;
                    // Height
                    case 3:
                        if (str.Contains("cm"))
                        {
                            int hgt = int.Parse(str.Remove(str.IndexOf("cm")));
                            if (hgt < 150 || hgt > 193)
                            {
                                Debug.LogFormat("Found invalid Height ({0}) in entry {1}", str, i);
                                inputValidations[i] = false;
                            }
                        }
                        else if (str.Contains("in"))
                        {
                            int hgt = int.Parse(str.Remove(str.IndexOf("in")));
                            if (hgt < 59 || hgt > 76)
                            {
                                Debug.LogFormat("Found invalid Height ({0}) in entry {1}", str, i);
                                inputValidations[i] = false;
                            }
                        }
                        else
                        {
                            Debug.LogFormat("Found invalid Height ({0}) in entry {1}", str, i);
                            inputValidations[i] = false;
                        }
                        break;
                    // Hair Colour
                    case 4:
                        if (str[0] != '#' || str.Length > 7)
                        {
                            Debug.LogFormat("Found invalid Hair Colour ({0}) in entry {1}", str, i);
                            inputValidations[i] = false;
                            break;
                        }
                        for (int charIndex = 1; charIndex < 7; charIndex++)
                        {
                            if (!((str[charIndex] >= '0' && str[charIndex] <= '9')
                                  || (str[charIndex] >= 'a' && str[charIndex] <= 'f')))
                            {
                                Debug.LogFormat("Found invalid Hair Colour ({0}) in entry {1}",
                                                str, i);
                                inputValidations[i] = false;
                                break;
                            }
                        }
                        break;
                    // Eye Colour
                    case 5:
                        if (!_possibleEyeColours.Contains(str))
                        {
                            Debug.LogFormat("Found invalid Eye Colour ({0}) in entry {1}", str, i);
                            inputValidations[i] = false;
                        }
                        break;
                    // Passport ID
                    case 6:
                        if (str.Length != 9)
                        {
                            Debug.LogFormat("Found invalid Passport ID ({0}) in entry {1}", str, i);
                            inputValidations[i] = false;
                            break;
                        }
                        for (int charIndex = 1; charIndex < 7; charIndex++)
                        {
                            if (!(str[charIndex] >= '0' && str[charIndex] <= '9'))
                            {
                                Debug.LogFormat("Found invalid Passport ID ({0}) in entry {1}",
                                                str, i);
                                inputValidations[i] = false;
                                break;
                            }
                        }
                        break;
                }
            }
        }

        // Count how many inputs are valid
        int validInputCount = 0;
        for (int v = 0; v < inputValidations.Count; v++)
        {
            if (inputValidations[v] == true)
            {
                validInputCount++;
            }
        }

        return validInputCount;
    }
}
