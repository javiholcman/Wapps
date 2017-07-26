using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Wapps.Validations;
using Wapps.ViewModels.Results;

namespace Wapps.ViewModels
{
    public abstract class Field : INotifyPropertyChanged
    {
        object _value;
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value
        {
            get { return _value; }
            set
            {
                if (value != _value)
                {
                    _value = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
                    ValueChanged?.Invoke(_value, null);
                }
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual string Title { get; set; }

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>The placeholder.</value>
        public virtual string Placeholder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Wapps.ViewModels.Field"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if is enabled; otherwise, <c>false</c>.</value>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        /// <value>The options.</value>
        public List<string> Options { get; set; }

        /// <summary>
        /// Gets or sets the validation result.
        /// </summary>
        /// <value>The validation result.</value>
        public ViewModelResult ValidationResult { get; set; }

        /// <summary>
        /// Gets or sets the rules.
        /// </summary>
        /// <value>The rules.</value>
        public virtual Rules Rules { get; set; }

        /// <summary>
        /// Occurs when validated.
        /// </summary>
        public event EventHandler<ViewModelResult> Validated;

        /// <summary>
        /// Occurs when reseted.
        /// </summary>
        public event EventHandler Reseted;

        /// <summary>
        /// Occurs when value changed.
        /// </summary>
        public event EventHandler<EventArgs> ValueChanged;

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public Field()
        {
        }

        public virtual ViewModelResult Validate()
        {
            if (Rules == null)
                return null;

            Rules.Target = this;

            var result = Rules.Validate().FirstOrDefault();
            ValidationResult = result;

            Validated?.Invoke(this, result);
            return result;
        }

        public virtual bool IsValid()
        {
            var result = ValidationResult;
            if (result == null || result is SuccessResult)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            if (Value is bool)
                Value = false;
            else
                Value = null;

            Reseted?.Invoke(this, null);
            ValidationResult = null;
        }
    }

    public delegate ViewModelResult ValidationDelegate(Field field);

    public class Field<T> : Field
    {
        public new T Value
        {
            get
            {
                return (T)base.Value;
            }
            set
            {
                base.Value = value;
            }
        }
    }
}
