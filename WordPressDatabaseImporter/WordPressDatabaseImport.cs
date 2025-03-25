using System;
using System.Data.Common;
using WordPressDatabaseImporter.Writers;

namespace WordPressDatabaseImporter
{
    public class WordPressDatabaseImport
    {
        IWordPressDatabaseWriter _db;
        int _timeZone = 0;
        string _baseUrl = "http://localhost/";
        int _authorId;

        // TODO: Allow these to be changed
        string _defaultStatus = "draft";
        string _defaultCommentStatus = "open";
        string _defaultPingStatus = "open";

        public WordPressDatabaseImport(IWordPressDatabaseWriter db, string baseUrl, int authorId, int timeZone)
        {
            _db = db;
            _timeZone = timeZone;
            _baseUrl = baseUrl;
            _authorId = authorId;
        }

        public WordPressDatabaseImport ChangeDefaultPostStatus(string status)
        {
            this._defaultStatus = status;
            return this;
        }

        public PostBuilder CreatePost(string title, string slug)
        {
            return new PostBuilder(title, slug, _authorId, _baseUrl, _timeZone, _defaultStatus, _defaultCommentStatus, _defaultPingStatus, _db);
        }
    }
}
