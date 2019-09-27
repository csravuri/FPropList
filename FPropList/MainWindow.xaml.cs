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
        FileFunc fileFunctions = null;
        public MainWindow()
        {
            InitializeComponent();

            this.txtFolderPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            fileFunctions = new FileFunc();

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
            listView.ItemsSource = fileFunctions.GetFileList(this.txtFolderPath.Text);

            lblTotalSize.Content = fileFunctions.GetFolderTotalSize();
        }



    }
}
