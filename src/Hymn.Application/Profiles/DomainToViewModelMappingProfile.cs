using AutoMapper;
using Hymn.Application.ViewModels.Album;
using Hymn.Application.ViewModels.Artist;
using Hymn.Application.ViewModels.Song;
using Hymn.Application.ViewModels.SongVersion;
using Hymn.Domain.Models;

namespace Hymn.Application.Profiles
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Album, AlbumViewModel>();
            CreateMap<Artist, ArtistViewModel>();
            CreateMap<Song, SongViewModel>();
            CreateMap<SongVersion, SongVersionViewModel>();
        }
    }
}