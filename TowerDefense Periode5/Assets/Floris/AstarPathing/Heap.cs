using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heap<T> where T: IHeapItem<T>
{
    T[] items;
    int currentItemCount;
    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];

    }
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortingUp(item);
        currentItemCount++;
    }
    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }
    public int Count
    {
        get
        {
            return currentItemCount;
        }
    }
    public void UpdateItem(T item)
    {
        SortingUp(item);
    }
    public bool Contains(T item)
    {
        return item.HeapIndex < currentItemCount && Equals(items[item.HeapIndex], item);
    }
    void SortDown(T item)
    {
        while (true)
        {
            int childLeftIndex = 2 * item.HeapIndex + 1;
            if (childLeftIndex >= currentItemCount)
            {
                break;
            }

            int childRightIndex = 2 * item.HeapIndex + 2;
            int swapIndex = childLeftIndex;

            if (childRightIndex < currentItemCount &&
                items[childLeftIndex].CompareTo(items[childRightIndex]) < 0)
            {
                swapIndex = childRightIndex;
            }

            if (item.CompareTo(items[swapIndex]) < 0)
            {
                Swap(item, items[swapIndex]);
                item.HeapIndex = swapIndex;
            }
            else
            {
                break;
            }
        }
       
    }
    void SortingUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;

        while (parentIndex >= 0)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else
            {
                break;
            }
            parentIndex = (item.HeapIndex - 1) / 2;

        }
       

    }
    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
    // calculation for child left = 2*n+1
    // calculation for child right = 2*n+2
    // calculation for parent (n-1)/2
}
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}
