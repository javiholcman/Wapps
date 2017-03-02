using System;
using Wapps.Forms.Controls;
using Wapps.ViewModels;
using Wapps.ViewModels.Results;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
	public abstract class WPickerCell : WFieldCell
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

		public WPickerCell()
		{
			
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			LblTitle.Text = Field.Title;
			Picker.Placeholder = Field.Placeholder;
			Picker.BindableItems = Field.Options;
			Picker.SelectedItem = (string)Field.Value;
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
			Field.Value = Picker.SelectedItem;
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
			if (Picker.SelectedItem != Field.Value)
				Picker.SelectedItem = Field.Value;
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

		protected void ShowError(ViewModelResult result)
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
