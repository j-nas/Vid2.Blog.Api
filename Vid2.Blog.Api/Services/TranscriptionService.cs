using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;

namespace Vid2.Blog.Api.Services
{
    public class TranscriptionService(IOpenAIService openAiService, IYtdlService ytdlService) : ITranscriptionService
    {
        public async Task<string> GenerateBlogPost(string videoId)
        {
              var transcript = await ytdlService.GetTranscript(videoId);
            var blogpostResult =
                await openAiService.ChatCompletion.CreateCompletion(
                    new ChatCompletionCreateRequest
                    {
                        Messages = new List<ChatMessage>
                        {
                            ChatMessage.FromSystem(
                                Prompts.CreateBlogPostPrompt()),
                            ChatMessage.FromUser(
                                $"Please make a blog from the following: {transcript}")

                        },
                        Model = OpenAI.ObjectModels.Models.Gpt_4
                    });
            if (!blogpostResult.Successful)
            {
                throw new Exception(blogpostResult.Error?.Message);
            }

            return blogpostResult.Choices.First().Message.Content;
        }
    }
}
