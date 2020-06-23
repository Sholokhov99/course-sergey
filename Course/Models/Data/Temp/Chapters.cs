using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Models.Data.Temp
{
    public class Chapters
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }

        public static Chapters Add(List<string> data)
        {
            return new Chapters()
            {
                Id = Convert.ToInt32(data[0]),
                Title = data[1],
                Position = Convert.ToInt32(data[2]),
            };
        }

        public static List<Chapters> AddArr(List<List<string>> data)
        {
            List<Chapters> chapters = new List<Chapters>();
            foreach (List<string> value in data)
            {
                chapters.Add(Chapters.Add(value));
            }
            return chapters;
        }
    }
}
