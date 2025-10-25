using System.Reflection;

namespace Mangos.MySql;

internal static class SqlScripts
{
    public static string ReadEmbeddedResource(string folderName, string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"{assembly.GetName().Name}.{folderName}.{fileName}";
        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new Exception($"Could not fine embeded file {resourceName}");
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}