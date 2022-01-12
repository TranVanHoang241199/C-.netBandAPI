using AutoMapper;
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
    [Route("api/bandcollections")]
    public class BandsCollectionsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumepository;
        private readonly IMapper _mapper;

        public BandsCollectionsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumepository = bandAlbumRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<IEnumerable<BandDto>> CreateBandCollection([FromBody] IEnumerable<BandForCreatingDto> bandCallection)
        {
            var bandEntities = _mapper.Map<IEnumerable<Entities.Band>>(bandCallection);

            foreach(var band in bandEntities)
            {
                _bandAlbumepository.AddBand(band);
            }

            _bandAlbumepository.Save();

            return Ok();
        }
    }
}
