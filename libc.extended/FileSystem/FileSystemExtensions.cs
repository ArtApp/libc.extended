using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using libc.extended.Comparing;

namespace libc.extended.FileSystem
{
    public static class FileSystemExtensions
    {
        private static readonly DelegateComparer<DirectoryInfo> dc =
            new DelegateComparer<DirectoryInfo>((x, y) =>
                string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase));

        private static readonly DelegateComparer<FileInfo> fc =
            new DelegateComparer<FileInfo>((x, y) =>
                string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase));

        public static bool IsValidForFileName(this string s)
        {
            return Path.GetInvalidFileNameChars().All(a => !s.Contains(a));
        }

        public static bool IsValidForPath(this string s)
        {
            return Path.GetInvalidPathChars().All(a => !s.Contains(a));
        }

        public static void Empty(this DirectoryInfo dir)
        {
            var files = dir.GetFiles();
            var dirs = dir.GetDirectories();

            foreach (var f in files)
                try
                {
                    f.Delete();
                }
                catch (Exception)
                {
                    // ignored
                }

            foreach (var d in dirs)
                try
                {
                    d.Delete(true);
                }
                catch (Exception)
                {
                    // ignored
                }
        }

        public static bool DeleteFile(this string s)
        {
            try
            {
                if (!File.Exists(s)) return false;
                File.Delete(s);

                return true;
            }
            catch
            {
                // ignored
            }

            return false;
        }

        public static bool DeleteDirectory(this string s)
        {
            try
            {
                if (Directory.Exists(s))
                {
                    Directory.Delete(s, true);

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void EnsureDir(this string dir)
        {
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }

        public static void DeleteOldFiles(this string dir, int limit, string searchPattern,
            IComparer<FileInfo> comparer = null)
        {
            DeleteOldFiles(new DirectoryInfo(dir), limit, searchPattern, comparer);
        }

        public static void DeleteOldDirectories(this string dir, int limit, string searchPattern,
            IComparer<DirectoryInfo> comparer = null)
        {
            DeleteOldDirectories(new DirectoryInfo(dir), limit, searchPattern, comparer);
        }

        public static void DeleteOldDirectories(this DirectoryInfo dir, int limit, string searchPattern,
            IComparer<DirectoryInfo> comparer = null)
        {
            var c = comparer ?? dc;

            var dirs = dir.EnumerateDirectories(searchPattern, SearchOption.TopDirectoryOnly)
                .OrderByDescending(a => a, c)
                .Skip(limit)
                .ToList();

            foreach (var d in dirs)
                try
                {
                    d.Delete(true);
                }
                catch (Exception)
                {
                    // ignored
                }
        }

        public static void DeleteOldFiles(this DirectoryInfo dir, int limit, string searchPattern,
            IComparer<FileInfo> comparer = null)
        {
            var c = comparer ?? fc;

            var files = dir.EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly)
                .OrderByDescending(a => a, c)
                .Skip(limit)
                .ToList();

            foreach (var file in files)
                try
                {
                    file.Delete();
                }
                catch (Exception)
                {
                    // ignored
                }
        }
    }
}