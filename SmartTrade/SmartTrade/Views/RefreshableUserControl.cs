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
            EventBus.Subscribe<int>(this, "OnChangeNavigationStack", SetStack);

            EventBus.Subscribe<Type>(this, "OnNavigate", Refresh);
            EventBus.Subscribe<Type>(this, "OnNavigate", RefreshAsync);

            _currentStack = SmartTradeNavigationManager.Instance.CurrentStack;
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
            else if (!_stackHasChanged && !SmartTradeNavigationManager.Instance.IsViewOnStack(viewType, _currentStack))
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
            else if (!_stackHasChanged && !SmartTradeNavigationManager.Instance.IsViewOnStack(viewType, _currentStack))
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

        protected virtual void Dispose()
        {
            EventBus.UnsubscribeFromAllEvents(this);
        }

        private void SetStack(int stack)
        {
            _stackHasChanged = stack != _currentStack;
            _currentStack = stack;
        }
    }
}
