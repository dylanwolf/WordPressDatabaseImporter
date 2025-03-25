using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordPressDatabaseImporter
{
    public class WordPressBlock
    {
        public static WordPressBlock CreateTextNode(string html)
        {
            return new WordPressBlock() { ContentsHtml = html };
        }

        public static WordPressBlock CreateSeparator()
        {
            return new WordPressBlock()
            {
                BlockCode = "wp:separator",
                ContentsHtml = "<hr class=\"wp-block-separator has-alpha-channel-opacity\"/>"
            };
        }

        public static WordPressBlock CreateParagraph(string html)
        {
            return new WordPressBlock()
            {
                BlockCode = "wp:paragraph",
                Prefix = $"<p>",
                Suffix = $"</p>",
                ContentsHtml = html
            };
        }

        public static WordPressBlock CreateHeading(int level, IEnumerable<WordPressBlock> blocks)
        {
            return new WordPressBlock()
            {
                BlockCode = "wp:heading",
                BlockProperties = (level != 2) ? $"{{\"level\":{level}}} " : string.Empty,
                Prefix = $"<h{level} class=\"wp-block-heading\">",
                Suffix = $"</h{level}>",
                Children = blocks
            };
        }

        public static WordPressBlock CreateUnorderedList(IEnumerable<WordPressBlock> blocks)
        {
            return new WordPressBlock()
            {
                BlockCode = "wp:list",
                Prefix = "<ul class=\"wp-block-list\">",
                Suffix = "</ul>",
                Children = blocks
            };
        }

        public static WordPressBlock CreateListItem(IEnumerable<WordPressBlock> blocks)
        {
            return new WordPressBlock()
            {
                BlockCode = "wp:list-item",
                Prefix = "<li>",
                Suffix = "</li>",
                Children = blocks
            };
        }

        public static WordPressBlock CreateBlockQuote(IEnumerable<WordPressBlock> blocks)
        {
            return new WordPressBlock()
            {
                BlockCode = "wp:quote",
                Prefix = "<blockquote class=\"wp-block-quote\">",
                Suffix = "</blockquote>",
                Children = blocks
            };
        }

        public static WordPressBlock CreateImage(string imgTag, string? size = null, string? align = null, string? width = null, string? height = null)
        {
            var propDict = new Dictionary<string, string>();
            var classList = new List<string>() { "wp-block-image" };
            var styleList = new List<string>();

            if (size != null)
            {
                propDict["sizeSlug"] = size;
                classList.Add($"size-{size}");
            }

            if (align != null)
            {
                propDict["align"] = align;
                classList.Add($"align{align}");
            }

            if (width != null)
            {
                propDict["width"] = width;
                styleList.Add($"width:{width}");
            }

            if (height != null)
            {
                propDict["height"] = height;
                styleList.Add($"height:{height}");
            }

            if (width != null || height != null) { classList.Add("is-resized"); }

            imgTag = imgTag.Trim();
            if (imgTag.EndsWith(">") && !imgTag.EndsWith("/>")) imgTag = imgTag.Substring(0, imgTag.Length - 1) + "/>";
            imgTag = imgTag.Replace("/>", $" style=\"{string.Join(";", styleList)}\" />");

            return new WordPressBlock()
            {
                BlockCode = "wp:image",
                BlockProperties = ToPropertyString(propDict),
                ContentsHtml = $"<figure class=\"{string.Join(" ", classList)}\">{imgTag}</figure>"
            };
        }

        public static string ToPropertyString(Dictionary<string, string> properties)
        {
            if (properties == null || !properties.Any()) return string.Empty;
            return $"{{{string.Join(",", properties.Select(kvp => $"\"{kvp.Key}\":\"{kvp.Value}\""))}}} ";
        }

        public string BlockCode { get; internal set; }
        public string BlockProperties { get; internal set; }
        public string Prefix { get; internal set; }
        public string Suffix { get; internal set; }
        public string ContentsHtml { get; internal set; }
        public IEnumerable<WordPressBlock> Children { get; internal set; }

        public string ToHtml()
        {
            var innerHtml = $"{Prefix}{(Children != null ? Children.ToHtml() : ContentsHtml)}{Suffix}";

            if (string.IsNullOrWhiteSpace(BlockCode))
            {
                return innerHtml;
            }

            return $@"<!-- {BlockCode} {BlockProperties}-->
{innerHtml}
<!-- /{BlockCode} -->";
        }
    }

    public static class WordPressBlockEx
    {
        public static string ToHtml(this IEnumerable<WordPressBlock> blocks)
        {
            return string.Join("\n\n", blocks.Select(x => x.ToHtml()));
        }
    }
}
