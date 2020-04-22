using System;

namespace Graphs
{
    public class MinHeap
    {
        private int[] content;
        private int size;

        public MinHeap(int maxSize)
        {
            content = new int[maxSize];
        }

        private int GetLeftChildIndex(int currentIndex) => 2 * currentIndex + 1;
        private int GetRightChildIndex(int currentIndex) => 2 * currentIndex + 2;
        private int GetParentIndex(int currentIndex) => (currentIndex - 1) / 2;

        private bool HasLeftChild(int currentIndex) => GetLeftChildIndex(currentIndex) < size;
        private bool HasRightChild(int currentIndex) => GetRightChildIndex(currentIndex) < size;
        private bool IsRoot(int currentIndex) => currentIndex == 0;

        private int GetLeftChild(int currentIndex) => content[GetLeftChildIndex(currentIndex)];
        private int GetRightChild(int currentIndex) => content[GetRightChildIndex(currentIndex)];
        private int GetParent(int currentIndex) => content[GetParentIndex(currentIndex)];

        private void Swap(int first, int second)
        {
            int fn = content[first];
            content[first] = content[second];
            content[second] = fn;
        }

        private void RecalculateDown()
        {
            int currentIndex = 0;
            while (HasLeftChild(currentIndex))
            {
                int smallerChildIndex = GetLeftChildIndex(currentIndex);
                if (HasRightChild(currentIndex) && GetRightChild(currentIndex) < GetLeftChild(currentIndex)) smallerChildIndex = GetRightChildIndex(currentIndex);
                if (content[smallerChildIndex] >= content[currentIndex]) break;
                Swap(smallerChildIndex, currentIndex);
                currentIndex = smallerChildIndex;
            }
        }

        private void RecalculateUp()
        {
            int currentIndex = size - 1;
            while (!(currentIndex == 0) && content[currentIndex] < GetParent(currentIndex))
            {
                int parent = GetParentIndex(currentIndex);
                Swap(currentIndex, parent);
                currentIndex = parent;
            }
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public int Peek()
        {
            if (size == 0) throw new IndexOutOfRangeException();
            else return content[0];
        }

        public int Pop()
        {
            if (size == 0) throw new IndexOutOfRangeException();
            else
            {
                int result = content[0];
                content[0] = content[size - 1];
                size--;
                RecalculateDown();
                return result;
            }
        }

        public void Add(int newElement)
        {
            if (size == content.Length) throw new IndexOutOfRangeException();
            else
            {
                content[size] = newElement;
                size++;
                RecalculateUp();
            }
        }
    }
}
