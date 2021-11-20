using System.Collections.Generic;
using Xunit;

namespace WorkflowTests
{
    public class GreetUserTests : FakeXrmEasyTestsBase
    {
        [Fact]
        public void UserId_should_be_positive()
        {
            var inputs = new Dictionary<string, object>
            {
                {nameof(GreetUser.UserId), 0 }
            };

            var ex = Record.Exception(() => _context.ExecuteCodeActivity<GreetUser>(inputs));

            Assert.NotNull(ex);
            Assert.Equal(GreetUser.USER_ID_POSITIVE, ex.Message);
        }

        [Fact]
        public void GreetUser_should_return_online_status()
        {
            var inputs = new Dictionary<string, object>
            {
                {nameof(GreetUser.UserId), 2 }
            };

            var outputs = _context.ExecuteCodeActivity<GreetUser>(inputs);

            Assert.Contains(nameof(GreetUser.WasOnline), outputs.Keys);
            Assert.True((bool)outputs[nameof(GreetUser.WasOnline)]);
        }
    }
}
