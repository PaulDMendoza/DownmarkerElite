using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DownMarker.Core
{
    public class PropertyEvents<TNotifier> : IDisposable
        where TNotifier : INotifyPropertyChanged
    {
        private readonly TNotifier notifier;
        private Dictionary<string, PropertyEvent> map;

        /// <summary>
        /// Create a new <see cref="PropertyEvents"/> instance for the given
        /// <see cref="INotifyPropertyChanged"/> object.
        /// </summary>
        public PropertyEvents(TNotifier notifier)
        {
            this.notifier = notifier;
            this.map = new Dictionary<string, PropertyEvent>();
        }

        /// <summary>
        /// Returns a <see cref="PropertyEvent"/> for the property accessed
        /// in the given expression.
        /// </summary>
        public PropertyEvent this[Expression<Func<TNotifier, object>> expression]
        {
            get
            {
                return GetPropertyEvent(expression.GetPropertyName());
            }
            set
            {
                // ignore, only necessary to enable += operator
            }
        }

        /// <summary>
        /// Raises all events that have been subscribed to.
        /// </summary>
        /// <remarks>
        /// This is a convenience method which can be called after a view
        /// has subcribed itself to the events it is interested in. This way,
        /// the view can respond to the resulting change events rather than
        /// going through a separate initialization routine for each property.
        /// </remarks>
        public void RaiseAll()
        {
            foreach (var propertyEvent in this.map.Values)
            {
                propertyEvent.Raise();
            }
        }

        private PropertyEvent GetPropertyEvent(string propertyName)
        {
            PropertyEvent propertyEvent;
            if (!map.TryGetValue(propertyName, out propertyEvent))
            {
                propertyEvent = new PropertyEvent(this.notifier, propertyName);
                map[propertyName] = propertyEvent;
            }
            return propertyEvent;
        }

        public void Dispose()
        {
            foreach (var propertyEvent in this.map.Values)
            {
                propertyEvent.Dispose();
            }
            this.map.Clear();
        }
    }
}
