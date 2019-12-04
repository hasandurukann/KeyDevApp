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
    public class CompanyPositionDE
    {
        public List<CompanyPosition> GetCompanyPosition(int id)
        {
            var paramu = new { CompanyPositionID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CompanyPositionGET";
                List<CompanyPosition> lstCompanyPosition = null;
                var lists = cnn.Query<CompanyPosition, Company, CandidatePool, CompanyPosition>(sql, (cp, c, cp2) =>
                 {
                     cp.CandidatePool = cp2;
                     cp.Company = c;
                     return cp;
                 }, paramu, commandType: CommandType.StoredProcedure, splitOn: "CompanyID,CandidatePoolID");
                lstCompanyPosition = lists.ToList<CompanyPosition>();
                return lstCompanyPosition;

            }
        }
        public CompanyPosition AddCompanyPosition(CompanyPosition cp)
        {
            if (cp != null)
            {
                var paramu = new
                {
                    Title = cp.Title,
                    CompanyID = cp.Company.CompanyID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_CompanyPositionSET";
                    CompanyPosition us = cnn.QuerySingle<CompanyPosition>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateCompanyPosition(CompanyPosition cp)
        {
            if (cp != null)
            {
                var paramu = new
                {
                    CompanyPositionID = cp.CompanyPositionID,
                    Title = cp.Title,
                    CompanyID = cp.Company.CompanyID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_CompanyPositionUpdate";
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
        public bool DeleteCompanyPosition(int id)
        {
            var paramu = new { CompanyPositionID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CompanyPositionDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;

            }
        }
    }
}
