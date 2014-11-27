// NewTask.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using Blank.Data.Implementations.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Web.Data.Implementations.Requests
{
    public class TaskRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Subject { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? DueDate { get; set; }

        public List<User> Assignees { get; set; }
    }
}