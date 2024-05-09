namespace Html.Builder.Interfaces;

public interface IHtmlBuilder
{
	void IncrementIndent();
    void DecrementIndent();
	void AppendToHtml(string html);
    IHtmlBuilder TableOpen();
	IHtmlBuilder TableClose();
	IHtmlBuilder TableBodyOpen();
	IHtmlBuilder TableBodyClose();
	IHtmlBuilder TableRowOpen();
	IHtmlBuilder TableRowClose();
	IHtmlBuilder TableCellOpen(int colSpan = 1);
	IHtmlBuilder TableCellClose();
	IHtmlBuilder Img(string src, string alt = "", string? style = null);
	IHtmlBuilder H2(string text);
	IHtmlBuilder H3(string text);
	IHtmlBuilder P(string text);
	IHtmlBuilder UlOpen();
	IHtmlBuilder UlClose();
	IHtmlBuilder Li(string text);
	IHtmlBuilder Strong(string text);
	IHtmlBuilder Video(string src);
	IHtmlBuilder UlWithLi(Dictionary<string, string> items, Func<KeyValuePair<string, string>, string> itemTemplate);
}