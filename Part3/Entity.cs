using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    class PersonComparer : IComparer<person>
    {
        public int Compare(person p1, person p2)
        {
            if (p1 is null || p2 is null)
                throw new ArgumentException("Некорректное значение параметра");
            return p1.Place.Length - p2.Place.Length;
        }
    }
    internal class person: IInit, IComparable<person>
    {
        protected string status;     // студент/студент-заочник/школьник
        protected string place;     //место учебы: Школа/вуз
        protected string gender;   //гендер 

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string Place
        {
            get { return place; }
            set { place = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public person() : this("status", "place", "gender") { } //конструктор без параметров

        public person(string status, string place, string gender) //конструктор с параметрами 
        {
            Status = status;
            Place = place;
            Gender = gender;
        }
        public virtual void ShowVirt() //виртуальный 
        {
            Console.WriteLine($"(Вирутальный метод)\nСтатус: {status}.\nМесто обучения: {place}.");
        }
        public void Show() //не виртуальный 
        {
            Console.WriteLine($"(Не вирутальный метод)\nСтатус: {status}.\nМесто обучения: {place}.");
        }

        static Random random = new Random();
        public virtual void Init()
        {
            this.Status = random.Next(0, 100).ToString();
            this.Gender = random.Next(0, 100).ToString();
            this.Place = random.Next(0, 10000).ToString();
        }

        public override string ToString()
        {
            ShowVirt();
            return "";
        }

        public int CompareTo(person other)
        {
            if (other is null) throw new ArgumentException("Некорректное значение параметра");
            return Place.CompareTo(other.Place);
        }
    }
    internal class schoolboy : person
    {
        public override void Init()
        {
            base.Init();
            this.Progress = new Random().Next(0, 100).ToString();
        }
        string progress;
        public string Progress
        {
            get { return progress; }
            set { progress = value; }
        }
        public schoolboy() : base() //конструтор по-умолчанию с вызовом конструктора базового класса
        {
            progress = "undefined";
        }
        public schoolboy(string progress, string status, string place, string gender) : base("Школьник", place, "Женский") //конструтор с вызовом конструктора базового класса
        {
            Progress = progress;
        }
        public override void ShowVirt() //виртуальный 
        {
            base.ShowVirt();
            Console.WriteLine($"Прогресс: {progress}.");
        }
        public new void Show() //не виртуальный 
        {
            base.Show();
            Console.WriteLine($"Прогресс: {progress}.");
        }
    }
    internal class StDistant : person
    {
        string form;
        public override void Init()
        {
            base.Init();
            Form = new Random().Next(0, 100).ToString();
        }
        public string Form
        {
            get { return form; }
            set { form = value; }
        }
        public StDistant() : base() //конструтор по-умолчанию с вызовом конструктора базового класса
        {
            form = "undefined";
        }
        public StDistant(string form, string status, string place, string gender) : base("Студент-заочник", place, "Мужской") //конструтор с вызовом конструктора базового класса
        {
            Form = form;
        }
        public override void ShowVirt() //виртуальный 
        {
            base.ShowVirt();
            Console.WriteLine($"Форма обучения: {form}.");
        }
        public new void Show() //не виртуальный 
        {
            base.Show();
            Console.WriteLine($"Форма обучения: {form}.");
        }
        public override string ToString()
        {
            ShowVirt();
            return "";
        }
    }

    internal class student : person
    {
        public override void Init()
        {
            base.Init();
            course = new Random().Next(0, 1);
        }
        int course;
        public int Course
        {
            get { return course; }
            set
            {
                if (value <= 0)
                {
                    course = 0;
                    Console.WriteLine("Не учится в ВУЗе");
                }
                else course = value;
            }
        }
        public student() : base() //конструтор по-умолчанию с вызовом конструктора базового класса
        {
            Course = 0;
        }
        public student(int course, string status, string place, string gender) : base("Студент", place, "Мужской") //конструтор с вызовом конструктора базового класса
        {
            Course = course;
        }
        public override void ShowVirt() //виртуальный 
        {
            base.ShowVirt();
            Console.WriteLine($"Курс: {course}.");
        }
        public new void Show() //не виртуальный 
        {
            base.Show();
            Console.WriteLine($"Курс: {course}.");
        }
    }   
}

