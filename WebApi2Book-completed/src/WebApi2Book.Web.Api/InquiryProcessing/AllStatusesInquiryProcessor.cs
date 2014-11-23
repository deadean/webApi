// AllStatusesInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class AllStatusesInquiryProcessor : IAllStatusesInquiryProcessor
    {
        private readonly IAllStatusesQueryProcessor _queryProcessor;

        public AllStatusesInquiryProcessor(IAllStatusesQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public virtual List<Status> GetStatuses(IEnumerable<Data.Entities.Status> statusEntities)
        {
					var statuses = statusEntities.Select(x => new Status() { Name = x.Name, Ordinal = x.Ordinal}).ToList();
            return statuses;
        }

				public StatusesInquiryResponse GetStatuses()
				{
					return new StatusesInquiryResponse() { Statuses = this.GetStatuses(_queryProcessor.GetStatuses()) };
				}
		}
}