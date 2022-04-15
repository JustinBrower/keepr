using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class ProfilesRepository
    {

        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Account GetProfileById(string id)
        {
            string sql = @"
            SELECT
            *
            FROM accounts
            WHERE id = @id;
            ";
            return _db.QueryFirstOrDefault<Account>(sql, new { id });
        }

        internal List<Keep> GetKeepsByProfileId(string id)
        {
            string sql = @"
            SELECT
            *
            FROM keeps
            WHERE keeps.creatorId = @id;
            ";
            return _db.Query<Keep>(sql, new { id }).ToList();
        }

        internal List<Vault> GetVaultsByProfileId(string id)
        {
            string sql = @"
            SELECT
            *
            FROM vaults
            WHERE vaults.creatorId = @id
            AND vaults.isPrivate = 0;
            ";
            return _db.Query<Vault>(sql, new { id }).ToList();
        }
    }
}