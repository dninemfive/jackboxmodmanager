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
            foreach (int v in validVersionsAfterMajor) result += $".{v}";
            return result;
        }
        public int CompareTo(object obj)
        {
            if (obj is null) return 1;
            if (obj is not ModVersion other)
            {
                throw new ArgumentException($"{obj} is not a ModVersion!");
            }
            else
            {
                foreach((int a, int b) in ValidVersionNumbers.Zip(other.ValidVersionNumbers))
                {
                    int comparison;
                    if ((comparison = Utils.Compare(a, b)) != 0) return comparison;
                }
            }
            return 0;
        }
        private IEnumerable<int> validVersionsAfterMajor
        {
            get
            {
                if(_minor is not null)
                {
                    yield return _minor.Value;
                    if (_hotfix is not null) yield return _hotfix.Value;
                }
            }
        }
        public IEnumerable<int> ValidVersionNumbers
        {
            get
            {
                yield return Major;
                foreach (int v in validVersionsAfterMajor) yield return v;
            }
        }
        public static bool operator <(ModVersion a, ModVersion b) => a.CompareTo(b) < 0;
        public static bool operator >(ModVersion a, ModVersion b) => a.CompareTo(b) > 0;
    }
}
