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
  public class VaultController : Controller
  {

    VaultRepository _repo;
    public VaultController(VaultRepository repo)
    {
      _repo = repo;
    }

    [HttpGet]
    public IEnumerable<Vault> Get()
    {
      return _repo.GetAll();
    }

    [HttpGet("{id}")]
    public Vault GetVaultByID([FromRoute] int id)
    {
      return _repo.GetByID(id);
    }

    [Authorize]
    [HttpPost]
    public Vault Post([FromBody] Vault vault)
    {
      if (ModelState.IsValid)
      {
        vault.UserId = HttpContext.User.Identity.Name;
        return _repo.Create(vault);
      }
      throw new Exception("INVALID VAULT");
    }

    [Authorize]
    [HttpPut]
    public Vault Put([FromBody] Vault vault)
    {
      return _repo.Update(vault);
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