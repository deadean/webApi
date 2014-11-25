// PagedDataRequest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

namespace WebApi.Data.Implementations.Requests
{
    public class PagedDataRequest
    {
        public PagedDataRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public bool ExcludeLinks { get; set; }
    }
}