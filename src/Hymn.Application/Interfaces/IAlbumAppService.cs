using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.ViewModels.Album;

namespace Hymn.Application.Interfaces
{
    public interface IAlbumAppService : IDisposable
    {
        Task<AlbumViewModel> GetById(Guid id);
        Task<IEnumerable<AlbumViewModel>> GetByArtistId(Guid id);
        Task<IEnumerable<AlbumViewModel>> GetAll();

        Task<ValidationResult> Create(CreateAlbumViewModel createAlbumViewModel);
        Task<ValidationResult> Update(UpdateAlbumViewModel updateAlbumViewModel);
        Task<ValidationResult> Remove(Guid id);

        Task<IList<AlbumHistoryData>> GetAllHistory(Guid id);
    }
}