using SmartTrade.Views;

namespace SmartTrade.Helpers;

public class LoadingScreenManager
{
    private readonly MainView _mainView;
    public bool IsLoadingHome = false;
    public bool IsLoadingCart = false;
    public bool IsLoadingUser = false;
    
    private static LoadingScreenManager _instance;
    public static LoadingScreenManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new LoadingScreenManager();
            }
            return _instance;
        }
    }

    private LoadingScreenManager()
    {
        _mainView = SmartTradeNavigationManager.Instance.MainView;
    }

    public int StartLoading()
    {
        ShowLoadingScreen();

        if (_mainView.SelectedButton == 0)  
            IsLoadingHome = true;
        else if(_mainView.SelectedButton == 1)
            IsLoadingCart = true;
        else if(_mainView.SelectedButton == 2)
            IsLoadingUser = true;

        return _mainView.SelectedButton;
    }

    public void ShowLoadingScreen()
    {
        _mainView.Loading.IsVisible = true;
        _mainView.ViewContent.IsVisible = false;
    }

    public void HideLoadingScreen()
    {
        _mainView.Loading.IsVisible = false;
        _mainView.ViewContent.IsVisible = true;
    }

    public void StopLoading(int i)
    {
        if (i == 0)
            IsLoadingHome = false;
        else if (i == 1)
            IsLoadingCart = false;
        else if (i == 2)
            IsLoadingUser = false;

        HideLoadingScreen();
    }
}