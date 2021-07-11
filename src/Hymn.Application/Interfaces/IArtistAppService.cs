using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.ViewModels.Artist;

namespace Hymn.Application.Interfaces
{
    public interface IArtistAppService : IDisposable
    {
        Task<ArtistViewModel> GetById(Guid id);
        Task<IEnumerable<ArtistViewModel>> GetAll();

        Task<ValidationResult> Create(CreateArtistViewModel createArtistViewModel);
        Task<ValidationResult> Update(UpdateArtistViewModel updateArtistViewModel);
        Task<ValidationResult> Remove(Guid id);

        Task<IList<ArtistHistoryData>> GetAllHistory(Guid id);
    }
}