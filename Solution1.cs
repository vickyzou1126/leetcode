using System.Collections.Generic;

namespace CodePractice
{
    class Solution1
    {
        int[] nums;
        Dictionary<int, List<int>> indexDic = new Dictionary<int, List<int>>();
        Dictionary<int, List<int>> posDic = new Dictionary<int, List<int>>();
        public Solution1(int[] nums)
        {
            this.nums = nums;
            for(int i=0;i<nums.Length;i++)
            {
                if (!indexDic.ContainsKey(nums[i]))
                {
                    indexDic.Add(nums[i], new List<int>());
                    posDic.Add(nums[i], new List<int>());
                }
                indexDic[nums[i]].Add(i);
                posDic[nums[i]].Add(i);
            }
        }

        public int Pick(int target)
        {
            return 0;
        }
    }
}
