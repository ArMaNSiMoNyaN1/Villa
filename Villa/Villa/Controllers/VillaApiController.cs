using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Villa.Data;
using Villa.Models.Dto;

namespace Villa.Controllers;

[Route("api/VillaApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    public VillaApiController()
    {
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVilla()
    {
        return Ok(VillaStore.villaList);
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VillaDTO> GetVillaId(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(u => u.id == id);
        if (villa == null)
        {
            return NotFound();
        }

        return Ok(villa);
    }

    [HttpPost]
    public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDto)
    {
        if (VillaStore.villaList.FirstOrDefault(u => u.name.ToLower() == villaDto.name.ToLower() != null) != null)
        {
            ModelState.AddModelError("CustomerError", "Villa already Exists!");
            return BadRequest(ModelState);
        }

        {
        }
        if (villaDto == null)
        {
            return BadRequest(villaDto);
        }

        if (villaDto.id > 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        villaDto.id = VillaStore.villaList.OrderByDescending(u => u.id).FirstOrDefault().id + 1;
        VillaStore.villaList.Add(villaDto);

        return CreatedAtAction("GetVilla", new { id = villaDto.id }, villaDto);
    }


    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public IActionResult DeleteVilla(int id)
    {
        var villa = VillaStore.villaList.FirstOrDefault(u => u.id == id);
        if (villa == null)
        {
            return NotFound();
        }

        VillaStore.villaList.Remove(villa);
        return NoContent();
    }

    [HttpPut("{id:int}", Name = "UpdateVilla")]
    public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDto)
    {
        if (villaDto == null || id != villaDto.id)
        {
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(u => u.id == id);
        villa.name = villaDto.name;
        villa.Sqft = villaDto.Sqft;
        villa.occupancy = villaDto.occupancy;

        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdateVilla")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        { 
            return BadRequest();
        }

        var villa = VillaStore.villaList.FirstOrDefault(u => u.id == id);
        if (villa == null)
        {
            return BadRequest();
        }

        patchDTO.ApplyTo(villa, ModelState);
        Models.Villa model = new Models.Villa()
        {
            Amenity = VillaDTO.Amenity,
            Details = VillaDTO.Details,
            Id = VillaDTO.id,
            ImageUrl = VillaDTO.ImageUrl,
            name = VillaDTO.name,
            occupancy = VillaDTO.occupancy,
            Rate = VillaDTO.Rate,
            Sqft = VillaDTO.Sqft
        };

        return NoContent();
    }
}