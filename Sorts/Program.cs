// Сортировка выбором (сложность: O(n^2))
using System.Diagnostics;

var arraysLength = 5000;

var array = Enumerable.Range(0, arraysLength).ToArray();

var clonedArray = (int[])array.Clone();
var watch = Stopwatch.StartNew();
InsertionSort(clonedArray);
watch.Stop();
Console.WriteLine($"Сортировка выбором: { watch.Elapsed.TotalMilliseconds } мс.");

clonedArray = (int[])array.Clone();
watch = Stopwatch.StartNew();
QuickSort(clonedArray);
watch.Stop();
Console.WriteLine($"Быстрая сортировка: {watch.Elapsed.TotalMilliseconds} мс.");

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

static int[] QuickSort(int[] values)
{
    if (values.Length < 2)
    {
        return values;
    }

    var pivot = values[0];

    var valuesGreaterThanPivot = values[1..].Where(value => value > pivot).ToArray();

    QuickSort(valuesGreaterThanPivot);

    var valuesLessThanPivot = values[1..].Where(value => value <= pivot).ToArray();

    QuickSort(valuesLessThanPivot);

    Array.Copy(valuesGreaterThanPivot, 0, values, 0, valuesGreaterThanPivot.Length);
    Array.Copy(new int[] { pivot }, 0, values, valuesGreaterThanPivot.Length, 1);
    Array.Copy(valuesLessThanPivot, 0, values, valuesGreaterThanPivot.Length + 1, valuesLessThanPivot.Length);

    return values;
}