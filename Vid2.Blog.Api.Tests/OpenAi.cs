using Vid2.Blog.Api.Services;
using YoutubeExplode;

namespace Vid2.Blog.Api.Tests;

public class OpenAiTests
{
    public string transcript { get; set; }
    [SetUp]
    public async Task Setup()
    {
        var ytdlService = new YtdlService(new YoutubeClient());
        transcript = await  ytdlService.GetTranscript("ipYQpxAyunc");
    }

    [Test]
    public async Task BlogPostMarkdown()
    {
        
    }
}
