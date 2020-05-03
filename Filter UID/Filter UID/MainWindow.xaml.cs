using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using CSVLib;
using System.Windows.Threading;
using Path = System.IO.Path;

namespace Filter_UID
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> dataIdBefore = new List<string>();
        private List<string> dataPhoneBefore = new List<string>();
        private List<string> dataIdAfter = new List<string>();
        private List<string> dataPhoneAfter = new List<string>();
        private List<string> dataIdNoDuplicate = new List<string>();
        private List<string> dataPhoneNoDuplicate = new List<string>();
        private List<string> dataIdOpponent = new List<string>();
        private List<string> dataIdNoDuplicateFinal = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

        }


        private void UIDBefore_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"Documents";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (CsvFileReader csvReader = new CsvFileReader(openFileDialog.FileName))
                    {
                        UIDBefore.Text = openFileDialog.FileName;
                        List<string> row = new List<string>();
                        List<string> dataid = new List<string>();
                        List<string> dataphone = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            dataid.Add(row[1]);
                            dataphone.Add(row[4]);
                        }
                        dataIdBefore = dataid.Distinct().ToList();
                        dataPhoneBefore = dataphone.Distinct().ToList();
                    }
                }
                catch (Exception)
                {
                    UIDBefore.Text = "";
                    MessageBoxResult result = MessageBox.Show("Lỗi. Bạn đang mở file hoặc file bạn đang bị lỗi. Làm ơn check lại !!!!");
                }
            }
        }

        private void UIDAfter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"Documents";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (CsvFileReader csvReader = new CsvFileReader(openFileDialog.FileName))
                    {
                        NameFileExportFolder.Text = openFileDialog.FileName.Substring(0, openFileDialog.FileName.Length - openFileDialog.SafeFileName.Length - 1);
                        UIDAfter.Text = openFileDialog.FileName;
                        List<string> row = new List<string>();
                        List<string> dataid = new List<string>();
                        List<string> dataphone = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            dataid.Add(row[1]);
                            dataphone.Add(row[4]);
                        }
                        dataIdAfter = dataid.Distinct().ToList();
                        dataPhoneAfter = dataphone.Distinct().ToList();
                    }
                }
                catch (Exception)
                {
                    NameFileExportFolder.Text = "";
                    UIDAfter.Text = "";
                    MessageBoxResult result = MessageBox.Show("Lỗi. Bạn đang mở file hoặc file bạn đang bị lỗi. Làm ơn check lại !!!!");
                }
            }

        }

        private void UIDOpponent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files (*.csv)|*.csv|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"Documents";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (CsvFileReader csvReader = new CsvFileReader(openFileDialog.FileName))
                    {
                        UIDOpponent.Text = openFileDialog.FileName;
                        List<string> row = new List<string>();
                        List<string> dataid = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            dataid.Add(row[0]);
                        }
                        dataIdOpponent = dataid.Distinct().ToList();
                    }
                }
                catch (Exception)
                {
                    UIDOpponent.Text = "";
                    MessageBoxResult result = MessageBox.Show("Lỗi. Bạn đang mở file hoặc file bạn đang bị lỗi. Làm ơn check lại !!!!");
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataIdNoDuplicate = dataIdAfter.Except(dataIdBefore).ToList();
            dataPhoneNoDuplicate = dataPhoneAfter.Except(dataPhoneBefore).ToList();
            if (dataIdOpponent.Count() == 0)
            {
                DirectoryInfo openFileDialog = new DirectoryInfo(NameFileExportFolder.Text);
                if (openFileDialog.Exists)
                {
                    if (checkUID.IsChecked == true)
                    {
                        string path = openFileDialog.FullName + "/" + NameFileExport.Text + "_UID" + ".txt";
                        if (dataIdNoDuplicate.Count() > 0 && dataIdNoDuplicate.ElementAt(0).ToString() == "Id")
                        {
                            dataIdNoDuplicate.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (string item in dataIdNoDuplicate)
                            {
                                sw.WriteLine(item);
                            }
                        }
                    }
                    if (checkPhone.IsChecked == true)
                    {
                        string path = openFileDialog.FullName + "/" + NameFileExport.Text + "_Phone" + ".txt";
                        if (dataPhoneNoDuplicate.Count() > 0 && dataPhoneNoDuplicate.ElementAt(0).ToString() == "Điện thoại")
                        {
                            dataPhoneNoDuplicate.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (string item in dataPhoneNoDuplicate)
                            {
                                sw.WriteLine(item);
                            }
                        }
                    }
                    if (checkUID.IsChecked == false && checkPhone.IsChecked == false)
                    {
                        MessageBoxResult result = MessageBox.Show("Chọn xuất ra file UID hoặc số điện thoại");
                        return;
                    }
                    MessageBoxResult success = MessageBox.Show("Export success");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Folder không tồn tại");
                }
            }
            else
            {
                dataIdNoDuplicateFinal = dataIdNoDuplicate.Except(dataIdOpponent).ToList();
                DirectoryInfo openFileDialog = new DirectoryInfo(NameFileExportFolder.Text);
                if (openFileDialog.Exists)
                {
                    if (checkUID.IsChecked == true)
                    {
                        string path = openFileDialog.FullName + "/" + NameFileExport.Text + "_UID" + ".txt";
                        if (dataIdNoDuplicateFinal.Count() > 0 && dataIdNoDuplicateFinal.ElementAt(0).ToString() == "Id")
                        {
                            dataIdNoDuplicateFinal.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (string item in dataIdNoDuplicateFinal)
                            {
                                sw.WriteLine(item);
                            }
                        }
                    }
                    if (checkPhone.IsChecked == true)
                    {
                        string path = openFileDialog.FullName + "/" + NameFileExport.Text + "_Phone" + ".txt";
                        if (dataPhoneNoDuplicate.Count() > 0 && dataPhoneNoDuplicate.ElementAt(0).ToString() == "Điện thoại")
                        {
                            dataPhoneNoDuplicate.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (string item in dataPhoneNoDuplicate)
                            {
                                sw.WriteLine(item);
                            }
                        }
                    }
                    if (checkUID.IsChecked == false && checkPhone.IsChecked == false)
                    {
                        MessageBoxResult result = MessageBox.Show("Chọn xuất ra file UID hoặc số điện thoại");
                        return;
                    }
                    MessageBoxResult success = MessageBox.Show("Export success");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Folder không tồn tại");
                }
            }

        }


        private void Comparefilebeforeafter_Click(object sender, RoutedEventArgs e)
        {
            dataIdNoDuplicate = dataIdAfter.Except(dataIdBefore).ToList();
            dataPhoneNoDuplicate = dataPhoneAfter.Except(dataPhoneBefore).ToList();
            MessageBoxResult result = MessageBox.Show("Compare success");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += Check_Button;
            dt.Start();
        }

        private void Check_Button(object sender, EventArgs e)
        {
            if (UIDBefore.Text != "" && UIDAfter.Text != "")
            {
                if (UIDAfter.Text != "")
                {
                        ExportFile.IsEnabled = true;
                }
            }
        }

        private void ResetEverything()
        {
            UIDBefore.Text = "";
            UIDAfter.Text = "";
            UIDOpponent.Text = "";
            NameFileExportFolder.Text = "";
            NameFileExport.Text = "";
            checkUID.IsChecked = true;
            checkPhone.IsChecked = false;
            dataIdBefore = new List<string>();
            dataPhoneBefore = new List<string>();
            dataIdAfter = new List<string>();
            dataPhoneAfter = new List<string>();
            dataIdNoDuplicate = new List<string>();
            dataPhoneNoDuplicate = new List<string>();
            dataIdOpponent = new List<string>();
            dataIdNoDuplicateFinal = new List<string>();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetEverything();
        }
    }
}
