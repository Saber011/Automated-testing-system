using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = InitdData();

            foreach (var VARIABLE in data)
            {
                // todo пример вывода данных обьекта на примере двух полей
                Console.WriteLine($"{VARIABLE.company_name} {VARIABLE.type}");
            }
            
            // количетсво элементов в списке
            Console.WriteLine(data.Count);
            
            // Нахождение количества записей с определённым наименованием публикации.
            var findedLine = Console.ReadLine();

            foreach (var VARIABLE in data.Where(x => x.publish_name == findedLine))
            {
                Console.WriteLine($"{VARIABLE.company_name} {VARIABLE.type}");
            }
            
            Console.WriteLine("Нахождение средней цены.");
            Console.WriteLine(data.Average(x => x.price));
            // методы по фильтрации и прочие которые могут понадобиться:
            // Distinct() убрать дулбли
            // FirstOrDefault(x => x.price == 123) найти перввый элемент по условиям 
            // OrderBy или OrderByDescending сортировка в разных направлениях
            // Max Min - агрегирующие функции поиска минимума или максиумам
            // GroupBy групировка элементов
            
        }

        static List<Catalog> InitdData()
        {
            var catalogs = new List<Catalog>()
            {
                new Catalog
                {
                    id = 1,
                    publish_name = "Домовой",
                    company_name = "Издательский дом Родионова",
                    type = "Журнал",
                    numberofcopies = 13000,
                    iscolored = true,
                    price = 400,
                    subscribers = 36000,
                },
                new Catalog
                {
                    id = 1,
                    publish_name = "Краски осени",
                    company_name = "Пирамида",
                    type = "Журнал",
                    numberofcopies = 13000,
                    iscolored = true,
                    price = 400,
                    subscribers = 36000,
                },
                // todo more
            };

            return catalogs;
        }
    }
    
    
    class Catalog
    {
        public int id { get; set; }
        public string  publish_name { get; set; }
        public string  company_name { get; set; }
        public string  type { get; set; }
        public int  numberofcopies { get; set; }
        public bool  iscolored { get; set; }
        public int  price { get; set; }
        public int  subscribers { get; set; }
    }

}