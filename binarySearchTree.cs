/* NOT COMPLETED, still working on it
08th April 2017
Basic implementation of Binary Search Tree. 
Note:
=>No duplicate values are allowed in the tree.
=>For some functions both iterative and recursive versions are provided.

 */

using System;

namespace adt
{
    interface IBinarySearchTree
    {
        /* features
        deletion
        isBst
        height
        successor
        predecessor 
        isBalanced
        Level order print
        
        */
        void insert(int val);

        void delete(int val);

        void InOrder();
        void PreOrder();
        void PostOrder();

    }

    class baseForBst
    {
        protected Node root = null;

        protected Node getRoot() { return root; }

        protected virtual Node getMin(Node node) { return null; }
        protected virtual Node getMax(Node node) { return null; }

        public bool useIterative = false;
    }

    // contains recursive functions
    class recursiveFunctions : baseForBst
    {
        protected Node insertR(Node node, int val)
        {
            if(node == null)
            {
                node = new Node(val);

                return node;
            }

            Node temp = null;
            if(val < node.val) 
            {
                // going left
                temp = insertR(node.left, val);
                node.left = temp;
                temp.parent = node;
            }
                
            else if( val > node.val)
            {
                // going right
                temp  = insertR(node.right, val);
                node.right = temp;
                temp.parent = node;
            }
            else
            {
                // avoiding duplicates
                Console.WriteLine("[ERROR] insertR(Node,int): {0} already exists", val);
            }

            return node;
        }

        protected Node deleteR(Node node, int val)
        {
            if(node == null) return null;
            /*
                Case 1: Have no nodes in left and right
                Case 2: Have node in left  but not in right
                Case 3: Have node in right but not in left
                Case 4: Have nodes in left and right
             */

            // finding node to be deleted
            if( val < node.val ) node.left = deleteR(node.left, val);
            else if(val > node.val) node.right = deleteR(node.right, val);
            else
            {
                // node found. deleting it
                
                // case 1: no children;
                if(node.left == null && node.right == null)
                {
                    node = null;
                    return node;
                }

                // case 2: only have left child
                if(node.left != null && node.right == null)
                {
                    // if root node
                    if(node.parent == null)
                    {   
                        node = node.left;
                        node.parent = null;
                        return node;
                    }

                    /*
                    // parent of left child points to parent of node
                    node.left.parent = node.parent;
                    
                    // node is left or right child of its parent? 
                    if(node.parent.left == node) 
                        node.parent.left = node.left;
                    else 
                        node.parent.right = node.left;

                    return node.left

                    simple version of this code is written below
                     */

                    Node parentOfNode = node.parent;
                    Node leftChildOfNode = node.left;

                    leftChildOfNode.parent = parentOfNode;

                    // node is in left or right of its parent?
                    if(parentOfNode.left == node)
                        parentOfNode.left = leftChildOfNode;
                    else
                        parentOfNode.right = leftChildOfNode;

                    return leftChildOfNode;

                }

                // case 3: only have right child
                if(node.right != null && node.left == null)
                {
                    // if root node
                    if(node.parent == null)
                    {   
                        node = node.right;
                        node.parent = null;
                        return node;
                    }

                    /*
                    // same as case 2 but on right side
                    node.right.parent = node.parent

                    if(node.parent.right == node) 
                        node.parent.right = node.right;
                    else 
                        node.parent.left = node.right;

                    return node.right;

                    simple version of this code is written below
                     */

                    Node parentOfNode = node.parent;
                    Node rightChildOfNode = node.right;

                    rightChildOfNode.parent = parentOfNode;

                    // node is left or right child of parent?
                    if(parentOfNode.right == node)
                        parentOfNode.right = rightChildOfNode;
                    else
                        parentOfNode.left = rightChildOfNode;
                    
                    return rightChildOfNode;
                }
                
                /*
                if you do not have to play with "parent" pointer than you can
                handle case 1, 2, 3 just by using this 4 lines:
                
                if(node.left == null)
                    return node.right;
                
                if(node.right == null)
                    return node.left;
                */
                
                // case 4: have both children
                // replacing node value with the minimum value in right subtree
                node.val = getMin(node.right).val;
                // removing duplicate value from right subtree
                node.right = deleteR(node.right, node.val);
            }
            
            return node;
             
        }

        protected void InOrderR(Node node)
        {
            // left - root - right
            if(node == null ) return;

            InOrderR(node.left);
            Console.Write("{0} ", node.val);
            InOrderR(node.right);
        }

        protected void PreOrderR(Node node)
        {
            // root - left - right
            if(node == null) return;

            Console.Write("{0} ", node.val);
            PreOrderR(node.left);
            PreOrderR(node.right);
        }

        protected void PostOrderR(Node node)
        {
            if(node == null) return;
            
            // left - right - root;
            
            PostOrderR(node.left);
            PostOrderR(node.right);
            Console.Write("{0} ", node.val);
        }

        protected Node findValR(Node node, int val)
        {
            if(node == null) return null;

            // value found
            if(node.val == val)
                return node;
            
            Node temp = null;
            if(val < node.val)
                temp = findValR(node.left, val);
            else
                temp = findValR(node.right, val);

            return temp;
        }

        protected bool valExistsR(Node node, int val)
        {
            if(node == null) return false;

            // node found
            if(node.val == val)
                return true;
            
            // using pre-order traversal
            bool temp = false;
            if(val < node.val)
                temp = valExistsR(node.left, val);
            else
                 temp = valExistsR(node.right, val);

            return temp;
        }

        protected Node getParentR(Node curr, Node node)
        {
            if(curr == null || node == null) return null;

            if(node.val == getRoot().val) return null;

            if( (curr.left  != null && curr.left.val  == node.val) ||
                (curr.right != null && curr.right.val == node.val) ) return curr;

            if(node.val < curr.val) return getParentR(curr.left, node);
            else return getParentR(curr.right, node);

        }

        protected Node getMinR(Node node)
        {
            if(node == null) return null;

            if(node.left == null) return node;

            return getMinR(node.left);

        }

        protected Node getMaxR(Node node)
        {
            if(node == null) return null;

            if(node.right == null) return node;

            return getMaxR(node.right);
        }

    }

    class iterativeFunctions: recursiveFunctions
    {
        protected void insertI(int val)
        {
            Node node = new Node(val);

            if(getRoot() == null)
            {
                root = node;
                return;
            }

            Node curr = getRoot();
            Node parent = null;

            while(true)
            {
                parent = curr;

                if(val < parent.val)
                {
                    //going left
                    curr = curr.left;

                    if(curr == null)
                    {
                        parent.left = node;
                        node.parent = parent;
                        return;
                    }
                }
                else if(val > parent.val)
                {
                    // going right
                    curr = curr.right;

                    if(curr == null)
                    {
                        parent.right = node;
                        node.parent = parent;
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("[ERROR] insertI(int): {0} already exists", val);
                    return;
                }

            } //while() END

        }

        protected void deleteI(int val)
        {
            /*
            case 1: no child
            case 2: only have left child
            case 3: only have right child;
            case 4: have both children;
             */

            Node curr = getRoot();

            // find node that need to be deleted
            while(curr != null)
            {
                if(curr.val == val) break; // found node;

                if(val < curr.val) curr = curr.left;
                else curr = curr.right;
            }

            if(curr == null) return;  // node not found or tree is empty

            // curr is the node that needs to be deleted

            /*
            Notes: Parent pointers

            This code deals with nodes who have parent pointers in 
            their Node Structure.

            For the case where you don't have parent pointers in 
            your Node structure than there are 3 options:

            a. add parent pointer (you have to change insert/add function too)

            b. use a util function to get parent node. I already 
            provided (check getParent(Node node)). There might be minor performance loss.

            c. check other version of this function (deleteI2(int val)) which keeps
            track of the parent pointer and does not rely on any util functions.
            
            */

            // case 1: "look Mom no child!"
            if(curr.left == null && curr.right == null)
            {
                // dealing with root node;
                if(curr.parent == null)
                {
                    root = null;
                    return;
                }

                // if not root node
                // curr is left or right child of its parent? 
                if(curr.parent.left == curr) curr.parent.left = null;
                else curr.parent.right = null;
                return;
            }
            
            // case 2: only have left child
            if(curr.left != null && curr.right == null)
            {
                Console.WriteLine("deleting left child [{0}]", curr.val);
                if(curr.parent == null) // root node
                {
                    curr = curr.left;
                    curr.parent = null;
                    return;
                }

            /*
                what we going to do here now is this:

                // setting parent of left child to parent of curr node
                curr.left.parent = curr.parent;

                if(curr.parent.left == curr)
                    curr.parent.left = curr.left;
                else 
                    curr.parent.right = curr.right;
                
                return;

                Below I wrote a simple version of this code
            */

                Node parentOfCurr = curr.parent;
                Node leftChildOfCurr = curr.left;

                leftChildOfCurr.parent = parentOfCurr;

                // curr is left or right child of its parent? 
                if(parentOfCurr.left == curr)
                    parentOfCurr.left = leftChildOfCurr;
                else 
                    parentOfCurr.right = leftChildOfCurr;

                return;
            }

            // case 3: only have right child
            if(curr.right != null && curr.left == null)
            {
                Console.WriteLine("deleting right child [{0}]", curr.val);
                if(curr.parent == null) // root node
                {
                    curr = curr.right;
                    curr.parent = null;
                    return;
                }

                /*
                similar to case 2

                // setting parent of rigth child to parent of curr node
                curr.right.parent = curr.parent;

                // curr is left or right of its parent? 
                // connecting child and parent of curr
                if(curr.parent.left == curr)
                    curr.parent.left = curr.right;
                else 
                    curr.parent.right = curr.right;
                
                return;

                simple version below
                */

                Node parentOfCurr = curr.parent;
                Node rightChildOfCurr = curr.right;

                rightChildOfCurr.parent = parentOfCurr;

                // curr is left or right child of its parent? 
                if(parentOfCurr.left == curr)
                    parentOfCurr.left = rightChildOfCurr;
                else 
                    parentOfCurr.right = rightChildOfCurr;

                return;
            }

            // case 4: node with 2 children
            // find max value in left subtree of current
            Node maxInLeft = getMaxI(curr.left); // using iterative version of getMax(Node)

            // if this happen, I will be very sad.
            if(maxInLeft == null)
            {
                Console.WriteLine("[ULTRA BAD ERROR] deleteI(int): maxInLeft is null!!!! ABORT!!1!");
                return;
            }

            // replace curr val with max val found in left subtree of curr
            curr.val = maxInLeft.val;

            // now delete the duplicate in left subtree of curr (maxInLeft)

            // removing maxInLeft;
            // maxInLeft is right or left child of its parent?
            if(maxInLeft.parent.left == maxInLeft) 
                    maxInLeft.parent.left = null;
            else maxInLeft.parent.right = null;
            
            // if somehow maxInLeft's parent is null than
            // we have huge problem to deal with.
            // although it should never happen... lets hope.

            return;
        }

        protected void InOrderI()
        {
            // this require Stack data structure
        }

        protected void PreOrderI()
        {
            // requires stack data structure
        }

        protected void PostOrderI()
        {
            // requires stack data structure
        }

        protected Node findValI(int val)
        {
            if(getRoot() == null) return null;

            Node curr = getRoot();

            while(curr.val != val)
            {
                if(val < curr.val)
                    curr = curr.left;
                else
                    curr = curr.right;
                if(curr == null)
                    return null;
            }

            return curr;
        }

        protected bool valExistsI(int val)
        {
            if(getRoot() == null) return false;            
            
            Node curr = getRoot();

            // binary searching
            while( curr.val != val )
            {
                if(val < curr.val)
                    curr = curr.left;
                else
                    curr = curr.right;

                // value not found
                if(curr == null)
                    return false;
            }

            // value found
            return true;
        }

        protected Node getParentI(Node node)
        {
            if(getRoot() == null) return null;

            Node curr = getRoot();
            if(curr == node && curr.val == node.val) return null; // root does not have parent

            while(curr != null)
            {
                if( (curr.left  != null && curr.left.val  == node.val) || 
                    (curr.right != null && curr.right.val == node.val) ) return curr;

                if(node.val < curr.val) curr = curr.left;
                else curr = curr.right;

            }

            return null;
        }

         protected Node getMinI(Node node)
        {
            if(node == null) return null;
            Node curr = node;

            while(curr.left != null)
            {
                curr = curr.left;
            }

            return curr;
        }

        protected Node getMaxI(Node node)
        {
            if(node == null) return null;

            Node curr = node;

            while(curr.right != null)
            {
                curr = curr.right;
            }

            return curr;
        }
        
    }

    // main class
    class BinarySearchTree : iterativeFunctions, IBinarySearchTree
    {

        public void insert(int val)
        {
            if(!useIterative) root = insertR(getRoot(), val); // recursive version
            else insertI(val); // iterative
        }

        public void delete(int val)
        {

            if(!useIterative) root = deleteR(getRoot(), val);
            else deleteI(val);
        }


#region dislay orders: InOrder, PreOrder, PostOrder, LevelOrder
    // In order --------------
        public void InOrder()
        {
            // using recursive version
            Console.WriteLine("=== Inorder ===");
            InOrderR(getRoot());
            Console.WriteLine();
        }      

    // Pre order -------------
        public void PreOrder()
        {
            Console.WriteLine("=== Pre Order ===");
            // recursive version
            PreOrderR(getRoot());
            Console.WriteLine();
        }
        

    // Post order ------------
        public void PostOrder()
        {
            Console.WriteLine("=== Post Order ===");
            //using recursive
            PostOrderR(getRoot());
            Console.WriteLine();
        }

        

    // Level order -----------
        public void LevelOrder()
        {
            // requires queue data structure
            Console.WriteLine("[ERROR] LevelOrder(): Not implemented");
        }
#endregion

#region searching: findVal, valExists
    // Finval ------    
        private Node findVal(int val)
        {
            if(!useIterative) return findValR(getRoot(), val); // recursive
            else return findValI(val); // iterative
        }        

    // valExists -------
        public bool valExists(int val)
        {
            if(!useIterative) return valExistsR(getRoot(), val);  // recursive version
            else return valExistsI(val); // iterative version
        }

#endregion

#region utilities: getParent, getMin, getMax
// get parent
        private Node getParent(Node node)
        {
            if(!useIterative) return getParentR(getRoot(), node);   // recursive
            else return getParentI(node); // iterative 
        }
// get min
        public int getMin()
        {
            Node temp = getMin(getRoot());
            return temp != null ? temp.val : -1;
        }
        protected override Node getMin(Node node)
        {
            if(!useIterative) return getMinR(node); // recursive
            else return getMinI(node); // iterative
        }
// get max
        public int getMax()
        {
            Node temp = getMax(getRoot());
            return temp != null ? temp.val : -1;
        }        
        protected override Node getMax(Node node)
        {
            if(!useIterative) return getMaxR(node); // recursive
            else return getMaxI(node); // iterative
        }

#endregion
        
       protected void deleteI2(int val)
       {
           //TODO: complete this function
           Console.WriteLine("[ERROR] deleteI2(int): sorry, this function is under construction");
       }

       public void testParent()
       {
           Console.WriteLine("=== test parent ===");
           testParent(getRoot());
           Console.WriteLine();
       }

       private void testParent(Node node)
       {
            if(node == null) return;

            testParent(node.left);
            testParent(node.right);
            Node p = node.parent;
            if(p != null)
                Console.Write("{0}<-[{1}] ", node.val, p.val);
            else
                Console.Write("{0}<-[null] ", node.val);
       }

    }   
}