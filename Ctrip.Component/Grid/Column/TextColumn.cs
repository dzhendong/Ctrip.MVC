namespace Ctrip.Component
{
    public class TextColumn : TableColumn
    {
        // Fields
        private int _textLen;

        // Properties
        public int TextLen
        {
            get
            {
                return this._textLen;
            }
            set
            {
                this._textLen = value;
            }
        }
    }
}

