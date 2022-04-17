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

        internal List<VaultKeepViewModel> GetKeepsByVaultId(int id)
        {
            string sql = @"
            SELECT
            a.*,
            k.*,
            vk.id AS VaultKeepId
            FROM vaultKeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN accounts a ON a.id = k.creatorId
            WHERE vk.vaultId = @id;
            ";
            return _db.Query<Account, VaultKeepViewModel, VaultKeepViewModel>(sql, (acc, keep) =>
            {
                keep.Creator = acc;
                return keep;
            },
             new { id }).ToList();
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