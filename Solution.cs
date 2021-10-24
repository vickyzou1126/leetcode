﻿using System;
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
    }
}
