using AlbanianXrm.Datamodel;
using AlbanianXrm.Plugins;
using Microsoft.Xrm.Sdk;

namespace AccountPlugins.Create.PreValidation
{
    public class EnsureHasContact : PluginBase
    {
        public const string MustHaveContact = "All Accounts must have a Primary Contact";
        public const string MustHaveName = "All Accounts must have a name";

        protected override void Execute(IContext context)
        {
            var account = context.GetTarget<Account>() ?? throw new InvalidPluginExecutionException(Messages.TargetMissing);
            if (account.PrimaryContactId == null) { throw new InvalidPluginExecutionException(MustHaveContact); }
            if (string.IsNullOrWhiteSpace(account.Name)) { throw new InvalidPluginExecutionException(MustHaveName); }
        }
    }
}
