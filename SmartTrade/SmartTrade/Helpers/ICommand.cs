namespace SmartTrade.Navigation;

public interface ICommand
{
    void Execute();
    void UnExecute();
}