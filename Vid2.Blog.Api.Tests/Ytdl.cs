

using Vid2.Blog.Api.Services;
using YoutubeExplode;

namespace Vid2.Blog.Api.Tests;

public class YtdlTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetTranscript()
    {
        YtdlService ytdlService = new YtdlService(new YoutubeClient());
        var transcript = await ytdlService.GetTranscript("ipYQpxAyunc");
        Console.WriteLine(transcript);
        Assert.Pass();
    }
}