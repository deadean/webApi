// AllStatusesInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Web.Data.Implementations.Response;
using WebApi.WebApiService.Bases.Processing.Inquiry;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
	public class AllStatusesInquiryProcessor : BaseInquiryProcessorImpl, IAllStatusesInquiryProcessor
    {
        private readonly IAllStatusesQueryProcessor _queryProcessor;

        public AllStatusesInquiryProcessor(IAllStatusesQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

				public async Task<StatusesResponse> GetStatuses()
				{
					try
					{
						return await Task.Run(() =>
						{
							return new StatusesResponse() { Items = _queryProcessor.GetStatuses().Select(x => new  Blank.Data.Implementations.Entities.Status(){Id = x.StatusId, Name = x.Name, Ordinal = x.Ordinal}) };
						});
					}
					catch (Exception ex)
					{
						modLog.Debug("GetPerson", ex);
						throw new Exception("Person not found");
					}
				}
		}
}