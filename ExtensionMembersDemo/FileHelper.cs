namespace ExtensionMembersDemo;

// FileUtils
public static class FileHelper
{
    public static FileStream CreateFileRecursively(string filePath, bool overwrite = false)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

        var directory = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directory))
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }
        else
        {
            throw new ArgumentException("Invalid file path: no directory specified.", nameof(filePath));
        }

        FileMode mode = overwrite ? FileMode.Create : FileMode.OpenOrCreate;

        try
        {
            return new FileStream(filePath, mode, FileAccess.ReadWrite, FileShare.None);
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to create or open file: {filePath}", ex);
        }
    }
}
