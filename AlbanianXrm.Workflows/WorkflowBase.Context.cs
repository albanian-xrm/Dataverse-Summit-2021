using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;

namespace AlbanianXrm.Workflows
{
    public partial class WorkflowBase
    {
        public class Context : IContext
        {
            private readonly CodeActivityContext codeActivityContext;
            private ITracingService tracingService;
            private IWorkflowContext workflowContext;
            private IArguments arguments;
            private IOrganizationServiceFactory organizationServiceFactory;
            private IOrganizationService organizationServiceUser;
            private IOrganizationService organizationServiceSystem;
            private Dictionary<Guid, IOrganizationService> organizationServices;

            public Context(CodeActivityContext codeActivityContext)
            {
                this.codeActivityContext = codeActivityContext ?? throw new ArgumentNullException(nameof(codeActivityContext));
            }

            public ITracingService TracingService
            {
                get
                {
                    if (tracingService == null)
                    {
                        tracingService = codeActivityContext.GetExtension<ITracingService>();
                    }
                    return tracingService;
                }
            }

            public IWorkflowContext WorkflowContext
            {
                get
                {
                    if (workflowContext == null)
                    {
                        workflowContext = codeActivityContext.GetExtension<IWorkflowContext>();
                    }
                    return workflowContext;
                }
            }

            public IArguments Arguments
            {
                get
                {
                    if (arguments == null)
                    {
                        arguments = new Arguments(codeActivityContext);
                    }
                    return arguments;
                }
            }

            public IOrganizationService GetOrganizationService()
            {
                if (organizationServiceUser != null)
                {
                    return organizationServiceUser;
                }
                if (workflowContext == null)
                {
                    workflowContext = codeActivityContext.GetExtension<IWorkflowContext>();
                }
                if (organizationServiceFactory == null)
                {
                    organizationServiceFactory = codeActivityContext.GetExtension<IOrganizationServiceFactory>();
                }
                organizationServiceUser = organizationServiceFactory.CreateOrganizationService(workflowContext.UserId);
                return organizationServiceUser;
            }

            public IOrganizationService GetOrganizationService(Guid systemuserid)
            {
                IOrganizationService organizationService;
                if (organizationServices == null)
                {
                    organizationServices = new Dictionary<Guid, IOrganizationService>();
                }
                else if (organizationServices.TryGetValue(systemuserid, out organizationService))
                {
                    return organizationService;
                }
                if (workflowContext == null)
                {
                    workflowContext = codeActivityContext.GetExtension<IWorkflowContext>();
                }
                if (organizationServiceFactory == null)
                {
                    organizationServiceFactory = codeActivityContext.GetExtension<IOrganizationServiceFactory>();
                }
                if (systemuserid == workflowContext.UserId)
                {
                    if (organizationServiceUser == null)
                    {
                        organizationServiceUser = organizationServiceFactory.CreateOrganizationService(systemuserid);
                    }
                    organizationService = organizationServiceUser;
                }
                else
                {
                    organizationService = organizationServiceFactory.CreateOrganizationService(systemuserid);
                }

                organizationServices.Add(systemuserid, organizationService);
                return organizationService;
            }

            public IOrganizationService GetOrganizationServiceSystem()
            {
                if (organizationServiceSystem != null)
                {
                    return organizationServiceSystem;
                }
                if (organizationServiceFactory == null)
                {
                    organizationServiceFactory = codeActivityContext.GetExtension<IOrganizationServiceFactory>();
                }
                organizationServiceSystem = organizationServiceFactory.CreateOrganizationService(null);
                return organizationServiceSystem;
            }
        }
    }
}
