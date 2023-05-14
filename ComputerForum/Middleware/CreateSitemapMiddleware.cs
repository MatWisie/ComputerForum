using AspNetCore.SEOHelper.Sitemap;

namespace ComputerForum.Middleware
{
    public class CreateSitemapMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public CreateSitemapMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var siteList = new List<SitemapNode>();
            siteList.Add(new SitemapNode { Url = "https://localhost:7176/" });
            siteList.Add(new SitemapNode { Url = "https://localhost:7176/Topic/Index/" });
            siteList.Add(new SitemapNode { Url = "https://localhost:7176/Home/Topic/" });
            siteList.Add(new SitemapNode { Url = "https://localhost:7176/User/UserDetails" });
            siteList.Add(new SitemapNode { Url = "https://localhost:7176/User/UserSettings" });

            new SitemapDocument().CreateSitemapXML(siteList, _env.ContentRootPath);


            await _next(context);
        }
    }

    public static class CreateSitemapMiddlewareExtensions
    {
        public static IApplicationBuilder UseCreateSitemap(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CreateSitemapMiddleware>();
        }
    }
}

