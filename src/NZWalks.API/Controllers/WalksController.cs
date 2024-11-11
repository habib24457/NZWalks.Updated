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
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			//Map addWalksRequestDto Dto to domainmodel walk
			var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
			walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

			//map domain to dto
			var walkDtoModel = mapper.Map<WalkDto>(walkDomainModel);

			return CreatedAtAction(nameof(Create), new { id = walkDtoModel.Id, walkDtoModel });
		}


		//GET all walks
		[HttpGet]
		public async Task<IActionResult> GetAllWalk()
		{
			var walksDomainModel = await walkRepository.GetWalk();
			
			var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

			return Ok(walksDto);
		}

		//Get Single Walk by ID
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.GetById(id);
			if(walkDomainModel == null)
			{
				return NotFound();
			}

			//Map: Domain to DTO
			var walkDto = mapper.Map<WalkDto>(walkDomainModel);
			return Ok(walkDto);
		}

		//Update walk
		[HttpPut]
        [Route("{id:Guid}")]
		public async Task<IActionResult> UpdateWalk([FromBody] UpdateWalkRequestDto updateWalkRequestDto, [FromRoute]Guid id)
		{
			//Map Dto to domain
			var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

			walkDomainModel = await walkRepository.UpdateWalk(id, walkDomainModel);

			if(walkDomainModel == null)
			{
				return NotFound();
			}

			//Map: Domain to dto
			var walkDto = mapper.Map<WalkDto>(walkDomainModel);
			return Ok(walkDto);
		}


		//Delete walk
		[HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.DeleteWalk(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }

			//Map: Domain to Dto
			var result = mapper.Map<WalkDto>(walkDomainModel);

			return Ok(result);
        }
    }
}

