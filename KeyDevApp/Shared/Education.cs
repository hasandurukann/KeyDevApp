using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class Education
    {
        int educationID;
        int educationLevel;
        int graduationYear;
        float gPA;
        Department department;
        Candidate candidate;
        public int EducationLevel { get => educationLevel; set => educationLevel = value; }
        public int GraduationYear { get => graduationYear; set => graduationYear = value; }
        public float GPA { get => gPA; set => gPA = value; }
        public Department Department { get => department; set => department = value; }
        public Candidate Candidate { get => candidate; set => candidate = value; }
        public int EducationID { get => educationID; set => educationID = value; }
    }
}
