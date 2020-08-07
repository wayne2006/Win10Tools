
using Win10Tools.Langs;

namespace Win10Tools.Tools.Extension
{
    public class LangExtension : HandyControl.Tools.Extension.LangExtension
    {
        public LangExtension()
        {
            Source = LangProvider.Instance;
            
        }
    }
}
