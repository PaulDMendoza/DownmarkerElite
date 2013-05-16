using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DownMarker.Core
{

    /// <summary>
    /// Abstract base class for view models, with infrastructure to help safely
    /// implement <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class ViewModelBase<TClass> : INotifyPropertyChanged
        where TClass : ViewModelBase<TClass>
    {

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event for the property
        /// in the given expression.
        /// </summary>
        /// <param name="expression">
        /// A member expression used to refer to the changed property in a type-safe way.
        /// For example, to raise an event for the "Foo" property you could pass the expression
        /// "x => x.Foo".
        /// </param>
        /// <exception cref="ArgumentException">
        /// The given expression is not a member expression for a property.
        /// </exception>
        protected void OnPropertyChanged(Expression<Func<TClass,object>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            // not in if block because we want the ArgumentException even if no one is listening
            string name = expression.GetPropertyName();

            OnPropertyChanged(name);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
