using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class CompanyPosition
    {
        int companyPositionID;
        string title;
        Company company;
        CandidatePool candidatePool;
        public string Title { get => title; set => title = value; }
        public Company Company { get => company; set => company = value; }
        public CandidatePool CandidatePool { get => candidatePool; set => candidatePool = value; }
        public int CompanyPositionID { get => companyPositionID; set => companyPositionID = value; }
    }
}
