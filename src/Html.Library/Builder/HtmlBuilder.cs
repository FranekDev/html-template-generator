using System.Text;

namespace HtmlTemplateGenerator.Html;

public class HtmlBuilder
{
    private readonly StringBuilder _htmlPage = new();
    public string ResultHtml => _htmlPage.ToString();
    
    private void AppendToHtml(string html)
	{
		_htmlPage.Append($"{html}\n");
	}
    
    public HtmlBuilder TableOpen()
	{
		AppendToHtml(HtmlTag.TableOpen);
		return this;
	}

	public HtmlBuilder TableClose()
	{
		AppendToHtml(HtmlTag.TableClose);
		return this;
	}
	
	public HtmlBuilder TableBodyOpen()
	{
		AppendToHtml(HtmlTag.TableBodyOpen);
		return this;
	}
	
	public HtmlBuilder TableBodyClose()
	{
		AppendToHtml(HtmlTag.TableBodyClose);
		return this;
	}
	
	public HtmlBuilder TableRowOpen()
	{
		AppendToHtml(HtmlTag.TableRowOpen);
		return this;
	}
	
	public HtmlBuilder TableRowClose()
	{
		AppendToHtml(HtmlTag.TableRowClose);
		return this;
	}
	
	public HtmlBuilder TableCellOpen(int colSpan = 1)
	{
		AppendToHtml(HtmlTag.TableCellOpen(colSpan));
		return this;
	}
	
	public HtmlBuilder TableCellClose()
	{
		AppendToHtml(HtmlTag.TableCellClose);
		return this;
	}
	
	public HtmlBuilder Img(string src, string alt = "")
	{
		AppendToHtml(HtmlTag.Img(src, alt));
		return this;
	}
	
	public HtmlBuilder H2(string text)
	{
		AppendToHtml(HtmlTag.H2(text));
		return this;
	}
	
	public HtmlBuilder H3(string text)
	{
		AppendToHtml(HtmlTag.H3(text));
		return this;
	}
	
	public HtmlBuilder P(string text)
	{
		AppendToHtml(HtmlTag.P(text));
		return this;
	}
	
	public HtmlBuilder UlOpen()
	{
		AppendToHtml(HtmlTag.UlOpen);
		return this;
	}
	
	public HtmlBuilder UlClose()
	{
		AppendToHtml(HtmlTag.UlClose);
		return this;
	}
	
	public HtmlBuilder Li(string text)
	{
		AppendToHtml(HtmlTag.Li(text));
		return this;
	}
	
	public HtmlBuilder Strong(string text)
	{
		AppendToHtml(HtmlTag.Strong(text));
		return this;
	}
	
	public HtmlBuilder Video(string src)
	{
		AppendToHtml(HtmlTag.Video(src));
		return this;
	}
	
	public HtmlBuilder UlWithLi(Dictionary<string, string> items, Func<KeyValuePair<string, string>, string> itemTemplate)
	{
		UlOpen();
		foreach (var item in items)
		{
			Li(itemTemplate(item));
		}
		UlClose();
		return this;
	}
}