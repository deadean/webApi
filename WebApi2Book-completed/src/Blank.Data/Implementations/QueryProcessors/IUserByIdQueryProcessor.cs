// IUserByIdQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi2Book.Data.Entities;

namespace WebApi.Data.QueryProcessors
{
    public interface IUserByIdQueryProcessor
    {
        User GetUser(long userId);
    }
}