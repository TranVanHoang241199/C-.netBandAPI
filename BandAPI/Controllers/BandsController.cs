using AutoMapper;
using BandAPI.Helpers;
using BandAPI.Models;
using BandAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Controllers
{
    [ApiController]
    [Route("api/bands")]
    public class BandsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumepository;
        private readonly IMapper _mapper;

        public BandsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumepository = bandAlbumRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BandDto>> GetBands([FromQuery] BandsResourceParameters bandsResourceParameters)
        {
            var bandsFromRepo = _bandAlbumepository.GetBands(bandsResourceParameters);

            return Ok(_mapper.Map<IEnumerable<BandDto>>(bandsFromRepo));
        }

        [HttpGet("{bandId}", Name ="GetBand")]
        public IActionResult GetBand(Guid bandId)
        {
            var bandFromRepo = _bandAlbumepository.GetBand(bandId);

            if (bandFromRepo == null)
                return NotFound();

            return Ok(bandFromRepo);
        }

        [HttpPost]
        public ActionResult<BandDto> CreateBand([FromBody] BandForCreatingDto band)
        {
            var bandEntity = _mapper.Map<Entities.Band>(band);
            _bandAlbumepository.AddBand(bandEntity);
            _bandAlbumepository.Save();

            var banToReturn = _mapper.Map<BandDto>(bandEntity);

            return CreatedAtRoute("GetBand", new { bandId = banToReturn.Id}, banToReturn);
        }
    }
}
