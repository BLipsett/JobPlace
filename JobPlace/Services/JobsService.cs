using System;
using System.Collections.Generic;
using JobPlace.Models;
using JobPlace.Repositories;

namespace JobPlace.Services
{
  public class JobsService
  {

    private readonly JobsRepository _JobRepo;

    public JobsService(JobsRepository jobRepo)
    {
      _JobRepo = jobRepo;
    }

    public List<Job> GetJobs()
    {
      return _JobRepo.GetAll();
    }

    public Job GetJobById(int id)
    {
      return _JobRepo.GetOne(id);
    }
    public object CreateJob(Job jobData)
    {
      var job = _JobRepo.Create(jobData);

      return job;
    }

    public Job Update(Job jobData, string id)
    {

      // get this job by its id and set it to workplace
      Job workplace = _JobRepo.GetOne(jobData.Id);
      //If that returned nothing you might have wrong id
      if (workplace == null)
      {
        throw new Exception("Invalid Id");
      }
      // ONly creator can update this job
      if (workplace.CreatorId != id)
      {
        throw new Exception("Only creator can update");
      }
      // the job id came back now you can update with the data
      return _JobRepo.Update(jobData);
    }

    public void RemoveJob(int id, string userId)
    {
      Job workplace = GetJobById(id);

      if (workplace.CreatorId != userId)
      {
        throw new Exception("You can't do that");
      }
      _JobRepo.RemoveJob(id);
    }

  }
}