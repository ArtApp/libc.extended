using System;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using libc.extended.Extensions;
using libc.extended.FileSystem;
using Microsoft.Extensions.Logging;
namespace libc.extended.Compression {
    public static class ZipHelper {
        /// <summary>
        ///     Can compress files up to 4GB size
        /// </summary>
        /// <param name="zipFilePath"></param>
        /// <param name="directories"></param>
        /// <param name="files"></param>
        /// <param name="password"></param>
        /// <param name="log"></param>
        /// <param name="deleteSources"></param>
        /// <returns></returns>
        public static FileInfo Create(string zipFilePath, string[] directories, string[] files,
            string password, ILogger log = null, bool deleteSources = false) {
            try {
                if (File.Exists(zipFilePath)) return null;
                using (var fs = File.Create(zipFilePath)) {
                    using (var s = new ZipOutputStream(fs)) {
                        s.UseZip64 = UseZip64.Off;
                        s.SetLevel((int) Deflater.CompressionLevel.BEST_COMPRESSION);
                        if (!string.IsNullOrWhiteSpace(password)) s.Password = password;

                        //insert files
                        if (files != null)
                            foreach (var file in files) {
                                var fi = new FileInfo(file);
                                addFile(s, fi, string.Empty, !string.IsNullOrWhiteSpace(password));
                            }

                        //insert folders
                        if (directories != null)
                            foreach (var directory in directories) {
                                var di = new DirectoryInfo(directory);
                                addDirectory(s, di, string.Empty, !string.IsNullOrWhiteSpace(password));
                            }
                    }
                }
                var res = new FileInfo(zipFilePath);
                if (!res.Exists) return null;
                if (deleteSources) {
                    if (directories != null)
                        foreach (var directory in directories)
                            directory.DeleteDirectory();
                    if (files != null)
                        foreach (var file in files)
                            file.DeleteFile();
                }
                return res;
            } catch (Exception ex) {
                log?.LogError(ex.ToString());
                return null;
            }
        }
        private static void addDirectory(ZipOutputStream s, DirectoryInfo di, string path, bool hasAes = false) {
            var currentPath = string.IsNullOrWhiteSpace(path) ? di.Name : $"{path.RemoveEnding("/")}/{di.Name}";
            foreach (var file in di.GetFiles()) addFile(s, file, currentPath, hasAes);
            foreach (var directory in di.GetDirectories()) addDirectory(s, directory, currentPath, hasAes);
        }
        private static void addFile(ZipOutputStream s, FileInfo fi, string path, bool hasAes = false) {
            var entryName = getFileEntryName(fi, path);
            var entry = new ZipEntry(entryName) {
                DateTime = fi.LastWriteTime,
                IsUnicodeText = true
            };
            if (hasAes) entry.AESKeySize = 256;
            s.PutNextEntry(entry);
            var buffer = new byte[4096];
            using (var fs = File.OpenRead(fi.FullName)) {
                StreamUtils.Copy(fs, s, buffer);
            }
            s.CloseEntry();
        }
        private static string getFileEntryName(FileInfo fi, string path) {
            var name = string.IsNullOrWhiteSpace(path) ? fi.Name : $"{path.RemoveEnding("/")}/{fi.Name}";
            return ZipEntry.CleanName(name);
        }
        public static bool Extract(FileInfo zipFile, DirectoryInfo directory, string password, ILogger log = null,
            bool deleteArchive = false) {
            try {
                if (!directory.Exists) directory.Create();
                var zip = new FastZip {
                    Password = password
                };
                zip.ExtractZip(zipFile.FullName, directory.FullName, null);
                var res = Directory.EnumerateFileSystemEntries(directory.FullName).Any() == false
                    ? null
                    : directory;
                if (res != null && deleteArchive) zipFile.FullName.DeleteFile();
                return res != null;
            } catch (Exception ex) {
                log?.LogError(ex.ToString());
                return false;
            }
        }
    }
}