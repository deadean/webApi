// AllStatusesInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Implementations.Entities;
using WebApi.Data.QueryProcessors;
using WebApi.Web.Data.Implementations.Requests;
using WebApi.Web.Data.Implementations.Response;
using WebApi.WebApiService.Bases.Processing.Inquiry;
using WebApi2Book.Common.TypeMapping;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
	public class AllStatusesInquiryProcessor : BaseInquiryProcessorImpl, IAllStatusesInquiryProcessor
	{
		private readonly IAllStatusesQueryProcessor _queryProcessor;
		private readonly IAutoMapper _autoMapper;

		public AllStatusesInquiryProcessor(IAllStatusesQueryProcessor queryProcessor, IAutoMapper autoMapper)
		{
			_queryProcessor = queryProcessor;
			_autoMapper = autoMapper;
		}

		public async Task<StatusesResponse> GetStatuses()
		{
			try
			{
				return await Task.Run(() =>
				{
					return new StatusesResponse() { Items = _queryProcessor.GetStatuses().Select(x => new Status() { Id = x.StatusId, Name = x.Name, Ordinal = x.Ordinal }) };
				});
			}
			catch (Exception ex)
			{
				modLog.Debug("GetPerson", ex);
				throw new Exception("Person not found");
			}
		}


		public async Task<StatusResponse> AddStatus(StatusRequest newStatus)
		{
			try
			{
				return await Task.Run(() =>
				{
					WebApi2Book.Data.Entities.Status res = _queryProcessor.AddStatus(_autoMapper.Map<WebApi2Book.Data.Entities.Status>(newStatus));
					return _autoMapper.Map<StatusResponse>(res);
				});
			}
			catch (Exception ex)
			{
				modLog.Debug("AddStatus", ex);
				throw new Exception("Can not add new Status");
			}
		}


		public async Task<string> RemoveStatus(string id)
		{
			try
			{
				return await Task.Run(() =>
				{
					bool isRemoved = _queryProcessor.RemoveStatus(id);
					return isRemoved ? "Status has been removed successfully" : "Status has not been removed";
				});
			}
			catch (Exception ex)
			{
				modLog.Debug("RemoveStatus", ex);
				throw new Exception(string.Format("Can not remove status with id:{0}", id));
			}
		}
	}
}