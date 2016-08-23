using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 创建自定义View
    /// </summary>
    public class StaticFileView : IView
    {
        public string FileName { get; private set; }

        public StaticFileView(string fileName)
        {
            this.FileName = fileName;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        { 
            byte [] buffer;
            using(FileStream fs = new FileStream(this.FileName, FileMode.Open) )
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
            }

            writer.Write(Encoding.UTF8.GetString(buffer));
        }
    }

    /// <summary>
    /// StaticFileView 中定义的内容完全是静态的
    /// 所以缓存显得很有必要
    /// </summary>
    internal class ViewEngineResultCacheKey
    {
        public string ControllerName { get; private set; }
        public string ViewName { get; private set;}

        public ViewEngineResultCacheKey(string controllerName, string viewName)
        {
            this.ControllerName = controllerName ?? string.Empty;
            this.ViewName = viewName ?? string.Empty;
        }

        public override int GetHashCode()
        {
            return this.ControllerName.ToLower().GetHashCode() ^ this.ViewName.ToLower().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ViewEngineResultCacheKey key = obj as ViewEngineResultCacheKey;

            if (null == key)
            {
                return false;
            }

            return key.GetHashCode() == this.GetHashCode();
        }
    }
}
