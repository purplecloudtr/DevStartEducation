using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Entities
{
	public class Review
	{
		[Key]
		public Guid ReviewId { get; set; }
		public int ReviewRating { get; set; }
		public DateTime ReviewDate { get; set; }
		public string ReviewComment { get; set; }
		public bool ReviewState { get; set; } = true;

		public Guid UserId { get; set; }


		public Guid CourseId { get; set; }
		public virtual Course Course { get; set; }

	}
}
