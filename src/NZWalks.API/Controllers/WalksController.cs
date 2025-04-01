using System;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Models.Domain;
using AutoMapper;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IDifficultyRepository difficultyRepository) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			var regions = await regionRepository.GetAllAsync();
			var difficulties = await difficultyRepository.GetDifficultyAsync();
			var checkRegion = regions.FirstOrDefault(x => x.Id == addWalkRequestDto.RegionId);
			var checkDifficulty = difficulties.FirstOrDefault(x => x.Id == addWalkRequestDto.DifficultyId);
			
			if (checkRegion == null)
			{
				addWalkRequestDto.RegionId = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6");
			}

			if (checkDifficulty == null)
			{
				addWalkRequestDto.DifficultyId = Guid.Parse("d37fa264-31a4-4156-b010-13b52c4f6ee9");
			}
				
			if (addWalkRequestDto.DifficultyId == null)
			{
				addWalkRequestDto.DifficultyId =Guid.Parse("d37fa264-31a4-4156-b010-13b52c4f6ee9");
			}
			
			var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
			walkDomainModel = await walkRepository.CreateWalkAsync(walkDomainModel);

			//map domain to dto
			var walkDtoModel = mapper.Map<WalkDto>(walkDomainModel);

			return CreatedAtAction(nameof(CreateWalk), new { id = walkDtoModel.Id, walkDtoModel });
		}
		
		[HttpGet]
		public async Task<IActionResult> GetAllWalk()
		{
			var walksDomainModel = await walkRepository.GetWalkAsync();
			var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);
			return Ok(walksDto);
		}

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

