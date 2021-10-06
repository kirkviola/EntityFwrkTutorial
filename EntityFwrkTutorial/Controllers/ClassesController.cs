using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFwrkTutorial.Models;

namespace EntityFwrkTutorial.Controllers
{
    class ClassesController
    {
        private readonly EdDbContext _context;

        public ClassesController()
        {
            _context = new EdDbContext();
        }

        public List<Class> GetAll()
        {
            return _context.Classes.ToList();
        }

        public Class GetByPk(int Id)
        {
            return _context.Classes.Find(Id);
        }

        public Class GetByCode(string code)
        {
            return _context.Classes.SingleOrDefault(c => c.Code == code);
        }

        public bool Create(Class newClass)
        {
            if (newClass == null)
                return false;
            if (newClass.Id != 0)
                return false;
            _context.Add(newClass);
            var rowsAffected = _context.SaveChanges();
            if (rowsAffected != 1)
                throw new Exception("Create failed!");
            return true;
        }

        public bool Change(int Id, Class cClass)
        {
            if (Id != cClass.Id)
                return false;
            if (_context.Classes.Find(Id) == null)
                return false;

            _context.Entry(cClass).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var rowsAffected = _context.SaveChanges();
            if (rowsAffected != 1)
                throw new Exception("Change failed!");

            return true;
        }

        public bool Remove(int Id)
        {
            if (_context.Classes.Find(Id) == null)
                return false;

            _context.Remove(Id);
            var rowsAffected = _context.SaveChanges();
            if (rowsAffected != 1)
                throw new Exception("Delete failed!");

            return true;
        }

    }    
}
