using AlbanianXrm.Workflows;
using Microsoft.Xrm.Sdk;
using System;
using System.Activities;

public class GreetUser : WorkflowBase
{
    public const string USER_ID_POSITIVE = "UserId must be positive";

    [RequiredArgument]
    public InArgument<int> UserId { get; set; }

    public OutArgument<bool> WasOnline { get; set; }

    protected override void Execute(IContext context)
    {
        int userId = context.Arguments.GetValue(UserId);
        if (userId <= 0)
        {
            throw new InvalidPluginExecutionException(USER_ID_POSITIVE);
        }

        context.Arguments.SetValue(WasOnline, userId % 2 == 0);
    }
}
