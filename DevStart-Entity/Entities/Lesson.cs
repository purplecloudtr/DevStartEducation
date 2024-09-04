using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Entities
{
	public class Lesson
	{
		[Key]
		public Guid LessonId { get; set; }
		public string LessonTitle { get; set; }
		public string LessonContent { get; set; }

        public string VideoLink { get; set; }

        public bool LessonState { get; set; } = true;

		public Guid CourseId { get; set; }

		public virtual Course Course { get; set; }

		//public Guid VideoId { get; set; }
		//public virtual Video Video { get; set; }

	}
}
