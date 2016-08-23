namespace Ctrip.Component
{
    public static class CommonHelper
    {
        // Methods
        public static string GetTargetName(EnumLinkActionType aType)
        {
            string str = string.Empty;
            switch (aType)
            {
                case EnumLinkActionType.Blank:
                    return "_blank";

                case EnumLinkActionType.Parent:
                    return "_parent";

                case EnumLinkActionType.Search:
                    return "_search";

                case EnumLinkActionType.Self:
                    return "_self";

                case EnumLinkActionType.Top:
                    return "_top";
            }
            return str;
        }

        public static string GetValue(object obj, string propertyName)
        {
            if ((((obj != null) && (obj.GetType() != null)) && (obj.GetType().GetProperty(propertyName) != null)) && (obj.GetType().GetProperty(propertyName).GetValue(obj, null) != null))
            {
                return obj.GetType().GetProperty(propertyName).GetValue(obj, null).ToString();
            }
            return string.Empty;
        }

        public static string GetValueByName(string srcStr)
        {
            string str = srcStr;
            if ((!string.IsNullOrEmpty(srcStr) && (srcStr.IndexOf('{') >= 0)) && (srcStr.IndexOf('}') >= 0))
            {
                str = srcStr.Split(new char[] { '{' })[1].Split(new char[] { '}' })[0];
            }
            return str;
        }

        public static string Interception(string textStr, int textLen)
        {
            if (string.IsNullOrEmpty(textStr) || (textLen <= 0))
            {
                return string.Empty;
            }
            if (textStr.Trim().Length <= textLen)
            {
                return textStr;
            }
            return (textStr.Trim().Substring(0, textLen) + "...");
        }

        public static bool IsConstValue(string srcStr)
        {
            if (string.IsNullOrEmpty(srcStr) || (!srcStr.Contains("{") && ((srcStr.IndexOf('{') < 0) || (srcStr.IndexOf('}') < 0))))
            {
                return false;
            }
            return true;
        }
    }
}

