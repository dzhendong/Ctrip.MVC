namespace Ctrip.Component
{
    public class TableColumn
    {
        // Fields
        private string _caption;
        private string _columnDataMember;
        private bool _isSort;
        private bool _isVisible = true;
        private int _order;
        private string _thClassName = string.Empty;
        private string _thInlineStyle = string.Empty;
        private double _thwidth;

        // Methods
        public static int Compare(TableColumn c1, TableColumn c2)
        {
            return c1._order.CompareTo(c2._order);
        }

        // Properties
        public string Caption
        {
            get
            {
                return this._caption;
            }
            set
            {
                this._caption = value;
            }
        }

        public string ColumnDataMember
        {
            get
            {
                return this._columnDataMember;
            }
            set
            {
                this._columnDataMember = value;
            }
        }

        public string[] EventParameter { get; set; }

        public bool IsSort
        {
            get
            {
                return this._isSort;
            }
            set
            {
                this._isSort = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this._isVisible;
            }
            set
            {
                this._isVisible = value;
            }
        }

        public string OnClick { get; set; }

        public int Order
        {
            get
            {
                return this._order;
            }
            set
            {
                this._order = value;
            }
        }

        public string ThClassName
        {
            get
            {
                return this._thClassName;
            }
            set
            {
                this._thClassName = value;
            }
        }

        public string ThInlineStyle
        {
            get
            {
                return this._thInlineStyle;
            }
            set
            {
                this._thInlineStyle = value;
            }
        }

        public double THWidth
        {
            get
            {
                return this._thwidth;
            }
            set
            {
                this._thwidth = value;
            }
        }
    }
}

