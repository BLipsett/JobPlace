using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GroupMe.Models;
using JobPlace.Models;

namespace JobPlace.Repositories
{
  public class JobsRepository
  {

    private readonly IDbConnection _db;

    public JobsRepository(IDbConnection db)
    {
      _db = db;
    }

    public List<Job> GetAll()
    {
      string sql = @"
      SELECT
      j.*,
      a.*
      FROM jobs j 
      JOIN accounts a ON j.creatorId = a.id;
      ";
      return _db.Query<Job, Profile, Job>(sql, (j, p) =>
      {
        j.Creator = p;
        return j;
      }, splitOn: "id").ToList();
    }

    public void RemoveJob(int id)
    {
      string sql = @"
      DELETE FROM jobs
      WHERE id = @id LIMIT 1;
      ";

      _db.Execute(sql, new { id });
    }

    public Job GetOne(int id)
    {
      string sql = @"
      SELECT j.*,
      a.*
      FROM jobs j
      JOIN accounts a ON j.creatorId = a.id
      WHERE j.id = @id;
      ";
      return _db.Query<Job, Profile, Job>(sql, (j, p) =>
      {
        j.Creator = p;
        return j;
      }, new { id }).FirstOrDefault();

    }


    public object Create(Job jobData)
    {
      string sql = @"
      INSERT INTO jobs
      (name, description, quote, creatorId)
      VALUES 
      (@Name, @Description, @Quote, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";

      var id = _db.ExecuteScalar<int>(sql, jobData);
      jobData.Id = id;
      return jobData;

    }
    public Job Update(Job jobData)
    {
      string sql = @"
      UPDATE jobs
      SET
      name = @Name
      WHERE id = @Id;
      ";
      _db.Execute(sql, jobData);
      return jobData;
    }
  }
}