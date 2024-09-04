using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Entities
{
	public class Category
	{
		[Key]
		public Guid CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }
		public bool CategoryState { get; set; } = true;


		public virtual List<Course> Courses { get; set; }



	}
}
