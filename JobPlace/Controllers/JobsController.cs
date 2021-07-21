using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using JobPlace.Models;
using JobPlace.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobPlace.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class JobsController : ControllerBase
  {
    private readonly JobsService _js;

    public JobsController(JobsService js)
    {
      _js = js;
    }

    [HttpGet]
    public ActionResult<List<Job>> Get()
    {
      try
      {
        List<Job> jobs = _js.GetJobs();
        return Ok(jobs);
      }
      catch (System.Exception e)
      {

        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Job> GetActionResult(int id)
    {
      try
      {
        Job job = _js.GetJobById(id);
        return Ok(job);
      }
      catch (System.Exception e)
      {

        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    async public Task<ActionResult<Job>> Create([FromBody] Job jobData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        jobData.CreatorId = userInfo.Id;
        // Populates the creator profile 
        jobData.Creator = userInfo;
        var j = _js.CreateJob(jobData);
        return Ok(j);
      }
      catch (System.Exception e)
      {

        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Job>> Update(int id, [FromBody] Job jobData)
    {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      jobData.Id = id;
      var j = _js.Update(jobData, userInfo.Id);
      return Ok(j);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Job>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _js.RemoveJob(id, userInfo.Id);
        return Ok("successfully removed");
      }
      catch (System.Exception e)
      {

        return BadRequest(e.Message);
      }
    }
  }
}