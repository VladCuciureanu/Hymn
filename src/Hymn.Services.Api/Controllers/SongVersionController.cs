using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.Interfaces;
using Hymn.Application.ViewModels.SongVersion;
using Hymn.Infra.CrossCutting.Identity.Attributes;
using Hymn.Infra.CrossCutting.Identity.Configs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hymn.Services.Api.Controllers
{
    [Authorize]
    public class SongVersionController : ApiController
    {
        private readonly ISongVersionAppService _songVersionAppService;

        public SongVersionController(ISongVersionAppService songVersionAppService)
        {
            _songVersionAppService = songVersionAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<SongVersionViewModel>> Get()
        {
            return await _songVersionAppService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<SongVersionViewModel> Get(Guid id)
        {
            return await _songVersionAppService.GetById(id);
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateSongVersionViewModel createSongVersionViewModel)
        {
            return !ModelState.IsValid
                ? CustomResponse(ModelState)
                : CustomResponse(await _songVersionAppService.Create(createSongVersionViewModel));
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateSongVersionViewModel updateSongVersionViewModel)
        {
            return !ModelState.IsValid
                ? CustomResponse(ModelState)
                : CustomResponse(await _songVersionAppService.Update(updateSongVersionViewModel));
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _songVersionAppService.Remove(id));
        }

        [AllowAnonymous]
        [HttpGet("history/{id:guid}")]
        public async Task<IList<SongVersionHistoryData>> History(Guid id)
        {
            return await _songVersionAppService.GetAllHistory(id);
        }
    }
}