using System;
using System.Collections.Generic;

// Интерфейс для вывода информации о студенте
public interface IStudentInformation
{
    void DisplayInformation(Student student);
}

// Класс, реализующий интерфейс IStudentInformation
public class StudentInformation : IStudentInformation
{
    public void DisplayInformation(Student student)
    {
        Console.WriteLine($"Информация о студенте {student.Name}");
        // Здесь можно добавить другие данные о студенте, если необходимо
    }
}

// Класс курса
public class Course
{
    public string CourseName { get; }
    private List<Student> students = new List<Student>();
    private IStudentInformation studentInformation;

    public Course(string courseName, IStudentInformation studentInformation)
    {
        CourseName = courseName;
        this.studentInformation = studentInformation;
    }

    public void AddStudent(Student student)
    {
        students.Add(student);
        Console.WriteLine($"{student.Name} добавлен в курс {CourseName}");
    }

    public void RemoveStudent(Student student)
    {
        students.Remove(student);
        Console.WriteLine($"{student.Name} удален из курса {CourseName}");
    }

    public void NotifyStudents(string message)
    {
        Console.WriteLine($"Уведомление для студентов курса {CourseName}: {message}");
        foreach (var student in students)
        {
            studentInformation.DisplayInformation(student);
            student.Update(message);
        }
    }

    public void DisplayStudentInformation(Student student)
    {
        studentInformation.DisplayInformation(student);
    }

    public List<Student> GetStudents()
    {
        return students;
    }
}

// Класс студента
public class Student
{
    public string Name { get; }

    public Student(string name)
    {
        Name = name;
    }

    public void Update(string message)
    {
        Console.WriteLine($"Студент {Name} получил уведомление: {message}");
    }
}

// Паттерн Singleton для создания единственного экземпляра курса
public class CourseSingleton
{
    private static Course instance;

    private CourseSingleton() { }

    public static Course Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Course("Программирование на C#", new StudentInformation());
            }
            return instance;
        }
    }
}

class Program
{
    static void Main()
    {
        // Использование Singleton
        Course csharpCourse = CourseSingleton.Instance;

        // Создание студентов
        Student student1 = new Student("Иван");
        Student student2 = new Student("Мария");

        // Добавление студентов в курс
        csharpCourse.AddStudent(student1);
        csharpCourse.AddStudent(student2);

        // Уведомление студентов о предстоящем уроке
        csharpCourse.NotifyStudents("Завтра лекция по C#!");

        // Взаимодействие со студентами
        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Удалить студента");
            Console.WriteLine("3. Послать уведомление");
            Console.WriteLine("4. Выйти");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Введите имя студента:");
                    string newStudentName = Console.ReadLine();
                    Student newStudent = new Student(newStudentName);
                    csharpCourse.AddStudent(newStudent);
                    break;
                case 2:
                    Console.WriteLine("Выберите студента для удаления:");
                    List<Student> students = csharpCourse.GetStudents();
                    for (int i = 0; i < students.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {students[i].Name}");
                    }
                    int removeChoice = int.Parse(Console.ReadLine());
                    if (removeChoice > 0 && removeChoice <= students.Count)
                    {
                        csharpCourse.RemoveStudent(students[removeChoice - 1]);
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор.");
                    }
                    break;
                case 3:
                    Console.WriteLine("Введите уведомление:");
                    string notification = Console.ReadLine();
                    csharpCourse.NotifyStudents(notification);
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}

