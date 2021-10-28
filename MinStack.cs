namespace CodePractice
{
    #region #155 review
    public class MinStack
    {

        private Node top;
        private int size = 0;

        /** initialize your data structure here. */
        public MinStack()
        {

        }

        public void Push(int x)
        {
            if (size == 0)
            {
                top = new Node(x);
                top.MinVal = x;
                size++;
                return;
            }
            var node = new Node(x);
            node.Next = top;
            top = node;
            top.MinVal = (top.Next.MinVal > x) ? x : top.Next.MinVal;
            size++;
        }

        public void Pop()
        {
            if (size == 0) return;

            top = top.Next;
            size--;
        }

        public int Top()
        {
            return (size > 0) ? top.Val : 0;
        }

        public int GetMin()
        {
            return (size > 0) ? top.MinVal : 0;
        }
    }

    public class Node
    {
        public Node Next { set; get; }
        public int Val { set; get; }
        public int MinVal { set; get; }

        public Node(int val)
        {
            Val = val;
        }
    }
    #endregion
}
