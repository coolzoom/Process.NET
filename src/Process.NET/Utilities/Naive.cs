using Process.NET.Modules;
using Process.NET.Patterns;
using System.Collections.Generic;
using System.Linq;

namespace Process.NET.Utilities
{
    public class Naive
    {
        public static int GetIndexOf(IMemoryPattern pattern, byte[] Data, IProcessModule module)
        {
            var patternData = Data;
            var patternDataLength = patternData.Length;

            for (var offset = 0; offset < patternDataLength; offset++)
            {
                if (
                    pattern.GetMask()
                        .Where((m, b) => m == 'x' && pattern.GetBytes()[b] != patternData[b + offset])
                        .Any())
                    continue;

                return offset;
            }
            return -1;
        }

        public static Dictionary<int,int> GetIndexesOf(IMemoryPattern pattern, byte[] Data, IProcessModule module)
        {
            Dictionary<int,int> indexes = new Dictionary<int,int>();
            var patternData = Data;
            var patternDataLength = patternData.Length - 1;

            for (var offset = 0; offset < patternDataLength - pattern.GetBytes().Count; offset++)
            {
                if (
                    pattern.GetMask()
                        .Where((m, b) => m == 'x' && pattern.GetBytes()[b] != patternData[b + offset])
                        .Any())
                    continue;

                indexes.Add(offset, offset);
            }
            return indexes;
        }
    }
}
