using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ctrip.Model
{
    public class MessageEntry
    {
        public MessageEntry(string category, string id)
        {
            this.Category = category;
            this.Id = id;
            this.Translations = new Dictionary<string, MessageText>();
        }
        public string Category { get; private set; }
        public string Id { get; private set; }
        public IDictionary<string, MessageText> Translations { get; private set; }

        public void AddMessageText(string text, CultureInfo culture = null)
        {
            string key = (null == culture) ? "" : culture.Name;
            this.Translations[key] = new MessageText(text, culture);
        }

        public string Format(params object[] args)
        {
            MessageText messageText = this.Translations.Values.FirstOrDefault(
                text => string.Compare(CultureInfo.CurrentUICulture.Name, text.TextLang, true) == 0);
            if (null == messageText)
            {
                messageText = this.Translations.Values.FirstOrDefault(text => string.IsNullOrEmpty(text.TextLang));
            }

            if (null == messageText)
            {
                throw new Exception("...");
            }

            return string.Format(CultureInfo.CurrentCulture, messageText.Text, args);
        }
    }  
}
