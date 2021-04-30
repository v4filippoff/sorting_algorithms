using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static AlgorithmsLib.SortingAlgorithms;

namespace SortingUnitTest
{
    [TestClass]
    public class SortingAlgorithmsTests
    {
        static int arrLength = 10000;

        [TestMethod]
        public void BubbleSort_Check()
        {
            int[] testArray = createTestArray();
            int[] sortedArray = createSortedArray();
            BubbleSort(testArray);
            CollectionAssert.AreEqual(testArray, sortedArray);
        }

        [TestMethod]
        public void SelectionSort_Check()
        {
            int[] testArray = createTestArray();
            int[] sortedArray = createSortedArray();
            SelectionSort(testArray);
            CollectionAssert.AreEqual(testArray, sortedArray);
        }

        [TestMethod]
        public void InsertionSort_Check()
        {
            int[] testArray = createTestArray();
            int[] sortedArray = createSortedArray();
            InsertionSort(testArray);
            CollectionAssert.AreEqual(testArray, sortedArray);
        }

        [TestMethod]
        public void ShellSort_Check()
        {
            int[] testArray = createTestArray();
            int[] sortedArray = createSortedArray();
            ShellSort(testArray);
            CollectionAssert.AreEqual(testArray, sortedArray);
        }

        [TestMethod]
        public void QuickSort_Check()
        {
            int[] testArray = createTestArray();
            int[] sortedArray = createSortedArray();
            QuickSort(testArray);
            CollectionAssert.AreEqual(testArray, sortedArray);
        }

        [TestMethod]
        public void MergeSort_Check()
        {
            int[] testArray = createTestArray();
            int[] sortedArray = createSortedArray();
            MergeSort(testArray);
            CollectionAssert.AreEqual(testArray, sortedArray);
        }

        [TestMethod]
        public void HeapSort_Check()
        {
            int[] testArray = createTestArray();
            int[] sortedArray = createSortedArray();
            HeapSort(testArray);
            CollectionAssert.AreEqual(testArray, sortedArray);
        }

        [TestMethod]
        public void RadixSort_Check()
        {
            int[] testArray = createTestArray();
            int[] sortedArray = createSortedArray();
            RadixSort(testArray, 10, countDigits(arrLength));
            CollectionAssert.AreEqual(testArray, sortedArray);
        }

        [TestMethod]
        public void InternalSort_Check()
        {
            int[] testArray = createTestArray();
            int[] sortedArray = createSortedArray();
            Array.Sort(testArray);
            CollectionAssert.AreEqual(testArray, sortedArray);
        }
        public static int[] createTestArray()
        {
            int[] testArray = new int[arrLength];
            for (int i = 0; i < arrLength; i++)
                testArray[i] = arrLength - i;
            return testArray;
        }

        public static int[] createSortedArray()
        {
            int[] sortedArray = new int[arrLength];
            for (int i = 0; i < arrLength; i++)
                sortedArray[i] = i + 1;
            return sortedArray;
        }

        public static int countDigits(int number)
        {
            int count = 0;
            while(number > 0)
            {
                number /= 10;
                count++;
            }
            return count;
        }
    }
}
