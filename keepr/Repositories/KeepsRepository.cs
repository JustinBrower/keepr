using System;
using System.Data;
using keepr.Models;
using System.Collections.Generic;
using Dapper;
using System.Linq;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, views, kept, creatorId)
            VALUES
            (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, keepData);
            keepData.Id = id;
            return keepData;
        }

        internal List<Keep> GetKeepsByVaultIdNow(int id)
        {
            throw new NotImplementedException();
        }

        internal List<Keep> GetAllKeeps()
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id;
            ";
            return _db.Query<Keep, Account, Keep>(sql, (keep, acc) =>
            {
                keep.Creator = acc;
                return keep;
            }
            ).ToList();
        }

        internal Keep GetKeepById(int id)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @id;
            ";
            return _db.Query<Keep, Account, Keep>(sql, (keep, acc) =>
            {
                keep.Creator = acc;
                return keep;
            },
            new { id }).FirstOrDefault();
        }

        internal void EditKeep(Keep original)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img,
            views = @Views,
            kept = @Kept
            WHERE id = @Id;
            ";
            _db.Execute(sql, original);
        }

        internal string RemoveKeep(int id)
        {
            string sql = @"
            DELETE
            FROM keeps
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