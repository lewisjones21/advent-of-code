using System.Collections.Generic;
using UnityEngine;

public class Calculator : CalculatorBase<long, long>
{
    private class TernaryGraphNode
    {
        public TernaryGraphNode child1, child2, child3;
    }

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

        // For each number, map the subsequent numbers it can connect to
        Dictionary<long, TernaryGraphNode> nodeMap = new Dictionary<long, TernaryGraphNode>();
        TernaryGraphNode currentNode;
        for (int i = _input.Count - 1; i >= 0; i--)
        {
            currentNode = new TernaryGraphNode();

            for (int offset = 1; offset <= 3; offset++)
            {
                if (i + offset < _input.Count)
                {
                    switch (_input[i + offset] - _input[i])
                    {
                        case 1:
                            nodeMap.TryGetValue(_input[i + offset], out currentNode.child1);
                            break;
                        case 2:
                            nodeMap.TryGetValue(_input[i + offset], out currentNode.child2);
                            break;
                        case 3:
                            nodeMap.TryGetValue(_input[i + offset], out currentNode.child3);
                            break;
                    }
                }
            }

            nodeMap.Add(_input[i], currentNode);
        }
        Debug.LogFormat("Generated {0} ternary tree nodes", nodeMap.Count);

        // Traverse the ternary tree, counting the number of possible paths 
        Dictionary<TernaryGraphNode, long> onwardPathCounts
            = new Dictionary<TernaryGraphNode, long>();
        onwardPathCounts.Add(nodeMap[_input[_input.Count - 1]], 1);
        CountOnwardPaths(onwardPathCounts, nodeMap[0]);

        for (int i = _input.Count - 1; i >= 0; i--)
        {
            Debug.LogFormat("Input {0} ({1}) has {2} onward path possibilities",
                            i, _input[i], onwardPathCounts[nodeMap[_input[i]]]);
        }

        return onwardPathCounts[nodeMap[_input[0]]];
    }

    private long CountOnwardPaths(Dictionary<TernaryGraphNode, long> onwardPathCounts,
                                  TernaryGraphNode node)
    {
        if (!onwardPathCounts.ContainsKey(node))
        {
            long count = 0;
            if (node.child1 != null)
            {
                count += CountOnwardPaths(onwardPathCounts, node.child1);
            }
            if (node.child2 != null)
            {
                count += CountOnwardPaths(onwardPathCounts, node.child2);
            }
            if (node.child3 != null)
            {
                count += CountOnwardPaths(onwardPathCounts, node.child3);
            }
            onwardPathCounts.Add(node, count);
        }
        return onwardPathCounts[node];
    }
}
