
namespace Ctrip.Component
{
    internal class PagerItem
    {
        // Methods
        public PagerItem(string text, int pageIndex, bool disabled, PagerItemType type)
        {
            this.Text = text;
            this.PageIndex = pageIndex;
            this.Disabled = disabled;
            this.Type = type;
        }

        // Properties
        internal bool Disabled { get; set; }

        internal int PageIndex { get; set; }

        internal string Text { get; set; }

        internal PagerItemType Type { get; set; }
    }
}

