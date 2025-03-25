using System;

namespace WordPressDatabaseImporter.Writers
{
    public interface IWordPressPost
    {
        int post_author { get; }
        int comment_count { get; }
        string comment_status { get; }
        UInt64 id { get; set; }
        int menu_order { get; }
        string ping_status { get; }
        string pinged { get; }
        string post_content { get; }
        string post_content_filtered { get; }
        DateTime post_date { get; }
        DateTime post_date_gmt { get; }
        DateTime post_modified { get; }
        DateTime post_modified_gmt { get; }
        string post_name { get; }
        int post_parent { get; }
        string post_password { get; }
        string post_status { get; }
        string post_title { get; }
        string post_type { get; }
        string post_mime_type { get; }
        string to_ping { get; }
        string guid { get; }
        string post_excerpt { get; }
    }
}