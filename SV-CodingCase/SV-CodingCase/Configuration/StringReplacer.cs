using System.Text.RegularExpressions;

public class StringReplacer
{
    public static void ReplaceStringInFile(string filePath, string stringToReplace, string envVar)
    {
        string? replacement = Environment.GetEnvironmentVariable(envVar)!;

        if (!string.IsNullOrEmpty(replacement) && !string.IsNullOrEmpty(filePath))
        {
            string content = File.ReadAllText(filePath);
            string updatedContent = Regex.Replace(content, Regex.Escape(stringToReplace), replacement);
            File.WriteAllText(filePath, updatedContent);
        }
    }
}
