using Birdi.TaskManagement.Core.Config;
using Birdi.TaskManagement.Core.Entity;
using Birdi.TaskManagement.Data.Contract;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Birdi.TaskManagement.Data.Repository
{
    public class TaskRepository : ITaskRepository
    {
        public readonly AppSettings _appSettings;
        public TaskRepository(IOptions<AppSettings> appSettings)
        {
            if (appSettings.Value == null)
            {
                throw new ArgumentNullException("Config values are not configured.");
            }
            _appSettings = appSettings.Value;
        }
        public async Task Add(UserTask task)
        {
            try
            {
                var parameters = new { Id = task.Id, Title = task.Title, Description = task.Description, Duedate = task.Duedate, UserId = task.UserId };
                using (var connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.ExecuteAsync("[dbo].sp_AddTask", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var parameters = new { id };
                using (var connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.ExecuteAsync("[dbo].sp_DeleteTask", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Edit(UserTask task)
        {
            try
            {
                var parameters = new { Id = task.Id, Title = task.Title, Description = task.Description, Duedate = task.Duedate, task.StatusId };
                using (var connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    await connection.ExecuteAsync("[dbo].sp_UpdateTask", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserTask> Task(Guid id)
        {
            try
            {
                var parameters = new { id };
                using (var connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    return await connection.QueryFirstOrDefaultAsync<UserTask>("[dbo].sp_GetTask", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<UserTask>> Tasks(Guid userId)
        {
            try
            {
                var parameters = new { userId };
                using (var connection = new SqlConnection(_appSettings.ConnectionString))
                {
                    return await connection.QueryAsync<UserTask>("[dbo].sp_GetTasks", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

