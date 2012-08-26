using System.Collections.Generic;

namespace LudumDare24.Tools.Models.Analysis
{
    public class ByteArrayEqualityComparer : IEqualityComparer<byte[]>
    {
        public bool Equals(byte[] x, byte[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(byte[] obj)
        {
            unchecked
            {
                const int p = 16777619;
                int hash = (int)2166136261;

                for (int i = 0; i < obj.Length; i++)
                {
                    hash = (hash ^ obj[i]) * p;
                }

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return hash;
            } 
        }
    }
}