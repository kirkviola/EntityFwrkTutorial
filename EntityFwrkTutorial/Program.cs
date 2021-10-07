using EntityFwrkTutorial.Models;
using EntityFwrkTutorial.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFwrkTutorial
{
    class Program
    {
        async static Task Main()
        {
            var majsCtrl = new MajorsController();
            var majors = await majsCtrl.GetAll();
            majors.ForEach(m => Console.WriteLine(m));

            var thisMajor = await majsCtrl.GetByPk(2);
            
        }

        static void OtherOtherMethod()
        {
            var Matt = new Student()
            {
                Id = 0,
                Lastname = "Kirkendall",
                Firstname = "Matthew",
                StateCode = "OH",
                Sat = 1600,
                Gpa = 3.96m
            };

            //var StudentCtrl = new StudentController();
            //StudentCtrl.Create(Matt);
            var _context = new EdDbContext();

            var StudentList = _context.Students.ToList();
            foreach(var student in StudentList)
                Console.WriteLine($"{student.Id} | {student.Firstname} {student.Lastname} | " +
                                $"{student.StateCode} | {student.Sat} | {student.Gpa}");

        }
            static void OtherMethod()
            {

                var _context = new EdDbContext();
                var majors = _context.Majors.ToList();
                var students = _context.Students.ToList();
                var classes = _context.Classes.ToList();
                var majorsclasses = _context.MajorClasses.ToList();

                // Displays all students with a GPA above the average alphabetized by last name.
                var gpaView = students.Where(s => s.Gpa > students.Average(s => s.Gpa)).OrderBy(s => s.Lastname);

                foreach (var v in gpaView)
                    Console.WriteLine($"{v.Lastname}, {v.Firstname}: GPA is {v.Gpa}");
                AddSeparator();

                // Displays All students with the Math major alphabetized by last name.
                var MajorsStudents = from m in majors
                                     join s in students
                                     on m.Id equals s.MajorId
                                     where m.Code == "MATH"
                                     orderby s.Lastname
                                     select new { m, s };

                foreach (var ms in MajorsStudents)
                    Console.WriteLine($"{ms.s.Firstname} {ms.s.Lastname} | {ms.m.Code} | {ms.m.Description}");
                AddSeparator();

                // Displays all students with the engineering major's classes ordered by last name in descending order.
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

                foreach (var smmcc in StudentsClasses)
                {
                    Console.WriteLine($"{smmcc.s.Lastname} | {smmcc.m.Description} | {smmcc.c.Code}");
                }

                AddSeparator();
                var mController = new MajorsController();
                var majList = mController.GetAll();

                //foreach (var maj in majList)
                //    Console.WriteLine(maj.Code);

                AddSeparator();
                var major = mController.GetByPk(2);
                Console.WriteLine(major);

                AddSeparator();

                var major2 = mController.GetByCode("MUSC");
                Console.WriteLine(major2);


                //try
                //{
                //    var rc = mController.Create(major3);
                //    if (!rc)
                //    {
                //    Console.WriteLine("Create failed!");
                //    }

                //}    catch (Exception ex)
                //{
                //    Console.WriteLine($"Exceptoin occurred: {ex.Message}");
                //}        

                major2.Description = "Classical Music";
                var rc = mController.Change(major2.Id, major2);

                Console.WriteLine(major2);
            }
        static void AddSeparator()
        {
            Console.WriteLine("---");
        }
    }

}

