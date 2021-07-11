using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.ViewModels.SongVersion;

namespace Hymn.Application.Interfaces
{
    public interface ISongVersionAppService
    {
        Task<SongVersionViewModel> GetById(Guid id);
        Task<IEnumerable<SongVersionViewModel>> GetByAlbumId(Guid id);
        Task<IEnumerable<SongVersionViewModel>> GetByArtistId(Guid id);
        Task<IEnumerable<SongVersionViewModel>> GetBySongId(Guid id);
        Task<IEnumerable<SongVersionViewModel>> GetAll();

        Task<ValidationResult> Create(CreateSongVersionViewModel createSongVersionViewModel);
        Task<ValidationResult> Update(UpdateSongVersionViewModel updateSongVersionViewModel);
        Task<ValidationResult> Remove(Guid id);

        Task<IList<SongVersionHistoryData>> GetAllHistory(Guid id);
    }
}