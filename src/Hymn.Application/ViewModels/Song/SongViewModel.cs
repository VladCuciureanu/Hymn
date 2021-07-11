using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hymn.Application.ViewModels.Song
{
    public class SongViewModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}