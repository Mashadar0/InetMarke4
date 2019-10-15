using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InetMarket.Models
{
    //������� ����������� ���������
    public class Provider
    {
        public int Id { get; set; }

        [Display(Name = "��������")]
        [Required(ErrorMessage = "�� ������� ��������")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "������������ �����")]
        public string Title { get; set; }

        [Display(Name = "���������")]
        public string Product { get; set; }

        [Display(Name = "����������")]
        public int Count { get; set; }

        [Display(Name = "����")]
        public string Price { get; set; }

        [Display(Name = "����")]
        public DateTime DateTime { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
