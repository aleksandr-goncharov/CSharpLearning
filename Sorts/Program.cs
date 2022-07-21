// Сортировка выбором (сложность: O(n^2))
using System.Diagnostics;

var arraysLength = 60000;

var array = Enumerable.Range(0, arraysLength).ToArray();

var clonedArray = (int[])array.Clone();
var watch = Stopwatch.StartNew();
InsertionSort(clonedArray);
watch.Stop();
Console.WriteLine($"Сортировка выбором: { watch.Elapsed.TotalMilliseconds } мс.");

clonedArray = (int[])array.Clone();
watch = Stopwatch.StartNew();
Array.Sort(clonedArray);
watch.Stop();
Console.WriteLine($"Array.Sort: {watch.Elapsed.TotalMilliseconds} мс.");

static void InsertionSort(int[] values)
{
    for (int i = 0; i < values.Length; i++)
    {
        var maxValueIndex = i;

        for (int j = i + 1; j < values.Length; j++)
        {
            if (values[j] > values[maxValueIndex])
            {
                maxValueIndex = j;
            }
        }

        (values[i], values[maxValueIndex]) = (values[maxValueIndex], values[i]);
    }
}

