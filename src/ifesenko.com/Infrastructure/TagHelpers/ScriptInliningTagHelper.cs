﻿using System;
using System.IO;
using System.Text;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;

namespace ifesenko.com.Infrastructure.TagHelpers
{
    [HtmlTargetElement("script", Attributes = "inline")]
    public class ScriptInliningTagHelper : TagHelper
    {
        private readonly IFileProvider _wwwroot;

        public ScriptInliningTagHelper(IHostingEnvironment env)
        {
            _wwwroot = env.WebRootFileProvider;
        }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public bool Inline { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Inline)
            {
                return;
            }

            var src = output.Attributes["src"];

            var path = src?.Value.ToString();
            if (path == null)
            {
                return;
            }

            var resolvedPath = path;
            var queryStringStartIndex = path.IndexOf('?');
            if (queryStringStartIndex != -1)
            {
                resolvedPath = path.Substring(0, queryStringStartIndex);
            }

            Uri uri;
            if (Uri.TryCreate(resolvedPath, UriKind.Absolute, out uri))
            {
                // Don't inline if the path is absolute
                return;
            }

            var fileInfo = _wwwroot.GetFileInfo(resolvedPath);
            var requestPathBase = ViewContext.HttpContext.Request.PathBase;
            if (!fileInfo.Exists)
            {
                if (requestPathBase.HasValue &&
                    resolvedPath.StartsWith(requestPathBase.Value, StringComparison.OrdinalIgnoreCase))
                {
                    resolvedPath = resolvedPath.Substring(requestPathBase.Value.Length);
                    fileInfo = _wwwroot.GetFileInfo(resolvedPath);
                }

                if (!fileInfo.Exists)
                {
                    // Don't inline if the file is not on the current server
                    return;
                }
            }

            using (var readStream = fileInfo.CreateReadStream())
            using (var reader = new StreamReader(readStream, Encoding.UTF8))
            {
                output.Content.AppendHtml(reader.ReadToEnd());
            }

            output.Attributes.Remove(src);
        }
    }
}
