namespace Ctrip.Component
{
    public class ImgColumn : TableColumn
    {
        // Fields
        private string _imgClassName = string.Empty;

        // Properties
        public string ImgInlineStyle { get; set; }

        public string ImgkClassName
        {
            get
            {
                return this._imgClassName;
            }
            set
            {
                this._imgClassName = value;
            }
        }

        public string SrcValue { get; set; }
    }
}

