using Microsoft.VisualBasic.FileIO;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleLinkedList
{
    public class SingleLinkedList<T> : ISingleLinkedList<T>
    {
        /*
Create an implementation of the data structure Single Linked List;
The consumer of this implementation should be able to specify the type for the value that this SLL will contain;
Implement AddFirst: should put the value at the start of the list;
Implement AddLast: should put the value at the end of the list;
Implement AddAfter: should put the value right after the informed node;
Implement Remove: the consumer of this method should be able to pass the node or the value that he wants to delete;
Bonus: this implementation should have unit tests coverage for the implemented methods.         
         */
        public SingleLinkedList()
        {
            length = 0;
        }

        private SingleLinkedListItem<T>? first;

        public SingleLinkedListItem<T>? First { get => first; set => first = value; }
        
        private int length;
        public int Length { get => length; }

        public void AddAfter(T? itemToAdd, T? itemToAddAfter)
        {
            if (itemToAdd == null)
            {
                throw new ArgumentNullException(nameof(itemToAdd));
            }

            if (itemToAddAfter == null)
            {
                throw new ArgumentNullException(nameof(itemToAddAfter));
            }

            if (First == null)
            {
                throw new Exception("List is empty");
            }

            var currentItemInList = First;
            SingleLinkedListItem<T>? listItemToAddAfter = null;

            do
            {
                var currentItemValue = currentItemInList.Value;
                if (currentItemValue != null)
                {
                    if (currentItemValue.Equals(itemToAddAfter))
                    {
                        listItemToAddAfter = currentItemInList;
                        break;
                    }
                }
                currentItemInList = currentItemInList.Next;
            }
            while (currentItemInList != null);

            if (listItemToAddAfter == null)
            {
                throw new Exception("item to add after is not in the list");
            }

            listItemToAddAfter.Next = new SingleLinkedListItem<T> { Value = itemToAdd, Next = listItemToAddAfter.Next };
            length++;
        }

        public void AddFirst(T itemToAdd)
        {
            if (itemToAdd == null) throw new ArgumentNullException(nameof(itemToAdd));

            First = new SingleLinkedListItem<T> { Value = itemToAdd, Next = First };
            length++;
        }

        public void AddLast(T itemToAdd)
        {
            if (itemToAdd == null) throw new ArgumentNullException(nameof(itemToAdd));

            if (First == null)
            {
                AddFirst(itemToAdd);
                return;
            }

            var lastItem = First;
            while (lastItem?.Next != null)
            {
                lastItem = lastItem.Next;
            }

            if (lastItem != null)
            {
                lastItem.Next = new SingleLinkedListItem<T> { Value = itemToAdd, Next = null };
                length++;

                return;
            }

            throw new Exception("add last failed");
        }

        public void Remove(T itemToRemove)
        {
            if (itemToRemove == null)
            {
                throw new ArgumentNullException(nameof(itemToRemove));
            }

            if (First == null)
            {
                throw new Exception("List is empty");
            }

            var currentItemInList = First;
            SingleLinkedListItem<T>? listItemToRemove = null;
            SingleLinkedListItem<T>? listItemBeforeItemToRemove = null;
            SingleLinkedListItem<T>? listItemAfterItemToRemove = null;

            do
            {
                var currentItemValue = currentItemInList.Value;
                if (currentItemValue != null)
                {
                    if (currentItemValue.Equals(itemToRemove))
                    {
                        listItemToRemove = currentItemInList;
                        listItemAfterItemToRemove = currentItemInList.Next;
                        break;
                    }
                }

                listItemBeforeItemToRemove = currentItemInList;
                currentItemInList = currentItemInList.Next;
            }
            while (currentItemInList != null);

            if (listItemToRemove == null)
            {
                throw new Exception("Item to remove not found");
            }

            if (listItemBeforeItemToRemove == null)
            {
                First = listItemAfterItemToRemove;

            }
            else
            {
                listItemBeforeItemToRemove.Next = listItemAfterItemToRemove;
            }

            length--;
        }

        public T?[] GetValues()
        {
            var values = new T?[length];
            var currentItem = First;

            var idx = 0;
            while (currentItem != null)
            {
                values[idx] = currentItem.Value;
                currentItem = currentItem.Next;
                idx++;
            }

            return values;
        }
    }

    public interface ISingleLinkedList<T>
    {
        int Length { get; }
        SingleLinkedListItem<T> First { get; set; }
        void AddFirst(T itemToAdd);
        void AddLast(T itemToAdd);
        void AddAfter(T itemToAdd, T itemToAddAfter);
        void Remove(T itemToRemove);
        T?[] GetValues();
    }

    public class SingleLinkedListItem<T>
    {
        public T? Value { get; set; }
        public SingleLinkedListItem<T>? Next;
    }
}
