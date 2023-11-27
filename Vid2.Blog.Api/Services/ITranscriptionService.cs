using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vid2.Blog.Api.Services
{
    public interface ITranscriptionService
    {
        Task<string> GenerateBlogPost(string transcript);
    }
}