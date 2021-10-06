using EntityFwrkTutorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFwrkTutorial.Controllers
{
    public class MajorsController
    {
        private readonly EdDbContext _context;

        public MajorsController()
        {
            this._context = new EdDbContext();
        }

        public List<Major> GetAll()
        {
            return _context.Majors.ToList();
        }

        public Major GetByPk(int id)
        {
            return  _context.Majors.Find(id);
        }

        public Major GetByCode(string code)
        {
            return _context.Majors.SingleOrDefault(m => m.Code == code);
        }

        public bool Create(Major major)
        {
            if (major.Id != 0)
                return false;

            // Handle other forms of invalid data here

            _context.Majors.AddAsync(major);
            var rowsAffected = _context.SaveChanges();
            if (rowsAffected != 1)
                throw new Exception("Create failed!");


            return true;
        }
        public bool Change(int Id, Major major) // pass in the row as well as the Id for the row -- make sure they are sure.
        {
            // Checks to make sure valid/correct data is sent to the method.
            if (major == null)
            {
                throw new Exception("Invalid Item");
            }
            if (Id != major.Id)
            {
                throw new Exception("Ids don't match!");
            }
            // major.Updated = DateTime.Now;
            _context.Entry(major).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var rowsAffected = _context.SaveChanges();
            if (rowsAffected != 1)
                throw new Exception("Update failed");
            return true;

        }

        public bool Remove(int Id)
        {
            var major = _context.Majors.Find(Id);
            if (major == null)
            {
                return false;
            }
            _context.Majors.Remove(major);
            _context.SaveChanges();
            return true;
        }
    }
}
