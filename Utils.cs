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
            _ => throw new Exception($"Jackbox {jackboxNumber} is currently unsupported. " +
                                     $"Please file an issue <a href=\"https://github.com/dninemfive/jbmm/issues \">here</> for help.")
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
                <= 5 => int.Parse(raw.Split(" ")[^0]),
                >= 9 => int.Parse(raw),
                _ => throw new Exception($"Jackbox {jackboxNumber} is currently unsupported. " +
                                     $"Please file an issue <a href=\"https://github.com/dninemfive/jbmm/issues \">here</> for help.")
            };
        }
    }
}
