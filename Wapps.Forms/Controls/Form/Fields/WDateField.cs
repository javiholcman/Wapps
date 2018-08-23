using System;
using Wapps.Core;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public class WDateField : WField
    {
        #region Properties

        /// <summary>
        /// Gets the text field.
        /// </summary>
        /// <value>The text field.</value>
        public virtual WDatePicker DatePicker { get; }

        /// <summary>
        /// Gets the text field.
        /// </summary>
        /// <value>The text field.</value>
        public virtual Label LblError { get; }

        /// <summary>
        /// Gets the text field.
        /// </summary>
        /// <value>The text field.</value>
        public virtual Label LblTitle { get; }

        #endregion

        public WDateField()
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LblTitle.Text = Field.Title;
            DatePicker.SelectedDate = (DateTime?)Field.Value;
            DatePicker.Placeholder = Field.Placeholder;
            ShowError(Field.ValidationResult);

            if (Field.Rules != null)
            {
                var prevSelDate = DatePicker.SelectedDate;
                var rule = Field.Rules.FindRule<DateBetweenRule>();
                if (rule != null)
                {
                    DatePicker.MinimumDate = rule.Min;
                    DatePicker.MaximumDate = rule.Max; // Changes the maxDate make change the selectedDate.
                }
                DatePicker.SelectedDate = prevSelDate;
            }

            Field.Validated += Field_Validated;
            DatePicker.Unfocused += DatePicker_Unfocused;
            DatePicker.SelectedDateChanged += DatePicker_SelectedDateChanged;
            Field.ValueChanged += Field_ValueChanged;
            Field_ValueChanged(Field, null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Field.Validated -= Field_Validated;
            DatePicker.Unfocused -= DatePicker_Unfocused;
            DatePicker.SelectedDateChanged -= DatePicker_SelectedDateChanged;
            Field.ValueChanged -= Field_ValueChanged;
        }

        protected virtual void Field_Validated(object sender, ViewModelResult e)
        {
            ShowError(e);
        }

        protected override void OnTapped()
        {
            base.OnTapped();
            DatePicker.Focus();
        }

        public override void Focus()
        {
            DatePicker.Focus();
        }

        protected virtual void DatePicker_SelectedDateChanged(object sender, EventArgs e)
        {
            Field.Value = DatePicker.SelectedDate;
        }

        protected virtual void Field_ValueChanged(object sender, EventArgs e)
        {
            if ((DateTime?)Field.Value != DatePicker.SelectedDate)
            {
                DatePicker.SelectedDate = (DateTime?)Field.Value;
            }
        }

        void DatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            var result = Field.Validate();
            ShowError(result);
        }

        protected virtual void ShowError(ViewModelResult result)
        {
            if (LblError == null)
                return;

            if (result == null || result is SuccessResult)
            {
                LblError.Text = "";
                LblError.IsVisible = false;
            }
            else
            {
                LblError.IsVisible = true;
                if (result is FieldErrorResult)
                    LblError.Text = ((FieldErrorResult)result).Reason;
                else
                    LblError.Text = result.Message;
            }
        }
    }
}
