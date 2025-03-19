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
	public class WalksController(IWalkRepository _walkRepository, IMapper _mapper, IRegionRepository _regionRepository, IDifficultyRepository _difficultyRepository) : ControllerBase
	{
		/*private readonly IWalkRepository _walkRepository;
		private readonly IMapper _mapper;
		private readonly IRegionRepository _regionRepository;
		private readonly IDifficultyRepository _difficultyRepository;*/

        /*public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IDifficultyRepository difficultyRepository)
		{
			this._walkRepository = walkRepository;
			this._regionRepository = regionRepository;
			this._difficultyRepository = difficultyRepository;
			this._mapper = mapper;
		}*/

		//Create Walk
		[HttpPost]
		public async Task<IActionResult> CreateWalk([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			var regions = await _regionRepository.GetAllAsync();
			var difficulties = await _difficultyRepository.GetDifficultyAsync();
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
				
			//Map addWalksRequestDto Dto to domainmodel walk
			if (addWalkRequestDto.DifficultyId == null)
			{
				addWalkRequestDto.DifficultyId =Guid.Parse("d37fa264-31a4-4156-b010-13b52c4f6ee9");
			}
			
			var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);
			walkDomainModel = await _walkRepository.CreateWalkAsync(walkDomainModel);

			//map domain to dto
			var walkDtoModel = _mapper.Map<WalkDto>(walkDomainModel);

			return CreatedAtAction(nameof(CreateWalk), new { id = walkDtoModel.Id, walkDtoModel });
		}


		//GET all walks
		[HttpGet]
		public async Task<IActionResult> GetAllWalk()
		{
			var walksDomainModel = await _walkRepository.GetWalkAsync();
			var walksDto = _mapper.Map<List<WalkDto>>(walksDomainModel);
			return Ok(walksDto);
		}

		//Get Single Walk by ID
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
		{
			var walkDomainModel = await _walkRepository.GetWalkByIdAsync(id);
			if(walkDomainModel == null)
			{
				return NotFound();
			}
			var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
			return Ok(walkDto);
		}

		//Update walk
		[HttpPut]
        [Route("{id:Guid}")]
		public async Task<IActionResult> UpdateWalk([FromBody] UpdateWalkRequestDto updateWalkRequestDto, [FromRoute]Guid id)
		{
			var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);
			walkDomainModel = await _walkRepository.UpdateWalkAsync(id, walkDomainModel);
			if(walkDomainModel == null)
			{
				return NotFound();
			}
			var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
			return Ok(walkDto);
		}


		//Delete walk
		[HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
		{
			var walkDomainModel = await _walkRepository.DeleteWalkAsync(id);
            if (walkDomainModel == null)
            {
                return NotFound();
            }
			var result = _mapper.Map<WalkDto>(walkDomainModel);
			return Ok(result);
        }
    }
}

