using System;
using System.Collections.Generic;
using System.Linq;
using Wapps.Core;
using Wapps.Forms;
using Wapps.Forms.Controls;
using Xamarin.Forms;

namespace Wapps.Forms.Controls
{
    public class WMultiField : WField
    {
        public static readonly BindableProperty FieldsProperty = BindableProperty.Create("Fields", typeof(Dictionary<string, MultiField>), typeof(WMultiFieldCell), null, BindingMode.TwoWay);
        public Dictionary<string, MultiField> Fields
        {
            get { return (Dictionary<string, MultiField>)GetValue(FieldsProperty); }
            set { SetValue(FieldsProperty, value); }
        }

        public WMultiField()
        {
            Fields = new Dictionary<string, MultiField>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            foreach (var key in Fields.Keys)
            {
                var field = Fields[key].Field;
                var control = Fields[key].Control;

                field.ValueChanged += Field_ValueChanged;
                field.Validated += Field_Validated;

                if (control is WEntry)
                {
                    var entry = control as WEntry;
                    entry.TextChanged += Entry_TextChanged;
                    entry.Text = (string)field.Value;
                    entry.Placeholder = field.Placeholder;
                    entry.IsEnabled = field.IsEnabled;

                    if (field.Rules != null)
                    {
                        if (field.Rules != null)
                        {
                            var rule = field.Rules.FindRule<LengthRule>();
                            if (rule != null)
                                entry.MaxLength = rule.Max;
                        }
                    }

                    Entry_TextChanged(entry, null);
                }

                else if (control is Picker)
                {
                    var picker = control as WPicker;
                    picker.ItemsSource = field.Options.Select(p => p.Text).ToList();

                    if (field.Value == null)
                    {
                        picker.SelectedIndex = -1;
                    }
                    else
                    {
                        if (field.Value is string)
                        {
                            if (picker.SelectedItem != field.Value)
                                picker.SelectedItem = field.Value;
                        }
                        else
                        {
                            var option = field.Options.Where(p => p.Source == field.Value).FirstOrDefault();
                            if ((string)picker.SelectedItem != option.Text)
                                picker.SelectedItem = option.Text;
                        }
                    }

                    picker.IsEnabled = field.IsEnabled;
                    picker.Placeholder = "picker";
                    FUtils.InvokeOnMainThread(TimeSpan.FromMilliseconds(100), () =>
                    {
                        picker.Placeholder = field.Placeholder;
                    });
                    picker.SelectedIndexChanged += Picker_SelectedIndexChanged;
                    Picker_SelectedIndexChanged(picker, null);
                }

                if (Fields[key].LblTitle != null)
                    Fields[key].LblTitle.Text = field.Title;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            foreach (var key in Fields.Keys)
            {
                var field = Fields[key].Field;
                var control = Fields[key].Control;

                field.ValueChanged += Field_ValueChanged;
                if (control is WEntry)
                    ((Entry)control).TextChanged -= Entry_TextChanged;
                else if (control is WPicker)
                    ((WPicker)control).SelectedIndexChanged -= Picker_SelectedIndexChanged;
            }
        }

        protected virtual void Field_Validated(object sender, ViewModelResult e)
        {
            ShowError(e);
        }

        protected virtual void ShowError(ViewModelResult result)
        {

        }

        protected virtual void Field_ValueChanged(object sender, EventArgs e)
        {
            var multifield = Fields.Values.Where(p => p.Field == sender).FirstOrDefault();
            if (multifield.Control is Entry)
            {
                if ((string)multifield.Field.Value != ((Entry)multifield.Control).Text)
                    ((Entry)multifield.Control).Text = (string)multifield.Field.Value;
            }
        }

        protected virtual void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var multifield = Fields.Values.Where(p => p.Control == sender).FirstOrDefault();
            multifield.Field.Value = ((Entry)multifield.Control).Text;
        }

        protected virtual void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var multifield = Fields.Values.Where(p => p.Control == sender).FirstOrDefault();
            var picker = multifield.Control as Picker;

            if (string.IsNullOrEmpty((string)picker.SelectedItem))
            {
                multifield.Field.Value = null;
            }
            else
            {
                var val = (string)picker.SelectedItem;
                var option = multifield.Field.Options.Where(p => p.Text == val).FirstOrDefault();
                if (option.Source != null)
                    multifield.Field.Value = option.Source;
                else
                    multifield.Field.Value = val;
            }
        }

        public override void Focus()
        {

        }
    }

    public class MultiField
    {
        public Field Field { get; set; }

        public View Control { get; set; }

        public Label LblTitle { get; set; }
    }
}
