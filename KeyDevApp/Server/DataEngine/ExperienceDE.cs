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
    public class ExperienceDE
    {
        public List<Experience> GetExperience(int id)
        {
            var paramu = new { ExperienceID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_ExperienceGET";
                List<Experience> lstExperience = null;
                var lists = cnn.Query<Experience, Candidate, Experience>(sql, (e, c) =>
                {
                    e.Candidate = c;
                    return e;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "CandidateID");
                lstExperience = lists.ToList<Experience>();
                return lstExperience;

            }
        }
        public Experience AddExperience(Experience e)
        {
            if (e != null)
            {
                var paramu = new
                {
                    CompanyName = e.CompanyName,
                    Position = e.Position,
                    StartingDate = e.StartingDate,
                    EndingDate = e.EndingDate,
                    Description = e.Description,
                    CandidateID = e.Candidate.CandidateID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_ExperienceSET";
                    Experience us = cnn.QuerySingle<Experience>(sql, paramu,
                        commandType: CommandType.StoredProcedure);
                    return us;
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateExperience(Experience e)
        {
            if (e != null)
            {
                var paramu = new
                {
                    ExperienceID = e.ExperienceID,
                    CompanyName = e.CompanyName,
                    Position = e.Position,
                    StartingDate = e.StartingDate,
                    EndingDate = e.EndingDate,
                    Description = e.Description,
                    CandidateID = e.Candidate.CandidateID
                };
                using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
                {
                    string sql = "dbo.SP_ExperienceUpdate";
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
        public bool DeleteExperience(int id)
        {
            var paramu = new { ExperienceID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_ExperienceDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;
            }
        }
    }
}
