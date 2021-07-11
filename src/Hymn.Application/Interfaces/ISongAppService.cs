using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.ViewModels.Song;

namespace Hymn.Application.Interfaces
{
    public interface ISongAppService : IDisposable
    {
        Task<SongViewModel> GetById(Guid id);
        Task<IEnumerable<SongViewModel>> GetAll();

        Task<ValidationResult> Create(CreateSongViewModel createSongViewModel);
        Task<ValidationResult> Update(UpdateSongViewModel updateSongViewModel);
        Task<ValidationResult> Remove(Guid id);

        Task<IList<SongHistoryData>> GetAllHistory(Guid id);
    }
}