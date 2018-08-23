using System;
using Wapps.Forms.Controls;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public abstract class WCheckCell : WFieldCell
    {
        #region Properties

        /// <summary>
        /// Gets the text field.
        /// </summary>
        /// <value>The text field.</value>
        public abstract Label LblTitle { get; }

        /// <summary>
        /// Gets the text field.
        /// </summary>
        /// <value>The text field.</value>
        public abstract Image CheckImage { get; }

        #endregion

        public WCheckCell()
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Field == null)
                throw new Exception("WCheckCell - Field not setted");

            if (LblTitle != null)
                LblTitle.Text = Field.Title;

            Field_ValueChanged(Field, null);
            Field.ValueChanged += Field_ValueChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Field.ValueChanged -= Field_ValueChanged;
        }

        protected virtual void Field_ValueChanged(object sender, EventArgs e)
        {
            if ((bool)Field.Value)
                CheckImage.IsVisible = true;
            else
                CheckImage.IsVisible = false;
        }

        public override void Focus()
        {

        }

        protected override void OnTapped()
        {
            base.OnTapped();
            Field.Value = !(bool)Field.Value;
        }
    }
}
