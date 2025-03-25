using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace WordPressDatabaseImporter.Writers
{
    public class DbWordPressDataWriter : IWordPressDatabaseWriter
    {
        DbConnection _db;
        string _prefix;

        public DbWordPressDataWriter(DbConnection db, string tablePrefix)
        {
            _db = db;
            _prefix = tablePrefix;
        }

        protected UInt64 GetNextID()
        {
            using (var cmd = _db.CreateCommand())
            {
                cmd.CommandText = $"SELECT MAX(id) FROM {_prefix}posts";
                var result = cmd.ExecuteScalar();

                if (result == null) return 1;
                return (UInt64)result + 1;
            }
        }

        protected void Insert(string sql, Dictionary<string, object> parameters)
        {
            using (var cmd = _db.CreateCommand())
            {
                cmd.CommandText = sql;
                foreach (var p in parameters)
                {
                    var sqlParam = cmd.CreateParameter();
                    sqlParam.ParameterName = p.Key;
                    sqlParam.Value = p.Value;
                    cmd.Parameters.Add(sqlParam);
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void WritePost(IWordPressPost post)
        {
            post.id = GetNextID();
            var sql = WordPressSqlGenerator.ToInsertSql(post, _prefix);
            Insert(sql.Sql, sql.Parameters);
        }
    }
}
