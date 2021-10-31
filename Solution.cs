using System;
using System.Collections.Generic;
using System.Linq;

namespace CodePractice
{
    public class Solution
    {
        #region sliding window
        #region #27
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

        #region #35 - review
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

        #region $53 -review
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

        #region #69
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

        #region #283
        public void MoveZeroes(int[] nums)
        {
            int length = nums.Length;
            if (length == 1) return;
            int lowIndex = 0;
            int highIndex = length - 1;
            while(lowIndex < highIndex)
            {
                if(nums[lowIndex] == 0)
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
            while(lowIndex < highIndex)
            {
                nums[lowIndex] = nums[lowIndex + 1];
                lowIndex++;
            }
            nums[highIndex] = 0;
        }
        #endregion
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

        #region Tree
        #region #94  review + remember
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

        #region #100
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p is null && q is null) return true;
            if (p is null || q is null) return false;

            return p.val == q.val && IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }
        #endregion

        #region #101 review 
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

        #region #104 - review
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
        #endregion

        #region ListNode
        #region #83 - review
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

        #region #141
        public bool HasCycle(ListNode head)
        {
            if (head == null) return false;
            if (head.val == int.MinValue) return true;

            head.val = int.MinValue;
            return HasCycle(head.next);
        }
        #endregion

        #region #206
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

        #region #226
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

        #region #234
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

        #region dictionary,grouping
        #region #136
        public int SingleNumber(int[] nums)
        {
            foreach (var item in nums.GroupBy(x => x))
            {
                if (item.Count() == 1) return item.Key;
            }
            return -1;
        }
        #endregion

        #region #169
        public int MajorityElement(int[] nums)
        {
            var group = nums.GroupBy(x => x);
            int majorNum = nums.Length / 2;
            return group.Where(x => x.Count() > majorNum).First().Key;
        }
        #endregion

        #region #448
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
}
