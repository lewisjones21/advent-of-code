using UnityEngine;

public class Calculator : MonoBehaviour
{
    private const int MIN = 278384, MAX = 824795;

    private void Awake()
    {
        Debug.Log("Output is: " + Calculate().ToString());
    }

    private int Calculate()
    {
        int validCount = 0;
        for (int i = MIN; i <= MAX; i++)
        {
            if (GetIsValid(i))
            {
                validCount++;
            }
        }
        return validCount;
    }

    private bool GetIsValid(int value)
    {
        int maxDigitIndex = (int)Mathf.Log10(value) + 1, digitIndex = 0, digit = 1, lastDigit = 0,
            removalSum = 0;

        // Value must have 6 digits
        if (maxDigitIndex != 6)
        {
            return false;
        }

        bool hasAdjacentMatch = false;
        while (digitIndex < maxDigitIndex)
        {
            digit = (int)(value / Mathf.Pow(10, maxDigitIndex - (digitIndex + 1))) - removalSum;

            // Digits may not decrease in size
            if (digit < lastDigit)
            {
                return false;
            }

            // There must be at least one pair of matching digits
            hasAdjacentMatch |= digit == lastDigit;

            lastDigit = digit;
            digitIndex++;
            removalSum += lastDigit;
            removalSum *= 10;
        }
        return hasAdjacentMatch;
    }
}
