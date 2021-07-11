using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.Interfaces;
using Hymn.Application.ViewModels.SongVersion;
using Hymn.Domain.Commands.SongVersion;
using Hymn.Domain.Interfaces;
using Hymn.Infra.Data.Repository.EventSourcing;
using NetDevPack.Mediator;

namespace Hymn.Application.Services
{
    public class SongVersionAppService : ISongVersionAppService
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly ISongVersionRepository _songVersionRepository;

        public SongVersionAppService(IMapper mapper,
            ISongVersionRepository songVersionRepository,
            IMediatorHandler mediatorHandler,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _songVersionRepository = songVersionRepository;
            _mediatorHandler = mediatorHandler;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<SongVersionViewModel> GetById(Guid id)
        {
            return _mapper.Map<SongVersionViewModel>(await _songVersionRepository.GetById(id));
        }

        public async Task<IEnumerable<SongVersionViewModel>> GetByAlbumId(Guid id)
        {
            return _mapper.Map<IEnumerable<SongVersionViewModel>>(await _songVersionRepository.GetByAlbumId(id));
        }

        public async Task<IEnumerable<SongVersionViewModel>> GetByArtistId(Guid id)
        {
            return _mapper.Map<IEnumerable<SongVersionViewModel>>(await _songVersionRepository.GetByArtistId(id));
        }

        public async Task<IEnumerable<SongVersionViewModel>> GetBySongId(Guid id)
        {
            return _mapper.Map<IEnumerable<SongVersionViewModel>>(await _songVersionRepository.GetBySongId(id));
        }

        public async Task<IEnumerable<SongVersionViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<SongVersionViewModel>>(await _songVersionRepository.GetAll());
        }

        public async Task<ValidationResult> Create(CreateSongVersionViewModel createSongVersionViewModel)
        {
            var createCommand = _mapper.Map<CreateSongVersionCommand>(createSongVersionViewModel);
            return await _mediatorHandler.SendCommand(createCommand);
        }

        public async Task<ValidationResult> Update(UpdateSongVersionViewModel updateSongVersionViewModel)
        {
            var updateCommand = _mapper.Map<UpdateSongVersionCommand>(updateSongVersionViewModel);
            return await _mediatorHandler.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveSongVersionCommand(id);
            return await _mediatorHandler.SendCommand(removeCommand);
        }

        public async Task<IList<SongVersionHistoryData>> GetAllHistory(Guid id)
        {
            return SongVersionHistory.ToJavaScriptSongVersionHistory(await _eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}