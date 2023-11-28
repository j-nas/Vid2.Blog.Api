namespace Vid2.Blog.Api;

public static class Prompts
{
    public static string CreateCondensePrompt(string transcript,
        int percentage = 50) =>
        $"You will condense this video transcript to about {percentage}% of its original length. You keep every detail, even if you think it doesn't matter. You keep the tone and style of speaking as well. The transcript follows: {transcript}";

    public static string CreateBlogPostPrompt(string transcript) =>
        $"You will create a blog post from this condensed transcript. You will write it from an expert's point of view, as if you were the one who made the video. You will format the output as markdown. Please make it at least 1500 words long. The transcript follows {transcript}";
}