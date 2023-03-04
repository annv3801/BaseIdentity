using System;
using System.Linq;

namespace WebApi.Common;
public static class Utils
{
    public static class File
    {
        public static string GenerateFileName(string fileName)
        {
            var raw = Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(fileName));
            return raw.Substring(0, raw.Length > 20 ? 20 : raw.Length);
        }
    }
}
