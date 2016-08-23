namespace Ctrip.Component
{
    public class ButtonColumn : TableColumn
    {
        // Fields
        private string _buttonClassName = string.Empty;
        private string _buttonInlineStyle = "decoration:none;font-size:12px;";
        private bool _isSubmit;
        public string EnableButtonValue = string.Empty;

        // Properties
        public string BackgroundImage { get; set; }

        public string ButtonClassName
        {
            get
            {
                return this._buttonClassName;
            }
            set
            {
                this._buttonClassName = value;
            }
        }

        public string[] ButtonEvents { get; set; }

        public string ButtonInlineStyle
        {
            get
            {
                return this._buttonInlineStyle;
            }
            set
            {
                this._buttonInlineStyle = value;
            }
        }

        public bool IsSubmit
        {
            get
            {
                return this._isSubmit;
            }
            set
            {
                this._isSubmit = value;
            }
        }
    }
}

