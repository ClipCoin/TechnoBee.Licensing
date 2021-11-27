
namespace TechnoBee.Licensing.Utilities.DeploymentAbstractions.Commands {
    public interface ICommand {
        CommandExecutionResult Execute();
    }

    public interface ICommand<T> : ICommand {
        T InputData { get; set; }
    }

    public interface ICommandResult<T> : ICommand {
        T OutputData { get; }
    }

}
