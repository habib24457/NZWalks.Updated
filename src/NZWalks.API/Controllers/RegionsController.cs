using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
	//https://localhost:1234/api/regions
	[Route("api/[controller]")]
	[ApiController]
	public class RegionsController : ControllerBase
	{
		private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
		private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
			this.regionRepository = regionRepository;
			this.mapper = mapper;
        }

        [HttpGet]
		public async Task<IActionResult> GetAll()
		{
            var regionsDomain = await regionRepository.GetAllAsync();
            var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);
			return Ok(regionsDto);
		}

		//get region by id
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
		{
			var region = await regionRepository.GetByIdAsync(id);
            if (region == null) {
				return NotFound();
			}
			var regionDto = mapper.Map<RegionDTO>(region);
            return Ok(regionDto);
		}


		[HttpPost]
		public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
			regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
			var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
            return CreatedAtAction(nameof(CreateRegion), new{id = regionDto.Id},regionDto);
		}


		[HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
			if(regionDomainModel == null)
			{
				return NotFound();
			}
			regionDomainModel.Code = updateRegionRequestDto.Code;
			regionDomainModel.Name = updateRegionRequestDto.Name;
			regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
			var regionDto = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDto);
		}

		[HttpDelete]
        [Route("{id:Guid}")]
		public async Task <IActionResult> DeleteAsync([FromRoute]Guid id)
		{
			var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            var result = mapper.Map<RegionDTO>(regionDomainModel);
			return Ok(result);
        }
    }
}

