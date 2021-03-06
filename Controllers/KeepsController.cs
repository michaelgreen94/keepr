using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class KeepsController : Controller
  {

    KeepRepository _repo;
    public KeepsController(KeepRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public IEnumerable<Keep> Get()
    {
      return _repo.GetAll();
    }

    //gets keep by keepid
    [HttpGet("{id}")]
    public Keep GetKeepByID([FromRoute] int id)
    {
      return _repo.GetByID(id);
    }

    [Authorize]
    [HttpPost]
    public Keep Post([FromBody] Keep keep)
    {
      if (ModelState.IsValid)
      {
        keep.UserId = HttpContext.User.Identity.Name;
        return _repo.Create(keep);
      }
      throw new Exception("INVALID KEEP");
    }

    [Authorize]
    [HttpPut]
    public Keep Put([FromBody] Keep keep)
    {
      return _repo.Update(keep);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public bool Delete([FromRoute] int id)
    {
      string UserId = HttpContext.User.Identity.Name;
      return _repo.Delete(id, UserId);
    }

  }

}