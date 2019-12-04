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
    public class ExamDE
    {
        public List<Exam> GetExam(int id)
        {
            var paramu = new { ExamID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_ExamGET";
                List<Exam> lstExam = null;
                var lists = cnn.Query<Exam, Keyword, Question, Exam>(sql, (e, k, q) =>
                {
                    e.Keyword = k;
                    e.Questions = new List<Question>();
                    e.Questions.Add(q);
                    return e;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "KeywordID,QuestionID");
                var result = lists.GroupBy(e => e.ExamID).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Questions = g.Select(p => p.Questions.Single()).ToList();
                    return groupedPost;
                });
                lstExam = result.ToList<Exam>();
                return lstExam;

            }
        }
        public Exam AddExam(Exam e)
        {
            if (e != null)
            {
                var paramu = new
                {
                    Time = e.Time,
                    KeywordID=e.Keyword.KeywordID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_ExamSET";
                    Exam us = cnn.QuerySingle<Exam>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateExam(Exam e)
        {
            if (e != null)
            {
                var paramu = new
                {
                    ExamID = e.ExamID,
                    Time = e.Time,
                    KeywordID = e.Keyword.KeywordID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_ExamUpdate";
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
        public bool DeleteExam(int id)
        {
            var paramu = new { ExamID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_ExamDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;
            }
        }
    }
}
