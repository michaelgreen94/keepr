using System.Collections.Generic;
using System.Data;
using System.Linq;
using keepr.Models;
using Dapper;

namespace keepr.Repositories
{
  public class VaultRepository
  {
    private IDbConnection _db;
    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }

    //CRUD VIA SQL

    //GET ALL KeepS
    public IEnumerable<Vault> GetAll()
    {
      return _db.Query<Vault>("SELECT * FROM vaults;");
    }

    //GET Keep BY ID
    public Vault GetByID(int id)
    {
      return _db.Query<Vault>("SELECT * FROM vaults WHERE id = @id;", new { id }).FirstOrDefault();
    }

    //CREATE Keep
    public Vault Create(Vault vault)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO vaults (name, description, userid)
      Values (@Name, @Description, @UserId);
      SELECT LAST_INSERT_ID();", vault
      );
      vault.Id = id;
      return vault;
    }

    //UPDATE Vault
    public Vault Update(Vault vault)
    {
      _db.Execute(@"UPDATE vaults
      SET name = @Name, description = @Description
      WHERE id = @Id;", vault);
      return vault;
    }

    //DELETE Vault
    public Vault Delete(Vault vault)
    {
      _db.Execute("DELETE FROM vaults WHERE id = @Id");
      return vault;
    }

    //DELETES A Vault BY ITS ID
    public int Delete(int id)
    {
      return _db.Execute("DELETE FROM vaults WHERE id = @id", new { id });
    }

  }
}