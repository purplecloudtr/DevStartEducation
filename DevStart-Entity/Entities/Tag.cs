using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Entities
{
	public class Tag
	{
		public Guid TagId { get; set; }
		public string TagName { get; set; }
		public string TagDescription { get; set; }

		public virtual List<Course> Courses { get; set; }

	}
}
