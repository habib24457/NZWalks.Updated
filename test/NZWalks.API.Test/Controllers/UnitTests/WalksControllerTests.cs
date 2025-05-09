using AutoMapper;
using NSubstitute;
using NZWalks.API.Controllers;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NZWalks.API.Test.Controllers
{
    public class WalksControllerTests
    {
       /* [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WhenWalkIsCreated()
        {
            // Arrange
            var walkRepository = Substitute.For<IWalkRepository>();
            var mockMapper = Substitute.For<IMapper>();
            var regionRepository = Substitute.For<IRegionRepository>();
            var difficultyRepository = Substitute.For<IDifficultyRepository>();
            var addWalkRequestDto = new AddWalkRequestDto
            {
                Name = "New Walk",
                Description = "Beautiful trail through forested hills",
                LengthInKm = 10.5,
                WalkImageUrl = "https://example.com/walk.jpg",
                DifficultyId = Guid.NewGuid(),
                RegionId = Guid.NewGuid()
            };

            // Mock RegionDTO and DifficultyDto
            var regionDto = new RegionDTO
            {
                Id = addWalkRequestDto.RegionId,
                Name = "Forest Region",
                Code = "FR",
                RegionImageUrl = "https://example.com/region.jpg"
            };

            var difficultyDto = new DifficultyDto
            {
                Id = addWalkRequestDto.DifficultyId,
                Name = "Moderate"
            };

            // Mock Walk domain model
            var walkDomainModel = new Walk
            {
                Id = Guid.NewGuid(),
                Name = addWalkRequestDto.Name,
                Description = addWalkRequestDto.Description,
                LengthInKm = addWalkRequestDto.LengthInKm,
                WalkImageUrl = addWalkRequestDto.WalkImageUrl,
                DifficultyId = addWalkRequestDto.DifficultyId,
                RegionId = addWalkRequestDto.RegionId
            };

            // Mock WalkDto
            var walkDtoModel = new WalkDto
            {
                Id = walkDomainModel.Id,
                Name = walkDomainModel.Name,
                Description = walkDomainModel.Description,
                LengthInKm = walkDomainModel.LengthInKm,
                WalkImageUrl = walkDomainModel.WalkImageUrl,
                Region = regionDto,
                Difficulty = difficultyDto
            };

            mockMapper.Map<Walk>(addWalkRequestDto).Returns(walkDomainModel);
            walkRepository.CreateWalkAsync(walkDomainModel).Returns(Task.FromResult(walkDomainModel));
            mockMapper.Map<WalkDto>(walkDomainModel).Returns(walkDtoModel);
            var walksController = new WalksController(walkRepository, mockMapper, regionRepository, difficultyRepository);

            //Act
            var result = await walksController.CreateWalk(addWalkRequestDto);


            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(walksController.CreateWalk), createdResult.ActionName);
            await walkRepository.Received(1).CreateWalkAsync(Arg.Is<Walk>(x =>
                x.Name == walkDomainModel.Name &&
                x.Description == walkDomainModel.Description &&
                x.LengthInKm == walkDomainModel.LengthInKm &&
                x.WalkImageUrl == walkDomainModel.WalkImageUrl &&
                x.DifficultyId == walkDomainModel.DifficultyId &&
                x.RegionId == walkDomainModel.RegionId
            ));
        }*/

        [Fact]
        public async Task GetAllWalk_ShouldReturnOkWithNull_WhenNoWalksExist()
        {
            // Arrange
            var walkRepository = Substitute.For<IWalkRepository>();
            var walksDomainModelWithNoWalk = new List<Walk>();
            var mockMapper = Substitute.For<IMapper>();
            var regionRepository = Substitute.For<IRegionRepository>();
            var difficultyRepository = Substitute.For<IDifficultyRepository>();
            walkRepository.GetWalkAsync().Returns(Task.FromResult(walksDomainModelWithNoWalk));
            var walksController = new WalksController(walkRepository, mockMapper, regionRepository, difficultyRepository);

            // Act
            var result = await walksController.GetAllWalk();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Null(okResult.Value);
        }
        
    }
}