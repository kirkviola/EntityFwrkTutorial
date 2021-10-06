using EntityFwrkTutorial.Models;
using System;
using System.Linq;

namespace EntityFwrkTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var _context = new EdDbContext();
            var majors = _context.Majors.ToList();
            var students = _context.Students.ToList();
            var classes = _context.Classes.ToList();
            var majorsclasses = _context.MajorClasses.ToList();

            var gpaView = students.Where(s => s.Gpa > students.Average(s => s.Gpa)).OrderBy(s => s.Lastname);

            foreach(var v in gpaView)
                Console.WriteLine($"{v.Lastname}, {v.Firstname}: GPA is {v.Gpa}");
            Console.WriteLine("---");

            var MajorsStudents = from m in majors
                                join s in students
                                on m.Id equals s.MajorId
                                where m.Code == "MATH"
                                orderby s.Lastname
                                select new{ m, s};

            foreach(var ms in MajorsStudents)
                Console.WriteLine($"{ms.s.Firstname} {ms.s.Lastname} | {ms.m.Code} | {ms.m.Description}");
            Console.WriteLine("---");

            var StudentsClasses = from s in students
                                  join m in majors
                                  on s.MajorId equals m.Id
                                  join mc in majorsclasses
                                  on m.Id equals mc.MajorId
                                  join c in classes
                                  on mc.ClassId equals c.Id
                                  where m.Description == "Engineering"
                                  orderby s.Lastname descending
                                  select new { s, m, mc, c };

            foreach(var smmcc in StudentsClasses)
            {
                Console.WriteLine($"{smmcc.s.Lastname} | {smmcc.m.Description} | {smmcc.c.Code}");
            }
        }
    }
}
