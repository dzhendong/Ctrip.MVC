namespace Ctrip.Component
{
    public class NumberColumn : TableColumn
    {
        // Fields
        private string _textType;

        // Properties
        public string TextType
        {
            get
            {
                return this._textType;
            }
            set
            {
                this._textType = value;
            }
        }
    }
}

