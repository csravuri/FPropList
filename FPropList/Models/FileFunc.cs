using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPropList.Models
{
    public class FileFunc
    {
        private long _totalFolderSize = 0;
        public List<FileProp> GetFileList(string folderPath)
        {
            List<FileProp> fileProps = new List<FileProp>();

            foreach (string eachFolder in Directory.GetDirectories(folderPath))
            {
                fileProps.Add(new FileProp()
                {
                    FullPath = eachFolder,
                    IsFile = false,
                    Name = GetCleanFileName(eachFolder, folderPath),
                    Size = GetFolderSize(eachFolder),
                    ModifiedDate = Directory.GetLastWriteTime(eachFolder).ToString(GetDefaultDateFormat())
                    
                });
            }

            foreach (string eachFile in Directory.GetFiles(folderPath))
            {
                FileInfo fileInfo = new FileInfo(eachFile);
                fileProps.Add(new FileProp()
                {
                    FullPath = eachFile,
                    IsFile = true,
                    Name = GetCleanFileName(eachFile, folderPath),
                    Size = GetReadableSize(fileInfo.Length),
                    ModifiedDate = fileInfo.LastWriteTime.ToString(GetDefaultDateFormat())
                    
                });

                _totalFolderSize += fileInfo.Length;
            }

            return fileProps;
        }

        private string GetCleanFileName(string fullPath, string folderPath)
        {
            return fullPath.Substring(folderPath.Length + 1);
        }

        private string GetFolderSize(string folderPath)
        {
            try
            {
                string[] allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
                long totalSize = allFiles.Select(x => new FileInfo(x).Length).Sum();
                _totalFolderSize += totalSize;
                return GetReadableSize(totalSize);
            }
            catch (UnauthorizedAccessException)
            {
                return "-";
            }
            catch(Exception)
            {
                return "E0012";
            }
        }

        private string GetReadableSize(long fileSize)
        {
            if (fileSize >= 1024 * 1024 * 1024)
            {
                return $"{fileSize / (1024 * 1024 * 1024)}GB";
            }
            else if (fileSize >= 1024 * 1024)
            {
                return $"{fileSize / (1024 * 1024)}MB";
            }
            else if (fileSize >= 1024)
            {
                return $"{fileSize / (1024)}KB";
            }
            else
            {
                return "0KB";
            }
        }

        public string GetFolderTotalSize()
        {
            return GetReadableSize(_totalFolderSize);
        }

        private string GetDefaultDateFormat()
        {
            return "dd-MM-yyyy HH:mm:ss";
        }
    }
}
