using System;
using Wapps.Forms.Controls;
using Xamarin.Forms;

namespace GEG.Views
{
    public abstract class WRadioCell : WFieldCell
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

        public int Index { get; set; } = -1;

        #endregion

        public WRadioCell()
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Field == null)
                throw new Exception("WRadioCell - Field not setted");

            if (LblTitle != null)
                LblTitle.Text = Field.Options[Index].Text;

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
            if ((string)Field.Value == Field.Options[Index].Text)
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
            Field.Value = Field.Options[Index].Text;
        }
    }
}
