using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;
using KeyDevApp.Shared;

namespace KeyDevApp.Server.DataEngine
{
    public class QuestionDE
    {
        public List<Question> GetQuestion(int id)
        {
            var paramu = new { QuestionID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_QuestionGET";
                List<Question> lstQuestion = null;
                var lists = cnn.Query<Question, Exam, Question>(sql, (q, e) =>
                {
                    q.Exam = e;
                    return q;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "ExamID");
                lstQuestion = lists.ToList<Question>();
                return lstQuestion;

            }
        }
        public Question AddQuestion(Question q)
        {
            if (q != null)
            {
                var paramu = new
                {
                    QContent = q.QContent,
                    Option1 = q.Option1,
                    Option2 = q.Option2,
                    Option3 = q.Option3,
                    Option4 = q.Option4,
                    Option5 = q.Option5,
                    AnswerOptionNo = q.AnswerOptionNo,
                    ExamID = q.Exam.ExamID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_QuestionSET";
                    Question us = cnn.QuerySingle<Question>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateQuestion(Question q)
        {
            if (q != null)
            {
                var paramu = new
                {
                    QuestionID = q.QuestionID,
                    QContent = q.QContent,
                    Option1 = q.Option1,
                    Option2 = q.Option2,
                    Option3 = q.Option3,
                    Option4 = q.Option4,
                    Option5 = q.Option5,
                    AnswerOptionNo = q.AnswerOptionNo,
                    ExamID = q.Exam.ExamID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_QuestionUpdate";
                    int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                        commandType: CommandType.StoredProcedure).ToString());
                    return affectedRow > 0 ? true : false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool DeleteQuestion(int id)
        {
            var paramu = new { QuestionID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_QuestionDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;
            }
        }
    }
}
