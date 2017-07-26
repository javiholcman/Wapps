using System;
using Wapps.ViewModels;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public abstract class WFieldCell : WViewCell
    {
        public static readonly BindableProperty FieldProperty = BindableProperty.Create("Field", typeof(Field), typeof(WEntryCell), null, BindingMode.TwoWay);
        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>The field.</value>
        public Field Field
        {
            get { return (Field)GetValue(FieldProperty); }
            set { SetValue(FieldProperty, value); }
        }

        public WFieldCell()
        {
        }

        /// <summary>
        /// Focus this instance.
        /// </summary>
        public abstract void Focus();
    }
}
