using System.Text;
using System.Web;
using DotNetPIS.Domain.Interfaces;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Services;

public abstract class BaseService
{
    protected string ParseStringValue(JToken token, string propertyName)
    {
        return token[propertyName]?.ToString() ?? string.Empty;
    }

    protected float ParseFloatValue(JToken token, string propertyName)
    {
        if (token[propertyName] != null && float.TryParse(token[propertyName]?.ToString(), out float value))
        {
            return value;
        }
        return 0.0f;
    }

    protected string ParseDateTimeString(JToken token, string propertyName)
    {
        string timeProperty = ParseStringValue(token, propertyName);

        string timeString = DateTime.Parse(timeProperty).ToShortTimeString();

        return timeString;
    }

    protected int ParseIntValue(JToken token, string propertyName)
    {
        int value = 0;

        if (token[propertyName] != null)
        {
            value = int.Parse(token[propertyName]?.ToString() ?? string.Empty);
        }

        return value;
    }

    protected bool ParseBooleanValue(JToken token, string propertyName)
    {
        string boolString = ParseStringValue(token, propertyName).ToLower();

        if (boolString.Equals("false") || boolString.Equals("n"))
        {
            return false;
        }
        
        return true;
    }

    protected string DecodeHtmlString(JToken token, string propertyName)
    {
        string parsedString = ParseStringValue(token, propertyName);
        
        return HttpUtility.HtmlDecode(parsedString);
    }

    protected static string RemoveSpecialCharacters(string str)
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            {
                stringBuilder.Append(c);
            }
        }
        return stringBuilder.ToString();
    }
}