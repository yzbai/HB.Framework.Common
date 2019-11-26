using System.IO;
using System.Threading;

namespace System
{
    public static class FileHelper
    {
        //public static async Task<byte[]> ReadFormFileAsync(IFormFile file)
        //{
        //    if (file == null)
        //    {
        //        throw new ArgumentNullException(nameof(file));
        //    }

        //    using (Stream stream = file.OpenReadStream())
        //    {
        //        byte[] buffer = new byte[stream.Length];

        //        await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

        //        stream.Close();

        //        return buffer;
        //    }
        //}

        public static void SaveToFile(byte[] buffer, string path)
        {
            ThrowIf.NullOrEmpty(path, nameof(path));
            ThrowIf.Null(buffer, nameof(buffer));

            using FileStream fileStream = new FileStream(path, FileMode.CreateNew);
            using BinaryWriter binaryWriter = new BinaryWriter(fileStream);

            binaryWriter.Write(buffer);

            binaryWriter.Close();

            fileStream.Close();
        }

        public static byte[] ComputeHash(string filePath)
        {
            ThrowIf.NullOrEmpty(filePath, nameof(filePath));

            int runCount = 1;

            while (runCount < 4)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        using FileStream fs = File.OpenRead(filePath);
                        using System.Security.Cryptography.SHA256 sha256Obj = System.Security.Cryptography.SHA256.Create();
                        return sha256Obj.ComputeHash(fs);
                    }
                    else
                    {
                        throw new FileNotFoundException();
                    }
                }
                catch (IOException ex)
                {
                    if (runCount == 3 || ex.HResult != -2147024864)
                    {
                        throw;
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(Math.Pow(2, runCount)));
                        runCount++;
                    }
                }
            }

            return new byte[20];
        }
    }
}
