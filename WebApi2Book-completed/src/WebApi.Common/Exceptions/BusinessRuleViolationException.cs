// BusinessRuleViolationException.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;

namespace WebApi.Common
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string incorrectTaskStatus) : base(incorrectTaskStatus)
        {
        }
    }
}