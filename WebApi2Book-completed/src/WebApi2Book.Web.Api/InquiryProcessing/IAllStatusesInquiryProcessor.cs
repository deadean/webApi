// IAllStatusesInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Threading.Tasks;
using WebApi.Web.Data.Implementations.Requests;
using WebApi.Web.Data.Implementations.Response;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public interface IAllStatusesInquiryProcessor
    {
			Task<StatusesResponse> GetStatuses();

			Task<StatusResponse> AddStatus(StatusRequest newStatus);
		}
}