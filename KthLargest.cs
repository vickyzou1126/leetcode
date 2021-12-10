using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePractice
{
    // #703
    public class KthLargest
    {
        int[] nums;
        int k = 0;
        
        public KthLargest(int k, int[] nums)
        {
            this.nums = new int[k];
            Array.Sort(nums);
            var len = nums.Count();
            int index = 0;
            for (int i = Math.Max(len - k, 0); i < len; i++)
            {
                this.nums[index] = nums[i];
                index++;
            }
            for (int i = index; i < k; i++)
            {
                this.nums[i] = int.MaxValue;
            }
            this.k = k;
        }

        public int Add(int val)
        {
            if (this.nums[k - 1] == int.MaxValue)
            {
                this.nums[k - 1] = val;
                Array.Sort(nums);
            }
            else if (val > nums[0])
            {
                nums[0] = val;
                Array.Sort(nums);
            }
            return nums[0];
        }
    }
}
