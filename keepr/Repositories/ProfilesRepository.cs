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
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.creatorId = @id;
            ";
            return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
            {
                keep.Creator = account;
                return keep;
            },
             new { id }).ToList();
        }

        internal List<Vault> GetVaultsByProfileId(string id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.creatorId = @id
            AND v.isPrivate = 0;
            ";
            return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
            {
                vault.Creator = account;
                return vault;
            },
             new { id }).ToList();
        }
    }
}