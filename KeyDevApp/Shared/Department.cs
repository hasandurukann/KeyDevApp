using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class Department
    {
        int departmentID;
        string name;
        Institution institution;
        List<Education> educations;
        public string Name { get => name; set => name = value; }
        public Institution Institution { get => institution; set => institution = value; }
        public List<Education> Educations { get => educations; set => educations = value; }
        public int DepartmentID { get => departmentID; set => departmentID = value; }
    }
}
