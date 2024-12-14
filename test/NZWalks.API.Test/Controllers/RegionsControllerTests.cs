using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NZWalks.API.Controllers;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using Xunit;
using Region = NZWalks.API.Models.Domain.Region;

namespace NZWalks.API.Test.Controllers;

public class RegionsControllerTests
{
    [Fact]
    public async Task CreateRegion_shouldReturn_CreatedAction_WhenRegionCreated()
    {
        var regionRepository = Substitute.For<IRegionRepository>();
        var mockMapper = Substitute.For<IMapper>();

        var addRegionRequestDto = new AddRegionRequestDto()
        {
            Name = "Muelheim and der ruhr",
            Code = "MR",
            RegionImageUrl = "http://newImage.com"
        };
        
        //addRegionRequest: map to domain
        var regionDomain = new Region()
        {
            Id = Guid.NewGuid(),
            Name = addRegionRequestDto.Name,
            Code = addRegionRequestDto.Code,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl
        };
        
        //map regionDomain to regionDto
        var regionDto = new RegionDTO
        {
            Id = regionDomain.Id,
            Name = regionDomain.Name,
            Code = regionDomain.Code,
            RegionImageUrl = regionDomain.RegionImageUrl
        };
        
        mockMapper.Map<Region>(addRegionRequestDto).Returns(regionDomain);
        regionRepository.CreateAsync(regionDomain).Returns(Task.FromResult(regionDomain));
        mockMapper.Map<RegionDTO>(regionDomain).Returns(regionDto);
        var regionController = new RegionsController(regionRepository, mockMapper);
        //Act
        var result = await regionController.CreateRegion(addRegionRequestDto);

        //Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(regionController.CreateRegion), createdResult.ActionName);
        await regionRepository.Received(1).CreateAsync(Arg.Is<Region>(r =>
            r.Id == regionDomain.Id &&
            r.Name == regionDomain.Name &&
            r.Code == regionDomain.Code &&
            r.RegionImageUrl == regionDomain.RegionImageUrl
            ));
    }
    
    [Fact]
    public async Task GetAllRegions_ShouldReturnOkWithNull_WhenNoWalksExist()
    {
        // Arrange
        var regionRepository = Substitute.For<IRegionRepository>();
        var walksDomainModelWithNoWalk = new List<Region>();
        var mockMapper = Substitute.For<IMapper>();
        regionRepository.GetAllAsync().Returns(Task.FromResult(walksDomainModelWithNoWalk));
        var regionsController = new RegionsController(regionRepository, mockMapper);

        // Act
        var result = await regionsController.GetAllRegion();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Null(okResult.Value);
    }
}