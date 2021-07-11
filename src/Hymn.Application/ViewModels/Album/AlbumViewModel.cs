using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hymn.Application.ViewModels.Album
{
    public class AlbumViewModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Artist Id")]
        public Guid ArtistId { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}