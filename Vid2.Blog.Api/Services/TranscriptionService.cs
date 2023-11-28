using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.Tokenizer.GPT3;

namespace Vid2.Blog.Api.Services
{
    public class TranscriptionService(IOpenAIService openAiService, IYtdlService ytdlService) : ITranscriptionService
    {
        private readonly int _maxTokens = 4096;
        public async Task<string> GenerateBlogPost(string videoId)
        {
              var transcript = await ytdlService.GetTranscript(videoId);
              // var condensedTranscript = GetCondensedTranscript(transcript);
            var blogpostResult =
                await openAiService.ChatCompletion.CreateCompletion(
                    new ChatCompletionCreateRequest
                    {
                        Messages = new List<ChatMessage>
                        {
                            ChatMessage.FromSystem(
                                "You are a very helpful assistant, who is an expert in creating blog posts from video transcript"),
                            ChatMessage.FromUser(
                                Prompts.CreateBlogPostPrompt(transcript))

                        },
                        
                        Model = OpenAI.ObjectModels.Models.Gpt_3_5_Turbo_16k,
                        MaxTokens = _maxTokens
                        
                    });
            if (!blogpostResult.Successful)
            {
                throw new Exception(blogpostResult.Error?.Message);
            }

            return blogpostResult.Choices.First().Message.Content;
        }

        // private async Task<string> GetCondensedTranscript(string transcript)
        // {
        //     var condensedTranscript =
        //         await openAiService.ChatCompletion.CreateCompletion(
        //             new ChatCompletionCreateRequest
        //             {
        //                 Messages = new List<ChatMessage>
        //                 {
        //                     ChatMessage.FromSystem(
        //                         "You are a helpful assistant, who specialized in condensing video transcripts."),
        //                     ChatMessage.FromUser(Prompts.CreateCondensePrompt(transcript))
        //                 },
        //                 Model = OpenAI.ObjectModels.Models.Gpt_3_5_Turbo,
        //                 MaxTokens = _maxTokens
        //             });
        // }
        //
        // private int GetAmountToCondense(string transcript)
        // {
        //     var transcriptTokens = TokenizerGpt3.TokenCount(transcript);
        //     return transcriptTokens.Length;
        // }
    }
}
