using HtmlTemplateGenerator.Html;
using HtmlTemplateGenerator.Models;
using HtmlTemplateGenerator.Static;

namespace HtmlTemplateGenerator.Builder;

public class TemplateBuilder
{
    public string GenerateHtmlTemplate(
        ProductListing productListing,
        bool shouldRenderSpecification,
        bool shouldRenderVideos,
        bool shouldRenderArrangementPhoto
    )
    {
        var html = new TemplateHtmlBuilder()
            .TableBody(table =>
                table.RenderCompanyBanner(productListing.CompanyBanner)
                    .RenderHeader("")
                    .RenderDescriptions(productListing.Descriptions)
            );

        if (shouldRenderVideos)
        {
            html.RenderVideos(productListing.Videos);
        }

        if (shouldRenderSpecification && productListing.Specification is not null)
        {
            html.RenderSpecification(productListing.Specification);
        }

        if (shouldRenderArrangementPhoto && productListing.ArrangementPhoto is not null)
        {
            html.RenderArrangementPhoto(productListing.ArrangementPhoto);
        }

        return html.ResultHtml;
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

        public TemplateHtmlBuilder RenderCompanyBanner(string bannerUrl, int columnSpan = 2)
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
            var shouldRenderTextFirst = true;

            foreach (var item in items)
            {
                TableRowOpen();
                RenderDescription(item, shouldRenderTextFirst);
                TableRowClose();
                shouldRenderTextFirst = !shouldRenderTextFirst;
            }

            return this;
        }

        private void RenderDescription(Description description, bool renderTextFirst)
        {
            if (renderTextFirst)
            {
                RenderCellWithText(description.Title, description.Text);
                RenderCellWithImage(description.ImageUrl);
            }
            else
            {
                RenderCellWithImage(description.ImageUrl);
                RenderCellWithText(description.Title, description.Text);
            }
        }

        private void RenderCellWithImage(string imageUrl)
        {
            TableCellOpen();
            H3(
                HtmlTag.Img(imageUrl)
            );
            TableCellClose();
        }

        private void RenderCellWithText(string title, string text)
        {
            TableCellOpen();
            H2(title);
            P(text);
            TableCellClose();
        }

        public void RenderVideos(IEnumerable<Video> videos)
        {
            foreach (var video in videos)
            {
                H2(video.Title);
                P(video.Description);
                P(HtmlTag.Video(video.Url));
            }
        }

        public void RenderSpecification(Specification specification)
        {
            H2($"{specification.Title}:");
            UlWithLi(specification.Items.MapToDict(), item =>
                $"{HtmlTag.Strong($"{item.Key}:")} {item.Value}"
            );
        }

        public void RenderArrangementPhoto(string imageUrl)
        {
            H2(HtmlTag.Img(imageUrl));
        }
    }
}