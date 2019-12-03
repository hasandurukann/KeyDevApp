using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class Keyword
    {
        int keywordID;
        string title;
        List<CandidateKeyword> candidateKeywords;
        Exam exam;

        public string Title { get => title; set => title = value; }
        public List<CandidateKeyword> CandidateKeywords { get => candidateKeywords; set => candidateKeywords = value; }
        public Exam Exam { get => exam; set => exam = value; }
        public int KeywordID { get => keywordID; set => keywordID = value; }
    }
}
