namespace CodePractice
{
    // 303
    public class NumArray
    {
        int[] array;

        public NumArray(int[] nums)
        {
            this.array = nums;
        }

        public int SumRange(int left, int right)
        {
            int sum = 0;
            for(int i = left; i <= right; i++)
            {
                sum += array[i];
            }
            return sum;
        }
    }
}
