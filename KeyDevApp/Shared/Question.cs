using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDevApp.Shared
{
    public class Question
    {
        int questionID;
        string qContent;
        string option1, option2, option3, option4, option5;
        int answerOptionNo;
        Exam exam;
        public int AnswerOptionNo { get => answerOptionNo; set => answerOptionNo = value; }
        public Exam Exam { get => exam; set => exam = value; }
        public string QContent { get => qContent; set => qContent = value; }
        public string Option1 { get => option1; set => option1 = value; }
        public string Option2 { get => option2; set => option2 = value; }
        public string Option3 { get => option3; set => option3 = value; }
        public string Option4 { get => option4; set => option4 = value; }
        public string Option5 { get => option5; set => option5 = value; }
        public int QuestionID { get => questionID; set => questionID = value; }
    }
}
