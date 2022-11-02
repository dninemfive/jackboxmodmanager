using System;
using System.Collections.Generic;
using System.Text;

namespace JackboxModManager
{
    public class Mod
    {
        public string Name;
        public string Description;
        public readonly bool VanillaGame;
        public Mod(string name, string description = null, bool vanilla = false)
        {
            Name = name;
            Description = description;
            VanillaGame = vanilla;
        }
    }
    public record JackboxVersion
    {
        public readonly int GameNumber;
        public readonly int Build;
        public JackboxVersion(int gameNumber, int build)
        {
            GameNumber = gameNumber;
            Build = build;
        }
    }
}
