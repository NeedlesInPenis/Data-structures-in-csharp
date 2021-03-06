/*
07th of April 2017
Basic implementation of stack using linked list
 */

 using System;
 
 namespace adt
 {
     

     interface IcStack
     {
         bool isEmpty();
         int getSize();
         
         void push(object data); // O(1)
         object pop();           // O(1)

         object peek();          // 0(1)
         void print();           // O(n)
     }

     class cStack: IcStack
     {
         protected Node head = null;
         protected int  size = 0;

         public int  getSize() { return size; }
         public bool isEmpty() { return getSize() <= 0 ? true : false; }

         
         public void push(object data)
         {
             Node node = new Node();
             node.data = data;
             node.next = null;

             if(isEmpty())
             {
                // creating stack
                head = node;    
             }
             else 
             {
                 // prepending
                 node.next = head;
                 head = node;
             }

             size++;
         }

         public object pop()
         {
             if(isEmpty())
             {
                 Console.WriteLine("[ERROR] pop(): Stack is empty");
                 return null;
             }

             object o = head.data;
             head = head.next;

             size--;
             return o;
         }

         // return top object without popping it
         public object peek() 
         {
             if(isEmpty()) return null;
             return head.data;
         }
         public void print()
         {
             Node curr = head;
             int index = 0;
             while(curr != null)
             {
                Console.WriteLine("[{0}]=>{1}", index, curr.data);
                index++;
                curr = curr.next;
             }
             Console.WriteLine("Stack size = " + getSize());
         }

     }
 }