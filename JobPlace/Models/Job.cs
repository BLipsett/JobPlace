using System;
using GroupMe.Models;

namespace JobPlace.Models
{
  public class Job
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string CreatorId { get; set; }
    public string Description { get; set; }
    public int Quote { get; set; }

    public Profile Creator { get; set; }

  }
}