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
        // FIXME NEED TO POPULATE CREATOR ON ALL THE THINGS

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
        internal List<Keep> GetAllKeeps()
        {
            string sql = @"
            SELECT
            *
            FROM keeps
            ";
            return _db.Query<Keep>(sql).ToList();
        }

        internal Keep GetKeepById(int id)
        {
            string sql = @"
            SELECT
            *
            FROM keeps
            WHERE id = @id;";
            return _db.QueryFirstOrDefault<Keep>(sql, new { id });
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