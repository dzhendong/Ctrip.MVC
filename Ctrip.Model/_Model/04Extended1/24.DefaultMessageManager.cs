using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ctrip.Model
{
    public class DefaultMessageManager : MessageManager1
    {
        public DefaultMessageManager()
        {
            var messages = new List<MessageEntry>();
            var messageEntry = new MessageEntry("Validation", "MandatoryField");
            messageEntry.AddMessageText("{0} is mandatory!", new CultureInfo("en-US"));
            messageEntry.AddMessageText("请输入{0}！", new CultureInfo("zh-CN"));
            messages.Add(messageEntry);
            this.Messages = messages;
        }
   
        public IEnumerable<MessageEntry> Messages { get; private set; } 

        public override string FormatMessage(string category, string id, params object[] args)
        {
            MessageEntry messageEntry = (from message in this.Messages
                                        where message.Category == category && message.Id == id
                                        select message).FirstOrDefault();
            if (null == messageEntry)
            {
                throw new Exception("...");
            }
   
            return messageEntry.Format(args);
        }
    }
}
