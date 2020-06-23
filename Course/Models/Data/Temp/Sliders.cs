using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Course.Models.Data.Temp
{
    public class Sliders
    {
        public static String tempWrite { get; } = @".//data/temp/tempwrite.rtf";
        public static String tempRead { get; } = @".//data/temp/tempread.rtf";


        // Время последнего обновления для других классов
        public static String lastUpdate { get { return _lastUpdate; } }

        // Коллекция слайдов
        private static BindingList<List<Byte[]>> _slides = new BindingList<List<Byte[]>>();
        // Время последнего обновления
        private static String _lastUpdate = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString();

        //
        //  Получение коллекции по номеру
        //
        public static List<Byte[]> GetSlide(Int32 index)
        {
            try
            {
                return _slides[index];
            }
            catch
            {
                Byte[] b = new Byte[] { };
                return new List<Byte[]> { b , b ,b ,b };
            }
        }
        //
        //  Количество слайдов
        //
        public static Int32 Count() => _slides.Count();

        //
        //  Добавление новой коллекции
        //
        public static void AddSlide(List<Byte[]> list)
        {
            _slides.Add(list);
            ListChanged();
        }
        //
        //  Копирование всей коллекции
        //
        public static void EditList(BindingList<List<Byte[]>> list)
        {
            _slides = list;
            ListChanged();
        }

        //
        //  Настройка коллекции
        //
        public static void Options()
        {
            _slides.RaiseListChangedEvents = true;
        }
        //
        //  Обработка изменения коллекции
        //
        private static void ListChanged()
        {
            _lastUpdate = DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToLongTimeString();
            //throw new NotImplementedException();
            // MessageBox.Show("New collection!");
        }
    }
}
