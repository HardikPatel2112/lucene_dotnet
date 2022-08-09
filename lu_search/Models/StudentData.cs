using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lu_search.Models
{
    public static class StudentData
    {
        public static List<Student> Students=new List<Student>
        {
             new Student { Id = 1, Name = "Hardik", Address = "G305" },
                 new Student { Id = 2, Name = "Ketul", Address = "G308" },
                 new Student { Id = 3, Name = "Ronak", Address = "G309" },
                 new Student { Id = 4, Name = "Akash", Address = "G301" },
                 new Student { Id = 5, Name = "Pc", Address = "G303" },
                 new Student { Id = 6, Name = "viral", Address = "G508" },
                new Student { Id = 7, Name = "Pratik", Address = "G909" }
        };

        public static List<Student> GetStudents()
        {
            return Students;

        }


    }
}