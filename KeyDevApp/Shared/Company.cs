using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class Company
    {
        int companyID;
        string title;
        string description;
        string country;
        string province;
        string subProvince;
        string address;
        string phone;
        string fax;
        string websiteURL;
        string mail;
        List<CompanyPosition> companyPositions;
        User user;
        List<CandidatePool> candidatePools;

        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public string Country { get => country; set => country = value; }
        public string Province { get => province; set => province = value; }
        public string SubProvince { get => subProvince; set => subProvince = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Fax { get => fax; set => fax = value; }
        public string WebsiteURL { get => websiteURL; set => websiteURL = value; }
        public string Mail { get => mail; set => mail = value; }
        public List<CompanyPosition> CompanyPositions { get => companyPositions; set => companyPositions = value; }
        public User User { get => user; set => user = value; }
        public List<CandidatePool> CandidatePools { get => candidatePools; set => candidatePools = value; }
        public int CompanyID { get => companyID; set => companyID = value; }
    }
}
