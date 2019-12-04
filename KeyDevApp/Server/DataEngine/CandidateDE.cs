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
    public class CandidateDE
    {
        public List<Candidate> GetCandidate(int id)
        {
            var paramu = new { CandidateID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CandidateGET";
                List<Candidate> lstCandidate = null;
                var lists = cnn.Query<Candidate, CandidateKeyword, Keyword, Experience,CandidatePool,Education,User,Candidate>(sql, (c, ck, k,e,cp,e2,u) =>
                {
                    c.User = u;
                    ck.Keyword = k;
                    c.CandidateKeywords = new List<CandidateKeyword>();
                    c.CandidateKeywords.Add(ck);
                    c.Experiences = new List<Experience>();
                    c.Experiences.Add(e);
                    c.CandidatePools = new List<CandidatePool>();
                    c.CandidatePools.Add(cp);
                    c.Educations = new List<Education>();
                    c.Educations.Add(e2);
                    return c;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "Grade,KeywordID,ExperienceID,CandidatePoolID,EducationID,UserID");
                var result = lists.GroupBy(cand => cand.CandidateID).Select(g =>
                {
                    var groupedPost = g.First();
                    groupedPost.Experiences = g.Select(p => p.Experiences.Single()).ToList();
                    groupedPost.CandidateKeywords = g.Select(p => p.CandidateKeywords.Single()).ToList();
                    groupedPost.CandidatePools = g.Select(p => p.CandidatePools.Single()).ToList();
                    groupedPost.Educations = g.Select(p => p.Educations.Single()).ToList();
                    return groupedPost;
                });
                lstCandidate = result.ToList<Candidate>();
                return lstCandidate;

            }
        }
        public Candidate AddCandidate(Candidate cand, User user)
        {
            var paramu = new
            {
                Name = cand.Name,
                Phone = cand.Phone,
                Country = cand.Country,
                Province = cand.Province,
                SubProvince = cand.SubProvince,
                DateOfBirth = cand.DateOfBirth,
                PhotoPath = cand.PhotoPath
            };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CandidateSET";
                Candidate us = cnn.QuerySingle<Candidate>(sql, paramu,
                    commandType: CommandType.StoredProcedure);
                UserDE usde = new UserDE();
                User sonucuser=usde.AddUser(user, us, null);
                if (sonucuser!=null){return us;}
                else{return null;}
            }
        }
        public bool UpdateCandidate(Candidate cand)
        {
            var paramu = new
            {
                CandidateID=cand.CandidateID,
                Name = cand.Name,
                Password = cand.Phone,
                Country = cand.Country,
                Province = cand.Province,
                SubProvince = cand.SubProvince,
                DateOfBirth = cand.DateOfBirth,
                PhotoPath = cand.PhotoPath
            };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CandidateUpdate";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;

            }
        }
        public bool DeleteCandidate(int id)
        {
            var paramu = new { CandidateID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_CandidateDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;

            }
        }
    }
}
