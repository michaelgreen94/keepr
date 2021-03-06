using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  [Authorize]
  [ApiController]
  public class VaultkeepController : Controller
  {

    VaultkeepRepository _repo;
    public VaultkeepController(VaultkeepRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public IEnumerable<Vaultkeep> Get()
    {
      return _repo.GetAll();
    }

    //gets keeps by vault id
    [HttpGet("{id}")]
    public IEnumerable<Keep> getallkeepsinvault([FromRoute] int id)
    {
      return _repo.GetByVaultID(id);
    }

    [Authorize]
    [HttpPost]
    public Vaultkeep Post([FromBody] Vaultkeep vaultkeep)
    {
      if (ModelState.IsValid)
      {
        vaultkeep.UserId = HttpContext.User.Identity.Name;
        return _repo.Create(vaultkeep);
      }
      throw new Exception("INVALID VAULTKEEP");
    }

    [Authorize]
    [HttpPut]
    public Vaultkeep Put([FromBody] Vaultkeep vaultkeep)
    {
      return _repo.Update(vaultkeep);
    }

    [Authorize]
    [HttpDelete("{vaultId}/{keepId}")]
    public bool Delete(int vaultId, int keepId)
    {
      string UserId = HttpContext.User.Identity.Name;
      return _repo.Delete(keepId, vaultId, UserId);
    }

  }

}