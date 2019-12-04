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
    public class KeywordDE
    {
        public List<Keyword> GetKeyword(int id)
        {
            var paramu = new { KeywordID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_KeywordGET";
                List<Keyword> lstKeyword = null;
                var lists = cnn.Query<Keyword, CandidateKeyword, Exam, Keyword>(sql, (k, ck, e) =>
                {
                    k.Exam = e;
                    k.CandidateKeywords = new List<CandidateKeyword>();
                    k.CandidateKeywords.Add(ck);
                    return k;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "Grade,ExamID");
                var result = lists.GroupBy(k => k.KeywordID).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.CandidateKeywords = g.Select(p => p.CandidateKeywords.Single()).ToList();
                    return groupedPost;
                });
                lstKeyword = result.ToList<Keyword>();
                return lstKeyword;

            }
        }
        public Keyword AddKeyword(Keyword k)
        {
            if (k != null)
            {
                var paramu = new
                {
                    Title=k.Title
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_KeywordSET";
                    Keyword us = cnn.QuerySingle<Keyword>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateKeyword(Keyword k)
        {
            if (k != null)
            {
                var paramu = new
                {
                    KeywordID = k.KeywordID,
                    Title = k.Title
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_KeywordUpdate";
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
        public bool DeleteKeyword(int id)
        {
            var paramu = new { KeywordID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_KeywordDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;
            }
        }
    }
}
