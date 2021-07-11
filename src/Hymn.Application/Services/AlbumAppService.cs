using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.Interfaces;
using Hymn.Application.ViewModels.Album;
using Hymn.Domain.Commands.Album;
using Hymn.Domain.Interfaces;
using Hymn.Infra.Data.Repository.EventSourcing;
using NetDevPack.Mediator;

namespace Hymn.Application.Services
{
    public class AlbumAppService : IAlbumAppService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;

        public AlbumAppService(IMapper mapper,
            IAlbumRepository albumRepository,
            IMediatorHandler mediatorHandler,
            IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _albumRepository = albumRepository;
            _mediatorHandler = mediatorHandler;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task<AlbumViewModel> GetById(Guid id)
        {
            return _mapper.Map<AlbumViewModel>(await _albumRepository.GetById(id));
        }

        public async Task<IEnumerable<AlbumViewModel>> GetByArtistId(Guid id)
        {
            return _mapper.Map<IEnumerable<AlbumViewModel>>(await _albumRepository.GetByArtistId(id));
        }

        public async Task<IEnumerable<AlbumViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<AlbumViewModel>>(await _albumRepository.GetAll());
        }

        public async Task<ValidationResult> Create(CreateAlbumViewModel createAlbumViewModel)
        {
            var createCommand = _mapper.Map<CreateAlbumCommand>(createAlbumViewModel);
            return await _mediatorHandler.SendCommand(createCommand);
        }

        public async Task<ValidationResult> Update(UpdateAlbumViewModel updateAlbumViewModel)
        {
            var updateCommand = _mapper.Map<UpdateAlbumCommand>(updateAlbumViewModel);
            return await _mediatorHandler.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Remove(Guid id)
        {
            var removeCommand = new RemoveAlbumCommand(id);
            return await _mediatorHandler.SendCommand(removeCommand);
        }

        public async Task<IList<AlbumHistoryData>> GetAllHistory(Guid id)
        {
            return AlbumHistory.ToJavaScriptAlbumHistory(await _eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}