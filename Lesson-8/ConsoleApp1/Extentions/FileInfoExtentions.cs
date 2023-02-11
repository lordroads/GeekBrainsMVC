using System.Diagnostics;

namespace Client.Extentions;

public static class FileInfoExtentions
{
    /// <summary>
    /// ����� ���������� ��� ����� FileInfo.
    /// ��������� ��������� ������ �����.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static Process? Execut(this FileInfo file)
    {
        var processorStartInfo = new ProcessStartInfo(file.FullName)
        {
            UseShellExecute = true,
        };

        return Process.Start(processorStartInfo);
    }
}