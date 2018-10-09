using System.Collections.Generic;
using System.Data;
using System.Linq;
using keepr.Models;
using Dapper;

namespace keepr.Repositories
{
  public class KeepRepository
  {
    private IDbConnection _db;
    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }

    //CRUD VIA SQL

    //GET ALL KeepS
    public IEnumerable<Keep> GetAll()
    {
      return _db.Query<Keep>("SELECT * FROM keeps;");
    }

    public Keep GetByID(int id)
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE id = @id;", new { id }).FirstOrDefault();
    }

    //CREATE Keep
    public Keep Create(Keep keep)
    {
      int id = _db.ExecuteScalar<int>(@"
      INSERT INTO keeps (name, description, img, shares, views, keeps, isprivate, userid)
      Values (@Name, @Description, @Img, @Shares, @Views, @Keeps, @IsPrivate, @UserId);
      SELECT LAST_INSERT_ID();", keep
      );
      keep.Id = id;
      return keep;
    }

    //UPDATE Keep
    public Keep Update(Keep keep)
    {
      _db.Execute(@"UPDATE keeps
      SET name = @Name, description = @Description
      WHERE id = @Id;", keep);
      return keep;
    }

    //DELETE Keep
    public Keep Delete(Keep keep)
    {
      _db.Execute("DELETE FROM keeps WHERE id = @Id");
      return keep;
    }

    //DELETES A Keep BY ITS ID
    public int Delete(int id)
    {
      return _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
    }

  }
}