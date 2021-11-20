using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;

namespace AlbanianXrm.Workflows
{
    public partial class WorkflowBase
    {
        public interface IContext
        {
            ITracingService TracingService { get; }
            IWorkflowContext WorkflowContext { get; }
            IArguments Arguments { get; }
            IOrganizationService GetOrganizationService();
            IOrganizationService GetOrganizationService(Guid systemuserid);
            IOrganizationService GetOrganizationServiceSystem();
        }
    }
}
