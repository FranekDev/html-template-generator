using System.Text;
using Html.Tags;

namespace Html.Builder;

public class HtmlBuilder
{
    private readonly StringBuilder _htmlPage = new();
    private int _indentLevel = 0;
    public string ResultHtml => _htmlPage.ToString();
    
    private void IncrementIndent()
	{
	    _indentLevel++;
	}

	private void DecrementIndent()
	{
		if (_indentLevel < 1)
		{
			_indentLevel = 0;
			return;
		}
		
		_indentLevel-- ;
	}
    
    private void AppendToHtml(string html)
    {
	    var indentSpaces = _indentLevel * 4;
	    var indent = new string(' ', indentSpaces);
		_htmlPage.Append($"{indent}{html}\n");
	}
    
    public HtmlBuilder TableOpen()
	{
		AppendToHtml(HtmlTag.TableOpen);
		IncrementIndent();
		return this;
	}

	public HtmlBuilder TableClose()
	{
		DecrementIndent();
		AppendToHtml(HtmlTag.TableClose);
		return this;
	}
	
	public HtmlBuilder TableBodyOpen()
	{
		AppendToHtml(HtmlTag.TableBodyOpen);
		IncrementIndent();
		return this;
	}
	
	public HtmlBuilder TableBodyClose()
	{
		DecrementIndent();
		AppendToHtml(HtmlTag.TableBodyClose);
		return this;
	}
	
	public HtmlBuilder TableRowOpen()
	{
		AppendToHtml(HtmlTag.TableRowOpen);
		IncrementIndent();
		return this;
	}
	
	public HtmlBuilder TableRowClose()
	{
		DecrementIndent();
		AppendToHtml(HtmlTag.TableRowClose);
		return this;
	}
	
	public HtmlBuilder TableCellOpen(int colSpan = 1)
	{
		AppendToHtml(HtmlTag.TableCellOpen(colSpan));
		IncrementIndent();
		return this;
	}
	
	public HtmlBuilder TableCellClose()
	{
		DecrementIndent();
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
		IncrementIndent();
		return this;
	}
	
	public HtmlBuilder UlClose()
	{
		DecrementIndent();
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