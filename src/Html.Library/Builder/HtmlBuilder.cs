using System.Text;
using Html.Builder.Interfaces;
using Html.Tags;

namespace Html.Builder;

public class HtmlBuilder : IHtmlBuilder
{
    private readonly StringBuilder _htmlPage = new();
    private int _indentLevel = 0;
    public string ResultHtml => _htmlPage.ToString();
    
    public void IncrementIndent()
	{
	    _indentLevel++;
	}

	public void DecrementIndent()
	{
		if (_indentLevel < 1)
		{
			_indentLevel = 0;
			return;
		}
		
		_indentLevel-- ;
	}
    
    public void AppendToHtml(string html)
    {
	    var indentSpaces = _indentLevel * 4;
	    var indent = new string(' ', indentSpaces);
		_htmlPage.Append($"{indent}{html}\n");
	}
    
    public IHtmlBuilder TableOpen()
	{
		AppendToHtml(HtmlTag.TableOpen);
		IncrementIndent();
		return this;
	}

	public IHtmlBuilder TableClose()
	{
		DecrementIndent();
		AppendToHtml(HtmlTag.TableClose);
		return this;
	}
	
	public IHtmlBuilder TableBodyOpen()
	{
		AppendToHtml(HtmlTag.TableBodyOpen);
		IncrementIndent();
		return this;
	}
	
	public IHtmlBuilder TableBodyClose()
	{
		DecrementIndent();
		AppendToHtml(HtmlTag.TableBodyClose);
		return this;
	}
	
	public IHtmlBuilder TableRowOpen()
	{
		AppendToHtml(HtmlTag.TableRowOpen);
		IncrementIndent();
		return this;
	}
	
	public IHtmlBuilder TableRowClose()
	{
		DecrementIndent();
		AppendToHtml(HtmlTag.TableRowClose);
		return this;
	}
	
	public IHtmlBuilder TableCellOpen(int colSpan = 1)
	{
		AppendToHtml(HtmlTag.TableCellOpen(colSpan));
		IncrementIndent();
		return this;
	}
	
	public IHtmlBuilder TableCellClose()
	{
		DecrementIndent();
		AppendToHtml(HtmlTag.TableCellClose);
		return this;
	}
	
	public IHtmlBuilder Img(string src, string alt = "", string? style = null)
	{
		AppendToHtml(HtmlTag.Img(src, alt, style));
		return this;
	}
	
	public IHtmlBuilder H2(string text)
	{
		AppendToHtml(HtmlTag.H2(text));
		return this;
	}
	
	public IHtmlBuilder H3(string text)
	{
		AppendToHtml(HtmlTag.H3(text));
		return this;
	}
	
	public IHtmlBuilder P(string text)
	{
		AppendToHtml(HtmlTag.P(text));
		return this;
	}
	
	public IHtmlBuilder UlOpen()
	{
		AppendToHtml(HtmlTag.UlOpen);
		IncrementIndent();
		return this;
	}
	
	public IHtmlBuilder UlClose()
	{
		DecrementIndent();
		AppendToHtml(HtmlTag.UlClose);
		return this;
	}
	
	public IHtmlBuilder Li(string text)
	{
		AppendToHtml(HtmlTag.Li(text));
		return this;
	}
	
	public IHtmlBuilder Strong(string text)
	{
		AppendToHtml(HtmlTag.Strong(text));
		return this;
	}
	
	public IHtmlBuilder Video(string src)
	{
		AppendToHtml(HtmlTag.Video(src));
		return this;
	}
	
	public IHtmlBuilder UlWithLi(Dictionary<string, string> items, Func<KeyValuePair<string, string>, string> itemTemplate)
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