using System;
using System.Collections.Generic;
using System.Linq;
namespace libc.extended.Extensions {
    public static class ByteExtensions {
        public static int ToUnsignedInt(this List<bool> bits) {
            if (bits == null) throw new ArgumentNullException(nameof(bits));
            var res = 0;
            for (var i = 0; i < bits.Count; i++) {
                var bit = bits[i];
                if (!bit) continue;
                res += (int) Math.Pow(2, i);
            }
            return res;
        }
        public static List<bool> GetBits(this List<byte> packet) {
            if (packet == null) return null;
            var res = new List<bool>();
            for (var i = 0; i < packet.Count; i++) res.AddRange(packet[i].ToBits());
            return res;
        }
        public static List<bool> ToBits(this byte b) {
            var res = new List<bool>();
            for (var i = 0; i < 8; i++) res.Add(b.GetBit(i));
            return res;
        }
        public static byte FromBits(this List<bool> bits) {
            if (bits.Count != 8)
                throw new ArgumentException("argument length is not 8!!",
                    nameof(bits));
            var res = (byte) 0;
            for (var i = 0; i < 8; i++)
                if (bits[i])
                    res.SetBit(i);
            return res;
        }
        /// <summary>
        ///     Sets a bit at the given index to 1 and returns the resulting byte.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index">Index of the bit</param>
        /// <returns></returns>
        public static byte SetBit(this byte b, int index) {
            var res = b;
            byte tmp = 0x01;
            tmp = (byte) (tmp << index);
            res = (byte) (res | tmp);
            return res;
        }
        /// <summary>
        ///     Gets a bit at the given index and if that bit was 1 returns true otherwise returns
        ///     false
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetBit(this byte b, int index) {
            return b.SetBit(index) == b;
        }
        /// <summary>
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte RightHex(this byte b) {
            return (byte) (b & 0x0F);
        }
        /// <summary>
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static byte LeftHex(this byte b) {
            var tmp = (byte) (b & 0xF0);
            return (byte) (tmp >> 4);
        }
        /// <summary>
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string ToHexString(this IEnumerable<byte> arr) {
            return arr.Aggregate(string.Empty,
                (current, b) => current + $"{b:X2} ");
        }
        public static string ToIntString(this byte[] arr) {
            var res = string.Empty;
            foreach (var b in arr) res += (int) b + "-";
            return res.Remove(res.Length - "-".Length);
        }
        public static bool IsEqualWith(this byte[] arr1, byte[] arr2) {
            if (arr1 == null || arr2 == null) return false;
            if (arr1.Length != arr2.Length) return false;
            for (var i = 0; i < arr1.Length; i++)
                if (arr1[i] != arr2[i])
                    return false;
            return true;
        }
        public static bool IsEqual(this byte[] arr1, byte[] arr2) {
            if (arr1 == null || arr2 == null || arr1.Length != arr2.Length) return false;
            return !arr1.Where((t, i) => t != arr2[i]).Any();
        }
    }
}