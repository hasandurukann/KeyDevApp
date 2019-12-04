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
    public class DepartmentDE
    {
        public List<Department> GetDepartment(int id)
        {
            var paramu = new { DepartmentID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_DepartmentGET";
                List<Department> lstDepartment = null;
                var lists = cnn.Query<Department, Education, Institution,Department>(sql, (d, e, i) =>
                {
                    d.Institution = i;
                    d.Educations = new List<Education>();
                    d.Educations.Add(e);
                    return d;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "EducationID,InstutionID");
                var result = lists.GroupBy(d => d.DepartmentID).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Educations = g.Select(p => p.Educations.Single()).ToList();
                    return groupedPost;
                });
                lstDepartment = result.ToList<Department>();
                return lstDepartment;

            }
        }
        public Department AddDepartment(Department d)
        {
            if (d != null)
            {
                var paramu = new
                {
                    Name = d.Name,
                    InstutionID=d.Institution.InstutionID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_DepartmentSET";
                    Department us = cnn.QuerySingle<Department>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateDepartment(Department d)
        {
            if (d != null)
            {
                var paramu = new
                {
                    DepartmentID = d.DepartmentID,
                    Name = d.Name,
                    InstutionID = d.Institution.InstutionID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_DepartmentUpdate";
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
        public bool DeleteDepartment(int id)
        {
            var paramu = new { DepartmentID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_DepartmentDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;
            }
        }
    }
}
