using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteQuery.Tables
{
    public class Chapters
    {
        public static string Id { get; private set; } = "Id";
        public static string Title { get; private set; } = "Title";
        public static string Position { get; private set; } = "Number";

        public static string TableName() => "Chapters";
    }
}
