using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace JackboxModManager
{
    public static class Utils
    {
        public static string ConfigName(this int jackboxNumber) => jackboxNumber switch
        {
            <= 5 => "config.jet",
            >= 9 => "jbg.config.jet",
            // todo: get the config names for previous versions from others
            _ => throw new Exception(jackboxNumber.UnsupportedMessage())
        };
        public static string TryGetJsonValue(this string path, string propertyName)
        {
            string json = File.ReadAllText(path);
            using JsonDocument doc = JsonDocument.Parse(json);
            return doc.RootElement.GetProperty(propertyName).GetString();
        }
        public static int ParseBuildNumber(this string path, int jackboxNumber)
        {
            string raw = path.TryGetJsonValue("buildVersion");
            return jackboxNumber switch
            {
                <= 5 => int.Parse(raw.Split(" ").Last()),
                >= 9 => int.Parse(raw),
                _ => throw new Exception(jackboxNumber.UnsupportedMessage())
            };
        }
        public static string UnsupportedMessage(this int jackboxNumber)
            => $"Jackbox {jackboxNumber} is currently unsupported. Please file an issue <a href=\"{Constants.IssuesUrl}\">here</> for help.";
        public static int Compare(int? a, int? b) => (a, b) switch
        {
            (null, null) => 0,
            (null, _) => 1,
            (_, null) => -1,
            _ => a.Value.CompareTo(b.Value)
        };
    }    
}
