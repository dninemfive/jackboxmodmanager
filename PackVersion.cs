using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackboxModManager
{
    public record PackVersion : IComparable
    {
        public readonly int PackNumber;
        public readonly int Build;
        public PackVersion(int packNumber, int build)
        {
            PackNumber = packNumber;
            Build = build;
        }
        public override string ToString() => $"Jackbox {PackNumber} build {Build}";
        public int CompareTo(object obj)
        {
            if (obj is null) return 1;
            if (obj is not PackVersion other) throw new ArgumentException($"{obj} is not a JackboxVersion!");
            else
            {
                if (PackNumber != other.PackNumber) return PackNumber.CompareTo(other.PackNumber);
                return Build.CompareTo(other.Build);
            }
        }
        public static bool operator <(PackVersion a, PackVersion b) => a.CompareTo(b) < 0;
        public static bool operator >(PackVersion a, PackVersion b) => a.CompareTo(b) > 0;
    }
}
