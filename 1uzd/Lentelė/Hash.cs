using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lentelė
{
    class Hash
    {
        private HashNode[] table;
        private int index;
        private int operationsCount;
        public Hash(int capacity)
        {
            table = new HashNode[capacity];
        }
        public void Add(string key, int value)
        {
            index = FindPlace(key);
            HashNode element = table[index];
            if(element == null)
            {
                table[index] = new HashNode(key, value);
            }
        }
        private int FindPlace(string key)
        {
            int indexx = Hashh(key);
            int indexxx = indexx;
            int i = 0;
            for (int j = 0; j < table.Length; j++)
            {
                if (table[indexx] == null || table[indexx].Key.Equals(key))
                {
                    return indexx;
                }
                i++;
                indexx = (indexxx + i) % table.Length;
            }
            return -1;
        }
        private int Hashh(string key)
        {
            int hash = key.GetHashCode();
            return Math.Abs(hash) % table.Length;
        }
        public bool ContainsValue(int k, int n, int value)
        {
            for (int i = k; i < n; i++)
            {
                operationsCount++;
                if(table[i] != null && table[i].Value == value)
                {
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (HashNode el in table)
            {
                if (el != null)
                {
                    result.Append(el.ToString());
                }
            }
            return result.ToString();
        }
        public int ReturnOperationsCount()
        {
            return operationsCount;
        }
        public void Clear()
        {
            operationsCount = 0;
        }
    }

    class HashNode
    {
        public string Key;
        public int Value;

        public HashNode()
        {

        }
        public HashNode(string key, int value)
        {
            this.Key = key;
            this.Value = value;
        }
        public override string ToString()
        {
            {
                return String.Format("Raktas: " + Key + " " + "Reikšmė: " + Value);
            }
        }
    }
}
