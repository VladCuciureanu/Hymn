using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.Interfaces;
using Hymn.Application.ViewModels.Artist;
using Hymn.Domain.Commands.Artist;
using Hymn.Domain.Interfaces;
using Hymn.Infra.Data.Repository.EventSourcing;
using NetDevPack.Mediator;

namespace Hymn.Application.Services
{
    public class ArtistAppService : IArtistAppService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;

        public ArtistAppService(IMapper mapper,
            IArtistRepository artistRepository,
            IMediatorHandler mediatorHandler,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
            _mediatorHandler = mediatorHandler;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<ArtistViewModel> GetById(Guid id)
        {
            return _mapper.Map<ArtistViewModel>(await _artistRepository.GetById(id));
        }

        public async Task<IEnumerable<ArtistViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ArtistViewModel>>(await _artistRepository.GetAll());
        }

        public async Task<ValidationResult> Create(CreateArtistViewModel createArtistViewModel)
        {
            var createCommand = _mapper.Map<CreateArtistCommand>(createArtistViewModel);
            return await _mediatorHandler.SendCommand(createCommand);
        }

        public async Task<ValidationResult> Update(UpdateArtistViewModel updateArtistViewModel)
        {
            var updateCommand = _mapper.Map<UpdateArtistCommand>(updateArtistViewModel);
            return await _mediatorHandler.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveArtistCommand(id);
            return await _mediatorHandler.SendCommand(removeCommand);
        }

        public async Task<IList<ArtistHistoryData>> GetAllHistory(Guid id)
        {
            return ArtistHistory.ToJavaScriptArtistHistory(await _eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}