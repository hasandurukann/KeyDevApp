using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class Institution
    {
        int instutionID;
        string name;
        List<Department> departments;

        public string Name { get => name; set => name = value; }
        public List<Department> Departments { get => departments; set => departments = value; }
        public int InstutionID { get => instutionID; set => instutionID = value; }
    }
}
