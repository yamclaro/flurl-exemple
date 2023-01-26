using Flurl;
using Flurl.Http;
using FlurlExamples.Model;

namespace FlurlExamples.Service;

public class FlurlRequestHandler : IRequestHandler
{
    private readonly IConfiguration _configuration;

    public FlurlRequestHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<List<Repository>> GetRepositories()
    {
        var result = await _configuration["Serviço:UrlBase"]
           .AppendPathSegments("user", "repos")
            //Caso tenha que passar algo no header 
            //.WithHeader("UserAgent", "UserAgentValue")
            //Caso tenha token 
            //.WithOAuthBearerToken("token")
            .GetJsonAsync<List<Repository>>();
        return result;
    }

    public async Task<Repository> CreateRepository(string user, string repository)
    {
        var repo = new Repository
        {
            Name = repository,
            FullName = $"{user}/{repository}",
            Description = "Generic description",
            Private = false
        };

        var result = await _configuration["Serviço:UrlBase"]
           .AppendPathSegments("user", "repos")
            //Caso tenha que passar algo no header 
            //.WithHeader("UserAgent", "UserAgentValue")
            //Caso tenha token 
            //.WithOAuthBearerToken("token")
            .PostJsonAsync(repo)
            .ReceiveJson<Repository>();

        return result;
    }

    public async Task<Repository> EditRepository(string user, string repository)
    {
        var repo = new Repository
        {
            Name = repository,
            FullName = $"{user}/{repository}",
            Description = "Modified repository",
            Private = false
        };

        var result = await _configuration["Serviço:UrlBase"]
            .AppendPathSegments("repos", user, repository)
            //Caso tenha que passar algo no header 
            //.WithHeader("UserAgent", "UserAgentValue")
            //Caso tenha token 
            //.WithOAuthBearerToken("token")
            .PatchJsonAsync(repo) //  .PutJsonAsync(repo)
            .ReceiveJson<Repository>();

        return result;
    }

    public async Task<IFlurlResponse> DeleteRepository(string user, string repository)
    {
        var result = await _configuration["Serviço:UrlBase"]
             .AppendPathSegments("repos", user, repository)
            //Caso tenha que passar algo no header 
            //.WithHeader("UserAgent", "UserAgentValue")
            //Caso tenha token 
            //.WithOAuthBearerToken("token")
            .DeleteAsync();
        return result;
    }

    public async Task<IFlurlResponse> DeleteRepositoryComBody(string user, string repository)
    {
        var repo = new Repository
        {
            Name = repository,
            FullName = $"{user}/{repository}",
        };
        var url = _configuration["Keycloak:UrlBase"];
        var result = await url
            .AppendPathSegments("repos", user, repository)
            //Caso tenha que passar algo no header 
            //.WithHeader("UserAgent", "UserAgentValue")
            //Caso tenha token 
            //.WithOAuthBearerToken("token")
            .SendJsonAsync(HttpMethod.Delete, repo);
        return result;
    }
}