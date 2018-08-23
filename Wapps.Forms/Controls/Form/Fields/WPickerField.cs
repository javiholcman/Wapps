using System;
using System.Linq;
using Wapps.Core;
using Wapps.Forms.Controls;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public abstract class WPickerField : WField
    {
        #region Properties

        /// <summary>
        /// Gets the picker.
        /// </summary>
        /// <value>The picker.</value>
        public abstract WPicker Picker { get; }

        /// <summary>
        /// Gets the text field.
        /// </summary>
        /// <value>The text field.</value>
        public abstract Label LblError { get; }

        /// <summary>
        /// Gets the text field.
        /// </summary>
        /// <value>The text field.</value>
        public abstract Label LblTitle { get; }

        #endregion

        public WPickerField()
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            LblTitle.Text = Field.Title;
            Picker.Placeholder = Field.Placeholder;
            Picker.ItemsSource = Field.Options.Select(p => p.Text).ToList();
            FieldValueToPickerSelection();
            Field_ValueChanged(Field, null);
            ShowError(Field.ValidationResult);

            Picker.Unfocused += Picker_Unfocused;
            Picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
            Field.ValueChanged += Field_ValueChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Picker.Unfocused -= Picker_Unfocused;
            Picker.SelectedIndexChanged -= Picker_SelectedIndexChanged;
            Field.ValueChanged -= Field_ValueChanged;
        }

        void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            PickerSelectionToFieldValue();
        }

        void PickerSelectionToFieldValue()
        {
            if (string.IsNullOrEmpty((string)Picker.SelectedItem))
            {
                Field.Value = null;
            }
            else
            {
                var val = (string)Picker.SelectedItem;
                var option = Field.Options.Where(p => p.Text == val).FirstOrDefault();
                if (option.Source != null)
                    Field.Value = option.Source;
                else
                    Field.Value = val;
            }
        }

        void FieldValueToPickerSelection()
        {
            if (Field.Value == null)
            {
                Picker.SelectedIndex = -1;
                return;
            }

            if (Field.Value is string)
            {
                if (Picker.SelectedItem != Field.Value)
                    Picker.SelectedItem = Field.Value;
            }
            else
            {
                var option = Field.Options.Where(p => p.Source == Field.Value).FirstOrDefault();
                if ((string)Picker.SelectedItem != option.Text)
                    Picker.SelectedItem = option.Text;
            }
        }

        void Field_Validated(object sender, ViewModelResult e)
        {
            ShowError(e);
        }

        public override void Focus()
        {
            Picker.Focus();
        }

        void Field_ValueChanged(object sender, EventArgs e)
        {
            FieldValueToPickerSelection();
        }

        void Picker_Unfocused(object sender, FocusEventArgs e)
        {
            var result = Field.Validate();
            ShowError(result);
        }

        protected override void OnTapped()
        {
            base.OnTapped();
            Picker.Focus();
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
