using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Entities
{
	public class Video
	{
		[Key]
		public Guid VideoId { get; set; }
		public string VideoLink { get; set; }
		public bool VideoState { get; set; } = true;

		//public virtual List<Lesson> Lessons { get; set; }
	}
}
