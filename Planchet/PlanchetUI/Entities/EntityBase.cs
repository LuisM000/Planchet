using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlanchetUI.Entities
{
    /// <summary>
    /// Base of PlanchetUI entities. Implemented INotifyPropertyChanged
    /// </summary>
    public class EntityBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;

            if (PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(expression.Member.Name);
                PropertyChanged(this, e);
            }

        }
    }
}
