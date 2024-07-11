using Birdi.TaskManagement.Core.Config;
using Birdi.TaskManagement.Core.Entity;
using Birdi.TaskManagement.Data.Contract;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Birdi.TaskManagement.Data.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly AppSettings _appSettings;
        public UserRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

            if (_appSettings.ConnectionString == null)
            {
                throw new Exception("Error connecting database");
            }
        }
        public async Task Add(User user)
        {
            try
            {
                var parameters = new { Id = user.Id, UserName = user.UserName, Password = user.Password };
                using (var connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.ExecuteAsync("[dbo].sp_RegisterUser", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task Add(User user)
        //{
        //    try
        //    {
        //        var parameters = new { Id = user.Id, UserName = user.UserName, Password = user.Password };
        //        using (var connection = new SqlConnection(_options.ConnectionString))
        //        {
        //            await connection.ExecuteAsync("[dbo].sp_RegisterUser", parameters, commandType: CommandType.StoredProcedure);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public async Task<User> Get(string userName)
        {
            try
            {
                var parameters = new { userName = userName };
                using (var connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    return await connection.QueryFirstOrDefaultAsync<User>("[dbo].sp_GetUser", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
