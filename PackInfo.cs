using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackboxModManager
{
    public record PackInfo : IComparable
    {
        public PackVersion Version;
        public string BasePath;
        public string GamesPath => $@"{BasePath}games\";
        public override string ToString() => $"Jackbox {Version.PackNumber}";
        public int CompareTo(object obj) => obj is null ? 1 : Version.CompareTo((obj as PackInfo).Version);
        public static bool operator <(PackInfo a, PackInfo b) => a.CompareTo(b) < 0;
        public static bool operator >(PackInfo a, PackInfo b) => a.CompareTo(b) > 0;
    }
}
