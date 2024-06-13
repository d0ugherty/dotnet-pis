using System.Text;
using Newtonsoft.Json.Linq;

namespace DotNetPIS.Domain.Services;

public abstract class BaseJsonService
{
    protected string GetStringValue(JToken token, string propertyName)
    {
        return token[propertyName]?.ToString() ?? string.Empty;
    }

    protected float GetFloatValue(JToken token, string propertyName)
    {
        if (token[propertyName] != null && float.TryParse(token[propertyName]?.ToString(), out float value))
        {
            return value;
        }
        return 0.0f;
    }

    protected string GetDateTimeValue(JToken token, string propertyName)
    {
        string timeProperty = GetStringValue(token, propertyName);
        
        string timeString = DateTime.Parse(timeProperty).ToShortTimeString();

        return timeString;
    }

    protected int GetIntValue(JToken token, string propertyName)
    {
        int value = 0;
        
        if (token[propertyName] != null)
        {
            value = int.Parse(token[propertyName]?.ToString() ?? string.Empty);
        }

        return value;
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