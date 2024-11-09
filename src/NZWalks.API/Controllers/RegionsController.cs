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
            /*DB connection without following the repository design pattern
			 * get the datas from DB - using this domain model
			 * var regionsDomain = await dbContext.Regions.ToListAsync();
			 */

            /*following the repository design pattern*/
            var regionsDomain = await regionRepository.GetAllAsync();

            /*Without the async await the code would have been like this#
             * method name: public IActionResult GetAll()...
			 * var regionsDomain = dbContext.Regions.ToList();
			 */

            /*Map domain models to DTOs and return the DTOs to the client*/

            /*Mapping without automapper
            var regionsDto = new List<RegionDTO>();
			foreach(var region in regionsDomain) {
				regionsDto.Add(new RegionDTO()
				{
					Id = region.Id,
					Code = region.Code,
					Name = region.Name,
					RegionImageUrl = region.RegionImageUrl
				});
			}
			*/

            /*Mapping with automapper: Map Domain to DTO
			 * mapper.Map<NeedAnArray<DestinationType>>(SourceType);
			 */
            var regionsDto = mapper.Map<List<RegionDTO>>(regionsDomain);

			return Ok(regionsDto);
		}

		//get region by id
		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
		{

			//var region = dbContext.Regions.Find(id);

			var region = await regionRepository.GetByIdAsync(id);

            /*another way of finding single property
			* Find only takes the primary key as parameter, 
			* FirstOrDefaul takes other property as well as parameter
			* */
            //var region1 = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null) {
				return NotFound();
			}


			/*Map Region Domain Model to RegionDTO
			var regionDto = new RegionDTO
			{
				Id = region.Id,
				Code = region.Code,
				Name = region.Name,
				RegionImageUrl = region.RegionImageUrl
			};
			*/

			/*Mapping with automapper
			 * mapper.Map<DestinationType>(SourceType);
			 */
			var regionDto = mapper.Map<RegionDTO>(region);


            return Ok(regionDto);
		}


		[HttpPost]
		public async Task<IActionResult> CreateRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
		{
			/*Mapping without automapper
            var regionDomainModel = new Region {
			Code = addRegionRequestDto.Code,
			Name = addRegionRequestDto.Name,
			RegionImageUrl = addRegionRequestDto.RegionImageUrl
			};
            */

			/*Automapper: From DTO to Domain
			 * mapper.Map<DestinationModel>(SourceModel);
			 */
			var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);


            /*DB Connection without using the repository design pattern
             * dbContext.Regions.Add(regionDomainModel);
             *dbContext.SaveChanges();*/

            /*DB Context by following the repository design pattern*/
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

			/*Map: Without automapper => domain model back to dto
			var regionDto = new RegionDTO
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
			};*/

			/*From Domain to DTO
			 * mapper.Map<Destination>(source);
			 */
			var regionDto = mapper.Map<RegionDTO>(regionDomainModel);


            return CreatedAtAction(nameof(CreateRegion), new{id = regionDto.Id},regionDto);
		}


		[HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
		{
			//var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

			/*Map: DTO to Domain
			 var regionDomainModel = new Region
			{
				Code = updateRegionRequestDto.Code,
				Name = updateRegionRequestDto.Name,
				RegionImageUrl = updateRegionRequestDto.RegionImageUrl
			};
			 */

			var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);


            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

			if(regionDomainModel == null)
			{
				return NotFound();
			}

			//on DB the domain model is saved
			regionDomainModel.Code = updateRegionRequestDto.Code;
			regionDomainModel.Name = updateRegionRequestDto.Name;
			regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

			/*convert domain to dto, to send to the client
			var regionDto = new RegionDTO
			{
				Id = regionDomainModel.Id,
				Code = regionDomainModel.Code,
				Name = regionDomainModel.Name,
				RegionImageUrl = regionDomainModel.RegionImageUrl
			};*/

			var regionDto = mapper.Map<RegionDTO>(regionDomainModel);


            return Ok(regionDto);
		}

		[HttpDelete]
        [Route("{id:Guid}")]
		public async Task <IActionResult> DeleteAsync([FromRoute]Guid id)
		{
			// var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
			var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            /*Automapper: Domain to DTO
			 * mapper.Map<Destination>(source);
			 */
            var result = mapper.Map<RegionDTO>(regionDomainModel);

			return Ok(result);
        }
    }
}

