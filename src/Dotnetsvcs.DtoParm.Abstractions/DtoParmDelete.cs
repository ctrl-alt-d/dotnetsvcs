﻿namespace Dotnetsvcs.DtoParm.Abstractions;

public abstract class DtoParmDelete : IDtoParm
{
    public abstract object?[] KeyValues { get; set; }
    public abstract object? Version { get; set; }
}
