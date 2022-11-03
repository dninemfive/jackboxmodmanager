using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackboxModManager
{
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
