using System;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Domain;
using AutoMapper;

namespace NZWalks.API.Controllers
{
	// /api/walks
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		private readonly IWalkRepository walkRepository;
		private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
		{
			this.walkRepository = walkRepository;
			this.mapper = mapper;
		}

		//Create Walk
		[HttpPost]
		public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			//Map addWalksRequestDto Dto to domainmodel walk
			var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
			walkDomainModel = await walkRepository.CreateWalkAsync(walkDomainModel);

			//map domain to dto
			var walkDtoModel = mapper.Map<WalkDto>(walkDomainModel);

			return CreatedAtAction(nameof(CreateWalk), new { id = walkDtoModel.Id, walkDtoModel });
		}


		//GET all walks
		[HttpGet]
		public async Task<IActionResult> GetAllWalk()
		{
			var walksDomainModel = await walkRepository.GetWalkAsync();
			var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);
			return Ok(walksDto);
		}

		//Get Single Walk by ID
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.GetWalkByIdAsync(id);
			if(walkDomainModel == null)
			{
				return NotFound();
			}
			var walkDto = mapper.Map<WalkDto>(walkDomainModel);
			return Ok(walkDto);
		}

		//Update walk
		[HttpPut]
        [Route("{id:Guid}")]
		public async Task<IActionResult> UpdateWalk([FromBody] UpdateWalkRequestDto updateWalkRequestDto, [FromRoute]Guid id)
		{
			var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
			walkDomainModel = await walkRepository.UpdateWalkAsync(id, walkDomainModel);
			if(walkDomainModel == null)
			{
				return NotFound();
			}
			var walkDto = mapper.Map<WalkDto>(walkDomainModel);
			return Ok(walkDto);
		}


		//Delete walk
		[HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.DeleteWalkAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
			var result = mapper.Map<WalkDto>(walkDomainModel);
			return Ok(result);
        }
    }
}

