using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStart_Entity.ViewModels
{
    public class TagViewModel
    {
        public Guid TagId { get; set; }
        public string TagName { get; set; }
        public string TagDescription { get; set; }


        public IEnumerable<string> CourseTitles { get; set; } // Tag'e bağlı Course bilgilerini göstermek için
    }
}

