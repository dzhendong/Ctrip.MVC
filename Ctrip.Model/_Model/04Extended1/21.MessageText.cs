using System.Globalization;

namespace Ctrip.Model
{
    public class MessageText
    {
        public MessageText(string text, CultureInfo culture = null)
        {
            this.Text = text;
            if (null != culture)
            {
                this.TextLang = culture.Name;
            }
        }

        public string Text { get; set; }
        public string TextLang { get; set; }
    }
}
