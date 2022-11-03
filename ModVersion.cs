using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackboxModManager
{
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
                int minorComparison = Utils.Compare(_minor, other._minor);
                if (_minor is null || minorComparison != 0) return minorComparison;
                return Utils.Compare(_hotfix, other._hotfix);
            }
        }
        public static bool operator <(ModVersion a, ModVersion b) => a.CompareTo(b) < 0;
        public static bool operator >(ModVersion a, ModVersion b) => a.CompareTo(b) > 0;
    }
}
