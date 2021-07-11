using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hymn.Application.EventSourcedNormalizers;
using Hymn.Application.Interfaces;
using Hymn.Application.ViewModels.Artist;
using Hymn.Infra.CrossCutting.Identity.Attributes;
using Hymn.Infra.CrossCutting.Identity.Configs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hymn.Services.Api.Controllers
{
    [Authorize]
    public class ArtistController : ApiController
    {
        private readonly IArtistAppService _artistAppService;

        public ArtistController(IArtistAppService artistAppService)
        {
            _artistAppService = artistAppService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ArtistViewModel>> Get()
        {
            return await _artistAppService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ArtistViewModel> Get(Guid id)
        {
            return await _artistAppService.GetById(id);
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateArtistViewModel createArtistViewModel)
        {
            return !ModelState.IsValid
                ? CustomResponse(ModelState)
                : CustomResponse(await _artistAppService.Create(createArtistViewModel));
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateArtistViewModel updateArtistViewModel)
        {
            return !ModelState.IsValid
                ? CustomResponse(ModelState)
                : CustomResponse(await _artistAppService.Update(updateArtistViewModel));
        }

        [AuthorizeRoles(Roles.Administrator)]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _artistAppService.Remove(id));
        }

        [AllowAnonymous]
        [HttpGet("history/{id:guid}")]
        public async Task<IList<ArtistHistoryData>> History(Guid id)
        {
            return await _artistAppService.GetAllHistory(id);
        }
    }
}