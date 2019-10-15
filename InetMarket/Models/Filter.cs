﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    public class Filter
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина")]
        public string Title { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<FilterAddititon> FilterAddititons { get; set; }
    }
}
