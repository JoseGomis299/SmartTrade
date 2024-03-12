using System;
using System.Collections.Generic;
using Avalonia.Controls;

namespace GetStartedProject
{
    public class ViewNavigator : Navigator
    {
        private readonly Dictionary<Type, UserControl> _views = new();

        public ViewNavigator(ContentControl mainView) : base(mainView)
        {
        }

        public override void NavigateTo(Type viewType)
        {
            UserControl? view = null;

            if (_views.ContainsKey(viewType))
            {
                view = _views[viewType];
            }
            else
            {
                view = Activator.CreateInstance(viewType) as UserControl;
                _views.Add(viewType, view);
            }

            MainView.Content = view;
        }

        public override void NavigateTo(ContentControl view)
        {
            MainView.Content = view;
            _views.AddOrUpdate(view.GetType(), (UserControl) view);
        }
    }
}
