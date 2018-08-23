using System;
using Wapps.Core;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public abstract class WSwitchCell : WFieldCell
    {
        #region Properties

        /// <summary>
        /// Gets the Switch.
        /// </summary>
        /// <value>The Switch.</value>
        public abstract Switch Switch { get; }

        /// <summary>
        /// Gets the text field.
        /// </summary>
        /// <value>The text field.</value>
        public abstract Label LblTitle { get; }

        #endregion

        public WSwitchCell()
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LblTitle.Text = Field.Title;
            if (Field.Value == null)
                Switch.IsToggled = false;
            else
                Switch.IsToggled = (bool)Field.Value;

            Field_ValueChanged(Field, null);

            Switch.Toggled += Switch_Toggled; ;
            Field.ValueChanged += Field_ValueChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Switch.Toggled -= Switch_Toggled;
            Field.ValueChanged -= Field_ValueChanged;
        }

        void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Field.Value = Switch.IsToggled;
        }

        public override void Focus()
        {
            // do nothing
        }

        void Field_ValueChanged(object sender, EventArgs e)
        {
            if (Field.Value == null)
                Switch.IsToggled = false;
            else if (Switch.IsToggled != (bool)Field.Value)
                Switch.IsToggled = (bool)Field.Value;
        }

    }
}
