using System.IO;
using System.Threading;

namespace System
{
    public static class FileUtil
    {
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public static bool TrySaveToFile(byte[] buffer, string path)
        {
            ThrowIf.NullOrEmpty(path, nameof(path));
            ThrowIf.Null(buffer, nameof(buffer));

            try
            {
                using FileStream fileStream = new FileStream(path, FileMode.CreateNew);
                using BinaryWriter binaryWriter = new BinaryWriter(fileStream);

                binaryWriter.Write(buffer);

                binaryWriter.Close();

                fileStream.Close();
            }
            catch(Exception ex)
            {

            }
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
