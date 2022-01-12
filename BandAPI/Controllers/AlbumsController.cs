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
    [Route("api/bands/{bandId}/albums")]
    public class AlbumsController : ControllerBase
    {
        private readonly IBandAlbumRepository _bandAlbumepository;
        private readonly IMapper _mapper;

        public AlbumsController(IBandAlbumRepository bandAlbumRepository, IMapper mapper)
        {
            _bandAlbumepository = bandAlbumRepository ??
                throw new ArgumentNullException(nameof(bandAlbumRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumsDto>> GetAlbumsForBand(Guid bandId)
        {
            if (!_bandAlbumepository.BandExists(bandId))
                return NotFound();

            var albumsFromRepo = _bandAlbumepository.GetAlbums(bandId);
            return Ok(_mapper.Map<IEnumerable<AlbumsDto>>(albumsFromRepo));
        }

        [HttpGet("{albumID}", Name ="GetAlbumForBand")]
        public ActionResult<AlbumsDto> GetAlbumForband(Guid bandId, Guid albumId)
        {
            if (!_bandAlbumepository.BandExists(bandId))
                return NotFound();

            var albumFromRepo = _bandAlbumepository.GetAlbum(bandId, albumId);
            if (albumFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<AlbumsDto>(albumFromRepo));
        }

        [HttpPost]
        public ActionResult<AlbumsDto> CreateAlbumForBand(Guid bandId, [FromBody] AlbumForCreatingDto album)
        {
            if (!_bandAlbumepository.BandExists(bandId))
                return NotFound();

            var albumEntity = _mapper.Map<Entities.Album>(album);
            _bandAlbumepository.AddAlbum(bandId, albumEntity);
            _bandAlbumepository.Save();

            var albumToReturn = _mapper.Map<AlbumsDto>(albumEntity);
            return CreatedAtRoute("GetAlbumForBand", new { bandId = bandId, albumId = albumToReturn.Id }, albumToReturn);
        }
    }
}
