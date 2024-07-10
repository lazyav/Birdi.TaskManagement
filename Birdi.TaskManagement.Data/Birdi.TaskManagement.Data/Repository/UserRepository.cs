using Birdi.TaskManagement.Core;
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

        private readonly AppSettings _options;
        public UserRepository(IOptions<AppSettings> options)
        {
            _options = options.Value;

            if (_options.ConnectionString == null)
            {
                throw new Exception("Error connecting database");
            }
        }
        public async Task Add(User user)
        {
            try
            {
                var parameters = new { Id = user.Id, UserName = user.UserName, Password = user.Password };
                using (var connection = new SqlConnection(_options.ConnectionString))
                {
                    await connection.ExecuteAsync("[dbo].sp_RegisterUser", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
