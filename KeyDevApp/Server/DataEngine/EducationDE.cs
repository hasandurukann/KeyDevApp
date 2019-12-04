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
    public class EducationDE
    {
        public List<Education> GetEducation(int id)
        {
            var paramu = new { EducationID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_EducationGET";
                List<Education> lstEducation = null;
                var lists = cnn.Query<Education, Candidate,Department, Education>(sql, (e, c, d) =>
                {
                    e.Department = d;
                    e.Candidate = c;
                    return e;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "CandidateID,DepartmentID");
                lstEducation = lists.ToList<Education>();
                return lstEducation;

            }
        }
        public Education AddEducation(Education e)
        {
            if (e != null)
            {
                var paramu = new
                {
                    EducationLevel = e.EducationLevel,
                    GraduationYear=e.GraduationYear,
                    GPA=e.GPA,
                    DepartmentID=e.Department.DepartmentID,
                    CandidateID=e.Candidate.CandidateID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_EducationSET";
                    Education us = cnn.QuerySingle<Education>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateEducation(Education e)
        {
            if (e != null)
            {
                var paramu = new
                {
                    EducationID = e.EducationID,
                    EducationLevel = e.EducationLevel,
                    GraduationYear = e.GraduationYear,
                    GPA = e.GPA,
                    DepartmentID = e.Department.DepartmentID,
                    CandidateID = e.Candidate.CandidateID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_EducationUpdate";
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
        public bool DeleteEducation(int id)
        {
            var paramu = new { EducationID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_EducationDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;
            }
        }
    }
}
