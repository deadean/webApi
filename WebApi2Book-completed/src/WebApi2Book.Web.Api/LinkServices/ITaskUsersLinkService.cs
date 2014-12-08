// ITaskUsersLinkService.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi.Web.Data.Implementations.Response;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.LinkServices
{
    public interface ITaskUsersLinkService
    {
        void AddLinks(TaskUsersResponse inquiryResponse);
    }
}