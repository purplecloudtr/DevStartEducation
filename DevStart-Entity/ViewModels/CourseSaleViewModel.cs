using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.ViewModels
{
    public class CourseSaleViewModel
    {
        public Guid CourseSaleId { get; set; }
        public DateTime CourseSaleDate { get; set; }
        public decimal CourseSaleTotalPrice { get; set; }
        public bool CourseSaleState { get; set; }

        // UI'da User bilgilerini göstermek için
        public Guid UserId { get; set; }  //Kullanıcı bilgisi ?? UserName tanımlamış mıydık??
    }
}
