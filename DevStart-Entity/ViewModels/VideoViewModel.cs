using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.ViewModels
{
    public class VideoViewModel
    {
        public Guid VideoId { get; set; }
        public string VideoLink { get; set; }
        public bool VideoState { get; set; }


        public IEnumerable<string> LessonTitles { get; set; } // Video'ya bağlı Lesson bilgilerini göstermek için
    }
}
