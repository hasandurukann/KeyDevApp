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
    public class UserDE
    {
        public List<User> GetUser(int id)
        {
            var paramu = new { UserID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_UserGET";
                List<User> lstUser = null;
                var lists = cnn.Query<User, Candidate, Company, User>(sql, (user, cand, comp) =>
                {
                    if (cand != null && cand.CandidateID != 0)
                    { user.Candidate = cand; }
                    else
                    {
                        user.Candidate = null;
                        if (comp != null && comp.CompanyID != 0)
                        { user.Company = comp; }
                        else { user.Company = null; }
                    }

                    return user;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "CandidateID,CompanyID");
                lstUser = lists.ToList<User>();
                return lstUser;

            }
        }
        public User AddUser(User user, Candidate cand, Company comp)
        {
            var paramu = new
            {
                Mail = user.Mail,
                Password = user.Password,
                Type = user.Type,
                CandidateID = user.Type ? 0 : (cand != null ? cand.CandidateID : 0),
                CompanyID = !user.Type ? 0 : (comp != null ? comp.CompanyID : 0)
            };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_UserSET";
                User us = cnn.QuerySingle<User>(sql, paramu,
                    commandType: CommandType.StoredProcedure);
                return us;

            }
        }
        public bool UpdateUser(User user, Candidate cand, Company comp)
        {
            var paramu = new
            {
                UserID = user.UserID,
                Mail = user.Mail,
                Password = user.Password,
                Type = user.Type,
                CandidateID = user.Type ? 0 : (cand != null ? cand.CandidateID : 0),
                CompanyID = !user.Type ? 0 : (comp != null ? comp.CompanyID : 0)
            };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_UserUpdate";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;

            }
        }
        public bool DeleteUser(int id)
        {
            var paramu = new { UserID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_UserDELETE";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;

            }
        }

        public User UserTryLogin(User u)
        {
            var paramu = new
            {
                Mail = u.Mail,
                //Password = Helper.HashPassword(u.Password),
                Password = u.Password
            };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_UserLogin";
                User user = cnn.Query<User>(sql, paramu,
                    commandType: CommandType.StoredProcedure).Single();
                return user;

            }
        }
    }
}
