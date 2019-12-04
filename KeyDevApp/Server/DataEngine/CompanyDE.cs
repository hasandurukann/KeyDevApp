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
    public class CompanyDE
    {
        public List<Company> GetCompany(int id)
        {
            var paramu = new { CompanyID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CompanyGET";
                List<Company> lstCompany = null;
                var lists = cnn.Query<Company, CompanyPosition, User, CandidatePool, Company>(sql, (c, cp, u, cp2) =>
                {
                    c.User = u;
                    c.CandidatePools = new List<CandidatePool>();
                    c.CompanyPositions = new List<CompanyPosition>();
                    c.CandidatePools.Add(cp2);
                    c.CompanyPositions.Add(cp);
                    return c;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "CompanyPositionID,UserID,CandidatePoolID");
                var result = lists.GroupBy(comp => comp.CompanyID).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.CandidatePools = g.Select(p => p.CandidatePools.Single()).ToList();
                    groupedPost.CompanyPositions = g.Select(p => p.CompanyPositions.Single()).ToList();
                    return groupedPost;
                });
                lstCompany = result.ToList<Company>();
                return lstCompany;

            }
        }
        public Company AddCompany(Company comp, User user)
        {
            var paramu = new
            {
                Title = comp.Title,
                Description = comp.Description,
                Country = comp.Country,
                Province = comp.Province,
                SubProvince = comp.SubProvince,
                Address = comp.Address,
                Phone = comp.Phone,
                Fax = comp.Fax,
                WebsiteURL = comp.WebsiteURL,
                Mail = comp.Mail
            };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CompanySET";
                Company us = cnn.QuerySingle<Company>(sql, paramu,
                    commandType: CommandType.StoredProcedure);
                UserDE usde = new UserDE();
                User sonucuser = usde.AddUser(user, null, us);
                if (sonucuser != null) { return us; }
                else { return null; }
            }
        }
        public bool UpdateCompany(Company comp)
        {
            var paramu = new
            {
                CompanyID=comp.CompanyID,
                Title = comp.Title,
                Description = comp.Description,
                Country = comp.Country,
                Province = comp.Province,
                SubProvince = comp.SubProvince,
                Address = comp.Address,
                Phone = comp.Phone,
                Fax = comp.Fax,
                WebsiteURL = comp.WebsiteURL,
                Mail = comp.Mail
            };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CompanyUpdate";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;

            }
        }
        public bool DeleteCompany(int id)
        {
            var paramu = new { CompanyID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CompanyDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;

            }
        }
    }
}
