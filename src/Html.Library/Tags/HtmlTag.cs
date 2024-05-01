namespace HtmlTemplateGenerator.Html;

public static class HtmlTag
{
    public static string TableOpen => "<table align=\"center\" border=\"0\" cellpadding=\"1\" cellspacing=\"15\">";
    public static string TableClose => "</table>";
    public static string TableBodyOpen => "<tbody>";
    public static string TableBodyClose => "</tbody>";
    public static string TableRowOpen => "<tr>";
    public static string TableRowClose => "</tr>";
    public static string TableCellOpen(int colSpan = 1) => $"<td colspan=\"{colSpan}\">";
    public static string TableCellClose => "</td>";
    public static string Img(string src, string alt = "") => $"<img src=\"{src}\" alt=\"{alt}\" />";
    public static string H2(string text) => $"<h2>{text}</h2>";
    public static string H3(string text) => $"<h3>{text}</h3>";
    public static string P(string text) => $"<p>{text}</p>";
    public static string UlOpen => "<ul>";
    public static string UlClose => "</ul>";
    public static string Li(string text) => $"<li>{text}</li>";
    public static string Strong(string text) => $"<strong>{text}</strong>";
    public static string Video(string src) => $"<iframe width=\"560\" height=\"315\" src=\"{src}\" title=\"YouTube video player\" frameborder=\"0\" allow=\"accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share\" referrerpolicy=\"strict-origin-when-cross-origin\" allowfullscreen></iframe>";

}