using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace JackboxModManager
{
    public class ModInfo
    {
        public string Name;
        public string Description;
        public readonly bool VanillaGame;
        public List<PackVersion> JackboxVersions = new();
        public string BaseFolder;
        public ModInfo(string name, string description = null, string baseFolder = null, bool vanilla = false, params PackVersion[] jackboxVersions)
        {
            Name = name;
            Description = description;
            BaseFolder = baseFolder ?? $"{MainWindow.CurrentConfig.ModFolder}/{name}";
            VanillaGame = vanilla;
            JackboxVersions = jackboxVersions.ToList();
        }
    }            
}
