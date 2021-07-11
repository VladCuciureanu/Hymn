using AutoMapper;
using Hymn.Application.ViewModels.Album;
using Hymn.Application.ViewModels.Artist;
using Hymn.Application.ViewModels.Song;
using Hymn.Application.ViewModels.SongVersion;
using Hymn.Domain.Commands.Album;
using Hymn.Domain.Commands.Artist;
using Hymn.Domain.Commands.Song;
using Hymn.Domain.Commands.SongVersion;

namespace Hymn.Application.Profiles
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            // Album
            CreateMap<CreateAlbumViewModel, CreateAlbumCommand>()
                .ConstructUsing(e => new CreateAlbumCommand(e.ArtistId, e.Name));
            CreateMap<UpdateAlbumViewModel, UpdateAlbumCommand>()
                .ConstructUsing(e => new UpdateAlbumCommand(e.Id, e.ArtistId, e.Name));

            // Artist
            CreateMap<CreateArtistViewModel, CreateArtistCommand>()
                .ConstructUsing(e => new CreateArtistCommand(e.Name));
            CreateMap<UpdateArtistViewModel, UpdateArtistCommand>()
                .ConstructUsing(e => new UpdateArtistCommand(e.Id, e.Name));

            // Song
            CreateMap<CreateSongViewModel, CreateSongCommand>()
                .ConstructUsing(e => new CreateSongCommand(e.Name));
            CreateMap<UpdateSongViewModel, UpdateSongCommand>()
                .ConstructUsing(e => new UpdateSongCommand(e.Id, e.Name));

            // SongVersion
            CreateMap<CreateSongVersionViewModel, CreateSongVersionCommand>()
                .ConstructUsing(e =>
                    new CreateSongVersionCommand(e.AlbumId, e.ArtistId, e.SongId, e.Content, e.DefaultKey, e.Name, e.Views));
            CreateMap<UpdateSongVersionViewModel, UpdateSongVersionCommand>()
                .ConstructUsing(e =>
                    new UpdateSongVersionCommand(e.Id, e.AlbumId, e.ArtistId, e.SongId, e.Content, e.DefaultKey, e.Name,
                        e.Views));
        }
    }
}