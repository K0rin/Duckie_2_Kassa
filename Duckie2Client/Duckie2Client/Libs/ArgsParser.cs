using System;
using System.Collections.Generic;
using System.Linq;

namespace Duckie2Client.Libs;

public class ArgsParser
{
    private readonly string[] _args;
    private readonly string _option;
    private const int ValidArgsNumber = 2;
    private readonly string[] _modeNames = ["console", "kassa"];
    private readonly Dictionary<string, string> _errMessages = new();

    public ArgsParser(string[] args, string option)
    {
        _args = args;
        _option = option;

        _errMessages.Add(
            "ERR01", "Invalid number of command line arguments.");
        _errMessages.Add(
            "ERR02", "There is no option in a command line arguments.");
        _errMessages.Add(
            "ERR03", "The option has invalid name.");
        _errMessages.Add(
            "ERR04", "There is no such application working mode: {*}.");
    }

    public void CheckArgs()
    {
        // Check valid number of args.
        if (_args.Length != ValidArgsNumber)
        {
            throw new Exception(_errMessages["ERR01"]);
        }

        // Check if first argument is option.
        if (!_args[0].StartsWith("--"))
        {
            throw new Exception(_errMessages["ERR02"]);
        }

        // Check if option has valid name.
        if (!_args[0].Equals($"--{_option}"))
        {
            throw new Exception(_errMessages["ERR03"]);
        }

        // Check if mode name is valid.

        var isNameValid = _modeNames.Contains(_args[1]);
        if (!isNameValid)
        {
            throw new Exception(
                _errMessages["ERR04"].Replace("{*}", _args[1]));
        }
    }
}