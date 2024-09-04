using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.Entities
{
	public class CourseSaleDetail
	{
		[Key]
		public Guid CourseSaleDetailId { get; set; }
		public bool CourseSaleDetailState { get; set; } = true;
        public int CourseSaleDetailQuantity { get; set; } //satış adedi için
        public Guid CourseSaleId { get; set; }
        public Guid CourseId { get; set; }

        public virtual CourseSale CourseSale { get; set; }		
		public virtual Course Course { get; set; }

	}
}
