using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class Candidate
    {
        int candidateID;
        string name;
        string surname;
        string phone;
        string country;
        string province;
        string subProvince;
        DateTime dateOfBirth;
        string photoPath;
        List<Experience> experiences;
        List<CandidateKeyword> candidateKeywords;
        List<CandidatePool> candidatePools;
        List<Education> educations;
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Country { get => country; set => country = value; }
        public string Province { get => province; set => province = value; }
        public string SubProvince { get => subProvince; set => subProvince = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }
        public string PhotoPath { get => photoPath; set => photoPath = value; }
        public List<Experience> Experiences { get => experiences; set => experiences = value; }
        public List<CandidateKeyword> CandidateKeywords { get => candidateKeywords; set => candidateKeywords = value; }
        public List<CandidatePool> CandidatePools { get => candidatePools; set => candidatePools = value; }
        public List<Education> Educations { get => educations; set => educations = value; }
        public int CandidateID { get => candidateID; set => candidateID = value; }
    }
}
