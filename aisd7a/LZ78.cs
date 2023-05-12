using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;


internal class LZ78
{
    struct Pair
    {
        public Pair(ushort position, byte next)
        {
            this.position = position;
            this.next = next;
        }
        public ushort position;
        public byte next;
    }

    public static byte[] Encode(byte[] content)
    {
        Dictionary<string, ushort> dict = new Dictionary<string, ushort>() { { "", 0 } };
        List<Pair> pairs = new List<Pair>();
        string prefix = "";
        for (int i = 0; i < content.Length; i++)
        {
            if (dict.ContainsKey(prefix + (char)content[i]))
            {
                prefix += (char)content[i];
            }
            else
            {
                if (dict.Count <= 65535) dict.Add(prefix + (char)content[i], (UInt16)dict.Count);
                pairs.Add(new Pair(dict[prefix], content[i]));
                prefix = "";
            }
        }
        if (!string.IsNullOrEmpty(prefix))
        {
            byte last_byte = (byte)prefix[prefix.Length - 1];
            prefix = prefix.Substring(0, prefix.Length - 1);
            pairs.Add(new Pair(dict[prefix], last_byte));
        }
        byte[] result = new byte[pairs.Count * 3];
        for (int i = 0, j = 0; i < pairs.Count; i++, j += 3)
        {
            result[j] = (byte)pairs[i].position;
            result[j + 1] = (byte)(pairs[i].position >> 8);
            result[j + 2] = pairs[i].next;
        }
        return result;
    }
}
