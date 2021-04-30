using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using static AlgorithmsLib.SortingAlgorithms;

namespace AlgorithmsTest
{
    class Program
    {
        public delegate void SortAlgorithm<T>(T[] array);

        static string writePath = @"C:\Users\Вадим\Desktop\SortResults.txt";

        static void Main(string[] args)
        {
            //Тест на массивах различных типов
            DifferentTypesArrayTest();
            //Тест на массивах различной длины (int)
            DifferentLengthArrayTest();
            //Тест на массивах по-разному порожденных (int)
            DifferentCreatedArrayTest();
        }

        //Тест на массивах различных типов
        public static void DifferentTypesArrayTest()
        {
            int arrayLength = 1000000;

            byte[] byteArray = createRandomByteArray(arrayLength);
            int[] intArray = createRandomIntArray(arrayLength, 1, arrayLength);
            string[] stringArray = createRandomStringArray(arrayLength, 1, 10);
            DateTime[] dateTimeArray = createRandomDateArray(arrayLength);

            TestSortAlgorithms(byteArray, "ByteArray (1M)");
            TestSortAlgorithms(intArray, "IntArray (1M)");
            TestSortAlgorithms(stringArray, "StringArray (1M)");
            TestSortAlgorithms(dateTimeArray, "DateTimeArray (1M)");
        }

        //Тест на массивах различной длины (int)
        public static void DifferentLengthArrayTest()
        {
            int arrayLength = 100;
            int[] intArray1 = createRandomIntArray(arrayLength, 1, arrayLength);
            int[] intArray2 = createRandomIntArray(arrayLength *= 10, 1, arrayLength);
            int[] intArray3 = createRandomIntArray(arrayLength *= 10, 1, arrayLength);
            int[] intArray4 = createRandomIntArray(arrayLength *= 10, 1, arrayLength);
            int[] intArray5 = createRandomIntArray(arrayLength *= 10, 1, arrayLength);

            TestSortAlgorithms(intArray1, "intArray (100)");
            TestSortAlgorithms(intArray2, "intArray (1k)");
            TestSortAlgorithms(intArray3, "intArray (10k)");
            TestSortAlgorithms(intArray4, "intArray (100k)");
            TestSortAlgorithms(intArray5, "intArray (1M)");
        }

        //Тест на массивах по-разному порожденных (int)
        public static void DifferentCreatedArrayTest()
        {
            int arrayLength = 1000000;
            int[] randomIntArray = createRandomIntArray(arrayLength, 1, arrayLength);
            int[] halfSortedIntArray = createHalfSortedIntArray(arrayLength, 1, arrayLength);
            int[] reversedIntArray = createReverseIntArray(arrayLength);

            TestSortAlgorithms(randomIntArray, "RandomIntArray (1M)");
            TestSortAlgorithms(halfSortedIntArray, "HalfSortedIntArray (1M)");
            TestSortAlgorithms(reversedIntArray, "ReversedIntArray (1M)");
        }

        //Прогон массива через все сортировки
        public static void TestSortAlgorithms<T>(T[] array, string testName) where T : IComparable<T>
        {
            List<double> timeList = new List<double>();
            timeList.Add(getSortTime(BubbleSort, array));
            timeList.Add(getSortTime(SelectionSort, array));
            timeList.Add(getSortTime(InsertionSort, array));
            timeList.Add(getSortTime(ShellSort, array));
            timeList.Add(getSortTime(QuickSort, array));
            timeList.Add(getSortTime(MergeSort, array));
            timeList.Add(getSortTime(HeapSort, array));
            timeList.Add(getSortTime(Array.Sort, array));

            writeSortDataInFile(testName);
            foreach(var time in timeList)
            {
                writeSortDataInFile("\t" + Convert.ToString(time));
            }
        }

        //Вычисление среднего времени сортировки (10 тестов)
        public static double getSortTime<T>(SortAlgorithm<T> sortAlgorithm, T[] array)
        {
            int testCount = 10;
            double total = 0;

            for (int i = 0; i < testCount; i++)
            {
                T[] helpArray = new T[array.Length];
                Array.Copy(array, helpArray, array.Length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                sortAlgorithm(helpArray);
                stopwatch.Stop();

                total += stopwatch.Elapsed.TotalMilliseconds;
            }
            return total / testCount;
        }

        //Вычисление времени поразрядной сортировки 
        public static double getRadixSortTime(int[] array, int range, int length)
        {
            int testCount = 10;
            double total = 0;

            for (int i = 0; i < testCount; i++)
            {
                int[] helpArray = new int[array.Length];
                Array.Copy(array, helpArray, array.Length);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                RadixSort(helpArray, range, length);
                stopwatch.Stop();

                total += stopwatch.Elapsed.TotalMilliseconds;
            }
            return total / testCount;
        }

        //Запис результатов сортировок в файл
        public static void writeSortDataInFile(string data)
        {
            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(data);
            }
        }
      
        //Вывода массива на экран
        static void printArray<T>(T[] array)
        {
            foreach(T item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        //Создание массивов различных типов
        public static byte[] createRandomByteArray(int length, byte minValue = 0, byte maxValue = 255)
        {
            byte[] array = new byte[length];
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                array[i] = (byte)random.Next(minValue, maxValue);
            }
            return array;
        }
        public static string[] createRandomStringArray(int length, int minSymbols, int maxSymbols)
        {
            string[] array = new string[length];
            Random random = new Random();
            int countSymbols;
            string currentString;

            for (int i = 0; i < length; i++)
            {
                currentString = "";
                countSymbols = random.Next(minSymbols, maxSymbols);
                for (int j = 0; j < countSymbols; j++)
                {
                    currentString += (char)(random.Next(97, 122));
                }
                array[i] = currentString;
            }
            return array;
        }
        public static DateTime[] createRandomDateArray(int length)
        {
            DateTime[] array = new DateTime[length];
            RandomDateTime date = new RandomDateTime();

            for (int i = 0; i < length; i++)
            {
                array[i] = date.Next();
            }
            return array;
        }

        //Создание по-разному порожденных массивов
        public static int[] createRandomIntArray(int length, int minValue, int maxValue)
        {
            int[] array = new int[length];
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(minValue, maxValue);
            }
            return array;
        }
        public static int[] createHalfSortedIntArray(int length, int minValue, int maxValue)
        {
            int[] array = new int[length];
            for (int i = 0; i < length / 2; i++)
            {
                array[i] = i + 1;
            }

            Random random = new Random();
            for (int i = length / 2; i < length; i++)
            {
                array[i] = random.Next(minValue, maxValue);
            }
            return array;
        }
        public static int[] createReverseIntArray(int length)
        {
            int[] array = new int[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = length - i;
            }
            return array;
        }
    }
}
