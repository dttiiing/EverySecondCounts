using System;
using System.Reflection;

public enum PlayerState
{
    INVALID = -1,
    [StateName("normalPlayer")]
    NORMAL = 0,
    [StateName("softPlayer")]
    SOFT = 1,
    [StateName("hardPlayer")]
    HARD = 2,
}

[AttributeUsage(AttributeTargets.Field)]
public class StateNameAttribute : Attribute
{
    public string Name { get; }

    public StateNameAttribute(string name) { Name = name; }
}

public static class PlayerStateExtensions
{
    public static string GetObjetName(this PlayerState state)
    {
        FieldInfo field = state.GetType().GetField(state.ToString());
        StateNameAttribute attribute = field?.GetCustomAttribute<StateNameAttribute>();
        return attribute?.Name ?? string.Empty;
    }
}
