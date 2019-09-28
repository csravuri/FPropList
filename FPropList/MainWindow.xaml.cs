using FPropList.Models;
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
        private FileFunc _fileFunctions = null;
        public MainWindow()
        {
            InitializeComponent();

            this.txtFolderPath.Text = "D:\\rufus_files"; // Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            _fileFunctions = new FileFunc();

            UpdateFileFolderList();

        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderDialog.ShowNewFolderButton = false;
            folderDialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                return;

            this.txtFolderPath.Text = folderDialog.SelectedPath;

            UpdateFileFolderList();

        }

        private void UpdateFileFolderList()
        {
            _fileFunctions.ResetFolderTotalSize();
            listView.ItemsSource = _fileFunctions.GetFileList(this.txtFolderPath.Text).OrderByDescending(x => x.RawSize);

            this.lblTotalSize.Content = _fileFunctions.GetFolderTotalSize();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileProp fileProp = listView.SelectedItem as FileProp;

            if (fileProp.IsFile)
                return;

            this.txtFolderPath.Text = fileProp.FullPath;

            UpdateFileFolderList();

        }

        private void TxtFolderPath_LostFocus(object sender, RoutedEventArgs e)
        {
            if(isValidFolder(this.txtFolderPath.Text))
            {
                UpdateFileFolderList();
                
            }
            else
            {
                System.Windows.MessageBox.Show("Not a valid Folder", "Warning");
                
            }

        }

        private bool isValidFolder(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        private void TxtFolderPath_GotFocus(object sender, RoutedEventArgs e)
        {
            this.txtFolderPath.SelectAll();
        }
    }
}
