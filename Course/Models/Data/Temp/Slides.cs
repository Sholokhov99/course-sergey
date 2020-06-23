using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Models.Data.Temp
{
    public class Slides
    {
        public int Id { get; set; }
        public int IdTopic { get; set; }
        public byte[] Content { get; set; }
        public int Position { get; set; }

        public static Slides Add(List<byte[]> data)
        {
            Slides slides = new Slides();
            slides.Id = Convert.ToInt32(Encoding.UTF8.GetString(data[0]));
            slides.IdTopic = Convert.ToInt32(Encoding.UTF8.GetString(data[1]));
            slides.Content = data[2];
            slides.Position = Convert.ToInt32(Encoding.UTF8.GetString(data[3]));

            return slides;
        }

        public static List<Slides> AddArr(List<List<byte[]>> data)
        {
            List<Slides> slides = new List<Slides>();
            foreach (List<byte[]> value in data)
            {
                slides.Add(Slides.Add(value));
            }
            return slides;
        }
    }
}
