using System.ComponentModel;

namespace Covalence.Core
{
    public static class ReflectionExtensions
    {

        public static object GetPropertyValue(
            this INotifyPropertyChanged viewModel,
            string propertyName)
        {
            return viewModel.GetType().GetProperty(propertyName).GetValue(viewModel, null);
        }

        public static void SetPropertyValue(
            this INotifyPropertyChanged viewModel,
            string propertyName,
            object value)
        {
            viewModel.GetType().GetProperty(propertyName).SetValue(viewModel, value, null);
        }
    }
}

