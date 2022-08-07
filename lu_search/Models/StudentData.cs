using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lu_search.Models
{
    public class StudentData
    {

        public static List<Student> GetStudents()
        {
            return new List<Student>{
                new Student { Id = 1, Name = "Hardik", Address = "G305" },
                 new Student { Id = 2, Name = "Ketul", Address = "G308" },
                new Student { Id = 3, Name = "Pratik", Address = "G309" }
            };
           
        }

        public static implicit operator StudentData(Student v)
        {
            throw new NotImplementedException();
        }
    }
}