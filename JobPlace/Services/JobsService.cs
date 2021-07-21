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

    internal List<Job> GetJobs()
    {
      return _JobRepo.GetAll();
    }

    internal object CreateJob(Job jobData)
    {
      var job = _JobRepo.Create(jobData);

      return job;
    }

    internal Job GetJobById(int id)
    {
      return _JobRepo.GetOne(id);
    }
  }
}