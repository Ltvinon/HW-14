// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Text;
using System.Threading;

public class Program
{
    public static int n;
    private static AutoResetEvent fooDone = new AutoResetEvent(false);
    private static AutoResetEvent barDone = new AutoResetEvent(true);

    public static void Main(string[] args)
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.Write("Введіть кількість ітерацій : ");
        n = Convert.ToInt32(Console.ReadLine());

        Thread thread1 = new (Foo);
        Thread thread2 = new (Bar); // Створення двох потоків для виконання методів Foo і Bar.

        // Запуск потоків.
        thread1.Start();
        thread2.Start();

        // Очікування завершення обох потоків.
        thread1.Join();
        thread2.Join();

        Console.ReadLine();
    }

    private static void Foo()
    {
        // Цикл повторення n разів.
        for (int i = 0; i < n; i++)
        {
                barDone.WaitOne();
                Console.Write("foo");
                fooDone.Set();
        }
    }

    private static void Bar()
    {
        // Цикл повторення n разів.
        for (int i = 0; i < n; i++)
        {
            fooDone.WaitOne();
            Console.Write("bar");
            barDone.Set();
        }
    }
}