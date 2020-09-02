using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RallyDakar.Domain.DbContextDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RallyDakar.Domain.Entities;
using RallyDakar.Domain.Interfaces;

namespace RallyDakar.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")] // vai ficar diferente, mais complexa
  public class TelemetriaController : ControllerBase
  {
    private readonly ITelemetriaRepository _telemetriaRepository;
    private readonly RallyDakarDbContext _rallyDakarDbContext;
    private readonly IMapper _mapper;

    public TelemetriaController(RallyDakarDbContext rallyDakarDbContext, IMapper mapper, ITelemetriaRepository telemetriaRepository)
    {
      _rallyDakarDbContext = rallyDakarDbContext;
      _mapper = mapper;
      _telemetriaRepository = telemetriaRepository;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
      if (id <= 0)
        return StatusCode(StatusCodes.Status404NotFound, "ID não localizado.");

      return StatusCode(StatusCodes.Status200OK);
    }
  }
}
