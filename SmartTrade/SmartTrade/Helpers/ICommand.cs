namespace SmartTrade;

public interface ICommand
{
    void Execute();
    void UnExecute();
}