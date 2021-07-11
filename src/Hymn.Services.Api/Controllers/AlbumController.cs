using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.Interfaces;
using Hymn.Application.ViewModels.Album;
using Hymn.Infra.CrossCutting.Identity.Attributes;
using Hymn.Infra.CrossCutting.Identity.Configs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hymn.Services.Api.Controllers
{
    [Authorize]
    public class AlbumController : ApiController
    {
        private readonly IAlbumAppService _albumAppService;

        public AlbumController(IAlbumAppService albumAppService)
        {
            _albumAppService = albumAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<AlbumViewModel>> Get()
        {
            return await _albumAppService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<AlbumViewModel> Get(Guid id)
        {
            return await _albumAppService.GetById(id);
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAlbumViewModel createAlbumViewModel)
        {
            return !ModelState.IsValid
                ? CustomResponse(ModelState)
                : CustomResponse(await _albumAppService.Create(createAlbumViewModel));
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAlbumViewModel updateAlbumViewModel)
        {
            return !ModelState.IsValid
                ? CustomResponse(ModelState)
                : CustomResponse(await _albumAppService.Update(updateAlbumViewModel));
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _albumAppService.Remove(id));
        }

        [AllowAnonymous]
        [HttpGet("history/{id:guid}")]
        public async Task<IList<AlbumHistoryData>> History(Guid id)
        {
            return await _albumAppService.GetAllHistory(id);
        }
    }
}