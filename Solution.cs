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
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head.next == null) return null;
            var dummyHead = new ListNode(head.val, null);
            var temp = head.next;
            while (temp != null)
            {
                var newNode = new ListNode(temp.val, dummyHead);
                dummyHead = newNode;
                temp = temp.next;
            }
            int index = 1;
            if (n == 1)
            {
                dummyHead = dummyHead.next;
            }
            var dummyEnd = new ListNode(dummyHead.val, null);
            var temp1 = dummyHead.next;
            index++;
            while (temp1 != null)
            {
                if (index == n)
                {
                    temp1 = temp1.next;
                    if (temp1 == null) break;
                }

                var newNode = new ListNode(temp1.val, dummyEnd);
                dummyEnd = newNode;
                temp1 = temp1.next;
                index++;
            }
            return dummyEnd;
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

        #region #33
        public int Search(int[] nums, int target)
        {
            int N = nums.Length;
            if (nums[0] == target) return 0;
            if (N == 1)
            {
                return -1;
            }
            bool rotated = false;
            for (int i = 1; i < N; i++)
            {
                if (nums[i] == target) return i;
                if (nums[i] < nums[i - 1])
                {
                    rotated = true;
                }

                if (rotated && nums[i] > target) return -1;
            }
            return -1;
        }
        #endregion

        #region #34
        public int[] SearchRange(int[] nums, int target)
        {
            int N = nums.Length;

            if (N == 0) return new int[] { -1, -1 };
            if (N == 1)
            {
                if (nums[0] == target) return new int[] { 0, 0 };
                return new int[] { -1, -1 };
            }

            return Search2(nums, 0, N - 1, target);
        }
        private int[] Search2(int[] nums, int startIndex, int endIndex, int target)
        {
            if (endIndex == startIndex && nums[startIndex] != target) return new int[] { -1, -1 };

            var midIndex = startIndex + (endIndex - startIndex) / 2;
            var midV = nums[midIndex];
            var lowIndex = startIndex;
            var highIndex = endIndex;
            if (midV == target)
            {
                lowIndex = midIndex;
                highIndex = midIndex;
                for (int i = midIndex - 1; i >= 0; i--)
                {
                    if (nums[i] == target)
                    {
                        lowIndex = i;
                    }
                    else
                    {
                        continue;
                    }
                }
                for (int i = midIndex + 1; i < nums.Length; i++)
                {
                    if (nums[i] == target)
                    {
                        highIndex = i;
                    }
                    else
                    {
                        return new int[] { lowIndex, highIndex };
                    }
                }
                return new int[] { lowIndex, highIndex };
            }
            else
            {
                if (midV < target)
                {
                    if (midIndex + 1 <= endIndex)
                    {
                        return Search2(nums, midIndex + 1, endIndex, target);
                    }
                }
                else
                {
                    if (midIndex - 1 >= 0)
                    {
                        return Search2(nums, startIndex, midIndex - 1, target);
                    }
                }
            }

            return new int[] { -1, -1 };
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
            for (int i = 0; i < 9; i++)
            {
                HashSet<int> numbersr = new HashSet<int>();
                int numsr = 0;
                HashSet<int> numbersc = new HashSet<int>();
                int numsc = 0;

                for (int j = 0; j < 9; j++)
                {

                    var grids = i % 3 == 0 && j % 3 == 0;
                    if (board[i][j] != '.')
                    {
                        numsr++;
                        numbersr.Add(board[i][j]);
                        if (numsr != numbersr.Count())
                        {
                            return false;
                        }
                    }
                    if (board[j][i] != '.')
                    {
                        numsc++;
                        numbersc.Add(board[j][i]);
                        if (numsc != numbersc.Count())
                        {
                            return false;
                        }
                    }
                    if (grids)
                    {
                        HashSet<int> numbers9 = new HashSet<int>();
                        int numsgrid = 0;
                        for (int k = i; k < i + 3; k++)
                        {
                            for (int l = j; l < j + 3; l++)
                            {
                                if (board[k][l] != '.')
                                {
                                    numbers9.Add(board[k][l]);
                                    numsgrid++;
                                    if (numbers9.Count() != numsgrid)
                                    {
                                        return false;
                                    }
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

        #region #56
        public int[][] Merge(int[][] intervals)
        {
            int[][] jaggedArray = new int[10000][];
            for (int i = 0; i < 10000; i++)
            {
                jaggedArray[i] = new int[] { i, -1 };
            }

            List<int> index = new List<int>();
            for (int i = 0; i < intervals.Length; i++)
            {
                int loc = intervals[i][0];
                jaggedArray[loc][1] = Math.Max(jaggedArray[loc][1], intervals[i][1]);
                if (!index.Contains(loc))
                {
                    index.Add(loc);
                }
            }
            index.Sort();
            var list = new List<int[]>();
            int min = -1;
            int max = -1;

            for (int loc = 0; loc < index.Count(); loc++)
            {
                var i = index[loc];
                if (jaggedArray[i][0] > max)
                {
                    var maxIsmin = max == -1;

                    if (!maxIsmin)
                    {
                        list.Add(new int[] { min, max });
                    }

                    min = jaggedArray[i][0];
                    max = jaggedArray[i][1];
                }
                else
                {
                    max = Math.Max(jaggedArray[i][1], max);
                }
            }

            list.Add(new int[] { min, max });

            int[][] res = new int[list.Count()][];
            for (int i = 0; i < list.Count(); i++)
            {
                res[i] = new int[] { list[i][0], list[i][1] };
            }

            return res;
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
        #endregion

        #region 301-400
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

            if(nums[mid] < target)
            {
                if (mid + 1 > end) return -1;
                return Search(nums, target, mid + 1, end);
            }
            else
            {
                if (mid - 1 < 0) return -1;
                return Search(nums, target, 0, mid -1);
            }
        }
        #endregion

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
            for (int i=0;i<array.Length;i++)
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

            var length = dic.Keys.Count()-1;
            int res = 0;
            foreach(var d in dic.Where(x => x.Value.Count > 1))
            {
                var maxIndex = d.Value.Max();
                var minIndex = d.Value.Min();
                foreach(var other in dic.Where(x=>x.Key != d.Key && x.Value.Any(v => v>minIndex && v<maxIndex )))
                {
                    res++;
                }
                if (d.Value.Count >2) res++;
            }
            return res;
        }
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
