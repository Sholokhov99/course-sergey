using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Models.Data.Temp
{
    public class Topics
    {
        public int Id { get; set; }
        public int IdChapter { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }

        public static Topics Add(List<string> data)
        {
            return new Topics()
            {
                Id = Convert.ToInt32(data[0]),
                IdChapter = Convert.ToInt32(data[1]),
                Name = data[2],
                Position = Convert.ToInt32(data[3]),
            };
        }

        public static List<Topics> AddArr(List<List<string>> data)
        {
            List<Topics> topics = new List<Topics>();
            foreach (List<string> value in data)
            {
                topics.Add(Topics.Add(value));
            }
            return topics;
        }
    }
}
