using System;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault GetVaultById(int id)
        {
            string sql = @"
            SELECT
            *
            FROM vaults
            WHERE id = @id;";
            return _db.QueryFirstOrDefault<Vault>(sql, new { id });
        }

        internal Vault CreateVault(Vault vaultData)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, isPrivate, creatorId)
            VALUES
            (@Name, @Description, @IsPrivate, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultData);
            vaultData.Id = id;
            return vaultData;
        }

        internal void EditVault(Vault original)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            isPrivate = @IsPrivate
            WHERE id = @Id;
            ";
            _db.Execute(sql, original);
        }

        internal string RemoveVault(int id)
        {
            string sql = @"
            DELETE
            FROM vaults
            WHERE id = @id LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { id });
            if (rowsAffected > 0)
            {
                return "delorted";
            }
            throw new Exception("Could Not Delort");
        }
    }
}