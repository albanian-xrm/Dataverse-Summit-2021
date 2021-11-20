using System.Activities;

namespace AlbanianXrm.Workflows
{
    public partial class WorkflowBase
    {
        public interface IArguments
        {
            T GetValue<T>(InArgument<T> @in);
            T GetValue<T>(InOutArgument<T> @in);
            void SetValue<T>(InOutArgument<T> @out, T value);
            void SetValue<T>(OutArgument<T> @out, T value);
        }
    }
}
