using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class Exam
    {
        int examID;
        Keyword keyword;
        int time;
        List<Question> questions;

        public Keyword Keyword { get => keyword; set => keyword = value; }
        public int Time { get => time; set => time = value; }
        public List<Question> Questions { get => questions; set => questions = value; }
        public int ExamID { get => examID; set => examID = value; }
    }
}
