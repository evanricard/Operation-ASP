using Dapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OperationASP
{
    class CoreUserStore : IUserPasswordStore<CoreUser>, IUserStore<CoreUser>
    {
        public async Task<IdentityResult> CreateAsync(CoreUser user, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "insert into CoreUsers[Id]," +
                    "[UserName]," +
                    "[NormalizedUserName]," +
                    "[PasswordHash]," +
                    "Values(@id,@userName,@normalizedUserName, @passwordHash)",
                    new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    }
                   );
            }

            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(CoreUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        /*Neat*/
        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection("Server=localhost;" + "Database=master;" + " Trusted_Connection=True;");
            connection.Open();

            return connection;
        }

        public async Task<CoreUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<CoreUser>("select * From CoreUser where Id = @id", new { id = userId });
            }
        }

        public Task<CoreUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            ;
        }

        public Task<string> GetNormalizedUserNameAsync(CoreUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(CoreUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(CoreUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(CoreUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(CoreUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(CoreUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(CoreUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(CoreUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(CoreUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
    }
}
