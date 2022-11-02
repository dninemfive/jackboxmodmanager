using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Text.Json;

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
        public IEnumerable<GameInfo> InstalledGames
        {
            get
            {
                foreach(string s in JackboxFolders)
                {
                    int version = int.Parse(s.Split(" ")[^0]);
                    int build = 0;
                    yield return new()
                    {
                        Version = new(version, build),
                        BasePath = s
                    };
                }
            }
        }
        public List<ModInfo> Mods { get; }
    }
}
