using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 211
namespace CodePractice
{
    public class WordDictionary
    {
        HashSet<string> list = new HashSet<string>();
        Dictionary<string, bool> dic = new Dictionary<string, bool>();

        public WordDictionary()
        {

        }

        public void AddWord(string word)
        {
            list.Add(word);
        }

        public bool Search(string word)
        {
            if (dic.ContainsKey(word))
            {
                if (dic[word]) return true;
            }
            else
            {
                dic.Add(word, false);
            }

            var len = word.Length;
            var index = word.Select((value, index) => value != '.' ? index : -1)
                .Where(index => index != -1).ToList();
            foreach (var w in list)
            {
                if (matchWord(w, word, len, index))
                {
                    dic[word] = true;
                    return true;
                }
            }
            return false;
        }

        private bool matchWord(string currentWord, string word, int len, List<int> index)
        {
            if (currentWord.Length != len) return false;
            foreach (int i in index)
            {
                if (currentWord[i] != word[i]) return false;
            }
            return true;
        }
    }

}
