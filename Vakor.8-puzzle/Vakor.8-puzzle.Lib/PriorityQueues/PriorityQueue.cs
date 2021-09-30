namespace Vakor._8_puzzle.Lib.PriorityQueues
{
    public class PriorityQueue<T>
    {
        /// <summary>
        /// Root node
        /// </summary>
        private Node<T> _root;

        /// <summary>
        /// Adds element to queue
        /// </summary>
        /// <param name="element">value of node</param>
        /// <param name="priority">priority of node</param>
        public void AddElement(T element, double priority)
        {
            if (_root == null)
            {
                _root = new Node<T>(null, element, priority);
            }
            else
            {
                Node<T>.AddNode(_root, element, priority);
            }
        }

        /// <summary>
        /// Checks for emptiness of queue
        /// </summary>
        /// <returns>true, if queue is empty</returns>
        public bool IsEmpty()
        {
            if (_root == null)
                return true;
            return false;
        }

        /// <summary>
        /// Returns element with the highest priority
        /// </summary>
        /// <returns>value of element with the highest priority</returns>
        public T Dequeue()
        {
            Node<T> targetNode = Dequeue(_root);
            T value = targetNode.Value;
            if (targetNode == _root)
            {
                _root = _root.RChild;
            }
            else
            {
                targetNode.DeleteNode();
            }

            return value;
        }

        /// <summary>
        /// Auxiliary method for Dequeue()
        /// </summary>
        private Node<T> Dequeue(Node<T> currentNode)
        {
            if (currentNode.LChild != null)
            {
                return Dequeue(currentNode.LChild);
            }

            return currentNode;
        }
    }
}