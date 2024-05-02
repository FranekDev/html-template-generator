using Html.Builder;
using Html.Tags;
using HtmlTemplateGenerator.Models;
using HtmlTemplateGenerator.Static;

namespace HtmlTemplateGenerator.Builder;

public class TemplateBuilder
{
    public string GenerateHtmlTemplate(
        Item item,
        bool shouldRenderBannerImage,
        bool shouldRenderHeader,
        bool shouldRenderDescriptions,
        bool shouldRenderSpecification,
        bool shouldRenderVideos
    )
    {
        var html = new TemplateHtmlBuilder()
            .TableBody(table => 
                RenderTableBody(table, item, shouldRenderBannerImage, shouldRenderHeader, shouldRenderDescriptions)
            );

        if (shouldRenderVideos)
        {
            html.RenderVideos(item.Videos);
        }

        if (shouldRenderSpecification && item.Specification is not null)
        {
            html.RenderSpecification(item.Specification);
        }

        return html.ResultHtml;
    }
    
    private TemplateHtmlBuilder RenderTableBody(
        TemplateHtmlBuilder table, 
        Item item, 
        bool shouldRenderBannerImage,
        bool shouldRenderHeader,
        bool shouldRenderDescriptions
    )
    {
        if (shouldRenderBannerImage && item.BannerImageSrc is not null)
        {
            table.RenderBannerImage(item.BannerImageSrc);
        }

        if (shouldRenderHeader && item.Header is not null)
        {
            table.RenderHeader(item.Header);
        }

        if (shouldRenderDescriptions)
        {
            table.RenderDescriptions(item.Descriptions);
        }

        return table;
    }

    private class TemplateHtmlBuilder : HtmlBuilder
    {
        public TemplateHtmlBuilder TableBody(Func<TemplateHtmlBuilder, TemplateHtmlBuilder> renderTableBody)
        {
            TableOpen();
            TableBodyOpen();
            renderTableBody(this);
            TableBodyClose();
            TableClose();
            return this;
        }

        public TemplateHtmlBuilder RenderBannerImage(string bannerUrl, int columnSpan = 2)
        {
            TableRowOpen();
            TableCellOpen(columnSpan);
            Img(bannerUrl);
            TableCellClose();
            TableRowClose();
            return this;
        }

        public TemplateHtmlBuilder RenderHeader(string headerText, int columnSpan = 2)
        {
            TableRowOpen();
            TableCellOpen(columnSpan);
            H2(headerText);
            TableCellClose();
            TableRowClose();
            return this;
        }

        public TemplateHtmlBuilder RenderDescriptions(IEnumerable<Description> items)
        {
            foreach (var item in items)
            {
                TableRowOpen();
                RenderDescription(item);
                TableRowClose();
            }

            return this;
        }

        private void RenderDescription(Description description)
        {
            TableCellOpen();
            if (description.Title is not null)
            {
                H2(description.Title);
            }

            if (description.Text is not null)
            {
                P(description.Text);
            }
            TableCellClose();
        }


        public void RenderVideos(IEnumerable<Video> videos)
        {
            foreach (var video in videos)
            {
                if (video.Title is not null)
                {
                    H2(video.Title);
                }

                if (video.Description is not null)
                {
                    P(video.Description);
                }

                if (video.Url is not null)
                {
                    P(HtmlTag.Video(video.Url));
                }
            }
        }

        public void RenderSpecification(Specification specification)
        {
            if (specification.Title is not null)
            {
                H2($"{specification.Title}:");
            }

            UlWithLi(specification.Items.MapToDict(), item =>
                $"{HtmlTag.Strong($"{item.Key}:")} {item.Value}"
            );
        }
    }
}