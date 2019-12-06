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
    public class CandidateKeywordDE
    {
        public List<CandidateKeyword> GetCandidateKeyword(int kid,int cid)
        {
            var paramu = new { KeywordID = kid, CandidateID=cid };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CandidateKeywordGET";
                List<CandidateKeyword> lstCandidateKeyword = null;
                var lists = cnn.Query<CandidateKeyword, Candidate, Keyword, CandidateKeyword>(sql, (ck, c, k) =>
                {
                    ck.Keyword = k;
                    ck.Candidate = c;
                    return ck;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "CandidateID,KeywordID");
                lstCandidateKeyword = lists.ToList<CandidateKeyword>();
                return lstCandidateKeyword;

            }
        }
        public CandidateKeyword AddCandidateKeyword(CandidateKeyword comp)
        {
            if (comp!=null)
            {
                var paramu = new
                {
                    KeywordID = comp.Keyword.KeywordID,
                    CandidateID = comp.Candidate.CandidateID,
                    Grade = comp.Grade
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_CandidateKeywordSET";
                    CandidateKeyword us = cnn.QuerySingle<CandidateKeyword>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateCandidateKeyword(CandidateKeyword comp)
        {
            if (comp != null)
            {
                var paramu = new
                {
                    KeywordID = comp.Keyword.KeywordID,
                    CandidateID = comp.Candidate.CandidateID,
                    Grade = comp.Grade
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_CandidateKeywordUpdate";
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
        public bool DeleteCandidateKeyword(CandidateKeyword ck)
        {
            if (ck!=null)
            {
                var paramu = new { CandidateID = ck.Candidate.CandidateID, KeywordID = ck.Keyword.KeywordID };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_CandidateKeywordDELETE";
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
    }
}
