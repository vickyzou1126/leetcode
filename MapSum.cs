using System.Collections.Generic;
using System.Linq;

namespace CodePractice
{
    public class MapSum
    {

        Dictionary<string, int> dictionary;
        public MapSum()
        {
            dictionary = new Dictionary<string, int>();
        }

        public void Insert(string key, int val)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = val;
                return;
            }
            this.dictionary.Add(key, val);
        }

        public int Sum(string prefix)
        {
            int sum = 0;
            foreach(var key in this.dictionary.Keys.Where(x => x.StartsWith(prefix)))
            {
                sum += dictionary[key];
            }
            return sum;
        }
    }
}
