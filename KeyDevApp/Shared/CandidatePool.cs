using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class CandidatePool
    {
        int candidatePoolID;
        Candidate candidate;
        Company company;
        bool status;
        CompanyPosition companyPosition;

        public Candidate Candidate { get => candidate; set => candidate = value; }
        public Company Company { get => company; set => company = value; }
        public bool Status { get => status; set => status = value; }
        public CompanyPosition CompanyPosition { get => companyPosition; set => companyPosition = value; }
        public int CandidatePoolID { get => candidatePoolID; set => candidatePoolID = value; }
    }
}
