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
    public class InstitutionDE
    {
        public List<Institution> GetInstitution(int id)
        {
            var paramu = new { InstitutionID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_InstutionGET";
                List<Institution> lstInstitution = null;
                var lists = cnn.Query<Institution, Department, Institution>(sql, (i, d) =>
                {
                    i.Departments = new List<Department>();
                    i.Departments.Add(d);
                    return i;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "DepartmentID");
                var result = lists.GroupBy(i => i.InstutionID).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Departments = g.Select(p => p.Departments.Single()).ToList();
                    return groupedPost;
                });
                lstInstitution = result.ToList<Institution>();
                return lstInstitution;

            }
        }
        public Institution AddInstitution(Institution i)
        {
            if (i != null)
            {
                var paramu = new
                {
                    Name = i.Name
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_InstutionSET";
                    Institution us = cnn.QuerySingle<Institution>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateInstitution(Institution i)
        {
            if (i != null)
            {
                var paramu = new
                {
                    InstitutionID = i.InstutionID,
                    Name = i.Name
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_InstutionUpdate";
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
        public bool DeleteInstitution(int id)
        {
            var paramu = new { InstutionID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_InstitutionDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;
            }
        }
    }
}
