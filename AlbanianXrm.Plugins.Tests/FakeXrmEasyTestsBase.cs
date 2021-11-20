using FakeXrmEasy;
using Microsoft.Xrm.Sdk;

namespace PluginsTests
{
    public class FakeXrmEasyTestsBase
    {
        protected readonly IOrganizationService _service;
        protected readonly XrmFakedContext _context;

        public FakeXrmEasyTestsBase()
        {
            _context = new XrmFakedContext();
            _service = _context.GetOrganizationService();
        }
    }
}
