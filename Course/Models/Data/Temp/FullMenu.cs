using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Models.Data.Temp
{
    public class FullMenu
    {
        public int ChapterId { get; set; }
        public string ChapterTitle { get; set; }
        public int ChapterNumber { get; set; }
        public int TopicId { get; set; }
        public int TopicIdChapter { get; set; }
        public string TopicName { get; set; }
        public int TopicPosition { get; set; }

        public static FullMenu Add(List<string> data)
        {
            return new FullMenu
            {
                ChapterId = Convert.ToInt32(data[0]),
                ChapterTitle = data[1],
                ChapterNumber = Convert.ToInt32(data[2]),
                TopicId = Convert.ToInt32(data[3]),
                TopicIdChapter = Convert.ToInt32(data[4]),
                TopicName = data[5],
                TopicPosition = Convert.ToInt32(data[6]),
            };
        }

        public static List<FullMenu> AddArr(List<List<string>> data)
        {
            List<FullMenu> temp = new List<FullMenu>();

            foreach (List<string> value in data)
                temp.Add(Add(value));

            return temp;
        }
    }
}
