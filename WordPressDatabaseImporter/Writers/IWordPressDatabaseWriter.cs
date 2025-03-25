using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressDatabaseImporter.Writers
{
    public interface IWordPressDatabaseWriter
    {
        void WritePost(IWordPressPost post);
    }
}
