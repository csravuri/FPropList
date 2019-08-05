using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FPropList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.txtFolderPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            LoadFoderFileList(GetCleanFilesList(GetFileList(this.txtFolderPath.Text), this.txtFolderPath.Text));
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderDialog.ShowNewFolderButton = false;
            folderDialog.ShowDialog();

            this.txtFolderPath.Text = folderDialog.SelectedPath;

            LoadFoderFileList(GetCleanFilesList(GetFileList(this.txtFolderPath.Text), this.txtFolderPath.Text));

        }


        private string[] GetFileList(string folderPath)
        {
            string[] filesList = Directory.GetDirectories(folderPath);
            filesList = filesList.Concat(Directory.GetFiles(folderPath)).ToArray();
            return filesList;
        }

        private string GetFolderSize(string folderPath)
        {
            string[] allFiles = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
            long totalSize = allFiles.Select(x => new FileInfo(x).Length).Sum();
            return GetReadableSize(totalSize);
        }

        private string[] GetCleanFilesList(string[] dirtyFilesList, string folderPath)
        {
            return dirtyFilesList.Select(x => x.Substring(folderPath.Length + 1)).ToArray();
        }


        private void LoadFoderFileList(string[] filesList)
        {
            lvFolderList.ItemsSource = filesList;
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

    }
}
