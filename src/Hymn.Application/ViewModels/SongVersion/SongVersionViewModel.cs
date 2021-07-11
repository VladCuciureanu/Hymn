using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hymn.Application.ViewModels.SongVersion
{
    public class SongVersionViewModel
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [DisplayName("Album Id")]
        public Guid? AlbumId { get; set; }
        
        [DisplayName("Artist Id")]
        public Guid? ArtistId { get; set; }

        [Required(ErrorMessage = "The Song Id is Required")]
        [DisplayName("Song Id")]
        public Guid SongId { get; set; }

        [Required(ErrorMessage = "The Content is Required")]
        [DisplayName("Content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "The Default Key is Required")]
        [DisplayName("Default Key")]
        public int DefaultKey { get; set; }

        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Views count is Required")]
        [DisplayName("Views")]
        public int Views { get; set; }
    }
}