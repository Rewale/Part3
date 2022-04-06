using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    interface IInit
    {
        /// <summary>
        /// Формирует объект с помощью ДСЧ
        /// </summary>
        /// <returns>Объект типа Т с случайными значениями свойств</returns>
        void Init();
    }

    class InitClass : IInit
    {
        public int num1 { get; set; }
        public int num2 { get; set; }
        public void Init()
        {
            Random random = new Random();
            num1 = random.Next(0, 100);
            num2 = random.Next(0, 100);
        }

        public override string ToString()
        {
            Console.WriteLine($"InitClass {num1} {num2}");
            return base.ToString();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<IInit> inits = new List<IInit>();
            inits.Add(new person());
            inits.Add(new person());
            inits.Add(new student());
            inits.Add(new student());
            inits.Add(new StDistant());
            inits.Add(new StDistant());
            inits.Add(new schoolboy());
            inits.Add(new schoolboy());
            inits.Add(new InitClass());
            inits.Add(new InitClass());

            Console.WriteLine("До вызова метода Init");
            inits.ForEach(p => p.ToString());

            inits.ForEach(p => p.Init());
            Console.WriteLine("После вызова метода Init");
            inits.ForEach(p => p.ToString());
            SortByComparer();
            Console.WriteLine("---------");
            SortWOComparer();
            Console.WriteLine("---------");
            FindObject();
            Console.WriteLine("---------");
            Copy();

            
           
            Console.Read();
        }
        static void FindObject()
        {
            Console.WriteLine("Поиск элемента");
            person[] people = new person[3];
            people[0] = new schoolboy() { Place="Школа № 100"};
            people[1] = new student(){ Place="Высшее учебное заведение № 100"};
            people[2] = new StDistant() { Place = "Лицей № 1000000" };
            Array.Sort(people);
            foreach (var item in people)
            {
                item.Show();
            }
            var findObject = new schoolboy() { Place="Школа № 100"};
            Console.WriteLine("Индекс искомого элемента: "+Array.BinarySearch(people, findObject));

        }
        static void SortWOComparer()
        {
            person[] people = new person[3];
            people[0] = new schoolboy() { Place="Школа № 100"};
            people[1] = new student(){ Place="Высшее учебное заведение № 100"};
            people[2] = new StDistant() { Place = "Лицей № 1000000" };
            Console.WriteLine("Сортировка без Comparer", ConsoleColor.Blue);
            Console.WriteLine("Элементы до сортировки:");
            for (int i = 0; i < people.Length; i++)
            {
                people[i].Show();
            }
            Console.WriteLine("Элементы после сортировки:");

            Array.Sort(people);
            for (int i = 0; i < people.Length; i++)
            {
                people[i].Show();
            }


        }

        static void SortByComparer()
        {
            person[] people = new person[3];
            people[0] = new schoolboy() { Place="Школа № 100"};
            people[1] = new student(){ Place="Высшее учебное заведение № 100"};
            people[2] = new StDistant() { Place = "Лицей № 1000000" };
            Console.WriteLine("Сортировка c Comparer", ConsoleColor.Blue);
            Console.WriteLine("Элементы до сортировки:");
            for (int i = 0; i < people.Length; i++)
            {
                people[i].Show();
            }
            Console.WriteLine("Элементы после сортировки:");

            Array.Sort(people, new PersonComparer());
            for (int i = 0; i < people.Length; i++)
            {
                people[i].Show();
            }

        }
        class InnerItem
        {
            public string name { get; set; }
            public InnerItem(string name)
            {
                this.name = name;
            }
            public override string ToString()
            {
                return name;
            }
        }
        class Item : ICloneable
        {
            string name { get; set; }
            public InnerItem InnerItem { get; set; }
            public Item(string name, InnerItem innerItem)
            {
                this.name = name;
                this.InnerItem = innerItem;
            }
            public Item ShallowCopy()
            {
                return (Item)this.MemberwiseClone();
            }
            public object Clone()
            {
                return new Item("Clone "+this.name, new InnerItem(InnerItem.name));
            }

            public override string ToString()
            {
                return $"Name={name}, InnerItem name={this.InnerItem.ToString()}";
            }
        }
        static void Copy()
        {
            var original = new Item("original", new InnerItem("inner item"));
            Console.WriteLine("Cоздан объект: "+ original.ToString());
            var ShallowClone = original.ShallowCopy();
            Console.WriteLine("Поверхностно скопирован объект: "+ ShallowClone.ToString());
            var DeepClone =(Item) original.Clone();
            Console.WriteLine("Скопирован объект: "+ DeepClone.ToString());

            // Меняем внутренний объект InnerItem
            DeepClone.InnerItem.name = "123";
            Console.WriteLine("Оригинал после изменения внутреннего объекта полного клона: "+ original.ToString());
            ShallowClone.InnerItem.name = "123";
            Console.WriteLine("Оригинал после изменения внутреннего объекта поверхностного клона: "+ original.ToString());


        }
    }
}
