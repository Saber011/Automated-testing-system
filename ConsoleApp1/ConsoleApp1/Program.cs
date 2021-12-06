using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ren = false;
            int n = 0, m = 0, k = 0;
            int summ = 0;
            double avg;
            while (!ren) // кол-во элементов в массиве
            {
                Console.WriteLine("Введите кол-во эл-тов в массиве:");
                string nnn = (Console.ReadLine());
                ren = int.TryParse(nnn, out n);
                if (!ren)
                {
                    Console.WriteLine("Это не целое число, попробуйте еще раз");
                }
                else
                {
                    if (n <= 0)
                    {
                        Console.WriteLine("Число должно быть > 0");
                        ren = false;
                    }
                }
            }
            ren = false;
            int[] mass = new int[n]; //создание массива
            var rnd = new Random();
            for (int i = 0; i < n; i++) //заполнение массива рандомными числами
            {
                mass[i] = rnd.Next(-100, 100) ;
            }
            Console.WriteLine("Ваш массив: ");
            foreach (var item in mass) //вывод массива
            {
                summ += item;
                Console.Write($"{item}; ");
            }
            avg = summ / n;
            mass = mass.Where(x => x < avg).ToArray(); //оставили только те значения, которые меньше avg
            Console.WriteLine("");
            foreach (var item in mass)
            {
                Console.Write($"{item}; ");
            }
            

            Console.WriteLine("Сколько элементов добавить вначало?");
            while (!ren) // кол-во элементов добавить вначало
            {
                Console.WriteLine("Введите кол-во эл-тов для добавления вначало:");
                string mmm = (Console.ReadLine());
                ren = int.TryParse(mmm, out m);
                if (!ren)
                {
                    Console.WriteLine("Это не целое число, попробуйте еще раз");
                }
                else
                {
                    if (n <= 0)
                    {
                        Console.WriteLine("Число должно быть > 0");
                        ren = false;
                    }
                }
            }
            ren = false;
            var list = new List<int>(); //Создаем новый лист
            for (int i = 0; i < m; i++) //закидываем туда новые значения
            {
                list.Add(rnd.Next(-100, 100));
            }
            list.AddRange(mass); // добавляем в лист старый лист
            mass = list.ToArray(); // превращаем лист в массив
            foreach (var item in mass) //вывод массива
            {
                summ += item;
                Console.Write($"{item}; ");
            }

            Console.WriteLine("На сколько элементов сдвинуть вправо?");
            while (!ren) // число для сдвига вправо
            {
                Console.WriteLine("Введите кол-во эл-тов для сдвига вправо:");
                string kkk = (Console.ReadLine());
                ren = int.TryParse(kkk, out k);
                if (!ren)
                {
                    Console.WriteLine("Это не целое число, попробуйте еще раз");
                }
                else
                {
                    if (n <= 0)
                    {
                        Console.WriteLine("Число должно быть > 0");
                        ren = false;
                    }
                }
            }
            ren = false;
            for (int i = 0; i < k; i++)
            {
                int sl = mass[^1];
                for (var y = mass.Length; y > 0; y--)
                {
                    mass[y - 2] = mass[y - 1];
                }
                mass[0] = sl;
            }
            foreach (var item in mass) //вывод массива
                         {
                             summ += item;
                             Console.Write($"{item}; ");
                         }
             
             
             
             
             
             
                     }
                 }
             }