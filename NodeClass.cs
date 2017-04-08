/*
8th of April 2017
Node class for different structures.
Using chain-inheritance
*/

namespace adt 
{
    class NodeLinkedList
    {
        public Node   next = null;
        public object data = null;
    }

    class NodeDoubleLinkedList : NodeLinkedList
    {
        public Node prev = null;
    }

    class NodeBinarySearchTree : NodeDoubleLinkedList
    {
        public Node right = null;
        public Node left = null;
        public int val = 0;
        
        public Node parent = null;
    }

    // final class
     class Node : NodeBinarySearchTree 
     {
         public Node() {}

         public Node(object _data) { data = _data; }

         public Node(int _val) { val = _val; }
     }

// public Node   next = null;
// public object data = null;
// public Node   prev = null; // need this for double linked list

// // for trees
// public Node right = null;
// public Node left  = null;
// public int  val   = 0;

}
