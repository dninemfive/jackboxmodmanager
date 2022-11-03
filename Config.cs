using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JackboxModManager
{
    public class Config
    {
        public string ModFolder, SteamFolder;
        public IEnumerable<string> JackboxFolders
        {
            get
            {
                foreach (string s in Directory.GetDirectories(SteamFolder)) if (Regex.IsMatch(s, ".*The Jackbox Party Pack \\d+")) yield return s;
            }
        }
        public IEnumerable<PackInfo> InstalledPacks
        {
            get
            {
                foreach(string s in JackboxFolders)
                {
                    string[] split = s.Split(" ");
                    int version = int.Parse(split.Last());
                    int build = $"{s}/{version.ConfigName()}".ParseBuildNumber(version);
                    yield return new()
                    {
                        Version = new(version, build),
                        BasePath = $@"{s}\"
                    };
                }
            }
        }
        public List<ModInfo> Mods { get; }
    }
}
