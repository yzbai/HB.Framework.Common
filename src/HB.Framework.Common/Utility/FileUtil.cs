#nullable enable

using System.IO;
using System.Threading;

namespace System
{
    public static class FileUtil
    {
        public static bool TrySaveToFile(byte[] buffer, string path)
        {
            try
            {
                using FileStream fileStream = new FileStream(path, FileMode.CreateNew);
                using BinaryWriter binaryWriter = new BinaryWriter(fileStream);

                binaryWriter.Write(buffer);

                binaryWriter.Close();

                fileStream.Close();

                return true;
            }
            catch (System.Security.SecurityException)
            {
                return false;
            }
            catch (System.UnauthorizedAccessException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (System.ObjectDisposedException)
            {
                return false;
            }
        }

        /// <summary>
        /// ComputeFileHash
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.Reflection.TargetInvocationException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        public static byte[] ComputeFileHash(string filePath)
        {
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
                    //-2147024864意思是 另一个程序正在使用此文件,进程无法访问
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

            throw new FileLoadException();
        }
    }
}
