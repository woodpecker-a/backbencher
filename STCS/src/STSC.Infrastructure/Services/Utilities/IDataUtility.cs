﻿using System.Data;

namespace STCS.Infrastructure.Services.Utilities;

public interface IDataUtility
{
    Task ExecuteCommandAsync(string command, Dictionary<string, object> parameters,
        CommandType commandType);
    Task<List<Dictionary<string, object>>> GetDataAsync(string command,
        Dictionary<string, object> parameters, CommandType commandType);
}
