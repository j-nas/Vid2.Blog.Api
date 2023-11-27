using System.Text.RegularExpressions;
using YoutubeExplode;


namespace Vid2.Blog.Api.Services;


public class YtdlService(YoutubeClient youtubeClient) : IYtdlService
{
    public async Task<string> GetTranscript(string videoId)
    {
        var trackManifest =
            await youtubeClient.Videos.ClosedCaptions
                .GetManifestAsync(videoId);

        var trackInfo = trackManifest.GetByLanguage("en");
        var track = new StringWriter();
        await youtubeClient.Videos.ClosedCaptions.WriteToAsync(trackInfo, track);
        var regexMatch =
            @"(?:\r?\n)*\d+\r?\n\d{2}:\d{2}:\d{2},\d{3} --> \d{2}:\d{2}:\d{2},\d{3}\r?\n";
        var regex = new Regex(regexMatch, RegexOptions.Multiline);
        var transcript = regex.Replace(track.ToString(), "\r");
        return transcript;


    }
}