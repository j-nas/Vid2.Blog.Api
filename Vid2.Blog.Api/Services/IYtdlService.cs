namespace Vid2.Blog.Api.Services;

public interface IYtdlService
{
    public Task<string> GetTranscript(string videoId);
}