using System;

namespace CodePractice
{
    public class Solution
    {
        // #27
        #region
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

        // #28
        #region
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

        // #35 - review (sliding window)
        #region
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
    }
}
