using System;
using System.Collections.Generic;
using System.Text;

namespace JackboxModManager
{
    public class ModInfo
    {
        public string Name;
        public string Description;
        public readonly bool VanillaGame;
        public List<JackboxVersion> JackboxVersions = new();
        public string BaseFolder;
        public ModInfo(string name, string description = null, bool vanilla = false, params JackboxVersion[] jackboxVersions)
        {
            Name = name;
            Description = description;
            VanillaGame = vanilla;

        }
    }
    public record ModVersion : IComparable
    {
        private readonly int Major;
        private readonly int? _minor;
        private readonly int? _hotfix;
        public ModVersion(int major, int? minor = null, int? hotfix = null)
        {
            if (minor is null && hotfix is not null) throw new ArgumentException("Cannot have a version with null minor version but non-null hotfix version.");
            Major = major;
            _minor = minor;
            _hotfix = hotfix;
        }
        public override string ToString()
        {
            string result = Major.ToString();
            if (_minor is not null)
            {
                result += $".{_minor}";
                if (_hotfix is not null) result += $".{_hotfix}";
            }
            return result;
        }
        public int CompareTo(object obj)
        {
            if (obj is null) return 1;
            if (obj is not ModVersion other) throw new ArgumentException($"{obj} is not a ModVersion!");
            else
            {
                if (Major != other.Major) return Major.CompareTo(other.Major);
                int minorComparison = Compare(_minor, other._minor);
                if (_minor is null || minorComparison != 0) return minorComparison;
                return Compare(_hotfix, other._hotfix);
            }
        }
        public static int Compare(int? a, int? b) => (a, b) switch
        {
            (null, null) => 0,
            (null, _) => 1,
            (_, null) => -1,
            _ => a.Value.CompareTo(b.Value)

        };
        public static bool operator <(ModVersion a, ModVersion b) => a.CompareTo(b) < 0;
        public static bool operator >(ModVersion a, ModVersion b) => a.CompareTo(b) > 0;
    }
    public record JackboxVersion : IComparable
    {
        public readonly int GameNumber;
        public readonly int Build;
        public JackboxVersion(int gameNumber, int build)
        {
            GameNumber = gameNumber;
            Build = build;
        }
        public override string ToString() => $"Jackbox {GameNumber} build {Build}";
        public int CompareTo(object obj)
        {
            if (obj is null) return 1;
            if (obj is not JackboxVersion other) throw new ArgumentException($"{obj} is not a JackboxVersion!");
            else
            {
                if (GameNumber != other.GameNumber) return GameNumber.CompareTo(other.GameNumber);
                return Build.CompareTo(other.Build);
            }
        }
        public static bool operator <(JackboxVersion a, JackboxVersion b) => a.CompareTo(b) < 0;
        public static bool operator >(JackboxVersion a, JackboxVersion b) => a.CompareTo(b) > 0;
    }
    public record GameInfo : IComparable
    {
        public JackboxVersion Version;
        public string BasePath;
        public override string ToString() => $"{Version} at {BasePath}";
        public int CompareTo(object obj) => obj is null ? 1 : Version.CompareTo((obj as GameInfo).Version);
        public static bool operator <(GameInfo a, GameInfo b) => a.CompareTo(b) < 0;
        public static bool operator >(GameInfo a, GameInfo b) => a.CompareTo(b) > 0;
    }
}
