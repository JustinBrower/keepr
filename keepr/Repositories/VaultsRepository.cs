using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.id = @id;
            ";
            return _db.Query<Vault, Account, Vault>(sql, (vault, acc) =>
            {
                vault.Creator = acc;
                return vault;
            },
            new { id }).FirstOrDefault();
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

        internal List<Vault> GetMyVaults(string id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = @id;";
            return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
            {
                vault.Creator = account;
                return vault;
            },
             new { id }).ToList();
        }


    }
}