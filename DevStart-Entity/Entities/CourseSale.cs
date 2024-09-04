using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Entities
{
	public class CourseSale
	{
		[Key]
		public Guid CourseSaleId { get; set; }
		public DateTime CourseSaleDate { get; set; }
		public decimal CourseSaleTotalPrice { get; set; }
		public bool CourseSaleState { get; set; } = true;

		public Guid UserId { get; set; }

		public virtual List<CourseSaleDetail> CourseSaleDetails { get; set; }
	}
}
