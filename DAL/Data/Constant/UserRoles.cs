using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Data.Constant
{
    
        public static class UserRoles
        {
        public const string Admin = "Admin";
        public const string Student = "Student";
        public const string Instructor = "Instructor";

        public static readonly HashSet<string> AllRoles = new() { Admin, Student, Instructor };
    }
    
}
