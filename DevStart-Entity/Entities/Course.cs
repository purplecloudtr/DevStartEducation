using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Entities
{
	public class Course
	{
		[Key]
		public Guid CourseId { get; set; }
		public string CourseTitle { get; set; }
		public string CourseDescription { get; set; }
		public decimal CoursePrice { get; set; }
		public DateTime CourseCreateDate { get; set; }
		public bool CourseState { get; set; } = true;
        public string PictureUrl { get; set; }
		public bool ShowCase { get; set; } = false;


        public Guid UserId { get; set; }

		public Guid CategoryId { get; set; }
		public virtual Category Category { get; set; }



		public virtual List<Review> Reviews { get; set; }
		public virtual List<Lesson> Lessons { get; set; }
		public virtual List<CourseSaleDetail> CourseSaleDetails { get; set; }
		public virtual List<Tag> Tags { get; set; }
	}
}
