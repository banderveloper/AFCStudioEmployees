namespace AFCStudioEmployees.Application.Extensions;

public static class StringExtensions
{
    public static string ToLowerFirstLetter(this string str)
        => char.ToLower(str[0]) + str.Substring(1);
}