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

    public class DataInfo
    {
        public string Directory { get; set; }
        public string NameFile { get; set; }
    }
    public partial class MainWindow : Window
    {
        private List<DataInfo> dataListFolder = new List<DataInfo>();
        private List<string> dataIdYesterday = new List<string>();
        private List<string> dataPhoneYesterday = new List<string>();
        private List<string> dataIdToday = new List<string>();
        private List<string> dataPhoneToday = new List<string>();
        private List<string> dataIdOpponent = new List<string>();
        private List<string> dataIdNoDuplicate = new List<string>();
        private List<string> dataPhoneNoDuplicate = new List<string>();
        private List<string> dataIdNoDuplicateFinal = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void DeleteListData()
        {
            dataListFolder.Clear();
        }
        private void ExportFile_Click(object sender, RoutedEventArgs e)
        {
            if (UIDCheck.IsChecked == true && PhoneCheck.IsChecked == true)
            {
                foreach (DataInfo item in dataListFolder)
                {
                    string[] filePaths_HomeTruoc = Directory.GetFiles(item.Directory + "\\UID_Hom_Truoc", "*.csv",
                                         SearchOption.TopDirectoryOnly);
                    // get UID_Hom_Truoc
                    using (CsvFileReader csvReader = new CsvFileReader(filePaths_HomeTruoc[0]))
                    {
                        List<string> row = new List<string>();
                        List<string> dataId = new List<string>();
                        List<string> dataPhone = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            dataId.Add(row[1]);
                            dataPhone.Add(row[4]);
                        }
                        dataIdYesterday = dataId.Distinct().ToList();
                        dataPhoneYesterday = dataPhone.Distinct().ToList();
                    }
                    string[] filePaths_HomeNay = Directory.GetFiles(item.Directory + "\\UID_Hom_Nay", "*.csv",
                                         SearchOption.TopDirectoryOnly);
                    // get UID_Hom_Nay
                    using (CsvFileReader csvReader = new CsvFileReader(filePaths_HomeNay[0]))
                    {
                        List<string> row = new List<string>();
                        List<string> dataId = new List<string>();
                        List<string> dataPhone = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            dataId.Add(row[1]);
                            dataPhone.Add(row[4]);
                        }
                        dataIdToday = dataId.Distinct().ToList();
                        dataPhoneToday = dataPhone.Distinct().ToList();
                    }
                    string[] filePaths_DoiThu = Directory.GetFiles(item.Directory + "\\UID_Doi_Thu", "*.csv",
                                        SearchOption.TopDirectoryOnly);
                    // get UID_Doi_Thu
                    using (CsvFileReader csvReader = new CsvFileReader(filePaths_DoiThu[0]))
                    {
                        List<string> row = new List<string>();
                        List<string> dataId = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            dataId.Add(row[0]);
                        }
                        dataIdOpponent = dataId.Distinct().ToList();
                    }
                    dataIdNoDuplicate = dataIdToday.Except(dataIdYesterday).ToList();
                    dataPhoneNoDuplicate = dataPhoneToday.Except(dataPhoneYesterday).ToList();
                    if (dataIdOpponent.Count() == 0)
                    {
                        string path = item.Directory + "/Ket_Qua/" + item.NameFile + "_UID" + ".txt";
                        if (dataIdNoDuplicate.Count() > 0 && dataIdNoDuplicate.ElementAt(0).ToString() == "Id")
                        {
                            dataIdNoDuplicate.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (string item1 in dataIdNoDuplicate)
                            {
                                sw.WriteLine(item1);
                            }
                        }
                        string path2 = item.Directory + "/Ket_Qua/" + item.NameFile + "_Phone" + ".txt";
                        if (dataPhoneNoDuplicate.Count() > 0 && dataPhoneNoDuplicate.ElementAt(0).ToString() == "Điện thoại")
                        {
                            dataPhoneNoDuplicate.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path2))
                        {
                            foreach (string item2 in dataPhoneNoDuplicate)
                            {
                                sw.WriteLine(item2);
                            }
                        }
                    }
                    else
                    {
                        dataIdNoDuplicateFinal = dataIdNoDuplicate.Except(dataIdOpponent).ToList();

                        string path = item.Directory + "/Ket_Qua/" + item.NameFile + "_UID" + ".txt";
                        if (dataIdNoDuplicateFinal.Count() > 0 && dataIdNoDuplicateFinal.ElementAt(0).ToString() == "Id")
                        {
                            dataIdNoDuplicateFinal.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (string item2 in dataIdNoDuplicateFinal)
                            {
                                sw.WriteLine(item2);
                            }
                        }

                        string path2 = item.Directory + "/Ket_Qua/" + item.NameFile + "_Phone" + ".txt";
                        if (dataPhoneNoDuplicate.Count() > 0 && dataPhoneNoDuplicate.ElementAt(0).ToString() == "Điện thoại")
                        {
                            dataPhoneNoDuplicate.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path2))
                        {
                            foreach (string item2 in dataPhoneNoDuplicate)
                            {
                                sw.WriteLine(item2);
                            }
                        }
                    }
                }
                MessageBoxResult result = MessageBox.Show("Success");
                return;
            }
            else if (UIDCheck.IsChecked == true && PhoneCheck.IsChecked == false)
            {
                foreach (var item in dataListFolder)
                {
                    string[] filePaths_HomeTruoc = Directory.GetFiles(item.Directory + "\\UID_Hom_Truoc", "*.csv",
                                      SearchOption.TopDirectoryOnly);
                    // get UID_Hom_Truoc
                    using (CsvFileReader csvReader = new CsvFileReader(filePaths_HomeTruoc[0]))
                    {
                        List<string> row = new List<string>();
                        List<string> dataId = new List<string>();
                        List<string> dataPhone = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            dataId.Add(row[1]);
                            dataPhone.Add(row[4]);
                        }
                        dataIdYesterday = dataId.Distinct().ToList();
                        dataPhoneYesterday = dataPhone.Distinct().ToList();
                    }
                    string[] filePaths_HomeNay = Directory.GetFiles(item.Directory + "\\UID_Hom_Nay", "*.csv",
                                    SearchOption.TopDirectoryOnly);
                    // get UID_Hom_Nay
                    using (CsvFileReader csvReader = new CsvFileReader(filePaths_HomeNay[0]))
                    {
                        List<string> row = new List<string>();
                        List<string> dataId = new List<string>();
                        List<string> dataPhone = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            dataId.Add(row[1]);
                            dataPhone.Add(row[4]);
                        }
                        dataIdToday = dataId.Distinct().ToList();
                        dataPhoneToday = dataPhone.Distinct().ToList();
                    }
                    string[] filePaths_DoiThu = Directory.GetFiles(item.Directory + "\\UID_Doi_Thu", "*.csv",
                                    SearchOption.TopDirectoryOnly);
                    // get UID_Doi_Thu
                    using (CsvFileReader csvReader = new CsvFileReader(filePaths_DoiThu[0]))
                    {
                        List<string> row = new List<string>();
                        List<string> dataId = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            dataId.Add(row[0]);
                        }
                        dataIdOpponent = dataId.Distinct().ToList();
                    }
                    dataIdNoDuplicate = dataIdToday.Except(dataIdYesterday).ToList();
                    dataPhoneNoDuplicate = dataPhoneToday.Except(dataPhoneYesterday).ToList();
                    if (dataIdOpponent.Count() == 0)
                    {
                        string path = item.Directory + "/Ket_Qua/" + item.NameFile + "_UID" + ".txt";
                        if (dataIdNoDuplicate.Count() > 0 && dataIdNoDuplicate.ElementAt(0).ToString() == "Id")
                        {
                            dataIdNoDuplicate.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (string item1 in dataIdNoDuplicate)
                            {
                                sw.WriteLine(item1);
                            }
                        }
                    }
                    else
                    {
                        dataIdNoDuplicateFinal = dataIdNoDuplicate.Except(dataIdOpponent).ToList();

                        string path = item.Directory + "/Ket_Qua/" + item.NameFile + "_UID" + ".txt";
                        if (dataIdNoDuplicateFinal.Count() > 0 && dataIdNoDuplicateFinal.ElementAt(0).ToString() == "Id")
                        {
                            dataIdNoDuplicateFinal.RemoveAt(0);
                        }
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (string item2 in dataIdNoDuplicateFinal)
                            {
                                sw.WriteLine(item2);
                            }
                        }
                    }
                }
                MessageBoxResult result = MessageBox.Show("Success");
                return;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Bạn nên chọn UID");
                return;
            }
        }

        private void FileChoosing_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
                        FileChoosing.Text = openFileDialog.FileName;
                        DeleteListData();
                        List<string> row = new List<string>();
                        while (csvReader.ReadRow(row))
                        {
                            DataInfo dataInfo = new DataInfo();
                            dataInfo.Directory = row[0];
                            dataInfo.NameFile = row[1];
                            dataListFolder.Add(dataInfo);
                        }
                    }
                }
                catch
                {
                    FileChoosing.Text = "";
                    MessageBoxResult result = MessageBox.Show("Lỗi. Bạn đang mở file hoặc file bạn đang bị lỗi. Làm ơn check lại !!!!");
                }
            }
        }
    }
}
