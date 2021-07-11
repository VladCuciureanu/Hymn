using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.Interfaces;
using Hymn.Application.ViewModels.Song;
using Hymn.Infra.CrossCutting.Identity.Attributes;
using Hymn.Infra.CrossCutting.Identity.Configs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hymn.Services.Api.Controllers
{
    [Authorize]
    public class SongController : ApiController
    {
        private readonly ISongAppService _songAppService;

        public SongController(ISongAppService songAppService)
        {
            _songAppService = songAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<SongViewModel>> Get()
        {
            return await _songAppService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<SongViewModel> Get(Guid id)
        {
            return await _songAppService.GetById(id);
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSongViewModel createSongViewModel)
        {
            return !ModelState.IsValid
                ? CustomResponse(ModelState)
                : CustomResponse(await _songAppService.Create(createSongViewModel));
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateSongViewModel updateSongViewModel)
        {
            return !ModelState.IsValid
                ? CustomResponse(ModelState)
                : CustomResponse(await _songAppService.Update(updateSongViewModel));
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _songAppService.Remove(id));
        }

        [AllowAnonymous]
        [HttpGet("history/{id:guid}")]
        public async Task<IList<SongHistoryData>> History(Guid id)
        {
            return await _songAppService.GetAllHistory(id);
        }
    }
}