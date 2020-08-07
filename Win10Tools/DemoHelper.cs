using System;
using System.IO;
using System.Windows;

public class DemoHelper
{
    public static string GetCode(string demoName)
    {
        var uri = new Uri($"/Win10Tools;component/{demoName}", UriKind.Relative);
        var resourceStream = Application.GetResourceStream(uri);
        if (resourceStream != null)
        {
            using var reader = new StreamReader(resourceStream.Stream);
            var code = reader.ReadToEnd();
            return code;
        }

        return "";
    }
}