using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackboxModManager
{
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
}
