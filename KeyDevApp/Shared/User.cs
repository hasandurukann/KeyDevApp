using System;

namespace KeyDevApp.Shared
{
    public class User
    {
        int userID;
        string mail;
        string password;
        Company company;
        Candidate candidate;
        bool type;

        //type 0 = candidate 1 = company

        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public Company Company
        {
            get { return company; }
            set { if (value == null) { this.Type = false; } else { this.Type = true;company = value; } }
        }
        public Candidate Candidate 
        {
            get { return candidate; }
            set { if (value == null) { this.Type = false; } else { this.Type = true; candidate = value; } }
        }
        public bool Type { get => type; set => type = value; }
        public int UserID { get => userID; set => userID = value; }
        public string Token { get; set; }
    }
}
