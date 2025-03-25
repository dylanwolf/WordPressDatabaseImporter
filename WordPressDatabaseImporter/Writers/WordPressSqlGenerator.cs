using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace WordPressDatabaseImporter.Writers
{
    public static class WordPressSqlGenerator
    {
        public class SqlValues
        {
            public string Sql;
            public Dictionary<string, object> Parameters;
        }

        public static SqlValues ToInsertSql(this IWordPressPost post, string tablePrefix)
        {
            return new SqlValues()
            {
                Sql = $@"
INSERT INTO {tablePrefix}posts (post_author, comment_count, comment_status, id, menu_order, ping_status, pinged, post_content, post_content_filtered, post_date, post_date_gmt, post_modified, post_modified_gmt, post_name, post_parent, post_password, post_status, post_title, post_type, post_mime_type, to_ping, guid, post_excerpt)
VALUES (@post_author, @comment_count, @comment_status, @id, @menu_order, @ping_status, @pinged, @post_content, @post_content_filtered, @post_date, @post_date_gmt, @post_modified, @post_modified_gmt, @post_name, @post_parent, @post_password, @post_status, @post_title, @post_type, @post_mime_type, @to_ping, @guid, @post_excerpt)",
                Parameters = new Dictionary<string, object>()
                {
                    { "@post_author", post.post_author },
                    { "@comment_count", post.comment_count },
                    { "@comment_status", post.comment_status },
                    { "@id", post.id },
                    { "@menu_order", post.menu_order },
                    { "@ping_status", post.ping_status },
                    { "@pinged", post.pinged },
                    { "@post_content", post.post_content },
                    { "@post_content_filtered", post.post_content_filtered },
                    { "@post_date", post.post_date },
                    { "@post_date_gmt", post.post_date_gmt },
                    { "@post_modified", post.post_modified },
                    { "@post_modified_gmt", post.post_modified_gmt },
                    { "@post_name", post.post_name },
                    { "@post_parent", post.post_parent },
                    { "@post_password", post.post_password },
                    { "@post_status", post.post_status },
                    { "@post_title", post.post_title },
                    { "@post_type", post.post_type },
                    { "@post_mime_type", post.post_mime_type },
                    { "@to_ping", post.to_ping },
                    { "@guid", post.guid },
                    { "@post_excerpt", post.post_excerpt }
                }
            };
        }
    }
}
