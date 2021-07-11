using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.Interfaces;
using Hymn.Application.ViewModels.Song;
using Hymn.Domain.Commands.Song;
using Hymn.Domain.Interfaces;
using Hymn.Infra.Data.Repository.EventSourcing;
using NetDevPack.Mediator;

namespace Hymn.Application.Services
{
    public class SongAppService : ISongAppService
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ISongRepository _songRepository;

        public SongAppService(IMapper mapper,
            ISongRepository songRepository,
            IMediatorHandler mediatorHandler,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
            _mediatorHandler = mediatorHandler;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<SongViewModel> GetById(Guid id)
        {
            return _mapper.Map<SongViewModel>(await _songRepository.GetById(id));
        }

        public async Task<IEnumerable<SongViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<SongViewModel>>(await _songRepository.GetAll());
        }

        public async Task<ValidationResult> Create(CreateSongViewModel createSongViewModel)
        {
            var createCommand = _mapper.Map<CreateSongCommand>(createSongViewModel);
            return await _mediatorHandler.SendCommand(createCommand);
        }

        public async Task<ValidationResult> Update(UpdateSongViewModel updateSongViewModel)
        {
            var updateCommand = _mapper.Map<UpdateSongCommand>(updateSongViewModel);
            return await _mediatorHandler.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveSongCommand(id);
            return await _mediatorHandler.SendCommand(removeCommand);
        }

        public async Task<IList<SongHistoryData>> GetAllHistory(Guid id)
        {
            return SongHistory.ToJavaScriptSongHistory(await _eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}