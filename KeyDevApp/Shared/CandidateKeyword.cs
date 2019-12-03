using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class CandidateKeyword
    {
        Candidate candidate;
        Keyword keyword;
        float grade;

        public Candidate Candidate { get => candidate; set => candidate = value; }
        public Keyword Keyword { get => keyword; set => keyword = value; }
        public float Grade { get => grade; set => grade = value; }
    }
}
