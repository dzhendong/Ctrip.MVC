namespace Ctrip.Component
{
    public class LinkColumn : TableColumn
    {
        // Fields
        private string _linkClassName = string.Empty;
        private string _linkInlineStyle = "decoration:none;font-size:12px;";
        private EnumLinkActionType _targetName;

        // Properties
        public string HrefValue { get; set; }

        public string LinkClassName
        {
            get
            {
                return this._linkClassName;
            }
            set
            {
                this._linkClassName = value;
            }
        }

        public string LinkInlineStyle
        {
            get
            {
                return this._linkInlineStyle;
            }
            set
            {
                this._linkInlineStyle = value;
            }
        }

        public EnumLinkActionType TargetName
        {
            get
            {
                return this._targetName;
            }
            set
            {
                this._targetName = value;
            }
        }
    }
}

