using System;
using Xamarin.Forms;
using Wapps.Core;

namespace Wapps.Forms.Controls
{
    public class WEntryCell : WFieldCell
    {
        #region Properties

        /// <summary>
        /// Gets the text field.
        /// </summary>
        /// <value>The text field.</value>
        public virtual WEntry Entry { get; }

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

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Wapps.Forms.Controls.WEntryCell"/> is secure.
        /// </summary>
        /// <value><c>true</c> if secure; otherwise, <c>false</c>.</value>
        public virtual bool IsPassword { get; set; } = false;

        /// <summary>
        /// Sets the keyboard.
        /// </summary>
        /// <value>The keyboard.</value>
        public Keyboard Keyboard { get; set; }

        /// <summary>
        /// Sets the type of the return key.
        /// </summary>
        /// <value>The type of the return key.</value>
        public WEntry.ReturnKeyTypes ReturnKeyType { get; set; }

        /// <summary>
        /// Occurs when completed.
        /// </summary>
        public event EventHandler Completed;

        #endregion

        public WEntryCell()
        {
            ReturnKeyType = WEntry.ReturnKeyTypes.Next;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Field == null)
                throw new Exception("WEntryCell - Field not setted");

            if (LblTitle != null)
                LblTitle.Text = Field.Title;

            Entry.Text = (string)Field.Value;
            Entry.Placeholder = Field.Placeholder;
            Entry.Keyboard = Keyboard;
            Entry.ReturnKeyType = ReturnKeyType;
            Entry.IsPassword = IsPassword;
            Entry.IsEnabled = Field.IsEnabled;
            ShowError(Field.ValidationResult);

            if (Field.Rules != null)
            {
                var rule = Field.Rules.FindRule<LengthRule>();
                if (rule != null)
                    Entry.MaxLength = rule.Max;
            }

            Entry.TextChanged += Entry_TextChanged;
            Entry.Unfocused += TxtField_Unfocused;
            Entry.Completed += TxtField_Completed;
            Field.ValueChanged += Field_ValueChanged;
            Field.Validated += Field_Validated;

            Field_ValueChanged(Field, null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Entry.Unfocused -= TxtField_Unfocused;
            Entry.Completed -= TxtField_Completed;
            Entry.TextChanged -= Entry_TextChanged;
            Field.Validated -= Field_Validated;
        }

        protected override void OnTapped()
        {
            base.OnTapped();
            Entry.Focus();
        }

        public override void Focus()
        {
            Entry.Focus();
        }

        void Field_ValueChanged(object sender, EventArgs e)
        {
            if ((string)Field.Value != Entry.Text)
            {
                Entry.Text = (string)Field.Value;
            }
        }

        void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Field.Value = Entry.Text;
        }

        void TxtField_Unfocused(object sender, FocusEventArgs e)
        {
            FUtils.InvokeOnMainThread(TimeSpan.FromMilliseconds(100), () =>
            {
                if (Entry.IsFocused)
                    return;

                var result = Field.Validate();
                ShowError(result);
            });
        }

        void TxtField_Completed(object sender, EventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        protected virtual void Field_Validated(object sender, ViewModelResult e)
        {
            ShowError(e);
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
