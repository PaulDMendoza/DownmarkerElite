using System;
using System.ComponentModel;

namespace DownMarker.Core
{
    public class PropertyEvent : IDisposable
    {
        private readonly INotifyPropertyChanged notifier;
        private readonly string propertyName;

        public PropertyEvent(INotifyPropertyChanged notifier, string propertyName)
        {
            this.propertyName = propertyName;
            this.notifier = notifier;
            this.notifier.PropertyChanged += HandlePropertyChanged;
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == this.propertyName)
            {
                Raise();
            }
        }

        /// <summary>
        /// Forces the <see cref="Changed"/> event to be raised.
        /// </summary>
        public void Raise()
        {
            if (Changed != null)
            {
                Changed(this, EventArgs.Empty);
            }
        }

        public event EventHandler Changed;

        public static PropertyEvent operator +(
            PropertyEvent propertyEvent,
            EventHandler eventHandler)
        {
            propertyEvent.Changed += eventHandler;
            return propertyEvent;
        }

        public void Dispose()
        {
            this.notifier.PropertyChanged -= HandlePropertyChanged;
        }
    }
}
