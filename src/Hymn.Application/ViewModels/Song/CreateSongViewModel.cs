using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hymn.Application.ViewModels.Song
{
    public class CreateSongViewModel
    {
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}