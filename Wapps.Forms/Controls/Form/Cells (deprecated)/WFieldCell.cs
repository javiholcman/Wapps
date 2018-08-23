using System;
using Wapps.Core;
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

        public static readonly BindableProperty ShowSeparatorProperty = BindableProperty.Create("ShowSeparator", typeof(bool), typeof(WFieldCell), true, BindingMode.TwoWay);
        /// <summary>
        /// Gets or sets the field.
        /// </summary>
        /// <value>The field.</value>
        public bool ShowSeparator
        {
            get { return (bool)GetValue(ShowSeparatorProperty); }
            set { SetValue(ShowSeparatorProperty, value); }
        }

        public WFieldCell()
        {

        }

        /// <summary>
        /// Focus this instance.
        /// </summary>
        public abstract void Focus();

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent != null)
                FUtils.InvokeOnMainThread(100, () => OnAppearing());
            else
                OnDisappearing();
        }

    }
}
