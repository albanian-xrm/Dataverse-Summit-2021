using AccountPlugins.Create.PreValidation;
using AlbanianXrm.Datamodel;
using AlbanianXrm.Plugins;
using Microsoft.Xrm.Sdk;
using PluginsTests;
using System;
using Xunit;

namespace AccountPluginsTests.Create.PreValidation
{
    public class EnsureHasContactTests : FakeXrmEasyTestsBase
    {
        [Fact]
        public void Account_should_have_a_primary_contact()
        {
            //Arrange      
            var target = new Account()
            {
                Id = Guid.NewGuid(),
                Name = "AlbanianXrm"
            };
            var pluginContext = _context.GetDefaultPluginContext();
            pluginContext.InputParameters.Add(PluginBase.Target, target);

            //Act
            var exception = Record.Exception(() => _context.ExecutePluginWith<EnsureHasContact>(pluginContext));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<InvalidPluginExecutionException>(exception);
            Assert.Equal(EnsureHasContact.MustHaveContact, exception.Message);
        }

        [Fact]
        public void Account_should_have_a_name()
        {
            //Arrange
            var target = new Account()
            {
                Id = Guid.NewGuid(),
                PrimaryContactId = new EntityReference(Contact.EntityLogicalName, Guid.NewGuid())
            };
            var pluginContext = _context.GetDefaultPluginContext();
            pluginContext.InputParameters.Add(PluginBase.Target, target);

            //Act
            var exception = Record.Exception(() => _context.ExecutePluginWith<EnsureHasContact>(pluginContext));

            //Assert
            Assert.NotNull(exception);
            Assert.IsType<InvalidPluginExecutionException>(exception);
            Assert.Equal(EnsureHasContact.MustHaveName, exception.Message);
        }
    }
}
