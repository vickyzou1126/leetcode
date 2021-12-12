using System;
using System.Collections.Generic;
using System.Linq;

namespace CodePractice
{
    public class Solution
    {
        #region 1-100
        #region 1
        public int[] TwoSum(int[] nums, int target)
        {
            var dict = new Dictionary<int, int>();
            var complement = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                complement = target - nums[i];
                if (dict.ContainsKey(complement))
                {
                    return new int[] { i, dict[complement] };

                }
                if (!dict.ContainsKey(nums[i]))
                {
                    dict.Add(nums[i], i);
                }
            }
            return new int[2];
        }
        #endregion

        #region 2 ListNode review
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            int sum = l1.val + l2.val;
            ListNode node = new ListNode(sum % 10);
            GenerateNextNode(node, l1.next, l2.next, sum >= 10);
            return node;

        }
        private ListNode GenerateNextNode(ListNode node, ListNode l1, ListNode l2, bool carry)
        {
            int sum = 0;
            while (l1 != null || l2 != null)
            {
                if (l1 != null && l2 != null)
                {
                    sum = l1.val + l2.val;
                    if (carry)
                    {
                        sum++;
                    }
                }
                else if (l1 != null)
                {
                    sum = carry ? l1.val + 1 : l1.val;
                }
                else
                {
                    sum = carry ? l2.val + 1 : l2.val;
                }
                carry = sum >= 10;
                node.next = new ListNode(sum % 10);
                node = node.next;
                if (l1 != null) l1 = l1.next;
                if (l2 != null) l2 = l2.next;
            }
            if (carry)
            {
                node.next = new ListNode(1);
            }
            return node;
        }
        #endregion

        #region #3 sliding window
        public int LengthOfLongestSubstring(string s)
        {
            int length = s.Length;
            if (length <= 1) return length;
            int lowIndex = 0;
            int highIndex = 1;
            int ans = 0;
            var dic = new Dictionary<int, int>();
            dic.Add(s[0], 0);
            while (highIndex < length)
            {
                if (dic.Keys.Contains(s[highIndex]))
                {
                    if (dic[s[highIndex]] >= lowIndex)
                    {
                        if (ans <= highIndex - lowIndex)
                        {
                            ans = highIndex - lowIndex;
                        }
                        lowIndex = dic[s[highIndex]] + 1;
                    }
                    dic[s[highIndex]] = highIndex;
                }
                else
                {
                    dic[s[highIndex]] = highIndex;
                }
                highIndex++;
            }

            if (ans <= highIndex - lowIndex)
            {
                ans = highIndex - lowIndex;
            }
            return ans;
        }
        #endregion

        #region #4
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int length1 = nums1.Length;
            int length2 = nums2.Length;
            int sum = length1 + length2;
            int mid = sum / 2;
            int index1 = 0;
            int index2 = 0;
            var list = new List<int>();
            int cul = 0;
            while (cul < mid + 1)
            {
                int val1 = int.MaxValue;
                if (index1 < length1)
                {
                    val1 = nums1[index1];
                }
                int val2 = int.MaxValue;
                if (index2 < length2)
                {
                    val2 = nums2[index2];
                }
                if (val1 <= val2)
                {
                    list.Add(nums1[index1]);
                    index1++;
                }
                else
                {
                    list.Add(nums2[index2]);
                    index2++;
                }
                cul++;
            }

            if (sum % 2 == 1) { return list.Last(); }
            return (list[mid - 1] + list[mid]) / 2.0;
        }
        #endregion

        #region #5
        public string LongestPalindrome(string s)
        {
            var charDic = new Dictionary<char, List<int>>();
            int index = 0;
            foreach (var c in s)
            {
                if (charDic.ContainsKey(c))
                {
                    charDic[c].Add(index);
                }
                else
                {
                    charDic.Add(c, new List<int> { index });
                }
                index++;
            }

            var indexDic = new Dictionary<int, List<int>>();
            string maxs = s.Substring(0, 1);
            foreach (var d in charDic.Where(x => x.Value.Count() >= 2))
            {
                if (maxs.Length >= d.Value.Last() - d.Value.First() + 1)
                {
                    continue;
                }
                for (int i = 0; i < d.Value.Count - 1; i++)
                {
                    for (int j = d.Value.Count - 1; j > i; j--)
                    {

                        if (maxs.Length < d.Value[j] - d.Value[i] + 1)
                        {
                            var subString = s.Substring(d.Value[i], d.Value[j] - d.Value[i] + 1);
                            if (IsPalindro(subString))
                            {
                                maxs = subString;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return maxs;
        }

        private bool IsPalindro(string s)
        {
            int length = s.Length;
            if (length == 1) return true;
            for (int i = 0; i < length / 2; i++)
            {
                if (s[i] != s[length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region #6
        public string Convert(string s, int numRows)
        {
            if (numRows == 1) return s;
            //culculate numCuls
            int length = s.Length;
            int charsNum = 2 * numRows - 2;
            int culsNum = numRows - 1;
            int setsNum = length / charsNum;
            int numCuls = setsNum * (culsNum) + 1;
            int remainder = length % charsNum - numRows;
            while (remainder > 0)
            {
                numCuls++; remainder--;
            }

            string[,] array = new string[numRows, numCuls];
            var chars = s.ToArray();
            int rowIndex = 0;
            int culIndex = 0;
            for (int i = 1; i <= length; i++)
            {
                array[rowIndex, culIndex] = chars[i - 1].ToString();
                var temp = i % charsNum;
                if (temp == numRows || temp == 0 || temp > numRows)
                {
                    rowIndex--;
                    culIndex++;
                }
                else
                {
                    rowIndex++;
                }
            }
            string ans = "";
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCuls; j++)
                {
                    if (array[i, j] != null)
                    {
                        ans += array[i, j];
                    }
                }
            }

            return ans;
        }
        #endregion

        #region #8
        public int MyAtoi(string s)
        {
            s = s.Trim();
            var min = s.StartsWith("-", StringComparison.CurrentCulture);

            if (min)
            {
                s = s.Substring(1, s.Length - 1);
            }
            else
            {
                var positive = s.StartsWith("+", StringComparison.CurrentCulture);
                if (positive)
                {
                    s = s.Substring(1, s.Length - 1);
                }
            }

            var chars = s.ToCharArray();
            var str = "";
            for (int i = 0; i < chars.Length; i++)
            {
                if (!Char.IsDigit(chars[i])) break;
                else str += chars[i].ToString();
            }
            if (str == "") return 0;
            if (Int32.TryParse(str, out int numValue))
            {
                if (min) return -1 * numValue;
                return numValue;
            }
            else
            {
                if (min) return int.MinValue;
                return int.MaxValue;
            }
        }
        #endregion

        #region #11
        public int MaxArea(int[] height)
        {
            int length = height.Length;
            int lowIndex = 0;
            int highIndex = length - 1;
            int max = 0;
            while(lowIndex < highIndex)
            {
                var leftIsLower = height[lowIndex] < height[highIndex];
                var area = (highIndex - lowIndex) * (leftIsLower ? height[lowIndex] : height[highIndex]);
                if (max < area) max = area;
                if (leftIsLower) lowIndex++;
                else highIndex--;
            }
            return max;
        }
        #endregion

        #region #12
        public string IntToRoman(int num)
        {
            var list = new List<int> { 1, 5, 10, 50, 100, 500, 1000 };
            var dic = new Dictionary<int, string>();
            dic.Add(1, "I");
            dic.Add(5, "V");
            dic.Add(10, "X");
            dic.Add(50, "L");
            dic.Add(100, "C");
            dic.Add(500, "D");
            dic.Add(1000, "M");
            return GetStr(num, dic, list, "");
        }

        private string GetStr(int num, Dictionary<int, string> dic, List<int> list, string ans)
        {
            while (num > 0)
            {
                if (dic.ContainsKey(num))
                {
                    ans += dic[num];
                    return ans;
                }
                var mostUpNear = FindMostUpNearNumber(num, list);
                var temp = dic[mostUpNear];
                if ((temp == "V" || temp == "X") && mostUpNear - 1 == num)
                {
                    ans += "I" + temp;
                    return ans;
                }
                else if ((temp == "L" || temp == "C") && mostUpNear - 10 == num)
                {
                    ans += "X" + temp;
                    return ans;
                }
                else if ((temp == "L" || temp == "C") && num % 10 != 0)
                {
                    ans = GetStr(num - num % 10, dic, list, ans);
                    num = num % 10;
                }
                else if (temp == "M" && num > mostUpNear)
                {
                    ans += temp;
                    num -= mostUpNear;
                }
                else if ((temp == "D" || temp == "M") && mostUpNear - 100 == num)
                {
                    ans += "C" + temp;
                    return ans;
                }
                else if ((temp == "D" || temp == "M") && num % 100 != 0)
                {
                    ans = GetStr(num - num % 100, dic, list, ans);
                    num = num % 100;
                }
                else
                {

                    var mostLowNear = FindMostLowNearNumber(num, list);
                    ans += dic[mostLowNear];
                    num -= mostLowNear;
                }
            }
            return ans;
        }
        private int FindMostLowNearNumber(int num, List<int> list)
        {
            for (int i = 1; i < 7; i++)
            {
                if (list[i] > num)
                {
                    return list[i - 1];
                }
            }
            return 1;
        }

        private int FindMostUpNearNumber(int num, List<int> list)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (list[i] < num)
                {
                    return list[i + 1];
                }
            }
            return 1000;
        }
        #endregion

        #region #15 review
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            int length = nums.Length;
            if (length < 3) return new List<IList<int>>();

            var groups = nums.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var values = groups.Keys.ToList();

            length = values.Count();
            var hash = new HashSet<List<int>>(new SequenceComparer<int>());
            for (int i = 0; i < length - 1; i++)
            {
                if (values[i] != 0 && groups[values[i]] > 1 && groups.ContainsKey(0 - values[i] * 2))
                {
                    hash.Add(new List<int> { values[i], values[i], 0 - values[i] * 2 });
                }
                for (int j = i + 1; j < length; j++)
                {
                    var toFind = 0 - values[i] - values[j];
                    if (toFind != values[i] && toFind != values[j] && groups.ContainsKey(toFind))
                    {
                        var list = new List<int> { values[i], values[j], toFind };
                        list.Sort();
                        if (!hash.Contains(list))
                        {
                            hash.Add(list);
                        }
                    }
                }
            }

            if (groups.ContainsKey(0) && groups[0] >= 3)
            {
                hash.Add(new List<int> { 0, 0, 0 });
            }

            if (values[length - 1] != 0 && groups[values[length - 1]] > 1 && groups.ContainsKey(0 - values[length - 1] * 2))
            {
                hash.Add(new List<int> { values[length - 1], values[length - 1], 0 - values[length - 1] * 2 });
            }

            return new List<IList<int>>(hash);
        }

        public IList<IList<int>> ThreeSum2(int[] nums)
        {
            int length = nums.Length;
            var list = new List<IList<int>>();
            if (length < 3) return list;

            Array.Sort(nums);
            for (int i = 0; i < length; i++)
            {
                if (nums[i] > 0) return list;
                if (i >= 1 && nums[i] == nums[i - 1]) continue;
                var left = i + 1;
                var right = length - 1;
                while (left < right)
                {
                    var sum = nums[i] + nums[left] + nums[right];
                    if (sum == 0)
                    {
                        list.Add(new List<int> { nums[i], nums[left], nums[right] });
                        left++;
                        right--;
                        while (left < length &&  nums[left] == nums[left - 1])
                        {
                            left++;
                        }
                    } else if (sum > 0)
                    {
                        right--;
                    }
                    else
                    {
                        left++;
                    }
                }
            }
            return list;
        }
        #endregion

        #region #16 -review
        public int ThreeSumClosest(int[] nums, int target)
        {
            int length = nums.Length;
            if (length == 3) return nums.Sum();

            var list = nums.ToList();
            list.Sort();

            var positive = false;
            int minDiff = int.MaxValue;

            for (int i = 0; i < length - 2; i++)
            {
                for (int j = i + 1; j < length - 1; j++)
                {
                    for (int k = j + 1; k < length; k++)
                    {
                        var temp = list[i] + list[j] + list[k];
                        if (minDiff > Math.Abs(temp - target))
                        {
                            minDiff = Math.Abs(temp - target);
                            if (minDiff == 0) return target;
                            positive = temp > target;
                        }
                    }
                }
            }

            return positive ? target + minDiff : target - minDiff;
        }

        public int ThreeSumClosest2(int[] nums, int target)
        {
            int length = nums.Length;
            if (length == 3) return nums.Sum();

            Array.Sort(nums);
            int minDiff = int.MaxValue;
            var ans = int.MaxValue;

            for (int i = 0; i < length; i++)
            {
                int left = i + 1;
                int right = length - 1;
                while (left < right)
                {
                    var sum = nums[i] + nums[left] + nums[right];
                    var diff = Math.Abs(sum - target);
                    if (minDiff > diff)
                    {
                        minDiff = diff;
                        ans = sum;
                    }
                    else if (sum < target)
                    {
                        left++;
                    }
                    else if (sum > target)
                    {
                        right--;
                    }
                    if (ans == target) return target;
                }
            }

            return ans;
        }
        #endregion

        #region #17 dictionary 
        public IList<string> LetterCombinations(string digits)
        {
            List<string> list = new List<string>();
            if (digits.Count() == 0) return list;

            var map = new Dictionary<string, List<string>>();
            map.Add("2", new List<string>() { "a", "b", "c" });
            map.Add("3", new List<string>() { "d", "e", "f" });
            map.Add("4", new List<string>() { "g", "h", "i" });
            map.Add("5", new List<string>() { "j", "k", "l" });
            map.Add("6", new List<string>() { "m", "n", "o" });
            map.Add("7", new List<string>() { "p", "q", "r", "s" });
            map.Add("8", new List<string>() { "t", "u", "v" });
            map.Add("9", new List<string>() { "w", "x", "y", "z" });

            list = map[digits[0].ToString()];
            if (digits.Count() == 1) return list;

            for (int i = 1; i < digits.Count(); i++)
            {
                List<string> temp = map[digits[i].ToString()];
                List<string> tempres = new List<string>();
                for (int j = 0; j < list.Count(); j++)
                {
                    for (int k = 0; k < temp.Count(); k++)
                    {
                        string value = list[j] + temp[k];
                        tempres.Add(value);
                    }
                }
                list = tempres;
            }
            return list;
        }
        #endregion

        #region #18
        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            IList<IList<int>> list = new List<IList<int>>();
            int N = nums.Count();
            if (N <= 3) return list;
            if (nums.Distinct().Count() == 1)
            {
                if (nums[0] * 4 == target)
                {
                    list.Add(new List<int> { nums[0], nums[0], nums[0], nums[0] });
                }
                return list;
            }
            var sorted = nums.ToList();
            sorted.Sort();
            int sum = 0;

            for (int i = 0; i < N - 3; i++)
            {
                if (sorted[i] > target && sorted[i] > 0) break;
                for (int j = i + 1; j < N - 2; j++)
                {
                    sum = sorted[i] + sorted[j];
                    if (sum > target && sum > 0) break;
                    for (int k = j + 1; k < N - 1; k++)
                    {
                        var lastIndex = sorted.LastIndexOf(target - sum - sorted[k]);
                        if (lastIndex > k)
                        {
                            list.Add(new List<int> { sorted[i], sorted[j], sorted[k], sorted[lastIndex] });
                        }
                    }
                }
            }

            return list.Select(o =>
            {
                var t = o.OrderBy(x => x).Select(i => i.ToString());
                return new { Key = string.Join("", t), List = o };
            }).GroupBy(o => o.Key).Select(o => o.FirstOrDefault()).Select(o => o.List).ToList();
        }

        public IList<IList<int>> FourSum2(int[] nums, int target)
        {
            IList<IList<int>> list = new List<IList<int>>();
            int len = nums.Count();
            if (len < 4) return list;

            Array.Sort(nums);

            for (int i = 0; i < len - 1; i++)
            {
                if (i >= 1 && nums[i] == nums[i - 1]) continue;

                for (int j = i + 1; j < len; j++)
                {
                    if (j >= i + 2 && nums[j] == nums[j - 1]) continue;
                    var newSum = nums[i] + nums[j];
                    int left = j + 1;
                    int right = len - 1;
                    while (left < right)
                    {
                        var leftVal = nums[left];
                        var sum = newSum + leftVal + nums[right];
                        if (sum == target)
                        {
                            list.Add(new List<int> { nums[i], nums[j], leftVal, nums[right] });
                            left++;
                            right--;
                            while (left < len && nums[left] == leftVal)
                            {
                                left++;
                            }
                        }
                        else if (sum < target)
                        {
                            left++;
                        }
                        else
                        {
                            right--;
                        }
                    }
                }
            }

            return list;
        }
        #endregion

        #region #19 ListNode
        public ListNode RemoveNthFromEnd(ListNode head, int n) {
            var newHead = ReverseList(head);
            int num = 1;
            ListNode newNode = null;
            while (newHead != null)
            {
                if (num != n)
                {
                    if (newNode == null)
                    {
                        newNode = new ListNode(newHead.val, null);
                    }
                    else
                    {
                        var temp = new ListNode(newHead.val, newNode);
                        newNode = temp;
                    }
                }
                newHead = newHead.next;
                num++;
            }
            return newNode;
        }
        #endregion

        #region 20 stack
        public bool IsValid(string s)
        {
            var length = s.Length;
            if (length % 2 == 1) return false;
            var stack = new Stack<char>();
            for(int i = 0; i < length; i++)
            {
                switch (s[i])
                {
                    case '(':
                    case '[':
                    case '{': stack.Push(s[i]);break;
                    case ')':
                        {
                            if (stack.Count()==0 ||  stack.Pop() != '(') return false;
                            break;
                        }
                    case ']':
                        {
                            if (stack.Count() == 0 || stack.Pop() != '[') return false;
                            break;
                        }
                    case '}':
                        {
                            if (stack.Count() == 0 || stack.Pop() != '{') return false;
                            break;
                        }
                }
            }
            return stack.Count()==0;
        }
        #endregion

        #region #22 - review
        public IList<string> GenerateParenthesis(int n)
        {
            var lists = new List<HashSet<string>>();
            lists.Add(new HashSet<string> { "()" });

            for (int i = 2; i <= n; i++)
            {
                var newList = new HashSet<string>();
                foreach (var str in lists[i - 2])
                {
                    var leftIndex = new List<int> { 0 };
                    var cul = 0;
                    for (int j = 0; j < str.Length; j++)
                    {
                        if (str[j] == '(') cul++;
                        if (str[j] == ')') cul--;
                        if (cul == 0)
                        {
                            foreach (int index in leftIndex)
                            {
                                // cover all
                                newList.Add(str.Insert(index, "(").Insert(j + 2, ")"));
                                // put aside
                                newList.Add(str.Insert(index, "()"));
                                newList.Add(str.Insert(j + 1, "()"));
                            }

                            leftIndex.Add(j + 1);
                        }

                    }
                }
                lists.Add(newList);
            }

            return lists[n - 1].ToList();
        }
        #endregion

        #region #24 ListNode
        public ListNode SwapPairs(ListNode head)
        {
            if (head is null) return null;
            int index = 0;
            var pre = head;
            while (pre.next != null)
            {
                if (index % 2 == 0)
                {
                    var val = pre.val;
                    pre.val = pre.next.val;
                    pre.next.val = val;
                }
                index++;
                pre = pre.next;
            }
            return head;
        }
        #endregion

        #region #26
        public int RemoveDuplicates(int[] nums)
        {
            int N = nums.Length;
            if (N <= 1) return N;

            int lowIndex = 0;
            int target = -101;

            for (int highIndex = 0; highIndex < N; highIndex++)
            {
                if (nums[highIndex] != target)
                {
                    nums[lowIndex] = nums[highIndex];
                    target = nums[lowIndex];
                    lowIndex++;
                }
            }
            return lowIndex;
        }
        #endregion

        #region #27 sliding window
        public int RemoveElement(int[] nums, int val)
        {
            int num = 0;
            int length = nums.Length;

            if (length == 0) return num;

            int high = nums.Length - 1;
            for (int low = 0; low < length; low++)
            {
                if (nums[low] == val)
                {
                    while (high >= 0 && nums[high] == val)
                    {
                        high--;
                    }
                    if (low <= high)
                    {
                        nums[low] = nums[high];
                        nums[high] = val;
                        high--;
                        num++;
                    }
                }
                else
                {
                    num++;
                }
            }

            return num;
        }

        public int RemoveElement1(int[] nums, int val)
        {
            if (nums.Length == 0) return 0;
            var low = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != val)
                {
                    nums[low] = nums[i];
                    low++;
                }
            }
            return low;
        }

        #endregion

        #region #28
        public int StrStr(string haystack, string needle)
        {
            if (needle == "") return 0;

            int length = haystack.Length;
            int needleLength = needle.Length;

            if (length == 0 || length < needleLength) return -1;

            for (int i = 0; i <= length - needleLength; i++)
            {
                var str = haystack.Substring(i, needleLength);
                if (str == needle) return i;
            }
            return -1;
        }
        #endregion

        #region #29 - review, not completed
        public int Divide(int dividend, int divisor)
        {
            bool isPositive = true;
            if (dividend < 0 && divisor > 0 || dividend > 0 && divisor < 0)
            {
                isPositive = false;
            }
            long ldividend = Math.Abs((long)dividend);
            long ldivisor = Math.Abs((long)divisor);
            long result = divide(ldividend, ldivisor);
            if (result > int.MaxValue)
            {
                return isPositive ? int.MaxValue : int.MinValue;
            }
            return isPositive ? (int)result : -(int)result;
        }

        public long divide(long dividend, long divisor)
        {
            if (dividend < divisor) return 0;
            long sum = divisor;
            long result = 1;
            while (dividend > sum + sum)
            {
                sum += sum;
                result += result;
            }
            return result + divide(dividend - sum, divisor);
        }
        #endregion

        #region 30
        public IList<int> FindSubstring(string s, string[] words)
        {
            var res = new List<int>();
            var len = words[0].Length * words.Length;
            var dict = words.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            for(int i = 0; i <= s.Length - len; i++)
            {
                var str = s.Substring(i, len);
                if(GetStrings(str, new Dictionary<string, int>(dict), words[0].Length))
                {
                    res.Add(i);
                }
            }
            return res;
        }

        private bool GetStrings(string str, Dictionary<string, int> dic, int intervl )
        {
            for(int i = 0; i < str.Length; i = i + intervl)
            {
                var temp = str.Substring(i, intervl);
                if (!dic.ContainsKey(temp)) return false;
                dic[temp]--;
                if (dic[temp] < 0) return false;
            }
            return true;
        }
        #endregion

        #region 31
        public void NextPermutation(int[] nums)
        {
            int len = nums.Length;
            if (len == 1) return;

            var dict = new SortedDictionary<int, int>();
            for (int i = len - 1; i >= 0; i--)
            {
                var greaterNums = dict.Keys.ToList().FirstOrDefault(x=>x>nums[i]);
                if (greaterNums != 0)
                {
                    var number = greaterNums;
                    var swap = nums[i];
                    nums[i] = number;
                    nums[dict[number]] = swap;
                   
                    for (int j = i + 1; j < len - 1; j++)
                    {
                        int min_temp = nums[j];
                        int index = j;
                        for (int k = j + 1; k < len; k++)
                        {
                            if (nums[k] < min_temp)
                            {
                                min_temp = nums[k];
                                index = k;
                            }
                        }
                        swap = nums[j];
                        nums[j] = min_temp;
                        nums[index] = swap;
                    }
                    return;
                }
                else
                {
                    if (!dict.ContainsKey(nums[i]))
                    {
                        dict.Add(nums[i], i);
                    }
                }
            }

            for (int i = 0; i < len / 2; i++)
            {
                int swap = nums[i];
                nums[i] = nums[len - i - 1];
                nums[len - i - 1] = swap;
            }
        }

        public void NextPermutationb(int[] nums)
        {
            int len = nums.Length;
            var set = new SortedSet<int>();
            for (int i = len - 1; i >= 0; i--)
            {
                if (nums[i] == 100)
                {
                    set.Add(nums[i]);
                    continue;
                }
                var subsets = set.GetViewBetween(nums[i] + 1, 100);
                if (subsets.Count() > 0)
                {
                    var min = subsets.Min;
                    var index = Array.LastIndexOf(nums, min);
                    var swap = nums[i];
                    nums[i] = min;
                    nums[index] = swap;
                    Array.Sort(nums, i + 1, len - i - 1);
                    return;
                }
                else
                {
                    set.Add(nums[i]);
                }
            }
            Array.Sort(nums);
        }
        #endregion

        #region #32 - review
        public int LongestValidParentheses(string s)
        {
            if (s.Length <= 1) return 0;
            int index = 0;
            var list = new List<int>();
            var length = s.Length;
            int left = 0;

            while (index < length)
            {
                if (s[index] == '(')
                {
                    list.Add(index);
                    left++;
                }
                else
                {
                    if (left > 0)
                    {
                        left--;
                        list.RemoveAt(list.Count - 1);
                    }
                    else
                    {
                        list.Add(index);
                    }
                }
                index++;
            }

            int num = list.Count;

            if (num == 0) return length;

            if (num == 1) return Math.Max(list[0] - 0, length - list[0] - 1);

            var max = Math.Max(list[0] - 0, length - list[num - 1] - 1);
            for (int i = 0; i < num - 1; i++)
            {
                if (list[i + 1] - list[i] - 1 > max)
                {
                    max = list[i + 1] - list[i] - 1;
                }
            }

            return max;
        }
        #endregion

        #region #33
        public int Search(int[] nums, int target)
        {
            int length = nums.Length;
            if (nums[0] == target) return 0;

            if (length == 1)
            {
                return -1;
            }
            if (nums[0] < nums[length - 1])
            {
                if (nums[0] > target) return -1;
                return FindValue(nums, 0, length - 1, target);
            }
            if (nums[0] > target)
            {
                for (int i = length - 1; i >= 0; i--)
                {
                    if (nums[i] == target) return i;
                    if (nums[i - 1] > nums[i])
                    {
                        return -1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < length - 1; i++)
                {
                    if (nums[i] == target) return i;
                    if (nums[i] > nums[i + 1]) return -1;
                }
            }
            return -1;
        }

        private int FindValue(int[] nums, int start, int end, int target)
        {
            int mid = (end - start) / 2 + start;
            if (nums[mid] == target) return mid;
            if (nums[mid] < target)
            {
                if (mid + 1 > end) return -1;
                return FindValue(nums, mid + 1, end, target);
            }
            else
            {
                if (start > mid - 1) return -1;
                return FindValue(nums, start, mid - 1, target);
            }
        }
        #endregion

        #region #34 - sliding window
        public int[] SearchRange(int[] nums, int target)
        {
            int length = nums.Length;
            if (length == 1 && nums[0] == target) return new int[] { 0, 0 };
            int[] ans = new int[2] { -1, -1};
            int lowIndex = 0;
            int highIndex = length - 1;
            while(lowIndex < highIndex)
            {
                int min = nums[lowIndex];
                int max = nums[highIndex];
                if(min>target || max<target)
                    return ans;
               
                if (min == target)
                {
                    ans[0] = lowIndex;
                }
                else
                {
                    lowIndex++;
                }

                if (max == target)
                {
                    ans[1] = highIndex;
                }
                else
                {
                    highIndex--;
                }

                if (ans[0] != -1 && ans[1] != -1) return ans;
                if (ans[0] != -1)
                {
                    highIndex = lowIndex + 1;
                    while (highIndex < length)
                    {
                        if (nums[highIndex] > target)
                        {
                            ans[1] = highIndex - 1;
                            return ans;
                        }
                        else
                        {
                            highIndex++;
                        }
                    }
                    ans[1] = highIndex - 1;
                    return ans;
                }
                else if(ans[1] != -1)
                {
                    lowIndex = highIndex - 1;
                    while (lowIndex >= 0)
                    {
                        if (nums[lowIndex] < target)
                        {
                            ans[0] = lowIndex + 1;
                            return ans;
                        }
                        else
                        {
                            lowIndex--;
                        }

                    }
                    ans[0] = 0;
                    return ans;
                }
            }
 
            return ans;
        }
        #endregion

        #region #35 - sliding window review
        public int SearchInsert(int[] nums, int target)
        {
            var low = 0;
            var high = nums.Length - 1;
            int mid;
            while (low <= high)
            {
                mid = (low + high) / 2;
                if (target < nums[mid])
                {
                    high = mid - 1;
                }
                else if (target > nums[mid])
                {
                    low = mid + 1;
                }
                else
                {
                    return mid;
                }
            }
            return low;
        }
        #endregion

        #region #36
        public bool IsValidSudoku(char[][] board)
        {
            var culDic = new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < 9; i++)
            {
                // row
                if (board[i].Where(x => x != '.').GroupBy(x => x).Select(x => x.Count()).Any(x => x > 1)) return false;

                for (int j = 0; j < 9; j++)
                {
                    // cul
                    if (board[i][j] != '.')
                    {
                        if (culDic.ContainsKey(j))
                        {
                            if (!culDic[j].Add(board[i][j])) return false;
                        }
                        else
                        {
                            culDic.Add(j, new HashSet<int> { board[i][j] });
                        }
                    }

                    var subBoardHash = new HashSet<int>();
                    // 3*3
                    if (i % 3 == 0 && j % 3 == 0)
                    {
                        for (int subi = i; subi <= i + 2; subi++)
                        {
                            for (int subj = j; subj <= j + 2; subj++)
                            {
                                if (board[subi][subj] != '.')
                                {
                                    if (!subBoardHash.Add(board[subi][subj])) return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }
        #endregion

        #region 38
        public string CountAndSay(int n)
        {
            var str = "1";
            n--;
            while (n > 0)
            {
                int len = str.Length;
                var temp = "";
                var preVal = str[0];
                int counter = 1;
                for (int i = 1; i < len; i++)
                {
                    if (str[i] != preVal)
                    {
                        temp += counter + "" + preVal;
                        preVal = str[i];
                        counter = 1;
                    }
                    else
                    {
                        counter++;
                    }
                }
                temp += counter + "" + preVal;
                str = temp;
                n--;
            }
            return str;
        }
        #endregion

        #region 39 review
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var list = new List<IList<int>>();
            candidates = candidates.Where(x => x <= target).ToArray();
            Array.Sort(candidates);
            if (candidates.Length == 0 || target < candidates[0]) return list;
            CombinationSum(candidates, target, 0, new List<int>(), list);
            return list;
        }

        private void CombinationSum(int[] candidates, int target, int sum, IList<int> temp, IList<IList<int>> list)
        {
            if (sum == target)
            {
                list.Add(new List<int>(temp));
                return;
            }
            if (sum > target) return;

            for (int i = 0; i < candidates.Length; i++)
            {
                if (sum + candidates[i] > target) break;

                if (temp.Count > 0 && candidates[i] < temp[temp.Count - 1]) continue;
                temp.Add(candidates[i]);
                CombinationSum(candidates, target, sum + candidates[i], temp, list);
                temp.RemoveAt(temp.Count - 1);
            }
        }

        public IList<IList<int>> CombinationSum39(int[] candidates, int target)
        {
            var list = new List<IList<int>>();
            candidates = candidates.Where(x => x <= target).ToArray();
            Array.Sort(candidates);
            if (candidates.Length == 0 || target < candidates[0]) return list;
            CombinationSum39(candidates, target, 0, new List<int>(), list, 0);
            return list;
        }

        private void CombinationSum39(int[] candidates, int target, int sum, List<int> temp, IList<IList<int>> list, int start)
        {
            if (sum == target)
            {
                list.Add(new List<int>(temp));
                return;
            }
            if (sum > target) return;

            for (int i = start; i < candidates.Length; i++)
            {
                if (temp.Count > 0 && candidates[i] < temp[temp.Count - 1]) continue;
                if (sum + candidates[i] > target) break;

                var remainder = (target - sum) % candidates[i];
                var counter = (target - sum) / candidates[i];
                for (int j = 1; j <= counter; j++)
                {
                    if (remainder != 0 || j != counter - 1)
                    {
                        if (temp.Count > 0 && temp[temp.Count() - 1] > candidates[i] * j) continue;
                        temp.AddRange(Enumerable.Repeat(candidates[i], j).ToList());
                        CombinationSum39(candidates, target, sum + candidates[i] * j, temp, list, i + 1);
                        temp.RemoveRange(temp.Count() - j, j);
                    }
                }
            }
        }

        #endregion

        #region #40 review 
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            var maxv = candidates.Max();
            var minv = candidates.Min();
            if (minv > target) return new List<IList<int>>();
            int n = candidates.Length;

            var hashdp = new Dictionary<int, List<int[]>>();

            if (minv == maxv)
            {
                var count = target / minv;
                if (target % minv == 0 && count <= n)
                {
                    var l = new int[count];
                    if (count == n)
                    {
                        l = candidates;
                    }
                    else
                    {
                        for (int i = 1; i <= count; i++)
                        {
                            l[i - 1] = minv;
                        }
                    }
                    hashdp.Add(target, new List<int[]> { l });
                    return hashdp[target].ToArray();
                }
                return new List<IList<int>>();
            }

            var hash = candidates.GroupBy(c => c).ToDictionary(gr => gr.Key, gr => gr.Count());
            var hashKeys = hash.Keys;

            for (int i = minv; i <= target; i++)
            {
                hashdp.Add(i, new List<int[]>());
                if (hashKeys.Contains(i))
                {
                    hashdp[i].Add(new int[] { i });
                }
                for (int j = minv; j <= i / 2; j++)
                {
                    for (int index = 0; index < hashdp[j].Count(); index++)
                    {
                        var nn = hashdp[j][index].Count();
                        foreach (var v2 in hashdp[i - j])
                        {
                            var templist = new int[nn];
                            for (var vvv = 0; vvv < nn; vvv++)
                            {
                                templist[vvv] = hashdp[j][index][vvv];
                            }
                            templist = templist.Concat(v2).ToArray();
                            Array.Sort(templist);
                            hashdp[i].Add(templist);
                        }
                    }
                }
            }
            var li = hashdp[target].Select(o =>
            {
                var t = o.OrderBy(x => x).Select(i => i.ToString());
                return new { Key = string.Join("", t), List = o };
            }).GroupBy(o => o.Key).Select(o => o.FirstOrDefault()).Select(o => o.List).ToArray();
            var result = new List<List<int>>();

            foreach (var v in li)
            {
                var h = v.GroupBy(c => c).ToDictionary(gr => gr.Key, gr => gr.Count());
                var canadd = true;
                foreach (var c in h)
                {
                    if (!hashKeys.Contains(c.Key) || hash[c.Key] < c.Value)
                    {
                        canadd = false;
                        break;
                    }
                }
                if (canadd)
                {
                    result.Add(v.ToList());
                }
            }

            return result.ToArray();
        }

        public IList<IList<int>> CombinationSum2a(int[] candidates, int target)
        {
            var list = new List<IList<int>>();
            candidates = candidates.Where(x => x <= target).ToArray();
            Array.Sort(candidates);
            if (candidates.Length == 0 || target < candidates[0]) return list;
            CombinationSuma(candidates, target, 0, new List<int>(), list, 0);
            return list;
        }

        private void CombinationSuma(int[] candidates, int target, int sum, IList<int> temp, IList<IList<int>> list, int start)
        {
            if (sum == target)
            {
                list.Add(new List<int>(temp));
                return;
            }
            if (sum > target) return;

            for (int i = start; i < candidates.Length; i++)
            {
                if (i > start && candidates[i - 1] == candidates[i]) continue;

                temp.Add(candidates[i]);
                CombinationSuma(candidates, target, sum + candidates[i], temp, list, i + 1);
                temp.RemoveAt(temp.Count - 1);
            }
        }
        #endregion

        #region 41
        public int FirstMissingPositive(int[] nums)
        {
            nums = nums.Distinct().Where(x => x > 0).ToArray();
            Array.Sort(nums);
            int len = nums.Length;
            if (!nums.Any()) return 1;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != i + 1) return i + 1;
            }
            return nums.Last() + 1;
        }
        #endregion

        #region 43
        public string Multiply(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0") return "0";
            int len1 = num1.Length;
            int len2 = num2.Length;

            if (len1 < len2)
            {
                var swap = num1;
                num1 = num2;
                num2 = swap;
                var swaplen = len1;
                len1 = len2;
                len2 = swaplen;
            }

            var carry = 0;
            var sum = new List<List<int>>();
            for (int i = len2 - 1; i >= 0; i--)
            {
                var list = new List<int>();
                carry = 0;
                for (int j = len1 - 1; j >= 0; j--)
                {
                    var temp = int.Parse(num2[i].ToString()) * int.Parse(num1[j].ToString());
                    temp += carry;
                    carry = temp / 10;
                    list.Insert(0, temp % 10);
                }
                if (carry > 0) list.Insert(0, carry);
                for (int k = len2 - i - 1; k > 0; k--)
                {
                    list.Add(0);
                }
                sum.Add(list);
            }

            var maxLen = sum.Max(x => x.Count());
            var len = sum.Count();
            for (int i = 0; i < len; i++)
            {
                var diff = maxLen - sum[i].Count();
                for (int j = 1; j <= diff; j++)
                {
                    sum[i].Insert(0, 0);
                }
            }
            var str = "";
            carry = 0;
            for (int j = maxLen - 1; j >= 0; j--)
            {

                var tempsum = 0;
                for (int i = 0; i < len; i++)
                {
                    tempsum += sum[i][j];

                }
                tempsum += carry;
                carry = tempsum / 10;
                str = (tempsum % 10) + str;
            }
            if (carry > 0)
            {
                str = carry + str;
            }
            return str;
        }
        #endregion

        #region 45
        public int Jump(int[] nums)
        {
            int len = nums.Length;
            if (len == 1) return 0;

            var steps = new Dictionary<int, int>();
            steps.Add(0, 0);
            steps.Add(1, 1);

            for (int i = 2; i < len; i++)
            {
                var min = int.MaxValue;
                for (int j = 0; j < i; j++)
                {
                    if (min > steps[j] && j + nums[j] >= i)
                    {
                        min = steps[j];
                    }
                }
                steps.Add(i, min + 1);
            }
            return steps[len - 1];
        }
        #endregion

        #region 46 review
        public IList<IList<int>> Permute(int[] nums)
        {
            int len = nums.Length;
            var temp = new List<IList<int>>();
            temp.Add(new List<int> { nums[0] });
            for (int i = 1; i < len; i++)
            {
                var newtemp = new List<IList<int>>();
                for (int j = 0; j < temp.Count(); j++)
                {
                    var jlen = temp[j].Count();
                    for (int k = 0; k < temp[j].Count(); k++)
                    {
                        var t = new int[jlen];
                        temp[j].CopyTo(t, 0);
                        var tl = t.ToList();
                        tl.Insert(k, nums[i]);
                        newtemp.Add(tl);
                    }
                    var t1 = new int[jlen];
                    temp[j].CopyTo(t1, 0);
                    var tl1 = t1.ToList();
                    tl1.Add(nums[i]);
                    newtemp.Add(tl1);
                }
                temp = newtemp;
            }

            return temp;
        }

        public IList<IList<int>> Permutea(int[] nums)
        {
            var list = new List<IList<int>>();
            Permutea(nums, list, new List<int>());
            return list;
        }

        private void Permutea(int[] nums, List<IList<int>> list, List<int> temp)
        {
            if (temp.Count() == nums.Count())
            {
                list.Add(temp.ToList());
                return;
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (!temp.Contains(nums[i]))
                {
                    temp.Add(nums[i]);
                    Permutea(nums, list, temp);
                    temp.RemoveAt(temp.Count() - 1);
                }
            }
        }
        #endregion

        #region 47
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            int len = nums.Length;
            var temp = new List<IList<int>>();
            temp.Add(new List<int> { nums[0] });
            for (int i = 1; i < len; i++)
            {
                var newtemp = new List<IList<int>>();
                var vals = new HashSet<string>();
                for (int j = 0; j < temp.Count(); j++)
                {
                    var jlen = temp[j].Count();
                    for (int k = 0; k < temp[j].Count(); k++)
                    {
                        var t = new int[jlen];
                        temp[j].CopyTo(t, 0);
                        var tl = t.ToList();
                        tl.Insert(k, nums[i]);
                        if (vals.Add(String.Concat(tl)))
                        {
                            newtemp.Add(tl);
                        }  
                    }
                    var t1 = new int[jlen];
                    temp[j].CopyTo(t1, 0);
                    var tl1 = t1.ToList();
                    tl1.Add(nums[i]);
                    if (vals.Add(String.Concat(tl1)))
                    {
                        newtemp.Add(tl1);
                    }
                }
                temp = newtemp;
            }

            return temp;
        }

        public IList<IList<int>> PermuteUniquea(int[] nums)
        {
            var list = new List<IList<int>>();
            Array.Sort(nums);
            Permutea(nums, list, new List<int>(), new HashSet<int>());
            return list;
        }

        private void Permutea(int[] nums, List<IList<int>> list, List<int> temp, HashSet<int> index)
        {
            if (temp.Count() == nums.Count())
            {
                list.Add(temp.ToList());
                return;
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (!index.Add(i)) continue;

                temp.Add(nums[i]);
                Permutea(nums, list, temp, index);
                temp.RemoveAt(temp.Count() - 1);
                index.Remove(i); 
                var val = nums[i];
                while (i < nums.Length && nums[i] == val)
                {
                    i++;
                }
                if (i < nums.Length && nums[i] != val)
                {
                    i--;
                }
            }
        }
        #endregion

        #region #48 --- review matrix rotate -> transpose
        public void Rotate(int[][] matrix)
        {
            int size = matrix[0].Length;
            if (size == 1) return;
            // transpose 
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    int swap = matrix[i][j];
                    matrix[i][j] = matrix[j][i];
                    matrix[j][i] = swap;
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size / 2; j++)
                {
                    int swap = matrix[i][j];
                    matrix[i][j] = matrix[i][size - j - 1];
                    matrix[i][size - j - 1] = swap;
                }
            }
        }
        #endregion

        #region #49
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            int length = strs.Count();
            var list = new List<IList<string>>();

            var dic = new Dictionary<string, IList<string>> ();
            for(int i=0;i<length;i++)
            {
                char[] arr = strs[i].ToCharArray();
                Array.Sort(arr);

                var temp = String.Join("", arr);
                if (dic.ContainsKey(temp))
                {
                    dic[temp].Add(strs[i]);
                }
                else
                {
                    dic.Add(temp, new List<string> { strs[i] });
                }
            }

            foreach(var d in dic)
            {
                list.Add(d.Value);
            }
            return list;
        }
        #endregion

        #region 50 review
        public double MyPow(double x, int n)
        {
            long p = (long)n;

            if (p < 0)
            {
                p = -p;
                x = 1 / x;
            }

            double result = 1;
            while (p > 0) 
            {
                if (p % 2 == 1)
                    result = result * x;
                x = x * x;
                p = p / 2;
            }

            return result;
        }
        #endregion

        #region #53 -sliding window review
        /*
         * solution:
         * remove negative prefix
         * https://www.youtube.com/watch?v=5WZl3MMT0Eg
         */
        public int MaxSubArray(int[] nums)
        {
            int length = nums.Length;
            if (length == 1) return nums[0];

            int sumMax = int.MinValue;
            int currentSum = int.MinValue;
            for (int i = 0; i < length; i++)
            {
                currentSum = currentSum > 0 ? currentSum + nums[i] : nums[i];
                sumMax = sumMax > currentSum ? sumMax : currentSum;
            }
            return sumMax;
        }
        #endregion

        #region 54
        public IList<int> SpiralOrder(int[][] matrix)
        {
            var row = matrix.Length;
            var col = matrix[0].Length;
            var list = new List<int>();
            int starti = 0;
            int startj = 0;
            int endj = col - 1;
            int endi = row - 1;
            int total = row * col;
            int counter = 0;
            while (counter < total)
            {
                for (int i = startj; i <= endj; i++)
                {
                    list.Add(matrix[starti][i]);
                    counter++;
                }
                starti++;
                for (int i = starti; i <= endi; i++)
                {
                    list.Add(matrix[i][endj]);
                    counter++;
                }
                endj--;
                if (counter == total) return list;
                for (int i = endj; i >= startj; i--)
                {
                    list.Add(matrix[endi][i]);
                    counter++;
                }
                endi--;
                for (int i = endi; i >= starti; i--)
                {
                    list.Add(matrix[i][startj]);
                    counter++;
                }
                startj++;
            }
            return list;
        }

        #endregion

        #region #55 - review
        // https://www.youtube.com/watch?v=muDPTDrpS28
        public bool CanJump(int[] nums)
        {
            int length = nums.Length;
            if (length == 1) return true;
            if (nums[0] == 0) return false;
            if (nums[0] >= length - 1) return true;

            int reachable = 0;
            int index = 0;
            while (index <= reachable)
            {
                if (index + nums[index] > reachable)
                {
                    reachable = index + nums[index];
                    if (reachable >= length - 1) return true;
                }
                index++;
            }
            return false;
        }
        #endregion

        #region #56
        public int[][] Merge(int[][] intervals)
        {
            int size = intervals.Count();
            if (size == 1) return intervals;

            Array.Sort(intervals, (a, b) => { return a[0] - b[0]; });
            var list = new List<int[]>();
            int min = intervals[0][0];
            int max = intervals[0][1];
            for (int i = 1; i < size; i++)
            {
                if (intervals[i][0] > max)
                {
                    list.Add(new int[] { min, max });
                    min = intervals[i][0];
                    max = intervals[i][1];
                }
                else if (intervals[i][0] == max)
                {
                    max = intervals[i][1];
                }
                else
                {
                    min = Math.Min(min, intervals[i][0]);
                    max = Math.Max(max, intervals[i][1]);
                }
            }
            list.Add(new int[] { min, max });
            return list.ToArray();
        }

        #endregion

        #region #57 - review code
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            int n = intervals.Length;
            if (n == 0) return new int[][] { newInterval };

            var start = newInterval[0];
            var end = newInterval[1];

            if (end < intervals[0][0]) return intervals.Prepend(newInterval).ToArray();
            if (end == intervals[0][0])
            {
                intervals[0][0] = start;
                return intervals;
            }

            var res = new List<int[]>();
            var min = start;
            var max = end;
            var added = false;
            for (int i = 0; i < n; i++)
            {
                if (intervals[i][1] < start)
                {
                    res.Add(intervals[i]);
                    if (i == n - 1)
                    {
                        res.Add(newInterval);
                    }
                }
                else if (intervals[i][0] > end)
                {
                    if (!added)
                    {
                        res.Add(newInterval);
                        added = true;
                    }

                    res.Add(intervals[i]);
                }
                else if (intervals[i][0] <= start && intervals[i][1] >= start)
                {
                    min = intervals[i][0];
                    max = Math.Max(intervals[i][1], end);

                    if (i == n - 1)
                    {
                        res.Add(new int[] { min, max });
                    }
                    else
                    {
                        i++;
                        var processed = false;

                        while (i < n)
                        {
                            if (!processed && intervals[i][0] > max)
                            {
                                processed = true;
                                added = true;
                                res.Add(new int[] { min, max });
                            }
                            if (processed)
                            {
                                res.Add(intervals[i]);
                            }
                            else
                            {
                                max = Math.Max(max, intervals[i][1]);
                                if (i == n - 1)
                                {
                                    res.Add(new int[] { min, max });
                                }
                            }

                            i++;
                        }
                    }
                }
                else if (intervals[i][0] <= end && intervals[i][1] >= end)
                {
                    min = Math.Min(start, intervals[i][0]);
                    max = intervals[i][1];

                    if (i == n - 1)
                    {
                        res.Add(new int[] { min, max });
                    }
                    else
                    {
                        i++;
                        var processed = false;
                        while (i < n)
                        {
                            if (!processed && intervals[i][0] > max)
                            {
                                res.Add(new int[] { min, max });
                                processed = true;
                                added = true;
                            }
                            if (processed)
                            {
                                res.Add(intervals[i]);
                            }
                            else
                            {
                                max = Math.Max(max, intervals[i][1]);
                                if (i == n - 1)
                                {
                                    res.Add(new int[] { min, max });
                                }
                            }

                            i++;
                        }

                    }
                }
                else if (start <= intervals[i][0] && intervals[i][1] <= end)
                {
                    min = start;
                    max = end;
                    if (i == n - 1)
                    {
                        res.Add(new int[] { min, max });
                    }
                    else
                    {
                        i++;
                        var processed = false;

                        while (i < n)
                        {
                            if (!processed && intervals[i][0] > max)
                            {
                                processed = true;
                                added = true;
                                res.Add(new int[] { min, max });
                            }
                            if (processed)
                            {

                                res.Add(intervals[i]);
                            }
                            else
                            {
                                max = Math.Max(max, intervals[i][1]);
                                if (i == n - 1)
                                {
                                    res.Add(new int[] { min, max });
                                }
                            }

                            i++;
                        }
                    }
                }
            }

            return res.ToArray();
        }

        public int[][] Inserta(int[][] intervals, int[] newInterval)
        {
            List<int[]> result = new List<int[]>();
            int i = 0;

            // Step 1 - add all intervals ending before newInterval starts
            while (i < intervals.Length && intervals[i][1] < newInterval[0])
                result.Add(intervals[i++]);

            // Step 2 - update the newInterval by merging with all overlapping intervals
            while (i < intervals.Length && intervals[i][0] <= newInterval[1])
            {
                newInterval[0] = Math.Min(newInterval[0], intervals[i][0]);
                newInterval[1] = Math.Max(newInterval[1], intervals[i][1]);
                i++;
            }
            result.Add(newInterval); // add updated interval

            // Step 3 - add remaining intervals
            while (i < intervals.Length)
                result.Add(intervals[i++]);

            return result.ToArray();
        }
        #endregion

        #region #58
        public int LengthOfLastWord(string s)
        {
            s = s.TrimEnd();
            var length = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == ' ')
                {
                    return length;
                }
                length++;
            }
            return length;
        }
        #endregion

        #region 59
        public int[][] GenerateMatrix(int n)
        {
            int[][] visited = new int[n][];
            for (int i = 0; i < n; i++)
            {
                visited[i] = new int[n];
            }
            int starti = 0;
            int startj = 0;
            int endj = n - 1;
            int endi = n - 1;
            int val = 1;
            while (val <= n * n)
            {
                for (int i = startj; i <= endj; i++)
                {
                    visited[starti][i] = val;
                    val++;
                }
                starti++;
                for (int i = starti; i <= endi; i++)
                {
                    visited[i][endj] = val;
                    val++;
                }
                endj--;
                for (int i = endj; i >= startj; i--)
                {
                    visited[endi][i] = val;
                    val++;
                }
                endi--;
                for (int i = endi; i >= starti; i--)
                {
                    visited[i][startj] = val;
                    val++;
                }
                startj++;
            }
            return visited;
        }
        #endregion

        #region 61
        public ListNode RotateRight(ListNode head, int k)
        {
            if (k == 0 || head==null) return head;
           
            var list = new List<int>();
            var temphead = head;
            while (temphead != null)
            {
                list.Add(temphead.val);
                temphead = temphead.next;
            }
            var len = list.Count();
            if (len == 1) return head;
            k = k%len;
            if (k == 0) return head;
            var node = new ListNode(list[len-k], null);
            var res = node;
            for(int i = len - k+1; i < len; i++)
            {
                node.next = new ListNode(list[i], null);
                node = node.next;
            }
            for(int i = 0; i < len - k; i++)
            {
                node.next = new ListNode(list[i], null);
                node = node.next;
            }
            return res;
        }
        #endregion

        #region #62
        public int UniquePaths(int m, int n)
        {
            if (m == 1 || n == 1) return Math.Max(m, n)-1;
            int[,] matrix = new int[m,n];
            // init first line
            for(int i = 0; i < n; i++)
            {
                matrix[0,i] = 1;
            }
            // first cul
            for(int i = 1; i < m; i++)
            {
                matrix[i, 0] = 1;
            }
            for(int i = 1; i < m; i++)
            {
                for(int j = 1; j < n; j++)
                {
                    matrix[i, j] = matrix[i, j - 1] + matrix[i - 1, j];
                }
            }
            return matrix[m - 1,n - 1];
        }
        #endregion

        #region 63
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            if (obstacleGrid[0][0] == 1) return 0;
            int row = obstacleGrid.Length;
            int col = obstacleGrid[0].Length;
            if (row == 1)
            {
                if (col == 1)
                    return 1;
                if (obstacleGrid[row - 1][col - 2] == 1) return 0;
            }
            if (col == 1 && obstacleGrid[row - 2][col - 1] == 1) return 0;

            if (row >= 2 && col >= 2 && obstacleGrid[row - 2][col - 1] == 1 && obstacleGrid[row - 1][col - 2] == 1) return 0;

            int[][] visited = new int[row][];
            // init
            for (int i = 0; i < row; i++)
            {
                visited[i] = new int[col];
            }

            for (int i = 0; i < col; i++)
            {
                if (obstacleGrid[0][i] != 1)
                {
                    visited[0][i] = 1;
                }
                else break;
            }

            for (int i = 1; i < row; i++)
            {
                if (obstacleGrid[i][0] != 1)
                {
                    visited[i][0] = 1;
                }
                else break;
            }

            for (int i = 1; i < row; i++)
            {
                for (int j = 1; j < col; j++)
                {

                    if (obstacleGrid[i][j] != 1)
                    {
                        visited[i][j] = visited[i - 1][j] + visited[i][j - 1];
                    }
                }
            }

            return visited[row - 1][col - 1];
        }
        #endregion

        #region #64
        public int MinPathSum(int[][] grid)
        {
            int row = grid.Count();
            int cul = grid[0].Count();
  
            int[][] sums = new int[row][];
            for (int i = 0; i < row; i++)
            {
                sums[i] = new int[cul];
            }
            // init 1st row
            int temp = 0;
            for (int i = 0; i < cul; i++)
            {
                sums[0][i] = temp + grid[0][i];
                temp += grid[0][i];
            }

            // 1st cul
            temp = 0;
            for (int j = 0; j < row; j++)
            {
                sums[j][0] = temp + grid[j][0];
                temp += grid[j][0];
            }

            for (int i = 1; i < row; i++)
            {
                for (int j = 1; j < cul; j++)
                {
                    sums[i][j] = Math.Min(sums[i][j - 1], sums[i - 1][j]) + grid[i][j];
                }
            }

            return sums[row - 1][cul - 1];
        }
        #endregion

        #region #66
        public int[] PlusOne(int[] digits)
        {
            var n = digits.Length;
            if (digits[n - 1] < 9)
            {
                digits[n - 1] += 1;
                return digits;
            }

            for (int i = n - 1; i >= 0; i--)
            {
                var sum = digits[i] + 1;
                if (sum >= 10)
                {
                    digits[i] = sum - 10;
                }
                else
                {
                    digits[i] = sum;
                    return digits;
                }
            }
            return digits.Prepend(1).ToArray();
        }
        #endregion

        #region #67
        public string AddBinary(string a, string b)
        {
            if (a.Length == 0 || a == null) return b;
            if (b.Length == 0 || b == null) return a;

            double[] chars1 = a.ToArray().Select(x => Char.GetNumericValue(x)).ToArray();
            double[] chars2 = b.ToArray().Select(x => Char.GetNumericValue(x)).ToArray();
            int chars1L = chars1.Length;
            int chars2L = chars2.Length;
            if (chars2L > chars1L)
            {
                var swap = chars1;
                chars1 = chars2;
                chars2 = swap;
                var swapL = chars1L;
                chars1L = chars2L;
                chars2L = swapL;
            }

            double[] chars = new double[chars1.Length];
            for (int k = 0; k < chars1L; k++)
            {
                chars[k] = 0;
            }
            var carry = false;
            int i = chars2.Length - 1;
            int diff = chars1L - chars2L;
            while (i >= 0)
            {
                var c1 = chars1[i + diff];
                var c2 = chars2[i];
                var sum = c1 + c2;
                if (carry)
                {
                    sum++;
                }
                carry = sum >= 2;
                chars[i + diff] = carry ? sum - 2 : sum;
                i--;
            }

            int j = diff - 1;
            while (j >= 0)
            {
                var c1 = chars1[j];
                var sum = c1;
                if (carry)
                {
                    sum++;
                }
                carry = sum >= 2;
                chars[j] = carry ? sum - 2 : sum;
                j--;
            }

            if (carry)
            {
                chars = chars.Prepend(1).ToArray();
            }

            return string.Join("", chars);
        }
        #endregion

        #region #69 sliding window
        public static int MySqrt(int x)
        {
            if (x == 0) return 0;
            var low = 1;
            var high = 65536;
            long mid;
            while (low <= high)
            {
                mid = (low + high) / 2;
                var temp = mid * mid;
                if (x == temp)
                {
                    return (int)mid;
                }
                if (x < temp)
                {
                    high = (int)mid - 1;
                }
                else
                {
                    low = (int)mid + 1;
                }
            }
            return low - 1;
        }
        #endregion

        #region #70
        public int ClimbStairs(int n)
        {
            if (n == 1) return n;
            int[] times = new int[n];
            times[0] = 1;
            times[1] = 2;

            for (int i = 2; i < n; i++)
            {
                // l[i] = l[i-1] +1
                times[i] = times[i - 1];
                // l[i] = l[i-2] + 2
                times[i] += times[i - 2];
            }
            return times[n - 1];
        }
        #endregion

        #region 73
        public void SetZeroes(int[][] matrix)
        {
            int row = matrix.Length;
            int col = matrix[0].Length;
            var markMatrix = new int[row][];
            for (int i = 0; i < row; i++)
            {
                markMatrix[i] = new int[col];
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (matrix[i][j] == 0 && markMatrix[i][j] != 1)
                    {
                        markMatrix[i][j] = 1;
                        // mark whole row 0
                        for (int k = 0; k < col; k++)
                        {
                            if (matrix[i][k] != 0)
                            {
                                matrix[i][k] = 0;
                                markMatrix[i][k] = 1;
                            }
                        }
                        // mark whole col 0
                        for (int k = 0; k < row; k++)
                        {
                            if (matrix[k][j] != 0)
                            {
                                matrix[k][j] = 0;
                                markMatrix[k][j] = 1;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 74
        public bool SearchMatrix(int[][] matrix, int target)
        {
            int len = matrix.Length;
            return SearchMatrix(matrix, target, 0, len - 1, len);
        }
        private bool SearchMatrix(int[][] matrix, int target, int startrow, int endrow, int len)
        {
            int mid = startrow + (endrow - startrow) / 2;

            if (matrix[mid][0] > target)
            {
                if (mid - 1 < startrow) return false;
                return SearchMatrix(matrix, target, startrow, mid - 1, len);
            }

            if (matrix[mid].Contains(target)) return true;

            if (mid + 1 > endrow) return false;
            return SearchMatrix(matrix, target, mid + 1, endrow, len);

        }
        #endregion

        #region #75
        public void SortColors(int[] nums)
        {
            int length = nums.Length;
            if (length == 1) return;

            int pointer = 0;
            while (pointer < length)
            {
                var val = nums[pointer];
                if (val > nums[length - 1])
                {
                    nums[pointer] = nums[length - 1];
                    nums[length - 1] = val;
                }
                else if(pointer>0)
                {
                    int orginalPointer = pointer;
                    int index = pointer-1;
                    while(index>=0 && nums[index] > nums[orginalPointer])
                    {
                        nums[orginalPointer] = nums[index];
                        nums[index] = val;
                        orginalPointer = index;
                        index--;
                    }
                }
                pointer++;
            }
        }

        public void SortColorsa(int[] nums) {
            var dic = nums.ToLookup(x => x).ToDictionary(x => x.Key, x => x.Count());
            int index = 0;
            var keys = dic.Keys.OrderBy(x => x);
            
            foreach(var key in keys)
            {
                for(int i = 1; i <= dic[key]; i++)
                {
                    nums[index] = key;
                    index++;
                }
            }
        }
        #endregion

        #region 77 review !!Depth-first search DFS
        public IList<IList<int>> Combine(int n, int k)
        {
            var list = new List<IList<int>>();
            for (int i = 1; i <= n; i++)
            {
                list.Add(new List<int> { i });
            }
            if (k == 1) return list;

            var previousList = new List<IList<int>>();
            while (k > 0)
            {
                previousList = new List<IList<int>>();
                foreach (var v in list)
                {
                    for(int i = v[v.Count()-1]+1; i <= n; i++)
                    {
                        previousList.Add(v.Append(i).ToList());

                    }
                }
                list = previousList;
                k--;
            }

            return list;
        }

        public IList<IList<int>> Combine2(int n, int k)
        {
            var list = new List<IList<int>>();
            Combine(1, n, k, new List<int>(), list);
            return list;
        }

        private void Combine(int start, int n, int k, IList<int> temp, List<IList<int>> list)
        {
            if (k == 0)
            {
                list.Add(new List<int>(temp));
            }
            else
            {
                for (int i = start; i <= n; i++)
                {
                    temp.Add(i);
                    Combine(i + 1, n, k - 1, temp, list);
                    temp.RemoveAt(temp.Count() - 1);
                }
            }
        }
        #endregion

        #region 78
        public IList<IList<int>> Subsets(int[] nums)
        {
            var list = new List<IList<int>>();
            list.Add(new List<int>());
            var len = nums.Length;
            Subsets(nums, 0, list, len, new List<int>());
            return list;
        }

        public void Subsets(int[] nums, int index, List<IList<int>> list, int len, List<int> temp)
        {
            for(int i = index; i < len; i++)
            {
                temp.Add(nums[i]);
                Subsets(nums, i + 1, list, len, temp);
                list.Add(new List<int>(temp));
                temp.RemoveAt(temp.Count() - 1);
            }
        }
        #endregion

        #region #79 review
        /*public bool Exist(char[][] board, string word)
        {
            int row = board.Count();
            int cul = board[0].Count();
            int wordl = word.Length;
            if (row * cul < wordl) return false;
            if (row * cul == 1) return board[0][0] == word[0];

            var dic = new Dictionary<char, List<int[]>>();
            var array = word.ToArray();
            var wordDic = array.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var lists = new List<int[]>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cul; j++)
                {
                    if (array.Contains(board[i][j]))
                    {
                        if (!dic.ContainsKey(board[i][j]))
                        {
                            dic.Add(board[i][j], new List<int[]>());
                        }
                        dic[board[i][j]].Add(new int[2] { i, j });
                        wordDic[board[i][j]]--;
                        lists.Add(new int[2] { i, j });
                    }
                }
            }

            if (wordDic.Values.Any(x => x > 0)) return false;

            foreach(var d in dic)
            {
                var temp = new List<int[]>();
                foreach(var v in d.Value)
                {
                    if(lists.Any(x=>isAdjecent(v, x)))
                    {
                        temp.Add(v); 
                    } 
                }
                if (temp.Count > 0)
                {
                    dic[d.Key] = temp;
                }
                else
                {
                    return false;
                }
            }

            return Exist(board, dic, array, new List<int[]>(), 0);

        }

        private bool Exist(char[][] board, Dictionary<char, List<int[]>> dictionary, char[] word, List<int[]> path, int index)
        {

            if (index == word.Length) return true;

            var val = word[index];
            foreach (var spot in dictionary[val])
            {

                if (path.Any(x => x[0] == spot[0] && x[1] == spot[1])) continue;
                if (path.LastOrDefault() != null)
                {
                    if (!isAdjecent(path.Last(), spot)) continue;
                }
                path.Add(spot);
                index++;
                if (Exist(board, dictionary, word, path, index)) return true;
                path.RemoveAt(path.Count() - 1);
                index--;
            }
            return false;
        }

        private bool isAdjecent(int[] a, int[] b)
        {
            int rowa = a[0];
            int cula = a[1];
            int rowb = b[0];
            int culb = b[1];
            if (rowa == rowb && Math.Abs(cula - culb) == 1) return true;
            if (cula == culb && Math.Abs(rowa - rowb) == 1) return true;
            return false;
        }*/

        public bool Exist(char[][] board, string word)
        {

            int row = board.Count();
            int cul = board[0].Count();
            bool[][] visited = new bool[row][]; 
            for(int i = 0; i < row; i++)
            {
                visited[i] = new bool[cul];
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cul; j++)
                {
                    if (word[0] == board[i][j])
                    {
                        visited[i][j] = true;
                        if (Exist(board, word, visited, 1, i, j, row, cul)) return true;
                        visited[i][j] = false;
                    }
                }
            }
            return false;
        }

        private bool Exist(char[][] board, string word, bool[][] visited, int index, int i, int j, int row, int cul)
        {
            if (index == word.Length) return true;

            var val = word[index];

            if (j - 1 >= 0 && board[i][j - 1] == val)
            {
                if (!visited[i][j-1])
                {
                    visited[i][j-1]= true;
                    if (Exist(board, word, visited, index + 1, i, j - 1, row, cul)) return true;
                    visited[i][j - 1] = false;
                }
            }
            if (j + 1 < cul && board[i][j + 1] == val)
            {
                if (!visited[i][j + 1])
                {
                    visited[i][j + 1] = true;
                    if (Exist(board, word, visited, index + 1, i, j + 1, row, cul)) return true;
                    visited[i][j + 1] = false;
                }
            }

            if (i - 1 >= 0 && board[i - 1][j] == val)
            {
                if (!visited[i-1][j ])
                {
                    visited[i-1][j ] = true;
                    if (Exist(board, word, visited, index +1, i-1, j, row, cul)) return true;
                    visited[i-1][j] = false;
                }
            }

            if (i + 1 < row && board[i + 1][j] == val)
            {
                if (!visited[i + 1][j])
                {
                    visited[i + 1][j] = true;
                    if (Exist(board, word, visited, index + 1, i +1, j, row, cul)) return true;
                    visited[i+1][j] = false;
                }
            }

            return false;
        }
        #endregion

        #region 80 review
        public int RemoveDuplicates80(int[] nums)
        {
            int diff = 0;
            int i = 0;
            var len = nums.Length;
            while (i < len - diff)
            {
                var val = nums[i];
                var lastIndex = Array.LastIndexOf(nums, val);
                var temp = lastIndex - i + 1;
                if (temp > 2)
                {
                    temp -= 2;
                    for (int j = lastIndex + 1; j < len - diff; j++)
                    {
                        nums[j - temp] = nums[j];
                    }
                    for (int j = len - diff - temp; j < len - diff; j++)
                    {
                        nums[j] = int.MaxValue;
                    }
                    diff += temp;
                    temp = 2;
                }
                i += temp;
            }

            return len - diff;
        }

        public int RemoveDuplicates80a(int[] nums)
        {
            var n = nums.Length;

            if (n <= 2) return nums.Length;

            var left = 1;
            var count = 1;
            for (int i = 1; i < n; i++)
            {
                if (nums[i - 1] == nums[i])
                {
                    count++;

                    if (count <= 2)
                    {
                        nums[left] = nums[i];
                        left++;
                    }
                }
                else
                {
                    count = 1;
                    nums[left] = nums[i];
                    left++;
                }
            }

            return left;
        }
        #endregion

        #region 81
        #endregion

        #region 82
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null) return null;
            var val = head.val;

            int counter = 1;
            ListNode res = null;
            ListNode final = null;

            while (head.next != null)
            {
                if (head.next.val == val)
                {
                    counter++;
                }
                else
                {
                    if (counter == 1)
                    {
                        if (res == null)
                        {
                            res = new ListNode(val);
                            final = res;
                        }
                        else
                        {
                            res.next = new ListNode(val);
                            res = res.next;
                        }
                    }
                    val = head.next.val;
                    counter = 1;
                }
                head = head.next;
            }

            if (counter == 1)
            {
                if (res == null)
                {
                    res = new ListNode(val);
                    final = res;
                }
                else
                {
                    res.next = new ListNode(val);
                    res = res.next;
                }
            }
            return final;
        }
        #endregion

        #region #83 - ListNode review
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null) return null;
            ListNode previous = head;
            ListNode next = head.next;
            var val = head.val;
            while (next != null)
            {
                if (next.val == val)
                {
                    previous.next = next.next;
                }
                else
                {
                    previous = next;
                    val = next.val;
                }
                next = next.next;
            }
            return head;
        }
        #endregion

        #region #88
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (m == 0)
            {
                for (int i = 0; i < n; i++)
                {
                    nums1[i] = nums2[i];
                }
            }
            else if (n == 0)
            {
                return;
            }
            else
            {
                int index1 = m - 1;
                int index2 = n - 1;
                int currentIndex = m + n - 1;
                while (index1 >= 0 && index2 >= 0)
                {
                    if (nums1[index1] < nums2[index2])
                    {
                        nums1[currentIndex] = nums2[index2];
                        index2--;
                    }
                    else
                    {
                        nums1[currentIndex] = nums1[index1];
                        index1--;
                    }
                    currentIndex--;
                }
                while (index1 >= 0)
                {
                    nums1[currentIndex] = nums1[index1];
                    index1--;
                    currentIndex--;
                }
                while (index2 >= 0)
                {
                    nums1[currentIndex] = nums2[index2];
                    index2--;
                    currentIndex--;
                }
            }
        }
        #endregion

        #region 90
        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            var final = new List<IList<int>>();
            var dict = nums.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            foreach (var d in dict)
            {
                var temp = new HashSet<IList<int>>();
                for (int i = 1; i <= d.Value; i++)
                {
                    var sub = new List<int>();
                    for (int j = 1; j <= i; j++)
                    {
                        sub.Add(d.Key);
                    }
                    temp.Add(sub);
                    if (final.Any())
                    {
                        foreach (var s in final)
                        {
                            temp.Add(sub.Concat(s).ToList());
                        }
                    }
                }
                final.AddRange(temp.ToList());
            }

            final.Add(new List<int>());
            return final;
        }
        #endregion

        #region #94 Tree review + remember
        public IList<int> InorderTraversal(TreeNode root)
        {
            return InorderTraversalSearch(root, new List<int>());
        }

        private IList<int> InorderTraversalSearch(TreeNode root, IList<int> list)
        {
            if (root == null) return list;
            list = InorderTraversalSearch(root.left, list);
            list.Add(root.val);
            return InorderTraversalSearch(root.right, list);
        }
        #endregion

        #region #100 Tree
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p is null && q is null) return true;
            if (p is null || q is null) return false;

            return p.val == q.val && IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }
        #endregion
        #endregion

        #region 101-200
        #region #101 Tree review 
        public bool IsSymmetric(TreeNode root)
        {
            return isMirror(root, root);
        }

        private bool isMirror(TreeNode root1, TreeNode root2)
        {
            if (root1 is null && root2 is null) return true;
            if (root1 is null || root2 is null) return false;

            return root1.val == root2.val && isMirror(root1.left, root2.right) && isMirror(root1.right, root2.left);
        }
        #endregion

        #region #104 - Tree review
        public int MaxDepth(TreeNode root)
        {
            var l = Depth(root, new List<int>(), 0);
            return l.Max();
        }

        private IList<int> Depth(TreeNode root, IList<int> list, int depth)
        {
            if (root == null)
            {
                list.Add(depth);
                return list;
            }
            depth++;

            list = Depth(root.left, list, depth);

            return Depth(root.right, list, depth);
        }

        #endregion

        #region 105 review
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            int len = preorder.Length;
            if (len == 0) return null;
            if (len == 1) return new TreeNode(preorder[0], null, null);

            var mid_inorder = Array.IndexOf(inorder, preorder[0]);
            var root = new TreeNode(preorder[0]);

            root.left = BuildTree(preorder.Skip(1).Take(mid_inorder).ToArray(), inorder.Take(mid_inorder).ToArray());
            root.right = BuildTree(preorder.Skip(mid_inorder + 1).Take(len - mid_inorder - 1).ToArray(), inorder.Skip(mid_inorder + 1).Take(len - mid_inorder - 1).ToArray());
            return root;
        }
        #endregion

        #region 106
        public TreeNode BuildTree2(int[] inorder, int[] postorder)
        {
            int len = postorder.Length;
            if (len == 0) return null;
            if (len == 1) return new TreeNode(inorder[0], null, null);

            var root = new TreeNode(postorder[len - 1]);
            var index = Array.IndexOf(inorder, postorder[len - 1]);
            root.left = BuildTree(inorder.Take(index).ToArray(), postorder.Take(index).ToArray());
            root.right = BuildTree(inorder.Skip(index + 1).Take(len - index - 1).ToArray(), postorder.Skip(index).Take(len - index - 1).ToArray());
            return root;
        }
        #endregion

        #region #108 TreeNode
        public TreeNode SortedArrayToBST(int[] nums)
        {
            return SortedArrayToBST2(nums, 0, nums.Length - 1);

        }

        private TreeNode SortedArrayToBST2(int[] nums, int start, int end)
        {
            var mid = (end - start) / 2 + start;
            var temp = new TreeNode(nums[mid]);
            if (start <= mid - 1)
            {
                temp.left = SortedArrayToBST2(nums, start, mid - 1);
            }
            if (mid + 1 <= end)
            {
                temp.right = SortedArrayToBST2(nums, mid + 1, end);
            }

            return temp;
        }
        #endregion

        #region #110 TreeNode - not completed

        #endregion

        #region #111 TreeNode
        public int MinDepth(TreeNode root)
        {
            if (root == null) return 0;
            int left = int.MaxValue;
            int right = int.MaxValue;

            if (root.left != null)
            {
                left = MinDepth(root.left.left, root.left.right, 2);
            }
            if (root.right != null)
            {
                right = MinDepth(root.right.left, root.right.right, 2);
            }
            var min = Math.Min(left, right);
            if (min == int.MaxValue) return 1;
            return min;
        }
        private int MinDepth(TreeNode root, TreeNode root2, int min)
        {
            if (root == null && root2 == null) return min;
            int left = int.MaxValue;
            int right = int.MaxValue;
            if (root != null)
            {
                left = MinDepth(root.left, root.right, min + 1);
            }
            if (root2 != null)
            {
                right = MinDepth(root2.left, root2.right, min + 1);
            }

            return Math.Min(left, right);
        }
        #endregion

        #region #112 TreeNode
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null) return false;
            return HasPathSum(root, targetSum, 0);
        }

        public bool HasPathSum(TreeNode root, int targetSum, int sum)
        {
            if (root.left == null && root.right == null) return (sum + root.val) == targetSum;

            if (root.left != null && HasPathSum(root.left, targetSum, sum + root.val)) return true;

            if (root.right != null && HasPathSum(root.right, targetSum, sum + root.val)) return true;

            return false;
        }
        #endregion

        #region #118 
        public IList<IList<int>> Generate(int numRows)
        {
            var l = new List<IList<int>>();
            l.Add(new List<int> { 1 });
            var previous = l[0];
            for (int i = 1; i < numRows; i++)
            {
                var temp = new List<int>();
                temp.Add(1);
                for (int j = 1; j < i; j++)
                {
                    temp.Add(previous[j - 1] + previous[j]);
                }
                temp.Add(1);
                l.Add(temp);
                previous = l[i];
            }
            return l;
        }
        #endregion

        #region #119
        public IList<int> GetRow(int rowIndex)
        {
            var init = new List<int> { 1 };

            for (int i = 1; i <= rowIndex; i++)
            {
                var temp = new List<int>();
                temp.Add(1);
                for (int j = 1; j < i; j++)
                {
                    temp.Add(init[j - 1] + init[j]);
                }
                temp.Add(1);
                if (i == rowIndex) return temp;
                init = temp;
            }

            return init;
        }
        #endregion

        #region 120
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            var len = triangle.Count();
            if(len==1) return triangle[0][0];
            
            var list = new List<List<int>>();
            list.Add(new List<int> { triangle[0][0] });

            for (int i = 1; i < len; i++)
            {
                var temp = new List<int>();
                temp.Add(list[i - 1][0] + triangle[i][0]);
                for (int j = 1; j < i; j++)
                {
                    temp.Add(Math.Min(list[i - 1][j], list[i - 1][j - 1]) + triangle[i][j]);
                }
                temp.Add(list[i - 1][i - 1] + triangle[i][i]);
                list.Add(temp);
            }
            return list[len - 1].Min();
        }
        #endregion

        #region #121 review
        public int MaxProfit1(int[] prices)
        {
            int currentMin = int.MaxValue;
            int profit = 0;
            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < currentMin) currentMin = prices[i];
                if (prices[i] - currentMin > profit) profit = prices[i] - currentMin;
            }
            return profit;
        }
        #endregion

        #region #122
        public int MaxProfit(int[] prices)
        {
            int profit = 0;
            int holdPrice = prices[0];
            int salePrice = holdPrice;
            int length = prices.Length;
            bool hold = false;

            for (int i = 1; i < length; i++)
            {
                if (prices[i] < holdPrice && !hold)
                {
                    holdPrice = prices[i];
                    salePrice = holdPrice;
                }
                else if (prices[i] > salePrice)
                {
                    hold = true;
                    salePrice = prices[i];
                    if (i == length - 1)
                    {
                        profit += salePrice - holdPrice;
                    }
                }
                else
                {
                    profit += salePrice - holdPrice;
                    holdPrice = prices[i];
                    salePrice = holdPrice;
                    hold = false;
                }
            }

            return profit;
        }
        #endregion

        #region #125 sliding window
        public bool IsPalindrome(string s)
        {
            var length = s.Count();
            int low = 0;
            int high = length - 1;
            s = s.ToLower();
            while (low < high)
            {
                if (!Char.IsLetterOrDigit(s[low]))
                {
                    low++;
                    continue;
                }
                if (!Char.IsLetterOrDigit(s[high]))
                {
                    high--;
                    continue;
                }
                if (s[low] != s[high]) return false;
                low++;
                high--;
            }
            return true;
        }
        #endregion

        #region 130
        public void Solve(char[][] board)
        {

        }
        #endregion

        #region #136 grouping
        public int SingleNumber(int[] nums)
        {
            foreach (var item in nums.GroupBy(x => x))
            {
                if (item.Count() == 1) return item.Key;
            }
            return -1;
        }
        #endregion

        #region #128
        public int LongestConsecutive(int[] nums)
        {
            int res = 0;
            var hash = nums.ToHashSet();
            var size = hash.Count;
            if (size <= 1) return size;
            int valmin = hash.Min();
            int valmax = hash.Max();
            while (valmin < valmax)
            {
                var temp = valmin;
                while (hash.Contains(valmin))
                {
                    valmin++;
                }
                res = Math.Max(res, valmin - temp);
                if (res >= size / 2) return res;
                valmin++;
                while (valmin < valmax && !hash.Contains(valmin))
                {
                    valmin++;
                }
            }
            return res;
        }
        #endregion

        #region 134
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            var gasStarters = gas.Select((value, index) => value > 0 ? index : -1)
            .Where(index => index != -1).ToList();
            int len = gasStarters.Count();
            for (int j = 0; j < len; j++)
            {
                var i = gasStarters[j];
                int diff = gas[i] - cost[i];
                if (diff >= 0)
                {
                    int index = i + 1;
                    var pass = true;
                    while (index < len)
                    {
                        if (diff + gas[index] - cost[index] < 0)
                        {
                            pass = false;
                            break;
                        }
                        diff += gas[index] - cost[index];
                        index++;
                    }
                    if (pass && i != 0)
                    {
                        index = 0;
                        while(index < i)
                        {
                            if (diff + gas[index] - cost[index] < 0)
                            {
                                pass = false;
                                break;
                            }
                            diff += gas[index] - cost[index];
                            index++;
                        }
                    }
                    if (pass) return i; 
                }
            }
            return -1;
        }
        #endregion

        #region 137
        public int SingleNumber137(int[] nums)
        {
            Array.Sort(nums);
            for (int i = 0; i < nums.Length-2; i = i + 3)
            {
                if (nums[i] != nums[i + 2]) return nums[i];
            }
            return nums[nums.Length - 1];
        }
        #endregion

        #region #139 review
        public bool WordBreak(string s, IList<string> wordDict)
        {
            int length = s.Length;
            bool[] dp = new bool[s.Length];

            for (int i = length - 1; i >= 0; i--)
            {
                if (wordDict.Contains(s.Substring(i, length - i))) dp[i] = true;
                else
                {
                    var list = wordDict.Where(x => x.StartsWith(s[i]));
                    if (!list.Any()) dp[i] = false;
                    else
                    {
                        foreach (var l in list)
                        {
                            if (i + l.Length < length && s.Substring(i, l.Length) == l && dp[i + l.Length])
                            {
                                dp[i] = true;
                                break;
                            }
                        }
                    }
                }
            }
            return dp[0];
        }

        #endregion

        #region #141 ListNode
        public bool HasCycle(ListNode head)
        {
            if (head == null) return false;
            if (head.val == int.MinValue) return true;

            head.val = int.MinValue;
            return HasCycle(head.next);
        }
        #endregion

        #region 142 ListNode
        public ListNode DetectCycle(ListNode head)
        {
            var copy = head;
            return DetectCycle(copy, head);
        }

        private ListNode DetectCycle(ListNode head, ListNode oghead)
        {
            if (head == null) return null;

            if (head.val == int.MinValue)
            {
                return oghead;
            }
            head.val = int.MinValue;
            return DetectCycle(head.next, oghead.next);
        }

        public ListNode DetectCyclea(ListNode head)
        {
            var hs = new HashSet<ListNode>();

            var node = head;

            while (node != null)
            {
                if (!hs.Add(node)) return node;

                node = node.next;
            }

            return null;
        }
        #endregion

        #region #144 TreeNode
        public IList<int> PreorderTraversal(TreeNode root)
        {
            return PreorderTraversal(root, new List<int>());
        }

        private IList<int> PreorderTraversal(TreeNode root, IList<int> list)
        {
            if (root == null) return list;
            list.Add(root.val);
            list = PreorderTraversal(root.left, list);
            return PreorderTraversal(root.right, list);
        }
        #endregion

        #region #145
        public IList<int> PostorderTraversal(TreeNode root)
        {
            return PostorderTraversal(root, new List<int>());
        }
        private IList<int> PostorderTraversal(TreeNode root, IList<int> list)
        {
            if (root == null) return list;
            list = PostorderTraversal(root.left, list);
            list = PostorderTraversal(root.right, list);
            list.Add(root.val);
            return list;
        }
        #endregion

        #region 150
        #endregion

        #region #151
        public string ReverseWords(string s)
        {
            var c = s.ToArray();
            int low = 0;
            var space = false;
            int N = s.Length;
            for (int i = 0; i < N; i++)
            {
                if (c[i] != ' ')
                {
                    c[low] = c[i];
                    low++;
                    space = false;
                }
                else if (!space && low != 0)
                {
                    c[low] = c[i];
                    space = true;
                    low++;
                }
            }
            for (int i = low; i < N; i++)
            {
                c[i] = ' ';
            }
            ReverseString(c, 0, N);
            low = 0;

            for (int i = 0; i < N; i++)
            {
                if (c[i] == ' ' || i == N - 1)
                {
                    if (low < i)
                    {
                        ReverseString(c, low, i == N - 1 ? i + 1 : i);
                    }
                    low = i + 1;
                }

            }
            return string.Join("", c).Trim(' ');
        }

        public void ReverseString(char[] s, int start, int end)
        {
            int n = end - start;
            for (int i = start; i < start + n / 2; i++)
            {
                var temp = s[i];
                var j = start + end - 1 - i;
                s[i] = s[j];
                s[j] = temp;
            }
        }
        #endregion

        #region 152 review 
        public int MaxProduct(int[] nums)
        {
            var n = nums.Length;

            if (n == 0) return 0;
            if (n == 1) return nums[0];

            var minArr = new int[n];
            var maxArr = new int[n];

            minArr[0] = nums[0];
            maxArr[0] = nums[0];

            for (int i = 1; i < n; i++)
            {
                maxArr[i] = Math.Max(Math.Max(maxArr[i - 1] * nums[i], minArr[i - 1] * nums[i]), nums[i]);
                minArr[i] = Math.Min(Math.Min(minArr[i - 1] * nums[i], maxArr[i - 1] * nums[i]), nums[i]);
            }

            return maxArr.Max();
        }
        #endregion

        #region #155 - see MinStack
        #endregion

        #region #160 ListNode - not completed
        #endregion

        #region 162
        public int FindPeakElement(int[] nums)
        {
            int len = nums.Length;
            if (len == 1) return 0;
            if (nums[0] > nums[1]) return 0;
            if (nums[len - 1] > nums[len - 2]) return len - 1;

            for (int i = 1; i < len-1 ; i++)
            {
                if (nums[i] > nums[i - 1] && nums[i] > nums[i + 1]) return i;
            }
            return -1;
        }
        #endregion

        #region #167
        public int[] TwoSum2(int[] numbers, int target)
        {
            int i = 0; 
            int j = numbers.Length-1;
            while (i < j)
            {
                var sum = numbers[i] + numbers[j];
                if (sum == target) return new int[2] { i + 1, j + 1 };
                if (sum < target) i++;
                else j++;
            }
            return new int[2] { 0, 0 };
        }
        public int[] TwoSum(int[] numbers, int target)
        {
            int lowIndex = 0;
            int highIndex = lowIndex + 1;
            int[] array = new int[2];
            int length = numbers.Length;
            while (lowIndex < length - 1)
            {
                var sum = numbers[lowIndex] + numbers[highIndex];
                if (sum == target) return new int[2] { lowIndex + 1, highIndex + 1 };
                if (sum < target)
                {
                    highIndex++;
                    if (highIndex == length)
                    {
                        lowIndex++;
                        highIndex = lowIndex + 1;
                    }
                }
                else
                {
                    lowIndex++;
                    highIndex = lowIndex + 1;
                }
            }
            return array;
        }
        #endregion

        #region #168  review
        public string ConvertToTitle(int columnNumber)
        {
            int index = 1;
            var dic = new Dictionary<int, char>();
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                dic[index] = letter;
                index++;
            }
            if (dic.ContainsKey(columnNumber)) return dic[columnNumber].ToString();
            var list = new List<double[]>();
            list.Add(new double[2] { 1, 26 });
            double sum = 26;
            int n = 1;
            while (sum < columnNumber)
            {
                n++;
                var min = list.Last()[1] + 1;
                double max = 0;
                for (int i = n; i >= 1; i--)
                {
                    max += Math.Pow(26, i);
                }
                list.Add(new double[2] { min, max });
                sum = max;
            }
            return GetString(n, "", columnNumber, dic);
        }

        private string GetString(int n, string str, double columnNumber, Dictionary<int, char> dic)
        {
            if (columnNumber == 0) return str;
            double max = 0;
            for (int j = n - 1; j >= 1; j--)
            {
                max += Math.Pow(26, j);
            }
          
            for (int i = 1; i <= 26; i++)
            {
                var remainder = columnNumber - i * Math.Pow(26, n - 1);
                if (remainder <= max)
                {
                    str = str + dic[i].ToString();
                    return GetString(n - 1, str, remainder, dic);
                }
            }
            return str;
        }
        #endregion

        #region #169 grouping
        public int MajorityElement(int[] nums)
        {
            var group = nums.GroupBy(x => x);
            int majorNum = nums.Length / 2;
            return group.Where(x => x.Count() > majorNum).First().Key;
        }
        #endregion

        #region #171
        public int TitleToNumber(string columnTitle)
        {
            var dic = new Dictionary<char, int>();
            int index = 1;
            for (char i = 'A'; i <= 'Z'; i++)
            {
                dic.Add(i, index);
                index++;
            }
            var array = columnTitle.ToArray();
            var length = array.Length;
            var sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += dic[array[i]] * (int)(Math.Pow(26, length - i - 1));
            }
            return sum;
        }
        #endregion

        #region #189
        public void Rotate(int[] nums, int k)
        {
            int len = nums.Length;
            if (len == 1) return;
            if (k == 0) return;
            if (k > len)
            {
                k = k % len;
            }
            int index;
            int val = nums[0];
            int[] visited = new int[len];
            for (int i = 0; i < len; i++)
            {
                index = i;
                if (visited[i] == 0)
                {
                    while (true)
                    {
                        if (index < len - k)
                        {
                            int nwVal = nums[index + k];
                            nums[index + k] = val;
                            index = index + k;
                            val = nwVal;
                        }
                        else
                        {
                            int nwVal = nums[index - (len - k)];
                            nums[index - (len - k)] = val;
                            index = index - (len - k);
                            val = nwVal;
                        }

                        if (visited[index] == 1) break;
                        visited[index] = 1;
                    }
                }
            }
        }
        #endregion

        #region #198
        public int Rob(int[] nums)
        {
            int length = nums.Length;
            if (length == 1) return nums[0];
            int max = Math.Max(nums[0], nums[1]);
            for (int i = 2; i < length; i++)
            {
                var temp = 0;
                for (int j = 0; j < i - 1; j++)
                {
                    temp = Math.Max(temp, nums[j]);
                }
                nums[i] = temp + nums[i];
                max = Math.Max(max, nums[i]);
            }
            return max;
        }
        #endregion

        #region 200
        #endregion
        #endregion

        #region 201-300
        #region #202
        public bool IsHappy(int n)
        {
            if (n == 1) return true;
            var hash = new HashSet<int>();
            while (true)
            {
                var sum = 0;
                var str = n.ToString();

                foreach (var c in str)
                {
                    var cs = int.Parse(c.ToString());
                    sum += cs * cs;
                }
                if (sum == 1) return true;
                if (hash.Contains(sum)) return false;
                hash.Add(sum);
                n = sum;
            }
        }
        #endregion

        #region #203 ListNode review
        public ListNode RemoveElements(ListNode head, int val)
        {
            ListNode previous = head;
            ListNode next = head;
            while (next != null)
            {
                if (next.val == val)
                {
                    if (next == head)
                    {
                        head = next.next;
                    }
                    else
                    {
                        previous.next = next.next;
                    }
                }
                else
                {
                    previous = next;
                }

                next = next.next;

            }
            return head;
        }

        #endregion

        #region 204 review
        public int CountPrimes(int n)
        {
            bool[] sieve = new bool[n];
            for(int i = 2; i < n; i++)
            {
                sieve[i] = true;
            }
            for(int i = 2; i < n; i++)
            {
                if (!sieve[i]) continue;
                var counter = 2;
                while (i * counter < n)
                {
                    sieve[i * counter] = false;
                    counter++;
                }
            }
            return sieve.Where(x => x).Count();
        }
        #endregion

        #region #205 word pattern review
        public bool IsIsomorphic(string s, string t)
        {
            int len = s.Length;
            var dic = new Dictionary<char, char>();
            char[] sArr = s.ToCharArray(), tArr = t.ToCharArray();

            for (int i = 0; i < len; i++)
            {
                if (dic.ContainsKey(sArr[i]))
                {
                    if (dic[sArr[i]] != tArr[i])
                        return false;
                }
                else
                {
                    if (dic.ContainsValue(tArr[i]))
                        return false;
                    else
                        dic.Add(sArr[i], tArr[i]);
                }
            }
            return true;
        }
        #endregion

        #region #206 ListNode
        public ListNode ReverseList(ListNode head)
        {
            if (head == null) return null;
            ListNode res = new ListNode(head.val, null);
            while (head.next != null)
            {
                res = new ListNode(head.next.val, res);
                head = head.next;
            }
            return res;
        }
        #endregion

        #region 209
        public int MinSubArrayLen(int target, int[] nums)
        {
            int N = nums.Length;

            int min = N + 1;
            int sum = nums[0];
            if (sum >= target) return 1;

            int low = 0;
            int high = 0;
            
            for (int i = 1; i < N; i++)
            {
                if (nums[i] >= target) return 1;
                high = i;

                sum += nums[i];
                if (sum >= target)
                {
                    while (sum >= target && low < high)
                    {
                        sum -= nums[low];
                        low++;

                    }
                    if ((high - low + 2) < min)
                    {
                        min = high - low + 2;
                    }
                }
            }
            return min == N + 1 ? 0 : min;
        }
        #endregion

        #region 213
        public int Rob213(int[] nums)
        {
            int len = nums.Length;
            if (len == 1) return nums[0];
            if (len <= 3) return nums.Max();

            int[] dp = new int[len];
            dp[0] = nums[0];
            dp[1] = nums[1];
            int max = dp[0];

            for (int i = 2; i < len - 1; i++)
            {
                dp[i] = max + nums[i];
                if (max < dp[i - 1])
                {
                    max = dp[i - 1];
                }
            }

            int res = Math.Max(dp[len - 2], dp[len - 3]);

            // without the first one
            dp[0] = nums[1];
            dp[1] = nums[2];
            max = dp[0];
            for (int i = 2; i < len - 1; i++)
            {
                dp[i] = max + nums[i + 1];
                if (max < dp[i - 1])
                {
                    max = dp[i - 1];
                }
            }
            var temp = Math.Max(dp[len - 1], dp[len - 2]);
            return Math.Max(temp, res);
        }
        #endregion

        #region #215 - review
        public int FindKthLargest(int[] nums, int k)
        {
            Array.Sort(nums);
            return nums[nums.Length - k];
        }

        public int FindKthLargestHeap(int[] nums, int k)
        {
            var heap = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (heap.Count < k)
                {
                    heap.Add(nums[i]);
                }
                else
                {
                    if (nums[i] > heap.Min())
                    {
                        heap.Remove(heap.Min());
                        heap.Add(nums[i]);
                    }
                }
            }
            heap.Sort();
            return heap.First();
        }

        public int FindKthLargest3(int[] nums, int k)
        {
            var dict = new Dictionary<int, int>();
            for (var i = 0; i < nums.Length; i++)
            {
                if (!dict.ContainsKey(nums[i]))
                    dict.Add(nums[i], 0);
                dict[nums[i]]++;
            }

            var list = dict.Keys.ToList();
            list.Sort();

            var result = 0;
            for (var i = list.Count - 1; i >= 0; i--)
            {
                result += dict[list[i]];
                if (result >= k)
                    return list[i];
            }
            return 0;
        }
        #endregion

        #region 216 review
        public IList<IList<int>> CombinationSum3(int k, int n)
        {
            var list = new List<IList<int>>();
            if (k == 1)
            {
                if (k == n)
                {
                    list.Add(new List<int> { n });
                }

                return list;
            }

            if (k >= n) return list;

            var dict = new Dictionary<int, List<IList<int>>>();
            dict.Add(1, new List<IList<int>> { new List<int> { 1 } });
            dict.Add(2, new List<IList<int>> { new List<int> { 2 } });
            for (int i = 3; i <= n; i++)
            {
                dict.Add(i, new List<IList<int>>());
                if (i <= 9)
                {
                    dict[i].Add(new List<int> { i });
                }
                var tempdic = new HashSet<string>();
                for (int j = 1; j <= i / 2; j++)
                {
                    if (i - j != j)
                    {
                        foreach (var d1 in dict[j])
                        {
                            var len = d1.Count();
                            if (len == k) continue;
                            int[] array = new int[len];
                            d1.CopyTo(array, 0);
                            foreach (var d2 in dict[i - j])
                            {
                                var tempLen = d2.Count() + len;
                                if (tempLen <= k)
                                {
                                    var tempList = array.ToList();
                                    tempList.AddRange(d2);

                                    if (tempList.ToHashSet().Count() != tempLen) continue;
                                    tempList.Sort();
                                    if (tempdic.Add(String.Join("", tempList)))
                                    {
                                        dict[i].Add(tempList);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return dict[n].Where(x => x.Count() == k).ToList();
        }

        public IList<IList<int>> CombinationSum3a(int k, int n)
        {

            var list = new List<IList<int>>();
            if (k == 1)
            {
                if (k == n)
                {
                    list.Add(new List<int> { n });
                }

                return list;
            }

            if (k >= n) return list;

            if (n > 45) return list;

            Backtrack(list, new List<int>(), k, n, 0, 1);

            return list;
        }

        void Backtrack(List<IList<int>> list, List<int> temp, int k, int n, int count, int start)
        {
            if (temp.Count() == k && temp.Sum() == n) list.Add(new List<int>(temp));
            for (int i = count; i < k; i++)
            {
                for (int j = start; j < 10; j++)
                {
                    temp.Add(j);
                    Backtrack(list, temp, k, n, i + 1, j + 1);
                    temp.RemoveAt(temp.Count() - 1);
                }
            }
        }
        #endregion

        #region #217 grouping
        public bool ContainsDuplicate(int[] nums)
        {
            // var groups = nums.GroupBy(x => x);
            // return groups.Any(x => x.Count() >= 2);
            var hash = new HashSet<int>();
            foreach (int n in nums)
            {
                if (hash.Contains(n)) return true;
                hash.Add(n);
            }
            return false;
        }
        #endregion

        #region #219
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            var dic = new Dictionary<int, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    if (i - dic[nums[i]].Last() <= k) return true;
                    dic[nums[i]].Add(i);
                }
                else
                {
                    dic.Add(nums[i], new List<int> { i });
                }
            }
            return false;
        }

        public bool ContainsNearbyDuplicate2(int[] nums, int k)
        {
            HashSet<int> hashSet = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > k)
                {
                    hashSet.Remove(nums[i - k - 1]);
                }
                if (!hashSet.Add(nums[i]))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region 220 treeset review
        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            int len = nums.Length;
            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j <= Math.Min(i + k, len - 1); j++)
                {
                    if ((double)(Math.Abs((double)nums[i] - nums[j])) <= t) return true;
                }
            }
            return false;
        }

        public bool ContainsNearbyAlmostDuplicateb(int[] nums, int k, int t)
        {
            var treeSet = new SortedSet<double>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (treeSet.GetViewBetween((double)nums[i] - t, (double)nums[i] + t).Count() > 0) return true;

                treeSet.Add(nums[i]);
                if (treeSet.Count() > k)
                {
                    treeSet.Remove(nums[i - k]);
                }
            }
            return false;
        }
        #endregion

        #region #226 ListNode
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null) return null;
            if (root.right == null && root.left == null) return root;
            var swap = root.left;
            root.left = root.right;
            root.right = swap;
            InvertTree(root.left);
            InvertTree(root.right);
            return root;

        }
        #endregion

        #region #228
        public IList<string> SummaryRanges(int[] nums)
        {
            var list = new List<string>();
            int length = nums.Length;
            if (length == 0) return list;
            int preIndex = 0;
            for(int i = 1; i < length; i++)
            {
                if (nums[i] > 1 + nums[i - 1])
                {
                    if(preIndex == i - 1)
                    {
                        list.Add(nums[preIndex].ToString());
                    }
                    else
                    {
                        list.Add(nums[preIndex].ToString() + "->" + nums[i - 1].ToString());
                    }
                    preIndex = i;
                }
            }
            if (preIndex == length-1)
            {
                list.Add(nums[preIndex].ToString());
            }
            else
            {
                list.Add(nums[preIndex].ToString() + "->" + nums[length - 1].ToString());
            }
            return list;
        }
        #endregion

        #region 229 two candidates idea 
        public IList<int> MajorityElement229(int[] nums)
        {
            return nums.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count()).Where(x => x.Value > nums.Length / 3).Select(x => x.Key).ToList();
        }
        #endregion

        #region #231
        public bool IsPowerOfTwo(int n)
        {
            if (n <= 0) return false;
            if (n == 1) return true;
            while (n % 2 != 1)
            {
                n = n / 2;
            }
            return n == 1; 
        }
        #endregion

        #region #234 ListNode
        public bool IsPalindrome(ListNode head)
        {
            var reversedNode = ReverseList(head);
            while (head != null)
            {
                if (head.val != reversedNode.val) return false;
                head = head.next;
                reversedNode = reversedNode.next;
            }
            return true;
        }
        #endregion

        #region #235 TreeNode  binary search tree  - review
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {

            if (root == null || p == null || q == null)
                return null;

            var maxValue = Math.Max(p.val, q.val);
            var minValue = Math.Min(p.val, q.val);

            if (maxValue < root.val)
            {
                return LowestCommonAncestor(root.left, p, q);
            }

            if (minValue > root.val)
                return LowestCommonAncestor(root.right, p, q);

            return root;
        }
        #endregion

        #region #237 ListNode review 
        public void DeleteNode(ListNode node)
        {
            node.val = node.next.val;
            node.next = node.next.next;
        }
        #endregion

        #region 238 review 
        public int[] ProductExceptSelf(int[] nums)
        {
            int len = nums.Length;
            var list = new int[len];
            var zero = nums.Select((x, index) => x == 0 ? index : -1).Where(x => x != -1).ToList();
            if (zero.Count() >= 2) return list;

            int i = 0;
            if (zero.Count() == 1)
            {
                var temp = 1;
                for (; i < len; i++)
                {
                    if (i == zero[0]) continue;
                    temp *= nums[i];
                }
                list[zero[0]] = temp;
                return list;
            }

            var preProduct = 1;

            for (; i < len - 1; i++)
            {
                var temp = preProduct;
                for (int j = i + 1; j < len; j++)
                {
                    temp *= nums[j];
                }
                list[i] = temp;
                preProduct *= nums[i];
            }

            list[len - 1] = preProduct;
            return list;
        }


        public int[] ProductExceptSelfa(int[] nums)
        {
            int len = nums.Length;
            var list = new int[len];
            var zero = nums.Select((x, index) => x == 0 ? index : -1).Where(x => x != -1).ToList();
            if (zero.Count() >= 2) return list;

            if (zero.Count() == 1)
            {
                var temp = 1;
                for (int i = 0; i < len; i++)
                {
                    if (i == zero[0]) continue;
                    temp *= nums[i];
                }
                list[zero[0]] = temp;
                return list;
            }

            var forwards = new int[len];
            var backwards = new int[len];
            forwards[0] = 1;
            backwards[len - 1] = 1;

            for (int i = 1; i < len; i++)
            {
                forwards[i] = forwards[i - 1] * nums[i - 1];
                backwards[len - i - 1] = backwards[len - i] * nums[len - i];
            }

            for (int i = 0; i < len; i++)
            {
                list[i] = backwards[i] * forwards[i];
            }

            return list;
        }
        #endregion

        #region 240
        public bool SearchMatrix240(int[][] matrix, int target)
        {
            int row = matrix.Length;
            if (row == 1) return matrix[0].Contains(target);
            return SearchMatrix(matrix, target, 0, matrix.Length - 1);
        }

        public bool SearchMatrix(int[][] matrix, int target, int start, int end)
        {
            int mid = start + (end - start) / 2;
            if (matrix[mid][0] == target) return true;
            if (matrix[mid][0] > target)
            {
                if (mid == 0) return false;
                if (start > mid - 1) return false;
                return SearchMatrix(matrix, target, start, mid - 1);
            }
            else
            {
                if (matrix[mid].Contains(target)) return true;
                if (mid == matrix.Length - 1) return false;
                // trace back
                if (mid >= 1 && start <= mid - 1 && SearchMatrix(matrix, target, start, mid - 1)) return true;

                if (mid < matrix.Length - 1 && mid + 1 <= end && SearchMatrix(matrix, target, mid + 1, end)) return true;
            }
            return false;
        }
        #endregion

        #region #242
        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;
            var dic = new Dictionary<char, int>();
            foreach (char c in s)
            {
                if (dic.ContainsKey(c))
                {
                    dic[c]++;
                }
                else
                {
                    dic.Add(c, 1);
                }
            }
            foreach (char c in t)
            {
                if (!dic.ContainsKey(c) || dic[c] <= 0) return false;

                dic[c]--;
            }
            return true;
        }
        #endregion

        #region 257
        public IList<string> BinaryTreePaths(TreeNode root)
        {
            string a = null;
            var t = a ?? "a";
            var list = new List<string>();
            BinaryTreePaths(root, list, new List<string>());
            return list;
        }

        private void BinaryTreePaths(TreeNode root, List<string> list, List<string> temp)
        {
            temp.Add(root.val.ToString());
            string[] array = new string[temp.Count()];
            temp.CopyTo(array, 0);
            if (root.left == null && root.right == null)
            {
                list.Add(String.Join("->", temp));
                return;
            }
            if (root.left != null)
            {
                BinaryTreePaths(root.left, list, temp);
            }
            temp = array.ToList();
            if (root.right != null)
            {
                BinaryTreePaths(root.right, list, temp);
            }
        }
        #endregion

        #region #258
        public int AddDigits(int num)
        {
            if (num < 10) return num;
            int sum = 0;
            var str = num.ToString();
            for(int i = 0; i < str.Length; i++)
            {
                sum += int.Parse(str[i].ToString());
            }
            return AddDigits(sum);
        }
        #endregion

        #region 259
        #endregion

        #region #263
        public bool IsUgly(int n)
        {
            if (n < 1) return false;
            while (n % 2 == 0)
            {
                n = n / 2;
            }
            while (n % 3 == 0)
            {
                n = n / 3;
            }
            while (n % 5 == 0)
            {
                n = n / 5;
            }
            return n == 1;
        }
        #endregion

        #region #268
        public int MissingNumber(int[] nums)
        {
            var sum = 0;
            for (int i = 0; i <= nums.Length; i++)
            {
                sum += i;
            }
            return sum - nums.Sum();
        }
        #endregion

        #region #283 sliding window
        public void MoveZeroes(int[] nums)
        {
            int length = nums.Length;
            if (length == 1) return;
            int lowIndex = 0;
            int highIndex = length - 1;
            while (lowIndex < highIndex)
            {
                if (nums[lowIndex] == 0)
                {
                    MoveElements(nums, lowIndex, highIndex);
                    highIndex--;
                }
                else
                {
                    lowIndex++;
                }
            }
        }

        private void MoveElements(int[] nums, int lowIndex, int highIndex)
        {
            while (lowIndex < highIndex)
            {
                nums[lowIndex] = nums[lowIndex + 1];
                lowIndex++;
            }
            nums[highIndex] = 0;
        }
        #endregion

        #region #290
        public bool WordPattern(string pattern, string s)
        {
            var array = s.Split(" ");
            if (pattern.Length != array.Length) return false;
            var dict = new Dictionary<string, char>();
            for(int i = 0; i < array.Length; i++)
            {
                if (!dict.ContainsKey(array[i]))
                {
                    if (dict.Values.Contains(pattern[i])) return false;
                    dict.Add(array[i], pattern[i]);
                }
                else
                {
                    if (dict[array[i]] != pattern[i]) return false;
                }
            }
            return true;
        }
        #endregion
        #endregion

        #region 301-400

        #region #322 -dp
        public int CoinChange(int[] coins, int amount)
        {
            if (amount == 0) return 0;
            var min = coins.Min();
            if (min > amount) return -1;

            long[] array = new long[amount + 1];
            array[0] = 0;
            for (int i = 1; i < min; i++)
            {
                array[i] = int.MaxValue;
            }

            for (int i = min; i <= amount; i++)
            {
                if (coins.Contains(i))
                {
                    array[i] = 1;
                }
                else
                {
                    array[i] = int.MaxValue;
                    for (int j = 1; j <= (i - 2) / 2 + 1; j++)
                    {
                        if (array[j] != int.MaxValue && array[i-j] != int.MaxValue)
                        {
                            if (array[i] > array[j] + array[i - j])
                            {
                                array[i] = array[j] + array[i - j];
                            }
                        }
                    }
                }
            }
            return array[amount] == int.MaxValue ? -1 : (int)array[amount];
        }

        #endregion

        #region #326
        public bool IsPowerOfThree(int n)
        {
            if (n == 1) return true;
            if (n < 3) return false;
            while (n % 3 == 0)
            {
                n = n / 3;
                if (n == 1) return true;
            }
            return false;
        }

        #endregion

        #region #338
        public int[] CountBits(int n)
        {
            int[] ans = new int[n + 1];
            ans[0] = 0;
            if (n == 0) return ans;
            ans[1] = 1;
            int[] digits = new int[1];
            digits[0] = 1;

            for (int i = 2; i < n + 1; i++)
            {
                digits = CountBitsPlusOne(digits);
                ans[i] = digits.Where(x => x == 1).Count();
            }
            return ans;
        }
        public int[] CountBitsPlusOne(int[] digits)
        {
            var n = digits.Length;
            var add = digits[n - 1] == 1;
            if (!add)
            {
                digits[n - 1] += 1;
                return digits;
            }

            for (int i = n - 1; i >= 0; i--)
            {
                if (add)
                {
                    var sum = digits[i] + 1;
                    if (sum >= 2)
                    {
                        digits[i] = sum - 2;
                    }
                    else
                    {
                        digits[i] = sum;
                        return digits;
                    }

                }
            }
            return digits.Prepend(1).ToArray();
        }
        #endregion

        #region #342
        public bool IsPowerOfFour(int n)
        {
            if (n <= 0) return false;
            if (n == 1) return true;
            while(n%4 == 0)
            {
                n = n / 4;
            }
            return n == 1;
        }
        #endregion

        #region #344
        public void ReverseString(char[] s)
        {
            int n = s.Length;
            for (int i = 0; i < n / 2; i++)
            {
                var temp = s[i];
                var j = n - 1 - i;
                s[i] = s[j];
                s[j] = temp;
            }
        }
        #endregion

        #region #345 sliding window
        public string ReverseVowels(string s)
        {
            int len = s.Length;
            var vowels = new List<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
            var array = s.ToArray();
            int lowIndex = 0;
            int highIndex = len - 1;
            while (lowIndex < highIndex)
            {
                if (vowels.Contains(array[lowIndex]))
                {
                    while (highIndex > lowIndex && !vowels.Contains(array[highIndex]))
                    {
                        highIndex--;
                    }
                    var swap = array[lowIndex];
                    array[lowIndex] = array[highIndex];
                    array[highIndex] = swap;
                    highIndex--;
                }
                lowIndex++;
            }
            return string.Concat(array);
        }
        #endregion

        #region 349
        public int[] Intersection(int[] nums1, int[] nums2)
        {
            var dic1 = nums1.ToHashSet();
            var dic2 = nums2.ToHashSet();
            return dic1.Where(x => dic2.Contains(x)).ToArray();
        }
        #endregion

        #region #350
        public int[] Intersect(int[] nums1, int[] nums2)
        {
            var dic1 = nums1.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var dic2 = nums2.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var list = new List<int>();
            foreach (var d in dic1)
            {
                if (dic2.ContainsKey(d.Key))
                {
                    for (int i = 0; i < Math.Min(d.Value, dic2[d.Key]); i++)
                    {
                        list.Add(d.Key);
                    }
                }
            }
            return list.ToArray();
        }
        #endregion

        #region 373
        public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
        {
            var dict = new SortedDictionary<int, List<List<int>>>();
            for(int i = 0; i < Math.Min(nums1.Count(), k+1); i++)
            {
                for(int j = 0; j < Math.Min(nums2.Count(), k + 1); j++)
                {
                    var sum = nums1[i] + nums2[j];
                    if (!dict.ContainsKey(sum))
                        dict.Add(sum, new List<List<int>>());
                    dict[sum].Add(new List<int> { nums1[i], nums2[j] });
                }
            }

            var list = new List<IList<int>>(0);
            int counter = 1;
            foreach(var d in dict)
            {
                for(int i=0;i<d.Value.Count() && counter<=k; i++)
                {
                    list.Add(d.Value[i]);
                    counter++;
                }
                if (counter > k) return list;
            }
            return list;

        }
        #endregion

        #region #387 review
        public int FirstUniqChar(string s)
        {
            var array = s.ToArray();
            var dic = array.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            dic = dic.Where(x => x.Value == 1).ToDictionary(x => x.Key, x => x.Value);
            int ans = 0;
            foreach (var c in array)
            {
                if (dic.ContainsKey(c)) return ans;
                ans++;
            }
            return -1;
        }

        public int FirstUniqChar2(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s.IndexOf(s[i]) == s.LastIndexOf(s[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        #endregion

        #region #389
        public char FindTheDifference(string s, string t)
        {
            if (s == "") return t[0];
            var dics = s.ToArray().GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var dict = t.ToArray().GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            foreach (var d in dict)
            {
                if (!dics.ContainsKey(d.Key)) return d.Key;
                if (d.Value != dics[d.Key]) return d.Key;
            }
            return t[0];
        }
        #endregion
        #endregion

        #region 401-500
        #region #412
        public IList<string> FizzBuzz(int n)
        {
            var list = new List<string>();
            for (int i = 1; i <= n; i++)
            {
                if (i % 15 == 0)
                {
                    //answer[i] == "FizzBuzz" if i is divisible by 3 and 5.
                    list.Add("FizzBuzz");
                }
                else if (i % 3 == 0)
                {
                    //answer[i] == "Fizz" if i is divisible by 3.
                    list.Add("Fizz");
                }
                else if (i % 5 == 0)
                {
                    //answer[i] == "Buzz" if i is divisible by 5.
                    list.Add("Buzz");
                }
                else
                {
                    list.Add(i.ToString());
                }
            }
            return list;
        }
        #endregion

        #region #415
        public string AddStrings(string num1, string num2)
        {
            int l1 = num1.Length;
            int l2 = num2.Length;
            if (num1[0] == '0') return num2;
            if (num2[0] == '0') return num1;

            if (l1 < l2)
            {
                var n = num1;
                num1 = num2;
                num2 = n;
                var ln = l1;
                l1 = l2;
                l2 = ln;
            }

            var diff = l1 - l2;
            var ans = "";
            bool carry = false;
            int i;

            for (i = l2 - 1; i >= 0; i--)
            {
                int val = int.Parse(num1[i + diff].ToString()) + int.Parse(num2[i].ToString());
                if (carry)
                {
                    val++;
                }
                carry = val >= 10;
                ans = ans.Insert(0, carry ? (val - 10).ToString() : val.ToString());
            }
            if (diff>0)
            {
                i = diff-1; // l1 - 1 - l2

                while (i >= 0)
                {
                    var val = int.Parse((num1[i]).ToString());
                    if (carry)
                    {
                        val++;
                    }
                    carry = val >= 10;
                    ans = ans.Insert(0, carry ? (val - 10).ToString() : val.ToString());
                    i--;
                }
            }
            if (carry)
                ans = ans.Insert(0, "1");
            return ans;
        }
        #endregion

        #region #448 grouping
        public IList<int> FindDisappearedNumbers(int[] nums)
        {
            int n = nums.Length;
            var dic = new Dictionary<int, int>();
            for (int i = 1; i <= n; i++)
            {
                dic.Add(i, 1);
            }
            for (int i = 0; i < n; i++)
            {
                dic[nums[i]]--;
            }
            return dic.Where(x => x.Value == 1).Select(x => x.Key).ToList();
        }
        #endregion

        #region 453 review
        public int MinMoves(int[] nums)
        {
            int len = nums.Length;
            int sum = nums.Sum();
            int min = nums.Min();
            return sum - len * min;

        }
        #endregion

        #region 455
        public int FindContentChildren(int[] g, int[] s)
        {
            int slen = s.Length;
            if (slen == 0) return 0;

            Array.Sort(g);
            Array.Sort(s);
            int index = 0;
            int res = 0;
            for (int i = 0; i < g.Length; i++)
            {
                if (g[i] <= s[index])
                {
                    res++;
                }
                else
                {
                    i = i - 1;
                }
                index++;
                if (index == slen) return res;
            }
            return res;
        }
        #endregion

        #region 463
        public int IslandPerimeter(int[][] grid)
        {
            int row = grid.Length;
            int cul = grid[0].Length;
            int ans = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cul; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        ans = ans + 4;
                        if (j - 1 >= 0 && grid[i][j - 1] == 1)
                        {
                            ans--;
                        }
                        if (j + 1 < cul && grid[i][j + 1] == 1)
                        {
                            ans--;
                        }
                        if (i - 1 >= 0 && grid[i - 1][j] == 1)
                        {
                            ans--;
                        }
                        if (i + 1 < row && grid[i + 1][j] == 1)
                        {
                            ans--;
                        }
                    }
                }
            }
            return ans;
        }
        #endregion

        #region 485
        public int FindMaxConsecutiveOnes(int[] nums)
        {
            int max = 0;
            if (!nums.Contains(1)) return max;
            int temp = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    max = Math.Max(max, temp);
                    temp = 0;
                }
                else
                {
                    temp++;
                }
            }

            return Math.Max(max, temp);
        }
        #endregion

        #region 495
        public int FindPoisonedDuration(int[] timeSeries, int duration)
        {
            if (duration == 0) return 0;
            int len = timeSeries.Length;
            if (len == 1) return duration;
            int totalSeconds = 0;
            int activeTime = timeSeries[0] + duration - 1;
            for (int i = 1; i < len; i++)
            {
                if (timeSeries[i] <= activeTime)
                {
                    totalSeconds += (timeSeries[i] - timeSeries[i - 1]);
                }
                else
                {
                    totalSeconds += duration;
                }
                activeTime = timeSeries[i] + duration - 1;
            }

            totalSeconds += duration;
            return totalSeconds;
        }
        #endregion

        #region 496
        public int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            var len1 = nums1.Length;
            int len2 = nums2.Length;
            int[] array = new int[len1];
            for (int i = 0; i < len1; i++)
            {
                int temp = Array.IndexOf(nums2, nums1[i]);
                array[i] = -1;
                for (int j = temp + 1; j < len2; j++)
                {
                    if (nums2[j] > nums1[i])
                    {
                        array[i] = nums2[j];
                        break;
                    }
                }
            }
            return array;
        }
        #endregion

        #region 500
        public string[] FindWords(string[] words)
        {
            var dic = new Dictionary<int, string>();
            dic.Add(1, "qwertyuiop");
            dic.Add(2, "asdfghjkl");
            dic.Add(3, "zxcvbnm");
            var list = new List<string>();
            for(int i = 0; i < words.Length; i++)
            {
                var str = words[i];
                int lenstr = str.Length;
                if (lenstr == 1)
                {
                    list.Add(str);
                    continue;
                } 
                int row = dic.First(x => x.Value.Contains(str[0], StringComparison.OrdinalIgnoreCase)).Key;
                var charts = dic[row];
                for(int j = 1; j < lenstr; j++)
                {
                    if (!charts.Contains(str[j], StringComparison.OrdinalIgnoreCase)) break;
                    if (j == lenstr - 1)
                    {
                        list.Add(str);
                    }
                }
            }
            return list.ToArray();
        }
        #endregion
        #endregion

        #region 501-600

        #region 506
        public string[] FindRelativeRanks(int[] score)
        {
            int len = score.Length;
            var dic = new Dictionary<int, int>();
            for(int i = 0; i < len; i++)
            {
                dic.Add(score[i], i);
            }
            Array.Sort(score);
            Array.Reverse(score);
            string[] ans = new string[len];
            for (int i = 0; i < len; i++)
            {
                var title = (i+1).ToString();
                if (i == 0) title = "Gold Medal";
                else if (i == 1) title = "Silver Medal";
                else if (i == 2) title = "Bronze Medal";

                ans[dic[score[i]]] = title;
            }
            return ans;
        }
        #endregion

        #region #509
        public int Fib(int n)
        {
            if (n <= 1) return n;
            var list = new List<int> { 0, 1 };
            int index = 2;
            while (index < n)
            {
                list.Add(list[index - 2] + list[index - 1]);
                index++;
            }
            return list[index - 2] + list[index - 1];
        }

        #endregion

        #region 542 review
        // https://massivealgorithms.blogspot.com/2017/04/leetcode-542-01-matrix.html
        public int[][] UpdateMatrix(int[][] mat)
        {
            int row = mat.Length;
            int cul = mat[0].Length;
            int[][] res = new int[row][];

            for (int i = 0; i < row; i++)
            {
                res[i] = new int[cul];
                for (int j = 0; j < cul; j++)
                {
                    res[i][j] = int.MaxValue - 1;
                    if (mat[i][j] == 0)
                    {
                        res[i][j] = 0;
                    }
                    else
                    {
                        if (i > 0) res[i][j] = Math.Min(res[i][j], res[i - 1][j] + 1);
                        if (j > 0) res[i][j] = Math.Min(res[i][j], res[i][j - 1] + 1);
                    }
                }
            }

            for (var i = row - 1; i >= 0; i--)
            {
                for (var j = cul - 1; j >= 0; j++)
                {

                    if (i < row - 1)
                        res[i][j] = Math.Min(res[i][j], res[i + 1][j] + 1);
                    if (j < cul - 1)
                        res[i][j] = Math.Min(res[i][j], res[i][j + 1] + 1);

                }
            }
            return res;
        }
        #endregion

        #region 561
        public int ArrayPairSum(int[] nums)
        {
            Array.Sort(nums);
            int res = 0;
            for(int i = 0; i < nums.Length - 1; i=i+2)
            {
                res += Math.Min(nums[i], nums[i + 1]);
            }
            return res;
        }
        #endregion

        #region 566
        public int[][] MatrixReshape(int[][] mat, int r, int c)
        {
            int row = mat.Length;
            int cul = mat[0].Length;
            if (row * cul != r * c) return mat;
            int nr = 0;
            int nc = 0;
            int[][] ans = new int[r][];
            for (int i = 0; i < r; i++)
            {
                ans[i] = new int[c];
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cul; j++)
                {
                    if (nc != 0 && nc % c == 0)
                    {
                        nc = 0;
                        nr++;
                    }
                    ans[nr][nc] = mat[i][j];
                    nc++;
                }
            }
            return ans;
        }
        #endregion

        #region 575
        public int DistributeCandies(int[] candyType)
        {
            var hash = candyType.ToHashSet();
            return Math.Min(candyType.Length / 2, hash.Count);
        }
        #endregion

        #region 594
        public int FindLHS(int[] nums)
        {
            int len = nums.Length;
            Array.Sort(nums);
            int max = nums[len - 1];

            if (nums[0] == max) return 0;

            int tempMax = 1;
            int maxcount = 0;
            int previouseIndex = len - 1;
            int index = len - 2;
            while (index >= 0)
            {
                if (nums[index] != max)
                {
                    if (max - nums[index] == 1)
                    {
                        if (nums[index] != nums[previouseIndex])
                        {
                            previouseIndex = index;
                        }

                        tempMax++;
                        index--;
                    }
                    else
                    {
                        if (max == nums[previouseIndex])
                        {
                            max = nums[index];
                            previouseIndex = index;
                        }
                        else
                        {
                            if (tempMax > 1)
                            {
                                maxcount = Math.Max(maxcount, tempMax);
                            }
                            max = nums[previouseIndex];
                        }
                        index = previouseIndex - 1;
                        tempMax = 1;
                    }
                }
                else
                {
                    tempMax++;
                    index--;
                }
            }
            if (tempMax > 1 && nums[0] != max)
            {
                maxcount = Math.Max(maxcount, tempMax);
            }
            return maxcount;
        }
        #endregion

        #region 598
        public int MaxCount(int m, int n, int[][] ops)
        {
            int opslen = ops.Length;
            if (opslen == 0) return m * n;
            int rowmin = int.MaxValue;
            int culmin = int.MaxValue;
            for (int i = 0; i < opslen; i++)
            {
                rowmin = Math.Min(rowmin, ops[i][0]);
                culmin = Math.Min(culmin, ops[i][1]);
            }
            
            return rowmin*culmin;
        }
        #endregion

        #region 599
        public string[] FindRestaurant(string[] list1, string[] list2)
        {
            var dict = new Dictionary<string, int>();
            for(int i = 0; i < list1.Length; i++)
            {
                dict.Add(list1[i], i);
            }
            var dict2 = new Dictionary<string, int>();
            for (int i = 0; i < list2.Length; i++)
            {
                if (dict.ContainsKey(list2[i]))
                {
                    dict2.Add(list2[i], dict[list2[i]] + i);
                }
            }
            var min = dict2.Values.Min();
            return dict2.Where(x => x.Value == min).Select(x => x.Key).ToArray();
        }
        #endregion
        #endregion
         
        #region 601-700

        #region 605 flowerbed[i - 1] + flowerbed[i] + flowerbed[i + 1] == 0
        public bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            if (n == 0) return true;
            int len = flowerbed.Length;
            int i = 0;
            if (flowerbed[0] == 1)
            {
                i = 2;
            }
            else
            {
                if (len > 1)
                {
                    if (flowerbed[1] == 0)
                    {
                        flowerbed[0] = 1;
                        i = 2;
                        n--;
                        if (n == 0) return true;
                    }
                    else
                    {
                        i = 3;
                    }
                }
                else
                {
                    return true;
                }
            }
            for (; i < len - 1; i++)
            {
                if (flowerbed[i - 1] + flowerbed[i] + flowerbed[i + 1] == 0)
                {
                    flowerbed[i] = 1;
                    i = i + 1;
                    n--;
                    if (n == 0) return true;
                }
            }
            if (len >= 2 && flowerbed[len - 1] + flowerbed[len - 2] == 0)
            {
                n--;
            }
            return n == 0;
        }
        #endregion

        #region #617 Tree review
        public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            if (root1 == null) return root2;
            if (root2 == null) return root1;

            root1.val += root2.val;

            root1.left = MergeTrees(root1.left, root2.left);
            root1.right = MergeTrees(root1.right, root2.right);

            return root1;
        }
        #endregion

        #region 628
        public int MaximumProduct(int[] nums)
        {
            int len = nums.Length;
            Array.Sort(nums);
            if (nums[len - 1] >= 0 && nums[0] < 0 && nums[1] < 0)
            {
                return Math.Max(nums[0] * nums[1] * nums[len - 1], nums[len - 1] * nums[len - 2] * nums[len - 3]);
            }
            return nums[len - 1] * nums[len - 2] * nums[len - 3];
        }
        #endregion

        #region 643 review
        public double FindMaxAverage(int[] nums, int k)
        {

            if (k == 0) return 0;
            double sum = 0;
            double max = 0;
            for(int i = 0; i < k; i++)
            {
                sum += nums[i];
                max = sum;
            }
            int len = nums.Length;
            for(int i=1;i<len && i + k <= len; i++)
            {
                sum = sum - nums[i - 1] + nums[i + k - 1];
                max = Math.Max(max, sum);
            }

            return max / k;
        }
        #endregion

        #region 645
        public int[] FindErrorNums(int[] nums)
        {
            int[] res = new int[2];
            var dict = nums.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            res[0] = dict.FirstOrDefault(x => x.Value == 2).Key;
            for(int i = 1; i <= nums.Length; i++)
            {
                if (!dict.ContainsKey(i))
                {
                    res[1] = i;
                    break;
                }
            }
            return res;
        }

        public int[] FindErrorNums2(int[] nums)
        {
            int[] res = new int[2];
            var sum = nums.Sum();
            var dict = nums.ToHashSet();
            var newsum = 0;
            for (int i = 1; i <= nums.Length; i++)
            {
                newsum += i;
                if (!dict.Contains(i))
                {
                    res[1] = i;
                }
            }
            res[0] = res[1] - (newsum - sum);

            return res;
        }
        #endregion

        #region 661 - not completed

        #endregion

        #region 674
        public int FindLengthOfLCIS(int[] nums)
        {
            int max = 1;
            int index = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] <= nums[i - 1])
                {
                    max = Math.Max(max, i - index);
                    index = i;
                }
            }
            return Math.Max(max, nums.Length - index);
        }
        #endregion

        #region 682
        public int CalPoints(string[] ops)
        {
            var list = new List<int>();
            foreach (var s in ops)
            {
                switch (s)
                {
                    case "+":
                        {
                            int len = list.Count;
                            int sum = list[len - 1] + list[len - 2];
                            list.Add(sum);
                            break;
                        }
                    case "D":
                        {
                            int sum = list.Last() * 2;
                            list.Add(sum);
                            break;
                        }
                    case "C":
                        {
                            list.RemoveAt(list.Count - 1);
                            break;
                        }
                    default:
                        list.Add(int.Parse(s));
                        break;
                }
            }
            return list.Sum();
        }
        #endregion

        #region 697
        public int FindShortestSubArray(int[] nums)
        {
            var dict = new Dictionary<int, List<int>>();

            for (var i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(nums[i]))
                    dict[nums[i]].Add(i);
                else dict[nums[i]] = new List<int> { i };
            }

            var f = dict.Max(d => d.Value.Count());
            return dict.Where(d => d.Value.Count == f).Min(a => a.Value[a.Value.Count - 1] - a.Value[0] + 1);
        }
        #endregion

        #endregion

        #region 701-800

        #region #704
        public int Search704(int[] nums, int target)
        {
            int length = nums.Length;
            return Search(nums, target, 0, length - 1);
        }

        private int Search(int[] nums, int target, int start, int end)
        {
            int mid = (end - start) / 2 + start;
            if (nums[mid] == target) return mid;

            if (nums[mid] < target)
            {
                if (mid + 1 > end) return -1;
                return Search(nums, target, mid + 1, end);
            }
            else
            {
                if (mid - 1 < 0) return -1;
                return Search(nums, target, 0, mid - 1);
            }
        }
        #endregion

        #region 713
        public int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            if (k == 0) return 0;

            int res = 0;
            int len = nums.Length;

            for (int i = 0; i < len; i++)
            {
                if (nums[i] >= k) continue;
                res++;
                int product = nums[i];
                int high = i + 1;
                while (high < len && product * nums[high] < k)
                {
                    product *= nums[high];
                    high++;
                    res++;
                }
            }
            return res;
        }
        #endregion

        #region 724
        public int PivotIndex(int[] nums)
        {
            int len = nums.Length;
            if (len == 1) return 0;

            int sum1 = 0;
            int sum2 = 0;

            for (int i = len - 1; i > 0; i--)
            {
                sum2 += nums[i];
            }

            if (sum1 == sum2) return 0;
            for (int i = 1; i < len; i++)
            {
                sum1 += nums[i - 1];
                sum2 -= nums[i];
                if (sum1 == sum2) return i;
            }
            return -1;
        }
        #endregion

        #region 733
        public int[][] FloodFill(int[][] image, int sr, int sc, int newColor)
        {
            if (image[sr][sc] == newColor) return image;
            int row = image.Length;
            int cul = image[0].Length;
            int[][] visited = new int[row][];
            for (int i = 0; i < row; i++)
            {
                visited[i] = new int[cul];
            }
            int og = image[sr][sc];
            image[sr][sc] = newColor;

            FloodFill(image, sr, sc, newColor, row, cul, og, visited);

            return image;

        }

        public void FloodFill(int[][] image, int sr, int sc, int newColor, int row, int cul, int ogcolor, int[][] visited)
        {
            if (sr < 0 || sr >= row || sc < 0 || sc >= cul || visited[sr][sc] == 1) return;
          
            if (sc > 0 && image[sr][sc - 1] == ogcolor)
            {
                image[sr][sc - 1] = newColor;
                FloodFill(image, sr, sc - 1, newColor, row, cul, ogcolor, visited);
            }

            if (sc < cul - 1 && image[sr][sc + 1] == ogcolor)
            {
                image[sr][sc + 1] = newColor;
                FloodFill(image, sr, sc + 1, newColor, row, cul, ogcolor, visited);
            }

            if (sr > 0 && image[sr - 1][sc] == ogcolor)
            {
                image[sr - 1][sc] = newColor;
                FloodFill(image, sr - 1, sc, newColor, row, cul, ogcolor, visited);
            }

            if (sr < row - 1 && image[sr + 1][sc] == ogcolor)
            {
                image[sr + 1][sc] = newColor;
                FloodFill(image, sr + 1, sc, newColor, row, cul, ogcolor, visited);
            }
            visited[sr][sc] = 1;
        }
        #endregion

        #region 746 dp 
        public int MinCostClimbingStairs(int[] cost)
        {
            int len = cost.Length;

            int[] dp = new int[len];
            dp[0] = cost[0];
            dp[1] = cost[1];
            if (len == 2) return dp.Min();
            for (int i = 2; i < len - 1; i++)
            {
                dp[i] = Math.Min(dp[i - 1], dp[i - 2]) + cost[i];
            }

            return Math.Min(dp[len - 2], dp[len - 3] + cost[len - 1]);
        }
        #endregion

        #region #763
        public IList<int> PartitionLabels(string s)
        {
            if (s.Length <= 1) return new List<int> { 1 };
            var list = new List<int>();
            var hash = s.ToHashSet();
            var dic = new Dictionary<char, int>();
            
            foreach(var h in hash)
            {
                dic.Add(h, s.LastIndexOf(h));
            }

            for(int i =0; i < s.Length; i++)
            {
                var lastIndex = dic[s[i]];
                for(int j = i + 1; j < lastIndex; j++)
                {
                    if (s[j] != s[i] && s.LastIndexOf(s[j]) > lastIndex)
                    {
                        lastIndex = s.LastIndexOf(s[j]);
                    }
                }
                list.Add(lastIndex + 1);
                i = lastIndex;
            }
            return list;

        }
        #endregion

        #region 766
        public bool IsToeplitzMatrix(int[][] matrix)
        {
            int row = matrix.Length;
            int cul = matrix[0].Length;
            for (int i = 0; i < row - 1; i++)
            {
                for (int j = 0; j < cul - 1; j++)
                {
                    if (matrix[i][j] != matrix[i + 1][j + 1]) return false;
                }
            }
            return true;
        }
        #endregion
        #endregion

        #region 801-900
        #region 812 three pointes triangle area formula review
        // 1/2 *abs(Ax*(By-Cy)+Bx*(Cy-Ay)+Cx*(Ay-By))
        public double LargestTriangleArea(int[][] points)
        {
            int len = points.Length;
            double max = 0;
            for (int i = 0; i < len - 2; i++)
            {
                for (int j = i + 1; j < len - 1; j++)
                {
                    for (int k = j + 1; k < len; k++)
                    {
                        max = Math.Max(max, area(points, i, j, k));
                    }
                }
            }
            return max;
        }

        private double area(int[][] points, int i, int j, int k)
        {
            return 0.5 * Math.Abs(points[i][0] * (points[j][1] - points[k][1]) + points[j][0] * (points[k][1] - points[i][1]) + points[k][0] * (points[i][1] - points[j][1]));
        }
        #endregion

        #region 821 review
        public int[] ShortestToChar(string s, char c)
        {
            var index = s.ToArray().Select((value, index) => value == c ? index : -1)
            .Where(index => index != -1).ToList();
            int len = s.Length;
            int[] res = new int[len];
            int indexlen = index.Count();
            if (indexlen == 1)
            {
                for (int i = 0; i < len; i++)
                {
                    res[i] = Math.Abs(i - index[0]);
                }
                return res;
            }

            int left = index[0];
            var right = index[1];
            int indexpos = 1;
            for (int i = 0; i < len; i++)
            {
                var diff1 = Math.Abs(i - left);
                var diff2 = Math.Abs(i - right);
                if (diff1 > diff2)
                {
                    left = right;
                    indexpos = Math.Min(indexpos + 1, indexlen - 1);
                    right = index[indexpos];
                    res[i] = diff2;
                    continue;
                }
                res[i] = diff1;
            }

            return res;
        }

        public int[] shortestToChar2(String s, char c)
        {
            int n = s.Length;
            int[] ans = new int[n];
            int index = n - 1;

            for (int i = 0; i < n; ++i)
            {
                ans[i] = n-1;
                if (s[i] == c)
                {
                    ans[i] = 0;
                    index = i;
                }
                else
                {
                    ans[i] = Math.Min(ans[i], Math.Abs(index - i));
                }
            }

            for (int i = n - 1; i >= 0; --i)
            {
                if (s[i] == c)
                {
                    index = i;                
                }
                else
                {
                    ans[i] = Math.Min(ans[i], Math.Abs(index - i));
                }
            }

            return ans;
        }
        #endregion

        #region 852 review
        public int PeakIndexInMountainArray(int[] arr)
        {
            int lo = 0;
            int hi = arr.Length - 1;
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                if (arr[mid] < arr[mid + 1])
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid;
                }
            }
            return lo;
        }
        #endregion

        #region 860
        public bool LemonadeChange(int[] bills)
        {
            int len = bills.Length;
            var dic = new Dictionary<int, int>();
            dic.Add(5, 0);
            dic.Add(10, 0);
            dic.Add(20, 0);
            for(int i = 0; i < len; i++)
            {
                dic[bills[i]]++;
                var change = bills[i] - 5;
                if (change == 5)
                {
                    if (dic[5] == 0) return false;
                    dic[5]--;
                }else if (change==15)
                {
                    if (dic[10] > 0)
                    {
                        change -= 10;
                        dic[10]--;
                    }
                    var num = change / 5;
                    if (dic[5] < num) return false;
                    dic[5] -= num;
                }
            }
            return true;
        }
        #endregion

        #region 867
        public int[][] Transpose(int[][] matrix)
        {
            int row = matrix.Length;
            int cul = matrix[0].Length;
            int[][] res = new int[cul][];
            for (int j = 0; j < cul; j++)
            {
                res[j] = new int[row];
            }
            int culIndex = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j <cul; j++)
                {
                    res[j][culIndex] = matrix[i][j];
                }
                culIndex++;
            }
            return res;
        }
        #endregion

        #region 888
        public int[] FairCandySwap(int[] aliceSizes, int[] bobSizes)
        {
            int len = aliceSizes.Length;
            int sumA = aliceSizes.Sum();
            int sumB = bobSizes.Sum();
            int diff=Math.Abs((sumA + sumB) /2 - Math.Min(sumA, sumB));
            if (sumA > sumB)
            {
                var dic = bobSizes.ToHashSet();
                for(int i = 0; i < len; i++)
                {
                    if (dic.Contains(aliceSizes[i] + diff))
                    {
                        return new int[] { aliceSizes[i], aliceSizes[i] + diff };
                    }
                }
            }
            else
            {
                var dic = aliceSizes.ToHashSet();
                for (int i = 0; i < len; i++)
                {
                    if (dic.Contains(bobSizes[i] + diff))
                    {
                        return new int[] { bobSizes[i] + diff, bobSizes[i]};
                    }
                }
            }

            return new int[2];
        }
        #endregion

        #region 896
        public bool IsMonotonic(int[] nums)
        {
            int len = nums.Length;
            int trend = 0;
            for (int i = 1; i < len; i++)
            {
                if (nums[i] > nums[i - 1])
                {
                    if (trend == 0)
                    {
                        trend = 1;
                    }
                    else if (trend == -1)
                    {
                        return false;
                    }
                }
                else if (nums[i] < nums[i - 1])
                {
                    if (trend == 0)
                    {
                        trend = -1;
                    }
                    else if (trend == 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #endregion

        #region 901-1000
        #region 914
        public bool HasGroupsSizeX(int[] deck)
        {
            var dic = deck.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            var min = dic.Values.Min();
            for (int i = 2; i <= min; i++)
            {
                if (!dic.Values.Any(x => x % i != 0)) return true;
            }
            return false;
        }
        #endregion

        #region 922
        public int[] SortArrayByParityII(int[] nums)
        {
            var dictionary = new Dictionary<bool, List<int>>();
            dictionary[true] = new List<int>();
            dictionary[false] = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if ((i % 2 == 1 && nums[i] % 2 == 1) || (i % 2 == 0 && nums[i] % 2 == 0)) continue;
                dictionary[i % 2 == 0].Add(i);
            }
            for (int i = 0; i < dictionary[true].Count; i++)
            {
                var temp = nums[dictionary[true][i]];
                nums[dictionary[true][i]] = nums[dictionary[false][i]];
                nums[dictionary[false][i]] = temp;
            }

            return nums;
        }
        #endregion

        #region 923
        public int ThreeSumMulti(int[] arr, int target)
        {
            double res = 0;
            Array.Sort(arr);
            if (arr[0] > target) return 0;
            int len = arr.Length;
            for (int i = 0; i < len; i++)
            {
                if (arr[i] * 3 > target) break;

                int left = i + 1;
                int right = len - 1;
                while (left < right)
                {
                    var sum = arr[i] + arr[left] + arr[right];
                    if (sum == target)
                    {
                        var preRight = arr[right];
                        var preLeft = arr[left];
                        if (preLeft != preRight)
                        {
                            var counter1 = 0;
                            while (arr[left] == preLeft)
                            {
                                counter1++;
                                left++;
                            }

                            var counter2 = 0;
                            while (arr[right] == preRight)
                            {
                                counter2++;
                                right--;
                            }
                            res += counter1 * counter2;
                        }
                        else
                        {
                            var tempcounter = right - left + 1;
                            res += (tempcounter * (tempcounter - 1)) / 2;
                            break;
                        }
                    }
                    else if (sum < target)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }
            return (int)(res % (Math.Pow(10, 9) + 7));
        }
        #endregion

        #region 929
            public int NumUniqueEmails(string[] emails)
        {
            var hash = new HashSet<string>();
            foreach (var s in emails)
            {
                var localName = s.Split('@')[0];
                var domainName = s.Split('@')[1];
                var pluspos = localName.IndexOf('+');

                if (pluspos != -1)
                {
                    localName = localName.Substring(0, pluspos);
                }
                localName = localName.Replace(".", "");
                hash.Add(localName + '@' + domainName);
            }
            return hash.Count();
        }
        #endregion

        #region 942
        public int[] DiStringMatch(string s)
        {
            var len = s.Length;
            int min = 0;
            int max = 0;
            int[] trend = new int[len + 1];
            trend[0] = 0;
            var list = new List<int>();
            list.Add(0);
            for (int i = 1; i <= len; i++)
            {
                list.Add(i);
                if (s[i - 1] == 'D')
                {
                    trend[i] = min - 1;
                    min = trend[i];
                }
                else
                {
                    trend[i] = max + 1;
                    max++;
                }
            }

            var start = list.First(x => x >= Math.Abs(min) && x <= len - max);

            for (int i = 0; i <= len; i++)
            {
                trend[i] += start;
            }
            return trend;
        }
        #endregion

        #region 953
        public bool IsAlienSorted(string[] words, string order)
        {
            if (words == null || words.Length <= 1) return true;
            var dict = new Dictionary<char, int>();
            for (int i = 0; i < order.Length; i++)
                dict[order[i]] = i;

            for (int i = 0; i < words.Length - 1; i++)
            {
                string word1 = words[i], word2 = words[i + 1];
                if (!IsValid(word1, word2, dict))
                    return false;
            }

            return true;
        }

        private bool IsValid(string s1, string s2, Dictionary<char, int> dict)
        {
            int length = Math.Min(s1.Length, s2.Length);
            for (int i = 0; i < length; i++)
                if (s1[i] != s2[i])
                    return dict[s1[i]] < dict[s2[i]];

            return s1.Length <= s2.Length;
        }
        #endregion

        #region 961
        public int RepeatedNTimes(int[] nums)
        {
            int len = nums.Length;
            int halfLen = len / 2;
            var dic = new Dictionary<int, int>();
            for (int i = 0; i < len; i++)
            {
                if (!dic.ContainsKey(nums[i]))
                {
                    dic.Add(nums[i], 0);
                }
                dic[nums[i]]++;
                if (dic[nums[i]] == halfLen) return nums[i];
            }
            return -1;
        }
        #endregion

        #region 976 review The necessary and sufficient condition for forming a triangle is that the sum of any two sides is greater than the third side.
        public int LargestPerimeter(int[] nums)
        {
            Array.Sort(nums);
            Array.Reverse(nums);
            int len = nums.Length;
            for (int i = 0; i < len - 2; i++)
            {
                for (int j = i + 1; j < len - 1; j++)
                {
                    for (int k = j + 1; k < len; k++)
                    {
                        var res = IsTriangle(nums[i], nums[j], nums[k]);
                        if (res == -1)
                        {
                            j = len - 1;
                        }
                        else if (res < 0)
                        {
                            k = len;
                        }
                        else
                        {
                            return nums[i] + nums[j] + nums[k];
                        }
                    }
                }
            }
            return 0;
        }
        public int IsTriangle(int a, int b, int c)
        {
            var p = (a + b + c) / 2.0;
            if (p <= a) return -1;
            if (p <= b) return -2;
            if (p <= c) return -3;

            return 1;
        }

        public int largestPerimeter2(int[] nums)
        {
            Array.Sort(nums);
            for (int i = nums.Length - 3; i >= 0; --i)
            {
                if (nums[i + 2] < nums[i + 1] + nums[i])
                {
                    return nums[i + 2] + nums[i + 1] + nums[i];
                }
            }

            return 0;
        }

        #endregion

        #region 989 review for high quality code
        public IList<int> AddToArrayForm(int[] num, int k)
        {
            var list = new List<int>();
            int len = num.Length;
            bool carry = false;
            var array = k.ToString().ToArray().Select(x => int.Parse(x.ToString())).ToArray();
            if (len < array.Count())
            {
                var swap = num;
                num = array;
                array = swap;
                len = num.Length;
            }
            int arrayLen = array.Count();
            var diff = len - arrayLen;

            for (int i = arrayLen - 1; i >= 0; i--)
            {
                var temp = num[i + diff] + array[i];
                if (carry)
                {
                    temp++;
                }
                carry = temp >= 10;
                list.Insert(0, carry ? temp - 10 : temp);
            }
            var index = diff - 1;
            while (index >= 0)
            {
                var temp = num[index];
                if (carry)
                {
                    temp++;
                }
                carry = temp >= 10;
                list.Insert(0, carry ? temp - 10 : temp);
                index--;
            }

            if (carry)
            {
                list.Insert(0, 1);
            }
            return list;
        }

        public IList<int> AddToArrayForm2(int[] A, int K)
        {
            var m = 0;
            var r = new List<int>();

            for (var i = A.Length - 1; i >= 0 || K > 0 || m > 0; i--)
            {
                var c = m + (i >= 0 ? A[i] : 0) + K % 10;
                m = c / 10;
                K /= 10;
                r.Insert(0, c % 10);
            }

            return r;
        }
        #endregion

        #region 997
        public int FindJudge(int n, int[][] trust)
        {
            int len = trust.Length;
            if (len == 0 && n == 1) return 1;
            var dic = new Dictionary<int, int>();
            for(int i = 1; i <= n; i++)
            {
                dic.Add(i, 0);
            }
            for (int i = 0; i < len; i++)
            {
                dic.Remove(trust[i][0]);
                if (dic.ContainsKey(trust[i][1]))
                    dic[trust[i][1]]++;
            }

            foreach (var d in dic)
            {
                if (d.Value == n - 1) return d.Key;
            }
            return -1;
        }
        #endregion
        #endregion

        #region 1001-1100
        #region 1002
        public IList<string> CommonChars(string[] words)
        {
            int len = words.Length;
            var charts = words[0].ToArray().Select(x => x.ToString()).ToList();
            if (len == 1) return charts;
            var dic1 = charts.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            for (int i = 0; i < len; i++)
            {
                charts = words[i].ToArray().Select(x => x.ToString()).ToList();
                var temp = charts.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
                foreach(var d in dic1)
                {
                    if (!temp.ContainsKey(d.Key)) dic1.Remove(d.Key);
                    else
                    {
                        dic1[d.Key] = Math.Min(dic1[d.Key], temp[d.Key]);
                    }
                }
                if (dic1.Keys.Count() == 0) return new List<string>();
            }
            var list = new List<string>();
            foreach(var d in dic1)
            {
                for(int i = 1; i <= d.Value; i++)
                {
                    list.Add(d.Key);
                }
            }
            return list;
        }
        #endregion

        #region 1005 review
        public int LargestSumAfterKNegations(int[] nums, int k)
        {
            Array.Sort(nums);
            var min = int.MaxValue;

            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < 0 && k > 0)
                {
                    nums[i] = -nums[i];
                    k--;
                    min = Math.Min(min, nums[i]);
                }
                else if (nums[i] >= 0)
                {
                    min = Math.Min(min, nums[i]);
                    break;
                }
            }

            if (k % 2 == 1) return nums.Sum() - 2 * min;

            return nums.Sum();
        }
        #endregion

        #region 1013 review
        public bool CanThreePartsEqualSum(int[] arr)
        {
            int len = arr.Length;
            var sum = arr.Sum();
            if (sum % 3 != 0) return false;
            var avg = sum / 3;
            var num = 1;
            sum = 0;
            for (int i = 0; i < len && num <= 3; i++)
            {
                sum += arr[i];
                if (sum == (num * avg))
                {
                    num++;
                }
            }

            return num > 3;
        }
        #endregion

        #region 1030
        public int[][] AllCellsDistOrder(int rows, int cols, int rCenter, int cCenter)
        {
            var dic = new Dictionary<int, List<int[]>>();
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    var dis = Math.Abs(i - rCenter) + Math.Abs(j - cCenter);
                    if (!dic.ContainsKey(dis))
                    {
                        dic.Add(dis, new List<int[]>());
                    }
                    dic[dis].Add(new int[] { i, j });
                }
            }
            var keys = dic.Keys.OrderBy(x=>x);
            var list = new List<int[]>();
            foreach(var k in keys)
            {
                list.AddRange(dic[k]);
            }
            return list.ToArray();
        }
        #endregion

        #region 1046 review
        public int LastStoneWeight(int[] stones)
        {
            int len = stones.Length;
            if (len == 1) return stones[0];

            Array.Sort(stones, 0, len);
            while (len >= 2)
            {
                var max1 = stones[len - 1];
                var max2 = stones[len - 2];
                if (max1 == max2) { len -= 2; }
                else
                {
                    stones[len - 2] = max1 - max2;
                    len -= 1;
                }
                Array.Sort(stones, 0, len);
            }

            if (len == 0) return 0;
            return stones[0];
        }

        public int LastStoneWeight2(int[] stones)
        {
            var dic = stones.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var keys = new List<int>();
            while (true)
            {
                var keysCount = dic.Keys.Count;
                var max = dic.Keys.Max();
                var remain = dic[max] % 2;
                if (keysCount == 1) return remain == 1 ? max : 0;
                dic.Remove(max);

                if (remain == 1)
                {
                    var newmax = dic.Keys.Max();
                    var diff = max - newmax;
                    if (keysCount == 2) return diff;
                    dic[newmax]--;
                    if (dic[newmax] == 0)
                    {
                        dic.Remove(newmax);
                        if (keysCount == 3) return dic.Keys.First();
                    }
                    if (!dic.ContainsKey(diff))
                    {
                        dic.Add(diff, 0);
                    }
                    dic[diff]++;
                }
            }
        }
        #endregion

        #region 1051
        public int HeightChecker(int[] heights)
        {
            int[] copy = new int[heights.Length];
            heights.CopyTo(copy, 0);

            Array.Sort(copy);
            int diff = 0;
            for (int i = 0; i < heights.Length; i++)
            {
                if (heights[i] != copy[i]) diff++;

            }
            return diff;
        }
        #endregion

        #region 1089 review
        public void DuplicateZeros(int[] arr)
        {
            int len = arr.Length;
            for (int i = 0; i < len - 1; i++)
            {
                if (arr[i] == 0)
                {
                    for (int j = len - 1; j >= i + 2; j--)
                    {
                        arr[j] = arr[j - 1];
                    }
                    arr[i + 1] = 0;

                    i = i + 1;
                }
            }
        }

        public void DuplicateZeros2(int[] arr)
        {
            int len = arr.Length;
            int endIndex = len - 1;
            var mark = false;
            for (int i = 0; i < len && i < endIndex; i++)
            {
                if (arr[i] == 0)
                {
                    endIndex--;
                    mark = i == endIndex;
                }
            }

            if (!mark)
            {
                arr[len - 1] = arr[endIndex];
                endIndex--;
                len--;
            }
            for (int i = len - 1; i >= 0; i--)
            {
                arr[i] = arr[endIndex];
                if (arr[endIndex] == 0 && i >= 1)
                {
                    i--;
                    arr[i] = 0;
                }
                endIndex--;
            }
        }
        #endregion

        #endregion

        #region 1101-1200

        #region 1122 review high quality code

        public int[] RelativeSortArray(int[] arr1, int[] arr2)
        {
            var list = new List<int>();
            var dic = arr1.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            for (int i = 0; i < arr2.Length; i++)
            {
                list.AddRange(Enumerable.Repeat(arr2[i], dic[arr2[i]]));
                dic.Remove(arr2[i]);
            }
            foreach (var d in dic.Keys.OrderBy(x => x))
            {
                list.AddRange(Enumerable.Repeat(d, dic[d]));
            }
            return list.ToArray();
        }

        public int[] RelativeSortArray2(int[] arr1, int[] arr2)
        {
            var dict = arr2.Select((x, index) => (x, index)).ToDictionary(a => a.x, b => b.index);
            Array.Sort(arr1, (x, y) => {
                if (dict.ContainsKey(x) && dict.ContainsKey(y))
                {
                    return dict[x] - dict[y];
                }
                if (dict.ContainsKey(x) && !dict.ContainsKey(y))
                {
                    return -1;
                }
                if (!dict.ContainsKey(x) && dict.ContainsKey(y))
                {
                    return 1;
                }
                return x - y;

            });
            return arr1;
        }
        #endregion

        #region 1184
        public int DistanceBetweenBusStops(int[] distance, int start, int destination)
        {
            int len = distance.Length;
            if (len == 1) return 0;
            var sum = distance.Sum();
            var temp = 0;
            if (start > destination)
            {
                var swap = start;
                start = destination;
                destination = swap;

            }
            for(int i = start; i < destination; i++)
            {
                temp += distance[i];
            }
            return Math.Min(temp, sum - temp);
        }

        public int DistanceBetweenBusStops2(int[] distance, int start, int destination)
        {
            int sum1 = 0, sum2 = 0, n = distance.Count();
            if (start > destination)
            {
                var swap = start;
                start = destination;
                destination = swap;

            }
            for (int i = 0; i < n; ++i)
            {
                if (i >= start && i < destination)
                {
                    sum1 += distance[i];
                }
                else
                {
                    sum2 += distance[i];
                }
            }
            return Math.Min(sum1, sum2);
        }
        #endregion

        #region #1189
        // balloon
        public int MaxNumberOfBalloons(string text)
        {
            var chars = new List<char> { 'b', 'a', 'l', 'o', 'n' };
            var dict = text.ToArray().GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            if (chars.Any(x => !dict.ContainsKey(x))) return 0;
            int min = Math.Min(dict['l'], dict['o']) / 2;
            while (min >= 0)
            {
                if (dict['b'] < min || dict['a'] < min || dict['n'] < min)
                {
                    min--;
                }
                else
                {
                    return min;
                }
            }
            return 0;
        }
        #endregion

        #region 1160 review high quality code
        public int CountCharacters(string[] words, string chars)
        {
            var dic = chars.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            int res = 0;
            int chartsLen = chars.Length;
            foreach (var w in words.Where(x => x.Length <= chartsLen))
            {
                var temp = w.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
                var canForm = true;
                foreach (var d in temp)
                {
                    if (!dic.ContainsKey(d.Key) || d.Value > dic[d.Key])
                    {
                        canForm = false;
                        break;
                    }
                }
                if (canForm)
                {
                    res += w.Length;
                }
            }
            return res;
        }

        public int CountCharacters2(string[] words, string chars)
        {
            IDictionary<char, int> ToCharCount(string s) => s.ToLookup(x => x).ToDictionary(x => x.Key, x => x.Count());
            var set = ToCharCount(chars);
            return words.Where(w => ToCharCount(w).All(x => set.ContainsKey(x.Key) && set[x.Key] >= x.Value)).Sum(w => w.Length);
        }
        #endregion

        #region 1200
        public IList<IList<int>> MinimumAbsDifference(int[] arr)
        {
            Array.Sort(arr);
            int min = int.MaxValue;
            var res = new List<IList<int>>();
            for(int i = 0; i < arr.Length - 1; i++)
            {
                var diff = arr[i + 1] - arr[i];
                if (diff <= min)
                {
                    if(diff < min)
                    {
                        res.Clear();
                        min = diff;
                    }
                    res.Add(new List<int> { arr[i], arr[i + 1] });
                }
            }
            return res;
        }
        #endregion
        #endregion

        #region 1201-1300
        #region 1207
        public bool UniqueOccurrences(int[] arr)
        {
            var dict = arr.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            return dict.Values.Distinct().Count() == dict.Values.Count();
        }

        public bool UniqueOccurrences1(int[] arr)
        {
            Array.Sort(arr);
            var hash = new HashSet<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                var lastIndex = Array.LastIndexOf(arr, arr[i]);
                if (!hash.Add(lastIndex - i + 1)) return false;
                i = lastIndex;
            }
            return true;
        }

        public bool UniqueOccurrences2(int[] arr)
        {
            var counts = arr.ToLookup(x => x).Select(x => x.Count()).ToList();
            return counts.Count() == counts.Distinct().Count();
        }
        #endregion

        #region 1232
        public bool CheckStraightLine(int[][] coordinates)
        {
            int diffx = coordinates[0][0] - coordinates[1][0];
            int diffy = coordinates[0][1] - coordinates[1][1];
            if (diffx == 0)
            {
                for (int i = 2; i < coordinates.Count(); i++)
                {
                    if (coordinates[i][0] != coordinates[0][0]) return false;
                }
            } else if (diffy == 0)
            {
                for (int i = 2; i < coordinates.Count(); i++)
                {
                    if (coordinates[i][1] != coordinates[0][1]) return false;
                }
            }
            else
            {
                var r = diffx / (diffy * 1.0);
                for (int i = 2; i < coordinates.Count(); i++)
                {
                    var d1 = coordinates[0][0] - coordinates[i][0];
                    var d2 = coordinates[0][1] - coordinates[i][1];
                    if ((d1 / (d2 * 1.0)) != r) return false;
                }
            }
            
            return true;
        }
        #endregion

        #region 1234
        public int BalancedString(string s)
        {
            var n = s.Length;
            var map = s.ToArray().GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            if (map.Keys.Count() == 1) return n - n / 4;
            map.Keys.ToList().ForEach(x =>
            {
                if (map[x] <= n / 4)
                    map.Remove(x);
                else
                    map[x] -= (n / 4);
            });

            if (map.Keys.Count() == 0) return 0;

            int res = n;
            for (int i = 0; i < n; i++)
            {
                if (!map.ContainsKey(s[i])) continue;
                var copyDic = map.ToDictionary(x => x.Key, x => x.Value);
                copyDic[s[i]]--;
                int j = i + 1;
                while (j < n && copyDic.Values.Any(x => x > 0) && j - i < res)
                {
                    if (copyDic.ContainsKey(s[j]))
                        copyDic[s[j]]--;
                    j++;

                }
                if (!copyDic.Values.Any(x => x > 0))
                {
                    res = Math.Min(res, j - i);
                }
            }
            return res;
        }

      
      
        #endregion

        #region 1266 review shortest time to visit 2 points
        public int MinTimeToVisitAllPoints(int[][] points)
        {
            int time = 0;
            int len = points.Count();
            if (len == 1) return 0;
            for(int i = 1; i < len; i++)
            {
                time += Math.Max(Math.Abs(points[i][0] - points[i - 1][0]), Math.Abs(points[i][1] - points[i - 1][1]));
            }
           
            return time;
        }
        #endregion

        #region 1287
        public int FindSpecialInteger(int[] arr)
        {
            int len = arr.Length;
            int quarterLen = len / 4;

            var dic = new Dictionary<int, int>();
            for (int i = 0; i < len; i++)
            {
                if (!dic.ContainsKey(arr[i]))
                {
                    dic.Add(arr[i], 0);
                }
                dic[arr[i]]++;
                if (dic[arr[i]] > quarterLen) return arr[i];
            }
           // return -1;
            var dict = arr.ToLookup(x => x).ToDictionary(x => x.Key, x => x.Count());
            return dict.First(x => x.Value > len / 4).Key;
        }
        #endregion

        #region 1295
        public int FindNumbers(int[] nums)
        {
            return nums.Where(x => x.ToString().Length % 2 == 0).Count();
        }
        #endregion

        #endregion

        #region 1301-1400
        #region 1331 review
        public int[] ArrayRankTransform(int[] arr)
        {
            int len = arr.Length;
            if (len == 0) return arr;
            var dict = new SortedDictionary<int, List<int>>();
            int[] res = new int[len];
            for(int i = 0; i < len; i++)
            {
                if (!dict.ContainsKey(arr[i]))
                {
                    dict.Add(arr[i], new List<int>());
                }
                dict[arr[i]].Add(i);
            }
            int count = 1;
            foreach(var k in dict.Keys)
            {
                for(int i = 0; i < dict[k].Count(); i++)
                {
                    res[dict[k][i]] = count;
                }
                count++;
            }
            return res;
        }

        public int[] ArrayRankTransform2(int[] arr)
        {
            var sortedArray = arr.Distinct().ToArray();
            Array.Sort(sortedArray);
            int rank = 1;
            var dic = sortedArray.ToDictionary(x => x, x=> rank++);
            int[] res = new int[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                res[i] = dic[arr[i]];
            }
            return res;
        }
        #endregion

        #region 1337
        public int[] KWeakestRows(int[][] mat, int k) 
        {
            int row = mat.Length;
            var dic = new SortedDictionary<int, List<int>>();
            for (int i = 0; i < row; i++)
            {
                int count = mat[i].Where(x => x == 1).Count();
                if (!dic.ContainsKey(count))
                {
                    dic.Add(count, new List<int>());
                }
                dic[count].Add(i);
            }
            
            int[] res = new int[k];
            int pos = 0;
            foreach (var d in dic)
            {
                foreach (var index in d.Value)
                {
                    res[pos] = index;
                    k--;
                    pos++;
                    if (k == 0) return res;
                }
            }
            return res;
        }
        #endregion

        #region 1346
        public bool CheckIfExist(int[] arr)
        {
            if (arr.Contains(0) && Array.IndexOf(arr,0) != Array.LastIndexOf(arr, 0)) return true;
            int[] newArr = arr.Distinct().ToArray();
            return newArr.Any(x => x!=0 && newArr.Contains(2 * x));
        }
        #endregion

        #region 1351
        public int CountNegatives(int[][] grid)
        {
            int len = grid.Length;
            int cul = grid[0].Length;
            int res = 0;
            for(int i = 0; i < len; i++)
            {
                for(int j = 0; j < cul; j++)
                {
                    if (grid[i][j] == 0)
                    {
                        res += cul - j-1;
                        break;
                    } else if(grid[i][j] < 0)
                    {
                        res += cul - j;
                        break;
                    }
                }
            }
            return res;
        }
        #endregion

        #region 1365 review
        public int[] SmallerNumbersThanCurrent(int[] nums)
        {
            var dict = nums.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            int len = nums.Length;
            int[] res = new int[len];
            var dic2 = new Dictionary<int, int>();
            for (int i = 0; i < len; i++)
            {
                if (!dic2.ContainsKey(nums[i]))
                {
                    dic2.Add(nums[i], dict.Where(x=>x.Key < nums[i]).Select(x=>x.Value).Sum());
                }
                res[i] = dic2[nums[i]];
            }
            return res;
        }

        public int[] SmallerNumbersThanCurrent2(int[] nums)
        {
            int[] arr = nums.OrderBy(n => n).ToArray();
            var dict = new Dictionary<int, int>();
            int count = 0;
            foreach (var num in arr)
            {
                if (!dict.ContainsKey(num))
                    dict.Add(num, count);
                count++;
            }

            int[] result = new int[nums.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = dict[nums[i]];

            return result;
        }
        #endregion

        #region 1380
        public IList<int> LuckyNumbers(int[][] matrix)
        {
            int row = matrix.Length;
            int col = matrix[0].Length;
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < col; i++)
            {
                int colmax = int.MinValue;

                for (int j = 0; j < row; j++)
                {
                    colmax = Math.Max(colmax, matrix[j][i]);
                }
                dict.Add(i, colmax);
            }
            var res = new HashSet<int>();

            for (int i = 0; i < row; i++)
            {
                int rowmin = matrix[i].Min();
                for (int j = 0; j < col; j++)
                {
                    if (matrix[i][j] == rowmin && dict[j] == rowmin) res.Add(rowmin);
                }
            }

            return res.ToList();
        }
        #endregion

        #region 1394
        public int FindLucky(int[] arr)
        {
            Array.Sort(arr);
            var count = 1;
            for (int i = arr.Length - 1; i >= 1; i--)
            {
                if (arr[i] == arr[i - 1])
                {
                    count++;
                }
                else
                {
                    if (arr[i] == count) return count;
                    count = 1;
                }
            }
            if (arr[0] == count) return count;
            return -1;
        }

        public int FindLucky2(int[] arr)
        {
            var dic = arr.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var key= dic.Where(x => x.Key == x.Value).Select(x => x.Key).OrderBy(x => x).ToList();
            key.Sort();
            return key.Count() > 0 ? key.Last() : -1;
        }
        #endregion

        #endregion

        #region 1401-1500
        #region 1431 
        public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            int max = candies.Max();
            return candies.Select(x => x + extraCandies >= max).ToList();
        }
        #endregion

        #region 1437
        public bool KLengthApart(int[] nums, int k)
        {
            var index = Array.IndexOf(nums, 1);
            for (int i = index + 1; i < nums.Length; i++)
            {
                if (nums[i] == 1)
                {
                    if (i - index - 1 < k) return false;
                    else index = i;
                }
            }
            return true;
        }
        #endregion

        #region 1450
        public int BusyStudent(int[] startTime, int[] endTime, int queryTime)
        {
            int counter = 0;
            for(int i = 0; i < startTime.Length; i++)
            {
                if(startTime[i]<= queryTime && queryTime <= endTime[i])
                {
                    counter++;
                }
            }
            return counter;
        }
        #endregion

        #region 1460
        public bool CanBeEqual(int[] target, int[] arr)
        {
            int len = target.Length;
            var dic = target.ToLookup(x => x).ToDictionary(x => x.Key, x => x.Count());
            if (arr.Distinct().Any(x => !dic.Keys.Contains(x))) return false;
            for (int i = 0; i < len; i++)
            {
                if (!dic.ContainsKey(arr[i])) return false;
                dic[arr[i]]--;
                if (dic[arr[i]] < 0) return false;
            }
            return true;
        }
        #endregion

        #region 1464
        public int MaxProduct1464(int[] nums)
        {
            Array.Sort(nums);
            int len = nums.Length;
            return (nums[len - 1] - 1) * (nums[len - 2] - 1);
        }
        #endregion

        #region 1470
        public int[] Shuffle(int[] nums, int n)
        {
            int[] res = new int[n * 2];
            int index = 0;
            for(int i = 0; i < n; i++)
            {
                res[index] = nums[i];
                res[index + 1] = nums[i + n];
                index += 2;
            }
           
            return res;
        }
        #endregion

        #region 1475
        public int[] FinalPrices(int[] prices)
        {
            for(int i = 0; i < prices.Length; i++)
            {
                for(int j = i + 1; j < prices.Length; j++)
                {
                    if (prices[j] <= prices[i])
                    {
                        prices[i] -= prices[j];
                        break;
                    }
                }
            }
            return prices;
        }
        #endregion

        #region 1480 review
        public int[] RunningSum(int[] nums)
        {
            int[] res = new int[nums.Length];
            int count = 0;
            for(int i = 0; i < nums.Length; i++)
            {
                count += nums[i];
                res[i] = count;
            }
            return res;
        }

        public int[] RunningSum2(int[] nums)
        {
            for (int i = 1; i < nums.Length; ++i)
            {
                nums[i] += nums[i - 1];
            }
            return nums;
        }
        #endregion

        #region 1491
        public double Average(int[] salary)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            var sum = 0;
            for(int i = 0; i < salary.Length; i++)
            {
                min = Math.Min(min, salary[i]);
                max = Math.Max(max, salary[i]);
                sum += salary[i];
            }
            return (sum - min - max) / ((salary.Length - 2) * 1.0);
        }

        public double Average2(int[] salary)
        {
            int min = salary.Min();
            int max = salary.Max();
            var sum = salary.Sum();
           
            return (sum - min - max) / ((salary.Length - 2) * 1.0);
        }

        public double Average3(int[] salary)
        {
            Array.Sort(salary);

            int len = salary.Length;

            return (double)(salary.Sum() - salary[0]-salary[len-1]) / (double)(len - 2);
        }
        #endregion

        #region 1498 remember

        public int NumSubseq(int[] nums, int target)
        {
            double res = 0;
            Array.Sort(nums);
            int len = nums.Length;

            long mod = (long)Math.Pow(10, 9) + 7;
            long[] cnt = new long[nums.Length];
            cnt[0] = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                cnt[i] = cnt[i - 1] * 2 % mod;
            }

            int left = 0;
            int right = len - 1;
            while (left <= right)
            {
                if (nums[left] + nums[right] > target)
                {
                    right--;
                }
                else
                {
                    res += cnt[right - left];
                    left++;
                }
            }

            return (int)(res % mod);
        }
        #endregion

        #region 1501-1600
        #region 1502
        public bool CanMakeArithmeticProgression(int[] arr)
        {
            Array.Sort(arr);
            var diff = arr[1] - arr[0];
            for(int i = 2; i < arr.Length; i++)
            {
                if (arr[i] - arr[i - 1] != diff) return false;
            }
            return true;
        }
        #endregion

        #region 1512
        public int NumIdenticalPairs(int[] nums)
        {
            int res = 0;
            var dict = nums.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var sumDic = new Dictionary<int, int>();
            foreach(var d in dict)
            {
                if (d.Value >= 2)
                {
                    if (!sumDic.ContainsKey(d.Value))
                    {
                        var sum = 0;
                        for (int j = 1; j < d.Value; j++)
                        {
                            sum += j;
                        }
                        sumDic.Add(d.Value, sum);
                    }
                    res += sumDic[d.Value];
                }
            }
            return res;
        }

        public int NumIdenticalPairs2(int[] nums)
        {
            int res = 0;
            Array.Sort(nums);
            var sumDic = new Dictionary<int, int>();
            int index = 0;
            while (index < nums.Length)
            {
                var val = nums[index];
                var lastIndex = Array.LastIndexOf(nums, val);
                var diff = lastIndex - index;
                if (diff>0)
                {
                    if (!sumDic.ContainsKey(diff))
                    {
                        var sum = 0;
                        for (int j = 1; j <= lastIndex - index; j++)
                        {
                            sum += j;
                        }
                        sumDic.Add(diff, sum);
                    }
                    res += sumDic[diff];
                }
                index = lastIndex + 1;
            }
            
            return res;
        }
        #endregion

        #region 1528
        public string RestoreString(string s, int[] indices)
        {
            char[] copys = new char[s.Length];
            for(int i = 0; i < s.Length; i++)
            {
                copys[indices[i]] = s[i];
            }
            return String.Concat(copys);
        }
        #endregion

        #region 1539 review
        public int FindKthPositive(int[] arr, int k)
        {
            int diff = 0;
            int len = arr.Length;
            for (int i = 0; i < len; i++)
            {
                if (i + diff != (arr[i] - 1))
                {
                    for (int j = arr[i] - 1 - i - diff; j >= 1; j--)
                    {
                        k--;
                        if (k == 0) return arr[i] - j;
                    }
                    diff = arr[i] - i - 1;
                }
            }
            int missingNum = arr[len - 1];
            while (k > 0)
            {
                k--;
                missingNum++;
            }
            return missingNum;
        }

        public int FindKthPositive2(int[] arr, int k)
        {

            int num = 1;
            int i = 0;
            int count = 0;
            while (i < arr.Length)
            {
                if (num == arr[i])
                    i++;
                else
                    count++;

                if (count == k)
                    return num;

                num++;
            }

            return num + k - count - 1;

        }
        #endregion

        #region 1550
        public bool ThreeConsecutiveOdds(int[] arr)
        {
            int len = arr.Length;
            for(int i = 0; i < len-2; i++)
            {
                if (arr[i] % 2 == 1 )
                {
                    if (arr[i + 1] % 2 == 1)
                    {
                        if (arr[i + 2] % 2 == 1)
                        {
                            return true;
                        }
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 1566 review
        public bool ContainsPattern(int[] arr, int m, int k)
        {
            int len = arr.Length;
            for (int i = 0; i <= len - m * k; i++)
            {
                int j = i;
                while (j <= m * k + i - m - 1)
                {
                    if (arr[j] != arr[j + m]) break;
                    j++;
                }
                if (j == m * k + i - m) return true;
            }
            return false;
        }
        #endregion

        #region 1572
        public int DiagonalSum(int[][] mat)
        {
            int len = mat.Length;
            bool reduce = len % 2 == 1;
            int res = 0;
            for(int i = 0; i < len; i++)
            {
                res += mat[i][len - i-1];
                res += mat[i][i];
            }
            if(reduce)
            {
                res -= mat[len / 2][len / 2];
            }
            return res;
        }
        #endregion

        #region 1582
        public int NumSpecial(int[][] mat)
        {
            var dic = new Dictionary<int, bool>();
            int res = 0;
            for (int i = 0; i < mat.Length; i++)
            {
                if (mat[i].Sum() == 1)
                {
                    var col = Array.IndexOf(mat[i], 1);
                    if (!dic.ContainsKey(col))
                    {
                        var sum = 0;
                        for (int j = 0; j < mat.Length; j++)
                        {
                            sum += mat[j][col];
                            if (sum > 1) break;
                        }
                        dic.Add(col, sum == 1);
                    }
                    if (dic[col]) res++;
                }
            }
            return res;
        }
        #endregion

        #region 1588
        public int SumOddLengthSubarrays(int[] arr)
        {
            var res = arr.Sum();
            var len = 3;
            while (len <= arr.Length)
            {
                for (int i = 0; i <= arr.Length - len; i++)
                {
                    int tempindex = i;
                    while (tempindex < i + len)
                    {
                        res += arr[tempindex];
                        tempindex++;
                    }
                }
                len += 2;
            }
            return res;
        }
        #endregion

        #region 1598
        public int MinOperations(string[] logs)
        {
            int depth = 0;
            for(int i = 0; i < logs.Length; i++)
            {
                switch (logs[i])
                {
                    case "../":
                        {
                            depth--;
                            if (depth < 0) depth = 0;
                            break;
                        }
                    case "./": break;
                    default: depth++;  break;
                }
            }
            return depth;
        }
        #endregion
        #endregion

        #region 1601-1700
        #region 1608
        public int SpecialArray(int[] nums)
        {
            var dict = nums.ToLookup(x => x).ToDictionary(x => x.Key, x => x.Count());
            for (int i = 1; i <= dict.Keys.Max(); i++)
            {
                var greaters = dict.Keys.Where(x => x >= i);
                var counter = greaters.Select(x => dict[x]).Sum();
                if (counter == i) return i;
            }
            return -1;
        }
        #endregion

        #region 1619
        public double TrimMean(int[] arr)
        {
            var len = arr.Length;
            Array.Sort(arr);

            var min = len * 0.05;
            var max = len * 0.95 - 1;

            var sum = 0;
            for (int i = (int)min; i <= max; i++)
            {
                sum += arr[i];
            }
            return sum / (max - min + 1);
        }
        #endregion

        #region 1629
        public char SlowestKey(int[] releaseTimes, string keysPressed)
        {
            int max = releaseTimes[0];
            int index = 0;
            for (int i = 1; i < releaseTimes.Count(); i++)
            {
                var diff = releaseTimes[i] - releaseTimes[i - 1];
                if (max <diff)
                {
                    max = diff;
                    index = i;
                }else if(max==diff && keysPressed[i] > keysPressed[index])
                {
                    index = i;
                }
            }
            return keysPressed[index];
        }
        #endregion

        #region 1636
        public int[] FrequencySort(int[] nums)
        {
            var dict = nums.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var newDic = new SortedDictionary<int, List<int>>();
            foreach(var d in dict)
            {
                if (!newDic.ContainsKey(d.Value))
                {
                    newDic.Add(d.Value, new List<int>());
                }
                newDic[d.Value].Add(d.Key);
            }

            var list = new List<int>();

            foreach(var d in newDic)
            {
                d.Value.Sort();
                d.Value.Reverse();
                foreach (var v in d.Value)
                {
                    list.AddRange(Enumerable.Repeat(v, d.Key));
                }
            }
            return list.ToArray();
        }
        #endregion

        #region 1640
        public bool CanFormArray(int[] arr, int[][] pieces)
        {
            var dic = new Dictionary<int, int[]>();
            for (int i = 0; i < pieces.Length; i++)
            {
                dic.Add(pieces[i][0], pieces[i]);
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (!dic.ContainsKey(arr[i])) return false;
                var items = dic[arr[i]];
                int index = 0;
                while (index < items.Length)
                {
                    if (arr[index + i] != items[index]) return false;
                    index++;
                }
                i += items.Length - 1;
            }
            return true;
        }
        #endregion
        #endregion

        #region 1901-2000
        #region #1929
        public int[] GetConcatenation(int[] nums)
        {
            var list = new List<int>();
            list.AddRange(nums);
            list.AddRange(nums);
            return list.ToArray();
        }
        #endregion

        #region #1930
        public int CountPalindromicSubsequence(string s)
        {
            var dic = new Dictionary<char, List<int>>();
            var array = s.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (dic.ContainsKey(array[i]))
                {
                    dic[array[i]].Add(i);
                }
                else
                {
                    dic.Add(array[i], new List<int> { i });
                }
            }

            var length = dic.Keys.Count() - 1;
            int res = 0;
            foreach (var d in dic.Where(x => x.Value.Count > 1))
            {
                var maxIndex = d.Value.Max();
                var minIndex = d.Value.Min();
                foreach (var other in dic.Where(x => x.Key != d.Key && x.Value.Any(v => v > minIndex && v < maxIndex)))
                {
                    res++;
                }
                if (d.Value.Count > 2) res++;
            }
            return res;
        }
        #endregion

        #region #1935
        public int CanBeTypedWords(string text, string brokenLetters)
        {
            var words = text.Split(" ");
            if (brokenLetters.Length == 0) return words.Length;
            var array = brokenLetters.ToArray();
            int num = 0;
            foreach(string s in words)
            {
                var flag = true;
                foreach(var c in s.ToArray())
                {
                    if (array.Contains(c))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) num++;
            }
            return num;
        }
        #endregion

        #region #1936
        public int AddRungs(int[] rungs, int dist)
        {
            int length = rungs.Length;
            if (rungs[length - 1] <= dist) return 0;
            int ans = 0;
            int step = 0;
            int index = 0;
            while (index < length && step < rungs[length - 1])
            {
                if (rungs[index] - step > dist)
                {
                    var steps = (rungs[index] - step) / dist;
                    if ((rungs[index] - step) % dist == 0)
                    {
                        steps = steps / 2;
                    }
                    ans += steps;
                    step += steps * dist;
                }
                else
                {
                    step = rungs[index];
                    index++;
                }
            }
            return ans;
        }
        #endregion

        #region 1941
        public bool AreOccurrencesEqual(string s)
        {
            var dic = s.ToArray().GroupBy(x => x).Select(x=>x.Count()).Distinct();
            return dic.Count() == 1;
        }
        #endregion

        #region 1945
        public int GetLucky(string s, int k)
        {
            var init = "";
            for(int i = 0; i < s.Length; i++)
            {
                init += (char.ToUpper(s[i]) - 64).ToString();
            }
            for(int i = 1; i <= k; i++)
            {
                init = GetLucky(init);
            }
            return int.Parse(init);

        }
        private string GetLucky(string init)
        {
            var res = 0;
            var array = init.ToArray();
            foreach(var a in array)
            {
                res += int.Parse(a.ToString());
            }
            return res.ToString();
        }
        #endregion

        #region #1995
        public int CountQuadruplets(int[] nums)
        {
            var dic = new Dictionary<int, List<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    dic[nums[i]].Add(i);
                }
                else
                {
                    dic.Add(nums[i], new List<int> { i });
                }
            }

            int length = nums.Length;
            int res = 0;
            for(int i = 0; i <= length - 3; i++)
            {
                for(int j=i+1; j<= length - 2; j++)
                {
                    for (int k=j+1;k< length; k++)
                    {
                        var val = nums[i] + nums[j] + nums[k];
                        if (dic.ContainsKey(val))
                        {
                            res += dic[val].Where(x => x > k).Count();
                        }
                    }
                }
            }
            return res;
        }
        #endregion
        #endregion
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    class SequenceComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T> seq1, IEnumerable<T> seq2)
        {
            return seq1.SequenceEqual(seq2);
        }

        public int GetHashCode(IEnumerable<T> seq)
        {
            int hash = 1234567;
            foreach (T elem in seq)
                hash = hash * 37 + elem.GetHashCode();
            return hash;
        }
    }
}
