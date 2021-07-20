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
  }
}