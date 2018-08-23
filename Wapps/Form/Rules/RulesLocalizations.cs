using System;

namespace Wapps.Core
{
    public abstract class RulesLocalizations
    {
        static RulesLocalizations _current;
        public static RulesLocalizations Current
        {
            get
            {
                if (_current == null)
                    _current = new RulesLocalization_EN();
                return _current;
            }
            set
            {
                _current = value;
            }
        }

        public abstract string IsRequired { get; }
        public abstract string IsInvalid { get; }
        public abstract string TooShort { get; }
        public abstract string TooLong { get; }
    }

    public class RulesLocalization_SP : RulesLocalizations
    {
        public override string IsInvalid
        {
            get { return "es invalido"; }
        }

        public override string IsRequired
        {
            get { return "es requerido"; }
        }

        public override string TooLong
        {
            get { return "es demasiado largo"; }
        }

        public override string TooShort
        {
            get { return "es demasiado corto"; }
        }
    }

    public class RulesLocalization_EN : RulesLocalizations
    {
        public override string IsInvalid
        {
            get { return "is invalid"; }
        }

        public override string IsRequired
        {
            get { return "is required"; }
        }

        public override string TooLong
        {
            get { return "is too long"; }
        }

        public override string TooShort
        {
            get { return "is too short"; }
        }
    }
}
