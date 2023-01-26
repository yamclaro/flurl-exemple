using Flurl.Http;
using FlurlExamples.Model;

namespace FlurlExamples.Service
{
    public interface IRequestHandler
    {
        Task<List<Repository>> GetRepositories();
        Task<Repository> CreateRepository(string user, string repository);
        Task<Repository> EditRepository(string user, string repository);
        Task<IFlurlResponse> DeleteRepository(string user, string repository);
        Task<IFlurlResponse> DeleteRepositoryComBody(string user, string repository);
    }
}