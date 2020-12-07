using System.Collections.Generic;

public class Calculator : CalculatorBase<int, long>
{
    protected override int ParseInputLine(string inputLine)
    {
        int returnValue = 0;
        if (inputLine.Length == 0)
        {
            return 0;
        }
        for (int i = 0; i < inputLine.Length; i++)
        {
            returnValue += 1 << (inputLine[i] - 'a');
        }
        return returnValue;
    }

    protected override long CalculateOutput()
    {
        // Combine the individual input entries into their collective group values
        List<int> groupBitMasks = new List<int>();
        groupBitMasks.Add(_input[0]);
        for (int i = 1; i < _input.Count; i++)
        {
            if (_input[i] == 0)
            {
                groupBitMasks.Add(_input[++i]);
            }
            else
            {
                groupBitMasks[groupBitMasks.Count - 1] |= _input[i];
            }
        }
        // Count the set bits in the bitmask of each group
        List<int> groupBitCounts = new List<int>();
        for (int g = 0; g < groupBitMasks.Count; g++)
        {
            groupBitCounts.Add(CountSetBits(groupBitMasks[g]));
        }
        // Sum the number of bits set to '1' across all grouped entries
        long sumAcrossGroups = 0;
        for (int g = 0; g < groupBitCounts.Count; g++)
        {
            sumAcrossGroups += groupBitCounts[g];
        }

        return sumAcrossGroups;
    }

    private int CountSetBits(int n)
    {
        int totalBitsSet = 0;
        // While any bits are set
        while (n != 0)
        {
            // Bitwise AND with n - 1 unsets the rightmost set bit
            n &= n - 1;
            // Count the bit that was removed
            totalBitsSet++;
        }
        return totalBitsSet;
    }
}
