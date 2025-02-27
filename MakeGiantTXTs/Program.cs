

using System.Text;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2 || args.Length > 3)
        {
            Console.WriteLine("Usage: MakeGiantTXTs <-random|text> <size> [output_file]");
            return;
        }

        string text = args[0];
        if (!TryParseSize(args[1], out long sizeInBytes))
        {
            Console.WriteLine("Invalid size argument.");
            return;
        }

        string outputPath = args.Length == 3 ? args[2] : "output.txt";
        using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            long currentSize = 0;
            int lastProgress = 0;
            Random random = new Random();
            long numberofBytestoWrite = sizeInBytes;

            while (currentSize < sizeInBytes)
            {
                if (text == "-random")
                {
                    byte[] content = GenerateRandomBytes(random, numberofBytestoWrite);
                    fs.Write(content, 0, content.Length);
                    currentSize += content.Length;
                    numberofBytestoWrite -= content.Length;
                }
                else
                {
                    byte[] content = Encoding.UTF8.GetBytes(text);
                    fs.Write(content, 0, content.Length);
                    currentSize += content.Length;
                    numberofBytestoWrite -= content.Length;
                }

                int progress = (int)Math.Min((currentSize * 100) / sizeInBytes, 100);
                if (progress > lastProgress)
                {
                    Console.Write($"\rGenerating file : {progress}%");
                    lastProgress = progress;
                }
            }
        }

        int finalfilesize = (int)new FileInfo(outputPath).Length;

        if (finalfilesize < sizeInBytes)
        {
            Console.WriteLine($"\n\nThe output file \"{outputPath}\" is created with {FormatSize(finalfilesize)} ({FormatNumber(finalfilesize)} bytes) in size.");
            Console.WriteLine($"The requested size was {FormatSize(sizeInBytes)} ({FormatNumber(sizeInBytes)} bytes).");
            Console.WriteLine("The file is smaller than the requested size.");
        }
        else if (finalfilesize > sizeInBytes)
        {
            Console.WriteLine($"\n\nThe output file \"{outputPath}\" is created with {FormatSize(finalfilesize)} ({FormatNumber(finalfilesize)} bytes) in size.");
            Console.WriteLine($"The requested size was {FormatSize(sizeInBytes)} ({FormatNumber(sizeInBytes)} bytes).");
            Console.WriteLine("The file is larger than the requested size.");
        }
        else
            Console.WriteLine($"\n\nThe output file \"{outputPath}\" is created with {FormatSize(sizeInBytes)} ({FormatSizeOctets(sizeInBytes)}) in size.");

    }

    static bool TryParseSize(string size, out long sizeInBytes)
    {
        sizeInBytes = 0;
        if (long.TryParse(size, out sizeInBytes))
        {
            return true;
        }

        var sizeSuffixes = new Dictionary<string, long>
                {
                    { "B", 1L },
                    { "KB", 1000L },
                    { "MB", 1000L * 1000L },
                    { "GB", 1000L * 1000L * 1000L },
                    { "TB", 1000L * 1000L * 1000L * 1000L },
                    { "K", 1024L },
                    { "M", 1024L * 1024L },
                    { "G", 1024L * 1024L * 1024L },
                    { "T", 1024L * 1024L * 1024L * 1024L }
                };

        foreach (var suffix in sizeSuffixes)
        {
            if (size.EndsWith(suffix.Key, StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(size.Substring(0, size.Length - suffix.Key.Length), out long number))
                {
                    sizeInBytes = number * suffix.Value;
                    return true;
                }
            }
        }

        return false;
    }

    static byte[] GenerateRandomBytes(Random random, long length)
    {
        const int maxChunkSize = 1000 * 1000; // 1 MB
        const int maxDataSize = 1000 * 1000; // 1 MB

        int streamSize = 0;

        using (MemoryStream ms = new MemoryStream())
        {
            while (length > 0)
            {
                int chunkSize = (int)Math.Min(maxChunkSize, length);
                byte[] chunk = new byte[chunkSize];
                random.NextBytes(chunk);
                ms.Write(chunk, 0, chunkSize);
                length -= chunkSize;
                streamSize += chunkSize;

                if (streamSize >= maxDataSize)
                {
                    return ms.ToArray();
                }

            }
            return ms.ToArray();
        }
    }

    static string FormatSize(long sizeInBytes)
    {
        string[] sizes = { "bytes", "KB", "MB", "GB", "TB" };
        double len = sizeInBytes;
        int order = 0;
        while (len >= 1000 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1000;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    static string FormatNumber(long number)
    {
        return number.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
    }

    static string FormatSizeOctets(long sizeInBytes)
    {
        string[] sizes = { "octets", "Ko", "Mo", "Go", "To" };
        double len = sizeInBytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

}
