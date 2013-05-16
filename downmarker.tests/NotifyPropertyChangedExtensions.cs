using System;
using System.ComponentModel;
using System.Linq.Expressions;
using DownMarker.Core;

namespace DownMarker.Tests
{
    public static class NotifyPropertyChangedExtensions
    {
        public static Func<bool> WatchPropertyEvent<TNotify>(
            this TNotify viewModel,
            Expression<Func<TNotify,object>> propertyExpression)
            where TNotify : INotifyPropertyChanged
        {
            string name = propertyExpression.GetPropertyName();
            bool raised = false;
            viewModel.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == name)
                    {
                        raised = true;
                    }
                };
            return delegate { return raised; };
        }
    }
}
