// StatusesController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Web.Common.Implementations.FilterAttributes;
using WebApi.Web.Common.Routing;
using WebApi.Web.Common.Validation;
using WebApi.Web.Data.Implementations.Requests;
using WebApi.Web.Data.Implementations.Response;
using WebApi2Book.Web.Api.InquiryProcessing;

namespace WebApi2Book.Web.Api.Controllers.V1
{
	[ApiVersion1RoutePrefix("statuses")]
	[UnitOfWorkAttribute]
	public class StatusesController : ApiController
	{
		private readonly IAllStatusesInquiryProcessor _allStatusesInquiryProcessor;

		public StatusesController(IAllStatusesInquiryProcessor allStatusesInquiryProcessor)
		{
			_allStatusesInquiryProcessor = allStatusesInquiryProcessor;
		}

		[ResponseType(typeof(StatusesResponse))]
		[Route("", Name = "GetStatusesRoute")]
		public async Task<IHttpActionResult> GetStatuses(HttpRequestMessage request)
		{
			try
			{
				return Ok(await _allStatusesInquiryProcessor.GetStatuses());
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("", Name = "AddStatusRoute")]
		[HttpPost]
		[ValidateModel]
		[ResponseType(typeof(StatusesResponse))]
		public async Task<IHttpActionResult> AddStatus(HttpRequestMessage requestMessage, StatusRequest newStatus)
		{
			try
			{
				return Ok(await _allStatusesInquiryProcessor.AddStatus(newStatus));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}