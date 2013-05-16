using System.ComponentModel;

namespace DownMarker.Core
{
    public static class NotifyPropertyChangedExtensions
    {
        public static PropertyEvents<TNotifier> CreatePropertyEvents<TNotifier>(this TNotifier notifier)
            where TNotifier : INotifyPropertyChanged
        {
            return new PropertyEvents<TNotifier>(notifier);
        }
    }
}
