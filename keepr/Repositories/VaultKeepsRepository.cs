using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<Keep> GetKeepsByVaultId(int id)
        {
            string sql = @"
            SELECT
            *
            FROM vaultKeeps
            WHERE vaultKeeps.vaultId = @id;
            ";
            return _db.Query<Keep>(sql, new { id }).ToList();
        }

        internal VaultKeep CreateVaultKeeps(VaultKeep vaultKeepData)
        {
            string sql = @"
            INSERT INTO vaultKeeps
            (vaultId, keepId, creatorId)
            VALUES
            (@VaultId, @KeepId, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
            vaultKeepData.Id = id;
            return vaultKeepData;
        }

        internal VaultKeep GetVaultKeepById(int id)
        {
            string sql = @"
            SELECT
            *
            FROM vaultKeeps
            WHERE id = @id;";
            return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
        }

        internal string RemoveVaultKeep(int id)
        {
            string sql = @"
            DELETE
            FROM vaultKeeps
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