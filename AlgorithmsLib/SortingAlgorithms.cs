using System;
using System.Collections;
using static AlgorithmsLib.HelpMethods;

namespace AlgorithmsLib
{
    public static class SortingAlgorithms
    {
        //Сортировка пузырьком
        public static void BubbleSort<T>(T[] array) where T : IComparable<T>
        {
            int len = array.Length;
            for (int i = 1; i < len; i++)
            {
                for (int j = 0; j < len - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0)
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        //Сортировка выбором
        public static void SelectionSort<T>(T[] array) where T : IComparable<T>
        {
            int min;
            int length = array.Length;

            for (int i = 0; i < length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < length; j++)
                    if (array[j].CompareTo(array[min]) < 0)
                        min = j;

                if (min != i)
                    Swap(ref array[i], ref array[min]);
            }
        }

        //Сортировка вставками
        public static void InsertionSort<T>(T[] array) where T : IComparable<T>
        {
            T cur; int j;
            for (int i = 1; i < array.Length; i++)
            {
                cur = array[i];
                j = i;
                while (j > 0 && cur.CompareTo(array[j - 1]) < 0)
                {
                    array[j] = array[j - 1];
                    j--;
                }
                array[j] = cur;
            }
        }

        //Сортировка Шелла
        public static void ShellSort<T>(T[] array) where T : IComparable<T>
        {
            int d = array.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < array.Length; i++)
                {
                    int j = i;
                    while ((j >= d) && (array[j - d].CompareTo(array[j]) > 0))
                    {
                        Swap(ref array[j], ref array[j - d]);
                        j = j - d;
                    }
                }
                d = d / 2;
            }
        }

        //Быстрая сортировка
        public static void QuickSort<T>(T[] array) where T : IComparable<T>
        {
            HelpMethods.QuickSort(array, 0, array.Length - 1);
        }

        //Сортировка слиянием
        public static void MergeSort<T>(T[] array) where T : IComparable<T>
        {
            HelpMethods.MergeSort<T>(array, 0, array.Length - 1);
        }

        //Сортировка кучей
        public static void HeapSort<T>(T[] array) where T : IComparable<T>
        {
            int n = array.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                Swap(ref array[0], ref array[i]);

                Heapify(array, i, 0);
            }
        }

        //Поразрядная сортировка
        public static void RadixSort(int[] array, int range, int length)
        {
            ArrayList[] lists = new ArrayList[range];
            for (int i = 0; i < range; ++i)
                lists[i] = new ArrayList();

            for (int step = 0; step < length; ++step)
            {
                for (int i = 0; i < array.Length; ++i)
                {
                    int temp = (array[i] % (int)Math.Pow(range, step + 1)) /
                                                  (int)Math.Pow(range, step);
                    lists[temp].Add(array[i]);
                }
                int k = 0;
                for (int i = 0; i < range; ++i)
                {
                    for (int j = 0; j < lists[i].Count; ++j)
                    {
                        array[k++] = (int)lists[i][j];
                    }
                }
                for (int i = 0; i < range; ++i)
                    lists[i].Clear();
            }
        }
    }
    static class HelpMethods
    {
        //метод поиска позиции минимального элемента подмассива, начиная с позиции n
        public static int IndexOfMin(int[] array, int n)
        {
            int result = n;
            for (var i = n; i < array.Length; ++i)
            {
                if (array[i] < array[result])
                {
                    result = i;
                }
            }

            return result;
        }

        //метод для обмена элементов
        public static void Swap<T>(ref T x, ref T y)
        {
            T t = x;
            x = y;
            y = t;
        }

        //метод возвращающий индекс опорного элемента (QuickSort)
        public static int Partition<T>(T[] array, int minIndex, int maxIndex) where T : IComparable<T>
        {
            T x = array[(minIndex + maxIndex) / 2];
            int i = minIndex - 1;
            int j = maxIndex + 1;

            while (true)
            {
                while (++i < maxIndex && array[i].CompareTo(x) < 0) ;
                while (--j > minIndex && array[j].CompareTo(x) > 0) ;

                if (i < j)
                    Swap(ref array[i], ref array[j]);
                else
                    return j;
            }
        }
        
        //быстрая сортировка
        public static void QuickSort<T>(T[] array, int minIndex, int maxIndex) where T : IComparable<T>
        {
            if (minIndex < maxIndex)
            {
                int q = Partition(array, minIndex, maxIndex);
                QuickSort(array, minIndex, q);
                QuickSort(array, q + 1, maxIndex);
            }
        }

        //метод для слияния массивов
        public static void Merge<T>(T[] array, int lowIndex, int middleIndex, int highIndex) where T : IComparable<T>
        {
            int left = lowIndex;
            int right = middleIndex + 1;
            T[] tempArray = new T[highIndex - lowIndex + 1];
            int index = 0;

            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left].CompareTo(array[right]) < 0)
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (int i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (int i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (int i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }

        //сортировка слиянием
        public static void MergeSort<T>(T[] array, int lowIndex, int highIndex) where T : IComparable<T>
        {
            if (lowIndex < highIndex)
            {
                int middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }
        }

        public static void Heapify<T>(T[] array, int n, int i) where T : IComparable<T>
        {
            int largest = i;
            int l = 2 * i + 1; 
            int r = 2 * i + 2; 

            if (l < n && array[l].CompareTo(array[largest]) > 0)
                largest = l;

            if (r < n && array[r].CompareTo(array[largest]) > 0)
                largest = r;

            if (largest != i)
            {
                Swap(ref array[i], ref array[largest]);

                Heapify(array, n, largest);
            }
        }
    }
}
