using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteQuery.Tables
{
    public class Slides
    {
        public static string Id { get; private set; } = "Id";
        public static string IdTopic { get; private set; } = "Id_topic";
        public static string Content { get; private set; } = "Content";
        public static string Position { get; private set; } = "Number_slide";
        public static string TableName () => "Slides";
    }
}
