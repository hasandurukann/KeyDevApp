using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class Experience
    {
        int experienceID;
        string companyName;
        string position;
        DateTime startingDate;
        DateTime endingDate;
        string description;
        Candidate candidate;

        public string CompanyName { get => companyName; set => companyName = value; }
        public string Position { get => position; set => position = value; }
        public DateTime StartingDate { get => startingDate; set => startingDate = value; }
        public DateTime EndingDate { get => endingDate; set => endingDate = value; }
        public string Description { get => description; set => description = value; }
        public Candidate Candidate { get => candidate; set => candidate = value; }
        public int ExperienceID { get => experienceID; set => experienceID = value; }
    }
}
