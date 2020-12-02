using System.IO;
using System.Collections.Generic;
using UnityEngine;

public abstract class CalculatorBase<TInput, TOutput> : MonoBehaviour
{
    [SerializeField]
    protected string _inputFilename = "Assets/Input.txt";

    [SerializeField]
    protected List<TInput> _input = new List<TInput>();

    [SerializeField]
    protected TOutput _output = default;

    private void Awake()
    {
        Process();
    }

    private void Process()
    {
        _input = FetchInput();
        if (_input == null || _input.Count == 0)
        {
            Debug.LogError("Cannot calculate output; input is empty");
            return;
        }
        else
        {
            Debug.LogFormat("Fetched {0} input entries", _input.Count);
        }
        _output = CalculateOutput();
        Debug.Log("Output is: " + _output);
    }

    private List<TInput> FetchInput()
    {
        List<TInput> values = new List<TInput>();
        if (string.IsNullOrWhiteSpace(_inputFilename))
        {
            Debug.LogError("Cannot fetch input; input filename is empty");
            return values;
        }
        StreamReader reader = new StreamReader(_inputFilename);
        while (!reader.EndOfStream)
        {
            values.Add(ParseInput(reader.ReadLine()));
        }
        return values;
    }

    protected abstract TInput ParseInput(string inputLine);

    protected abstract TOutput CalculateOutput();
}
