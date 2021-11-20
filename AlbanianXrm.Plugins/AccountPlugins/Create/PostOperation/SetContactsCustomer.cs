using AlbanianXrm.Datamodel;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbanianXrm.Plugins.AccountPlugins.Create.PostOperation
{
    public class SetContactsCustomer : PluginBase
    {
        protected override void Execute(IContext context)
        {
            var account = context.GetTarget<Account>() ?? 
                throw new InvalidPluginExecutionException(Messages.TargetMissing);
   
            var primaryContact = new Contact
            {
                Id = account.PrimaryContactId.Id,
                ParentCustomerId = account.ToEntityReference()
            };
            context.GetOrganizationService().Update(primaryContact);
        }
    }
}
