﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CodePractice
{
    public class Solution
    {
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
            while(highIndex < length)
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
                        if (!hash.Contains(list)){
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

            for(int i=0;i<=length- needleLength; i++)
            {
                var str = haystack.Substring(i, needleLength);
                if (str == needle) return i;
            }
            return -1;
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

            for(int i=2;i<n;i++)
            {
                // l[i] = l[i-1] +1
                times[i] = times[i-1];
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
            if (m==0)
            {
                for(int i = 0; i < n; i++)
                {
                    nums1[i] = nums2[i];
                }
            } else if (n == 0)
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
                while (index1 >=0)
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
            var l= Depth(root, new List<int>(), 0);
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

        #region #118 
        public IList<IList<int>> Generate(int numRows)
        {
            var l = new List<IList<int>>();
            l.Add(new List<int> { 1 });
            var previous = l[0];
            for(int i=1; i<numRows; i++)
            {
                var temp = new List<int>();
                temp.Add(1);
                for(int j = 1; j < i; j++)
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

            for(int i=1; i<length;i++)
            {
                if(prices[i] < holdPrice && !hold)
                {
                    holdPrice = prices[i];
                    salePrice = holdPrice;
                }
                else if(prices[i] > salePrice)
                {
                    hold = true;
                    salePrice = prices[i];
                    if (i == length -1)
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
            foreach(int n in nums)
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
                    dic[c] ++;
                }
                else
                {
                    dic.Add(c, 1);
                }
            }
            foreach (char c in t)
            {
                if (!dic.ContainsKey(c) || dic[c] <=0 ) return false;

                dic[c]--;
            }
            return true;
        }
        #endregion

        #region #268
        public int MissingNumber(int[] nums)
        {
            var sum = 0;
            for(int i = 0; i <= nums.Length; i++)
            {
                sum += i;
            }
            return sum-nums.Sum();
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

        #region #326
        public bool IsPowerOfThree(int n)
        {
            if (n == 1) return true;
            if (n < 3) return false;
            while(n%3==0)
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

        #region #412
        public IList<string> FizzBuzz(int n)
        {
            var list = new List<string>();
            for(int i=1; i <= n; i++)
            {
                if (i%15 == 0)
                {
                    //answer[i] == "FizzBuzz" if i is divisible by 3 and 5.
                    list.Add("FizzBuzz");
                } else if (i % 3 == 0)
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
        public int Search(int[] nums, int target)
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
