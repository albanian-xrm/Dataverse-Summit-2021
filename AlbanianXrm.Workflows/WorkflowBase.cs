using Microsoft.Xrm.Sdk;
using System;
using System.Activities;
using System.ServiceModel;

namespace AlbanianXrm.Workflows
{
    public abstract partial class WorkflowBase : CodeActivity
    {
        private readonly string workflowName;

        public WorkflowBase()
        {
            workflowName = GetType().Name;
        }

        protected override void Execute(CodeActivityContext codeActivityContext)
        {
            var context = new Context(codeActivityContext);
            try
            {
                Execute(context);
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                throw new InvalidPluginExecutionException($"An error occurred in the {workflowName} workflow step.", ex);
            }
            catch (Exception ex)
            {
                context.TracingService.Trace("{0}: {1}", workflowName, ex.ToString());
                throw;
            }
        }

        protected abstract void Execute(IContext context);
    }
}
