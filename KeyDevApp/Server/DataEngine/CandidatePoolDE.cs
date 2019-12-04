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
    public class CandidatePoolDE
    {
        public List<CandidatePool> GetCandidatePool(int id)
        {
            var paramu = new { CandidatePoolID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CandidatePoolGET";
                List<CandidatePool> lstCandidatePool = null;
                var lists = cnn.Query<CandidatePool, Candidate, Company, CompanyPosition, CandidatePool>(sql, (cp, c, c2, cp2) =>
                 {
                     cp.Company = c2;
                     cp.Candidate = c;
                     cp.CompanyPosition = cp2;
                     return cp;
                 }, paramu, commandType: CommandType.StoredProcedure, splitOn: "CandidateID,CompanyID,CompanyPositionID");
                lstCandidatePool = lists.ToList<CandidatePool>();
                return lstCandidatePool;

            }
        }
        public CandidatePool AddCandidatePool(CandidatePool cp)
        {
            if (cp != null)
            {
                var paramu = new
                {
                    CompanyID = cp.Company.CompanyID,
                    CandidateID = cp.Candidate.CandidateID,
                    CompanyPositionID = cp.CompanyPosition.CompanyPositionID,
                    Status = cp.Status
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_CandidatePoolSET";
                    CandidatePool us = cnn.QuerySingle<CandidatePool>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateCandidatePool(CandidatePool cp)
        {
            if (cp != null)
            {
                var paramu = new
                {
                    CandidatePoolID = cp.CandidatePoolID,
                    CompanyID = cp.Company.CompanyID,
                    CandidateID = cp.Candidate.CandidateID,
                    CompanyPositionID = cp.CompanyPosition.CompanyPositionID,
                    Status = cp.Status
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_CandidatePoolUpdate";
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
        public bool DeleteCandidatePool(int id)
        {
            var paramu = new { CandidatePoolID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CandidatePoolDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;
            }
        }
    }
}
