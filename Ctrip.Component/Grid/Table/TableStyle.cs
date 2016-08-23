namespace Ctrip.Component
{
    public class TableStyle
    {
        // Fields
        private int _cellpadding;
        private int _cellspacing;
        private bool _classStyleEnable;
        private double _height;
        private bool _isHeadVisible = true;
        private bool _isHighLight = true;
        private EnumStyleUnit _styleType;
        private string _tableClassName = string.Empty;
        private string _tableInlineStyle = string.Empty;
        private string _tbodyBorderStyle = string.Empty;
        private string _tbodyClassName;
        private string _tBodyID;
        private string _tbodyInlineStyle;
        private string _theadBorderStyle = string.Empty;
        private string _theadClassName;
        private string _theadID;
        private string _theadInlineStyle;
        private double _width = 100.0;

        // Methods
        public Options GetCurrentStyle(Options tConfig, int dataColumnCount)
        {
            Options options = tConfig;
            if (tConfig.CurrenTableStyle == null)
            {
                options.CurrenTableStyle = new TableStyle();
            }
            if (!tConfig.CurrenTableStyle.ClassStyleEnable && string.IsNullOrEmpty(tConfig.CurrenTableStyle.TableClassName))
            {
                options.CurrenTableStyle.Cellpadding = 10;
                options.CurrenTableStyle.Cellspacing = 0;
                if (string.IsNullOrEmpty(options.CurrenTableStyle.TableInlineStyle))
                {
                    options.CurrenTableStyle.TableInlineStyle = "font-size:12px;color:#333;font-family:宋体;";
                }
            }
            if (string.IsNullOrEmpty(tConfig.TableID))
            {
                options.TableID = "TableID";
            }
            if ((dataColumnCount > 0) && string.IsNullOrEmpty(tConfig.CurrenTableStyle.TheadID))
            {
                options.CurrenTableStyle.TheadID = "TheadID";
            }
            if ((dataColumnCount > 0) && string.IsNullOrEmpty(tConfig.CurrenTableStyle.TheadClassName))
            {
                options.CurrenTableStyle.TheadInlineStyle = "text-align:center;vertical-text;middle;";
            }
            if (string.IsNullOrEmpty(tConfig.CurrenTableStyle.TBodyID))
            {
                options.CurrenTableStyle.TBodyID = "TBodyID";
            }
            return options;
        }

        public string GetHeight()
        {
            string styleUnit = this.GetStyleUnit();
            if ((styleUnit == "%") && (this._height > 100.0))
            {
                styleUnit = "px";
            }
            if (this._height != 0.0)
            {
                return (this._height + styleUnit);
            }
            return "auto";
        }

        public string GetStyleUnit()
        {
            if (this._styleType != EnumStyleUnit.Percent)
            {
                return "px";
            }
            return "%";
        }

        public string GetWidth()
        {
            string styleUnit = this.GetStyleUnit();
            if ((styleUnit == "%") && (this._width > 100.0))
            {
                styleUnit = "px";
            }
            return (this._width + styleUnit);
        }

        // Properties
        public int Cellpadding
        {
            get
            {
                return this._cellpadding;
            }
            set
            {
                this._cellpadding = value;
            }
        }

        public int Cellspacing
        {
            get
            {
                return this._cellspacing;
            }
            set
            {
                this._cellspacing = value;
            }
        }

        public bool ClassStyleEnable
        {
            get
            {
                return this._classStyleEnable;
            }
            set
            {
                this._classStyleEnable = value;
            }
        }

        public double Height
        {
            get
            {
                return this._height;
            }
            set
            {
                this._height = value;
            }
        }

        public bool IsHeadVisible
        {
            get
            {
                return this._isHeadVisible;
            }
            set
            {
                this._isHeadVisible = value;
            }
        }

        public bool IsHighLight
        {
            get
            {
                return this._isHighLight;
            }
            set
            {
                this._isHighLight = value;
            }
        }

        public EnumStyleUnit StyleType
        {
            get
            {
                return this._styleType;
            }
            set
            {
                this._styleType = value;
            }
        }

        public string TableClassName
        {
            get
            {
                return this._tableClassName;
            }
            set
            {
                this._tableClassName = value;
            }
        }

        public string TableInlineStyle
        {
            get
            {
                return this._tableInlineStyle;
            }
            set
            {
                this._tableInlineStyle = value;
            }
        }

        public string TbodyBorderStyle
        {
            get
            {
                return this._tbodyBorderStyle;
            }
            set
            {
                this._tbodyBorderStyle = value;
            }
        }

        public string TbodyClassName
        {
            get
            {
                return this._tbodyClassName;
            }
            set
            {
                this._tbodyClassName = value;
            }
        }

        public string TBodyID
        {
            get
            {
                return this._tBodyID;
            }
            set
            {
                this._tBodyID = value;
            }
        }

        public string TbodyInlineStyleStr
        {
            get
            {
                return this._tbodyInlineStyle;
            }
            set
            {
                this._tbodyInlineStyle = value;
            }
        }

        public string TheadBorderStyle
        {
            get
            {
                return this._theadBorderStyle;
            }
            set
            {
                this._theadBorderStyle = value;
            }
        }

        public string TheadClassName
        {
            get
            {
                return this._theadClassName;
            }
            set
            {
                this._theadClassName = value;
            }
        }

        public string TheadID
        {
            get
            {
                return this._theadID;
            }
            set
            {
                this._theadID = value;
            }
        }

        public string TheadInlineStyle
        {
            get
            {
                return this._theadInlineStyle;
            }
            set
            {
                this._theadInlineStyle = value;
            }
        }

        public double Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }
    }
}

