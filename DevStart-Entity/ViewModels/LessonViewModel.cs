using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.ViewModels
{
    public class LessonViewModel
    {
        public Guid LessonId { get; set; }
        public string LessonTitle { get; set; }
        public string LessonContent { get; set; }
        public bool LessonState { get; set; } = true;

        public Guid CourseId { get; set; }
        // public string CourseTitle { get; set; } //Course
        public string VideoLink { get; set; }
    }
}
