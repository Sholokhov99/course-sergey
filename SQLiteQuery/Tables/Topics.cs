using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteQuery.Tables
{
    public class Topics
    {
        public static string Id { get; private set; } = "Id";
        public static string IdChapter { get; private set; } = "Id_chapter";
        public static string Name { get; private set; } = "Name";
        public static string Position { get; private set; } = "Position";
        public static string TableName () => "Topics";
    }
}
