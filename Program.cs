// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Text;
using System.Threading;

public class Program
{
    public static int n;
    private static object lockObject = new ();
    private static bool isFooTurn = true;

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
            lock (lockObject)
            {
                // Якщо не Foo черга, очікувати на іншому потоці.
                while (!isFooTurn)
                {
                    Monitor.Wait(lockObject);
                }

                // Виведення "foo" на екран.
                Console.Write("foo");

                // Помітка, що тепер черга у Bar.
                isFooTurn = false;

                // Повідомлення іншого потоку, що може виконуватися.
                Monitor.PulseAll(lockObject);
            }
        }
    }

    private static void Bar()
    {
        // Цикл повторення n разів.
        for (int i = 0; i < n; i++)
        {
            lock (lockObject)
            {
                // Якщо не Bar черга, очікувати на іншому потоці.
                while (isFooTurn)
                {
                    Monitor.Wait(lockObject);
                }

                // Виведення "bar" на екран.
                Console.Write("bar");

                // Помітка, що тепер черга у Foo.
                isFooTurn = true;

                // Повідомлення іншого потоку, що може виконуватися.
                Monitor.PulseAll(lockObject);
            }
        }
    }
}