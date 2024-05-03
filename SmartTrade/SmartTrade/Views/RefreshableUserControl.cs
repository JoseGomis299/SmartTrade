using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace SmartTrade.Views
{
    public class RefreshableUserControl : UserControl
    {
        private bool _stackHasChanged = true;
        private int _currentStack = 0;

        public RefreshableUserControl()
        {
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack += SetStack;

            SmartTradeNavigationManager.Instance.OnNavigate += Refresh;
            SmartTradeNavigationManager.Instance.OnNavigate += RefreshAsync;
        }

        private void Refresh(Type viewType)
        {
            if (viewType == GetType())
            {
                Refresh();

                if (_stackHasChanged)
                {
                    _stackHasChanged = false;
                }
            }
            else if (!_stackHasChanged)
            {
                Dispose();
            }
        }

        private async void RefreshAsync(Type viewType)
        {
            if (viewType == GetType())
            {
                await RefreshAsync();

                if (_stackHasChanged)
                {
                    _stackHasChanged = false;
                }
            }
            else if (!_stackHasChanged)
            {
                Dispose();
            }
        }

        protected virtual void Refresh()
        {
        }

        protected virtual async Task RefreshAsync()
        {
        }

        public void Dispose()
        {
            SmartTradeNavigationManager.Instance.OnNavigate -= Refresh;
            SmartTradeNavigationManager.Instance.OnNavigate -= RefreshAsync;
            SmartTradeNavigationManager.Instance.OnChangeNavigationStack -= SetStack;
        }

        private void SetStack(int stack)
        {
            _stackHasChanged = stack != _currentStack;
            _currentStack = stack;
        }
    }
}
