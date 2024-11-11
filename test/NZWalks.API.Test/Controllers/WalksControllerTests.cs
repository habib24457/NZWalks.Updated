using AutoMapper;
using NSubstitute;
using NZWalks.API.Controllers;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NZWalks.API.Test.Controllers
{
    public class WalksControllerTests
    {
        //private readonly IWalkRepository _mockWalkRepository;
        private readonly IMapper _mapper;

        public WalksControllerTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();  // Add your mapping profile here
            });
            _mapper = new Mapper(configuration);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction_WhenWalkIsCreated()
        {
            // Arrange
            var walkRepository = Substitute.For<IWalkRepository>();
            var addWalkRequestDto = new AddWalkRequestDto
            {
                Name = "New Walk",
                Description = "Beautiful trail through forested hills",
                LengthInKm = 10.5,
                WalkImageUrl = "https://example.com/walk.jpg",
                DifficultyId = Guid.NewGuid(),
                RegionId = Guid.NewGuid()
            };

            // Map the AddWalkRequestDto to Walk domain model using AutoMapper
            var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);
            walkDomainModel.Id = Guid.NewGuid();
            var walkDtoModel = _mapper.Map<WalkDto>(walkDomainModel);
            walkRepository.CreateAsync(walkDomainModel).Returns(Task.FromResult(walkDomainModel));
            var walkController = new WalksController(walkRepository,_mapper);

            // Act
            var result = await walkController.Create(addWalkRequestDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var resultValue = Assert.IsType<WalkDto>(createdResult.Value);
            Assert.Equal(walkDtoModel.Id, resultValue.Id);
            Assert.Equal(walkDtoModel.Name, resultValue.Name);
            Assert.Equal(walkDtoModel.Description, resultValue.Description);
            Assert.Equal(walkDtoModel.LengthInKm, resultValue.LengthInKm);
            Assert.Equal(walkDtoModel.WalkImageUrl, resultValue.WalkImageUrl);
            _mapper.Received(1).Map<Walk>(addWalkRequestDto);
            _mapper.Received(1).Map<WalkDto>(walkDomainModel);
            await walkRepository.Received(1).CreateAsync(walkDomainModel);
        }
    }
}