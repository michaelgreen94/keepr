using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Authorize]
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

    // [HttpGet("{id}")]
    // public Burger GetBurgerByID([FromRoute] int id)
    // {

    // }

    [Authorize]
    [HttpPost]
    public Keep Post([FromBody] Keep keep)
    {
      if (ModelState.IsValid)
      {
        keep = new Keep(keep.Name, keep.Description);
        return _repo.Create(keep);
      }
      throw new Exception("INVALID KEEP");
    }

    [HttpPut]
    public Keep Put([FromBody] Keep keep)
    {
      return _repo.Update(keep);
    }

    [HttpDelete("{id}")]
    public void Delete([FromRoute] int id)
    {
      _repo.Delete(id);
    }

  }

}