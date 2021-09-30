namespace Vakor._8_puzzle.Lib.PriorityQueues
{
    public class Node<T>
    {
        /// <summary>
        /// Left child of current Node
        /// </summary>
        public Node<T> LChild { get; private set; }

        /// <summary>
        /// Right child of current Node
        /// </summary>
        public Node<T> RChild { get; private set; }

        /// <summary>
        /// Priority of current Node
        /// </summary>
        private readonly double _priority;

        /// <summary>
        /// Parent Node of current
        /// </summary>
        private Node<T> _mother;

        /// <summary>
        /// Represents value of current Node
        /// </summary>

        public T Value { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mother">parent Node</param>
        /// <param name="value">value of current Node</param>
        /// <param name="priority">priority of current Node</param>
        public Node(Node<T> mother, T value, double priority)
        {
            _mother = mother;
            _priority = priority;
            Value = value;
        }

        /// <summary>
        /// Deletes current Node 
        /// </summary>
        public void DeleteNode()
        {
            if (RChild != null)
            {
                _mother.LChild = RChild;
                _mother.LChild._mother = _mother;
            }
            else
            {
                _mother.LChild = null;
            }
        }

        /// <summary>
        /// Adds Node
        /// </summary>
        /// <param name="parent">parent Node</param>
        /// <param name="element">value of Node</param>
        /// <param name="priority">priority of Node</param>
        public static void AddNode(Node<T> parent, T element, double priority)
        {
            if (priority <= parent._priority)
            {
                if (parent.LChild == null)
                {
                    parent.LChild = new Node<T>(parent, element, priority);
                }
                else
                {
                    AddNode(parent.LChild, element, priority);
                }
            }
            else if (priority > parent._priority)
            {
                if (parent.RChild == null)
                {
                    parent.RChild = new Node<T>(parent, element, priority);
                }
                else
                {
                    AddNode(parent.RChild, element, priority);
                }
            }
        }
    }
}