using System;
using System.Linq;
using System.Collections.Generic;

namespace Lab3
{
    class Program
    {
        private const string filename = @"TextFile1.txt";
        static void Main(string[] args)
        {
            Test0();
        }
        static void Test0()
        {
            FdblVector2 f = FdblVector2Types.f_third;
            V3DataArray array = new V3DataArray("Velocity", DateTime.Now, 4, 4, 1.0f, 1.0f, f);
            Console.WriteLine("Data:");
            Console.WriteLine(array.ToLongString("f"));
            float[] left = new float[2] { 0F, 0.5F };
            float[] right = new float[2] { 0.5F, 1F };
            bool res = array.Integrals(left, right);
            Console.WriteLine($"Result int = {res}");
            float[,] integrals = array.integrals;
            for (int i = 0; i < integrals.GetLength(0); i++)
            {
                for (int j = 0; j < integrals.GetLength(1); j++)
                {
                    if (j == 0)
                        Console.Write($"Segment {i}: ");
                    Console.Write($"{ integrals[i, j].ToString("F3")} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            left = new float[1] { 0F };
            right = new float[1] { 1F };
            res = array.Integrals(left, right);
            Console.WriteLine($"Result int = {res}");
            integrals = array.integrals;
            for (int i = 0; i < integrals.GetLength(0); i++)
            {
                for (int j = 0; j < integrals.GetLength(1); j++)
                {
                    if (j == 0)
                        Console.Write($"Segment {i}: ");
                    Console.Write($"{ integrals[i, j].ToString("F3")} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            left = new float[1] { 1F };
            right = new float[1] { 1F };
            res = array.Integrals(left, right);
            Console.WriteLine($"Result int = {res}");
            integrals = array.integrals;
            for (int i = 0; i < integrals.GetLength(0); i++)
            {
                for (int j = 0; j < integrals.GetLength(1); j++)
                {
                    if (j == 0)
                        Console.Write($"Segment {i}: ");
                    Console.Write($"{ integrals[i, j].ToString("F3")} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("Empty array");
            array = new V3DataArray("Velocity", DateTime.Now, 0, 0, 1.0f, 1.0f, f);
            left = new float[1] { 0F };
            right = new float[1] { 1F };
            res = array.Integrals(left, right);
            Console.WriteLine($"Result int = {res}");
            integrals = array.integrals;
            for (int i = 0; i < integrals.GetLength(0); i++)
            {
                for (int j = 0; j < integrals.GetLength(1); j++)
                {
                    if (j == 0)
                        Console.Write($"Segment {i}: ");
                    Console.Write($"{ integrals[i, j].ToString("F3")} ");
                }
                Console.WriteLine();
            }
        }

        static void Test1()
        {
            //28 and 38 strings are fixed!

            FdblVector2 f = FdblVector2Types.f2;
            V3DataArray array = new V3DataArray("Velocity", DateTime.Now, 3, 4, 1.0f, 1.0f, f);
            Console.WriteLine("Array until saving");
            Console.WriteLine(array.ToLongString("f"));
            V3DataArray.SaveBinary(filename, array);
            V3DataArray array1 = new V3DataArray("Velocity", DateTime.Now, 0, 0, 1.0f, 1.0f, f); //fixed
            V3DataArray.LoadBinary(filename, ref array1);
            Console.WriteLine("Array after saving");
            Console.WriteLine(array1.ToLongString("f"));

            V3DataList arrayConverted = (V3DataList)array;
            Console.WriteLine("Array until saving");
            Console.WriteLine(arrayConverted.ToLongString("f"));
            V3DataList.SaveAsText(filename, arrayConverted);
            V3DataList v3List = new V3DataList("", new DateTime());
            V3DataList.LoadAsText(filename, ref v3List); //fixed
            Console.WriteLine("Array after saving");
            Console.WriteLine(v3List.ToLongString("f"));
        }

        static void Test2()
        {
            FdblVector2 f = FdblVector2Types.f3;
            V3MainCollection mainCollection = new V3MainCollection();
            V3MainCollection emptyCollection = new V3MainCollection();
            V3DataArray array = new V3DataArray("Velocity", DateTime.Now, 3, 4, 1.0f, 1.0f, f);
            V3DataArray array1 = new V3DataArray("Electricity", DateTime.Now, 2, 2, 1.0f, 1.0f, f);
            V3DataList array2 = new V3DataList("Density", DateTime.MinValue);
            V3DataArray array3 = new V3DataArray("Electricity_zero", DateTime.Now, 0, 0, 1.0f, 1.0f, f); // zero elements
            V3DataList array4 = new V3DataList("Density_zero", DateTime.MinValue); // zero elements
            array2.Add(new DataItem(0.0f, 1.2f, f(0.0f, 1.2f)));
            array2.Add(new DataItem(2.0f, 3.2f, f(2.0f, 3.2f)));
            array2.Add(new DataItem(2.5f, 3.7f, f(2.5f, 3.7f)));
            mainCollection.Add(array);
            mainCollection.Add(array1);
            mainCollection.Add(array2);
            mainCollection.Add(array3);
            mainCollection.Add(array4);


            Console.WriteLine("MainCollection");
            Console.WriteLine(mainCollection.ToLongString("f"));
            Console.WriteLine("First request: Average lengths of points");
            double average = mainCollection.Average;
            Console.WriteLine(average);
            Console.WriteLine("EmptyCollection");
            Console.WriteLine(emptyCollection.ToLongString("f"));
            Console.WriteLine("First request for empty: Average lengths of points");
            double average_empt = emptyCollection.Average;
            Console.WriteLine(average_empt);
            Console.WriteLine();


            Console.WriteLine("MainCollection");
            Console.WriteLine(mainCollection.ToLongString("f"));
            Console.WriteLine("Second request: Difference between maximum and minimum absolute value of field");
            IEnumerable<float> diametr = mainCollection.DiaField;
            foreach (var el in diametr)
            {
                Console.WriteLine(el);
            }
            Console.WriteLine("EmptyCollection");
            Console.WriteLine(emptyCollection.ToLongString("f"));
            Console.WriteLine("Second request for empty: Difference between maximum and minimum absolute value of field");
            IEnumerable<float> diametr_empt = emptyCollection.DiaField;
            if (diametr_empt == null) Console.WriteLine("NULL");
            else
            {
                foreach (var el in diametr_empt)
                {
                    Console.WriteLine(el);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("MainCollection");
            Console.WriteLine(mainCollection.ToLongString("f"));
            Console.WriteLine("Third request: Group DataItem's by X");
            IEnumerable<IGrouping<double, DataItem>> group = mainCollection.GroupByX;
            foreach (IGrouping<double, DataItem> el in group)
            {
                foreach (DataItem item in el.ToList())
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine("EmptyCollection");
            Console.WriteLine(emptyCollection.ToLongString("f"));
            Console.WriteLine("Third request for empty: Group DataItem's by X");
            IEnumerable<IGrouping<double, DataItem>> group_empt = emptyCollection.GroupByX;
            if (group_empt == null) Console.WriteLine("NULL");
        }
    }
}