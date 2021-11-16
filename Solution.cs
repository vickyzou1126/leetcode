using System;
using System.Collections.Generic;
using System.Linq;

namespace CodePractice
{
    public class Solution
    {
        #region 1-100
        #region #2 ListNode review
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

            var list = nums.ToList();
            list.Sort();
            int minDiff = int.MaxValue;
            var ans = int.MaxValue;

            for (int i = 0; i < length - 2; i++)
            {
                int left = i + 1;
                int right = length - 1;
                while (left < right)
                {
                    var sum = list[i] + list[left] + list[right];
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

        #region #40
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

        #region #57
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

        #region #155 - see MinStack
        #endregion

        #region #160 ListNode - not completed
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
            if (pattern == s) return false;
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
        #endregion

        #region 501-600

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

        #endregion

        #region 601-700
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
