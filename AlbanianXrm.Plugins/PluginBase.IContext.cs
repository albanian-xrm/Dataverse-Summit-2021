using Microsoft.Xrm.Sdk;
using System;

namespace AlbanianXrm.Plugins
{
    public partial class PluginBase
    {
        public interface IContext
        {
            ITracingService TracingService { get; }
            IPluginExecutionContext PluginExecutionContext { get; }
            IOrganizationService GetOrganizationService();
            IOrganizationService GetOrganizationService(Guid systemuserid);
            IOrganizationService GetOrganizationServiceSystem();
            EntityReference GetTargetReference();
            Entity GetTarget();
            T GetTarget<T>() where T : Entity;
            Entity GetImage(string image = DefaultImage, bool postImage = false);
            T GetImage<T>(string image = DefaultImage, bool postImage = false) where T : Entity;
        }
    }
}
