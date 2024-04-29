using HtmlTemplateGenerator.Html;
using HtmlTemplateGenerator.Models;
using HtmlTemplateGenerator.Static;

namespace HtmlTemplateGenerator.Services;

public class TemplateBuilder
{
    public string GenerateHtmlTemplate(
        ProductListing productListing,
        bool shouldRenderSpecification,
        bool shouldRenderVideos,
        bool shouldRenderArrangementPhoto
    )
    {
        var html = new HtmlBuilder();

        html = html
            .TableOpen()
            .TableBodyOpen();

        html = RenderCompanyBanner(html, productListing.CompanyBanner);
        html = RenderHeader(html, "");
        html = RenderDescriptions(html, productListing.Descriptions);

        html = html
            .TableBodyClose()
            .TableClose();

        if (shouldRenderVideos)
        {
            html = RenderVideos(html, productListing.Videos);
        }

        if (shouldRenderSpecification)
        {
            html = RenderSpecification(html, productListing);
        }

        if (shouldRenderArrangementPhoto)
        {
            html = RenderArrangementPhoto(html, productListing.ArrangementPhoto);
        }

        return html.ResultHtml;
    }

    private HtmlBuilder RenderCompanyBanner(HtmlBuilder html, string bannerUrl, int colSpan = 2)
    {
        return html
            .TableRowOpen()
            .TableCellOpen(colSpan)
            .Img(bannerUrl)
            .TableCellClose()
            .TableRowClose();
    }

    private HtmlBuilder RenderHeader(HtmlBuilder html, string headerText, int colSpan = 2)
    {
        return html
            .TableRowOpen()
            .TableCellOpen(colSpan)
            .H2(headerText)
            .TableCellClose()
            .TableRowClose();
    }

    private HtmlBuilder RenderDescriptions(HtmlBuilder html, IEnumerable<Description> items)
    {
        var isEvenLoop = true;

        foreach (var item in items)
        {
            html = html.TableRowOpen();
            html = RenderDescription(item, html, isEvenLoop);
            html = html.TableRowClose();

            isEvenLoop = !isEvenLoop;
        }

        return html;
    }
    
    private HtmlBuilder RenderDescription(Description description, HtmlBuilder html, bool renderTextFirst)
    {
        if (renderTextFirst)
        {
            html = RenderCellWithText(description.Title, description.Text, html);
            html = RenderCellWithImage(description.ImageUrl, html);
        }
        else
        {
            html = RenderCellWithImage(description.ImageUrl, html);
            html = RenderCellWithText(description.Title, description.Text, html);
        }
        
        return html;
    }

    private HtmlBuilder RenderCellWithImage(string imageUrl, HtmlBuilder html)
    {
        return html
            .TableCellOpen()
            .H3(
                HtmlTag.Img(imageUrl)
            )
            .TableCellClose();
    }

    private HtmlBuilder RenderCellWithText(string title, string text, HtmlBuilder html)
    {
        return html
            .TableCellOpen()
            .H2(title)
            .P(text)
            .TableCellClose();
    }

    private HtmlBuilder RenderVideos(HtmlBuilder html, IEnumerable<Video> videos)
    {
        html = videos.Aggregate(html, (current, video) => current
            .H2(video.Title)
            .P(video.Description)
            .P(HtmlTag.Video(video.Url))
        );

        return html;
    }

    private HtmlBuilder RenderSpecification(HtmlBuilder html, ProductListing productListing)
    {
        return html
            .H2($"{productListing.Specification.Title}:")
            .UlWithLi(productListing.Specification.Items.MapToDict(), item =>
                $"{HtmlTag.Strong($"{item.Key}:")} {item.Value}"
            );
    }

    private HtmlBuilder RenderArrangementPhoto(HtmlBuilder html, string imageUrl)
    {
        return html
            .H2(HtmlTag.Img(imageUrl));
    }
}