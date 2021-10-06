using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFwrkTutorial.Models;

namespace EntityFwrkTutorial.Controllers
{
    public class StudentController
    {
        private readonly EdDbContext _context;

        public StudentController()
        {
            this._context = new EdDbContext();
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        public Student GetByPk(int Id)
        {
            return _context.Students.Find(Id);
        }

        public Student GetByLastname(string lastname)
        {
            return _context.Students.SingleOrDefault(s => s.Lastname == lastname);
        }

        public bool Create(Student student)
        {
            if (student == null)
                return false;
            if (student.Id != 0)
                return false;

            _context.Add(student);
            var rowsAffected = _context.SaveChanges();
            if (rowsAffected != 1)
                throw new Exception("Create failed!");
            return true;
        }

        public bool Change(int Id, Student student)
        {
            if (Id != student.Id)
                return false;

            if (_context.Students.Find(Id) == null)
                return false;

            _context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var rowsAffected = _context.SaveChanges();
            return true;
        }

        public bool Remove(int Id)
        {
            if (_context.Students.Find(Id) == null)
                return false;

            _context.Remove(Id);
            var rowsAffected = _context.SaveChanges();
            if (rowsAffected != 1)
                throw new Exception("Remove failed!");
            return true;

        }
    }
}
