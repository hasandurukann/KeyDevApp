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
        public bool UserSet(User u)
        {
            var paramu = new
            {
                UserID = u.UserID,
                Mail = u.Mail,
                Password = u.Password,
                Type = u.Type,
            };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_UserSET";
                int affectedRow = int.Parse(cnn.ExecuteScalar(sql, paramu,
                    commandType: CommandType.StoredProcedure).ToString());
                return affectedRow > 0 ? true : false;

            }
        }
        public bool UserDELETE(int id)
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
        public List<User> UserGet(int id)
        {
            var paramu = new { UserID = id };
            using (IDbConnection cnn = new SqlConnection(Helper.GetConnectionString()))
            {
                string sql = "dbo.SP_UserGET";
                List<User> lstUser = null;
                var lists = cnn.Query<User, Candidate, Company, User>(sql, (user, cand, comp) =>
                {
                    if (cand!=null&&cand.CandidateID!=0)
                    {user.Candidate = cand;}
                    else { user.Candidate = null; }
                    if (comp != null && comp.CompanyID != 0)
                    { user.Company = comp; }
                    else { user.Company = null; }
                    return user;
                }, paramu, commandType: CommandType.StoredProcedure, splitOn: "CandidateID,CompanyID");
                lstUser = lists.ToList<User>();
                return lstUser;

            }
        }
    }
}
