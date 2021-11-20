using AlbanianXrm.Datamodel;
using AlbanianXrm.Plugins.AccountPlugins.Create.PostOperation;
using PluginsTests;
using System;
using System.Linq;
using Xunit;

namespace AlbanianXrm.Plugins.Tests.AccountPluginsTests.Create.PostOperation
{
    public class SetContactsCustomerTests : FakeXrmEasyTestsBase
    {
        [Fact]
        public void Created_accounts_should_set_primary_contacts_parent_customer()
        {
            //Arrange
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "Betim",
                LastName = "Beja"
            };
            _context.Initialize(contact);

            var target = new Account()
            {
                Id = Guid.NewGuid(),
                Name = "AlbanianXrm",
                PrimaryContactId = contact.ToEntityReference()
            };
            var pluginContext = _context.GetDefaultPluginContext();
            pluginContext.InputParameters.Add(PluginBase.Target, target);

            //Act
            _context.ExecutePluginWith<SetContactsCustomer>(pluginContext);

            //Assert
            var updatedContact = _context.CreateQuery<Contact>().FirstOrDefault();
            Assert.NotNull(updatedContact);
            Assert.NotNull(updatedContact.ParentCustomerId);
            Assert.Equal(target.Id, updatedContact.ParentCustomerId.Id);
        }
    }
}
