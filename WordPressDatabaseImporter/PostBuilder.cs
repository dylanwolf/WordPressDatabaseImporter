using System;
using System.Collections.Generic;
using System.Text;
using WordPressDatabaseImporter.Writers;

namespace WordPressDatabaseImporter
{
    public class PostBuilder : IWordPressPost
    {
        internal PostBuilder(string title, string slug, int authorId, string basePath, int timeZone, string defaultStatus, string defaultCommentStatus, string defaultPingStatus, IWordPressDatabaseWriter db)
        {
            _db = db;
            _post_author = authorId;
            _post_title = title;
            _post_name = slug;
            _basePath = basePath;
            _timeZone = timeZone;
            _post_status = defaultStatus;
            _comment_status = defaultCommentStatus;
            _ping_status = defaultPingStatus;
        }

        IWordPressDatabaseWriter _db;

        UInt64? _id;
        int _timeZone;
        string _basePath;

        int _post_author;
        DateTime _post_date;
        string _post_content = string.Empty;
        string _post_title;
        string _post_status;
        string _comment_status;
        string _ping_status;
        string _post_password = string.Empty;
        string _post_name;
        string _to_ping = string.Empty;
        string _pinged = string.Empty;
        DateTime _post_modified = DateTime.Now;
        string _post_content_filtered = string.Empty;
        int _post_parent = 0;
        int _menu_order = 0;
        string _post_type = "post";
        string _post_mime_type = string.Empty;
        int _comment_count = 0;
        string _post_excerpt;

        public UInt64 id
        {
            get { return _id ?? 0; }

            set { _id = value; }
        }
        public int post_author => _post_author;
        public DateTime post_date => _post_date;
        public DateTime post_date_gmt => _post_date.AddHours(-_timeZone);
        public string post_content => _post_content;
        public string post_title => _post_title;
        public string post_status => _post_status;
        public string comment_status => _comment_status;
        public string ping_status => _ping_status;
        public string post_password => _post_password;
        public string post_name => _post_name;
        public string to_ping => _to_ping;
        public string pinged => _pinged;
        public DateTime post_modified => _post_modified;
        public DateTime post_modified_gmt => _post_modified.AddHours(-_timeZone);
        public string post_content_filtered => _post_content_filtered;
        public int post_parent => _post_parent;
        public int menu_order => _menu_order;
        public string post_type => _post_type;
        public string post_mime_type => _post_mime_type;
        public int comment_count => _comment_count;
        public string guid => $"{_basePath}?p={id}";
        public string post_excerpt => _post_excerpt ?? string.Empty;

        public PostBuilder SetStatus(string status)
        {
            _post_status = status;
            return this;
        }

        public PostBuilder SetPostExcerpt(string text)
        {
            _post_excerpt = text ?? string.Empty;

            return this;
        }

        public PostBuilder SetContent(string content)
        {
            _post_content = content;
            return this;
        }

        public PostBuilder SetPostDate(DateTime date)
        {
            _post_date = date;
            return this;
        }

        public void InsertPost()
        {
            _db.WritePost(this);
        }
    }
}
