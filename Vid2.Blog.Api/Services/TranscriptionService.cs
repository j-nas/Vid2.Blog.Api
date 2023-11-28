using Microsoft.EntityFrameworkCore;
using OpenAI.Interfaces;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.Tokenizer.GPT3;
using Vid2.Blog.Api.Data;
using Vid2.Blog.Api.Models;

namespace Vid2.Blog.Api.Services
{
    public class TranscriptionService(IOpenAIService openAiService, IYtdlService ytdlService, DataContext dataContext) : ITranscriptionService
    {
        private const int MaxTokens = 4096;

        public async Task<string> GenerateBlogPost(string videoId, bool forceRegenerate = false)
        {
           
        
            var preExistingGeneratedContent = await CheckForGeneratedPost(videoId);
            if (preExistingGeneratedContent != null && !forceRegenerate)
            {
                return preExistingGeneratedContent.CompletionResult;
            }
            
            var transcript = await ytdlService.GetTranscript(videoId);
            if (transcript == null)
            {
                throw new Exception("Could not get transcript");
            }
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
                        MaxTokens = MaxTokens
                        
                    });
            if (!blogpostResult.Successful)
            {
                throw new Exception(blogpostResult.Error?.Message);
            }
            await UpsertGeneratedResult(videoId, blogpostResult.Choices.First().Message.Content);

            return blogpostResult.Choices.First().Message.Content;
        }
        
        private async Task<Result?> CheckForGeneratedPost(string videoId)
        {
            var blogPost = await dataContext.Results.FirstOrDefaultAsync(x => x.YoutubeId== videoId);
            return blogPost;
        }
        private async Task UpsertGeneratedResult(string videoId, string content)
        {
            var result = await CheckForGeneratedPost(videoId);
            if (result == null)
            {
                result = new Result
                {
                    
                    YoutubeId = videoId,
                    CompletionResult = content
                };
                await dataContext.Results.AddAsync(result);
            }
            else
            {
                result.CompletionResult = content;
                dataContext.Results.Update(result);
            }

            await dataContext.SaveChangesAsync();
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
