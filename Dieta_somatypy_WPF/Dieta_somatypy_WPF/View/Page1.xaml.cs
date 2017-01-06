using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Dieta_somatypy_WPF.Model;
using Dieta_somatypy_WPF.Repo;
using Dieta_somatypy_WPF.ViewModel;


namespace Dieta_somatypy_WPF
{
    public partial class Page1 : Page
    {
        public ObservableCollection<OsobaViewModel> LocalCollection { get; set; }

        public Wzory Wzory;
        public XmlOperations XmlOperations;
        public Page1()
        {   // metoda wczytaj z pliku i do tego lista 
            
            Wzory = new Wzory();
            LocalCollection = new ObservableCollection<OsobaViewModel>();
            InitializeComponent();
            this.DataContext = this;
            ValueInit();
        }

        private void ValueInit()
        {
            Sex.ItemsSource = Enum.GetValues(typeof(SexEnum));
            ActivityComboBox.ItemsSource = Acrtivity.ActivityList;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            ExitWindow.Exit(1);
        }


        private void ObliczButton_OnClick_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var osoba = new OsobaViewModel()
                {   Imie = ImieTextBox.Text,
                    Nazwisko = NazwiskoTextBox.Text,
                    Plec = Sex.SelectedValue.ToString(),
                    Aktywnosc = double.Parse(ActivityComboBox.SelectedValue.ToString()),
                    Masa = double.Parse(WagaTextBox.Text),
                    Wiek = int.Parse(WiekTextBox.Text),
                    Date = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                };
                osoba = ParametersCount(osoba);
                LocalCollection.Add(osoba);
               
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Nie wszystkie dane zostały podane. ", "Obliczenia ");
            }
            catch (FormatException)
            {
                MessageBox.Show("Niepoprawny format danych." + Environment.NewLine+"Masa - x,xx" + Environment.NewLine + "Wiek - x", "Obliczenia ");
            }
        }

        private OsobaViewModel ParametersCount(OsobaViewModel osoba)
        {
            osoba.BMR = BMRCount(osoba.Plec, osoba.Wiek, osoba.Masa);
            osoba.BMRPLL = RequiredAmount(osoba.BMR, osoba.Aktywnosc);
            return osoba;
        }

        private double BMRCount(string plec, int wiek, double masa)
        {
            return plec == "Mężczyzna" ? Wzory.Man(wiek, masa) : Wzory.Woman(wiek, masa);
        }

        private double RequiredAmount(double BMR, double aktywnosc)
        {
            return Wzory.BMR_aktywnosc(BMR, aktywnosc);
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            LocalCollection.Clear();
        }

        private void SaveButton_OnClickButton_OnClick_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Własna lista osób";    // domyślna nazwa
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documentx (.xml)|* .xml";
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string filePath = dlg.FileName;
                ListToXmlFile(filePath);
            }
        }
        private void ListToXmlFile(string filePath)
        {
            using (var sw = new StreamWriter(filePath)) // taki try catch StreamWrite pisze do xml
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<OsobaViewModel>));
                serializer.Serialize(sw, LocalCollection);
            }
        }
        private void LoadButton_OnClickButton_OnClick_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documentx (.xml)|* .xml";
            Nullable<bool> result = dlg.ShowDialog();
            string filename = "";
            if (result == true)
            {
                filename = dlg.FileName;
            }

            if (File.Exists(filename))
            {
                XmlFileToList(filename);
            }
            

        }

        private void XmlFileToList(string filename)
        {
            LocalCollection.Clear();
          
            using (var sr = new StreamReader(filename))
            {
                var deserializer = new XmlSerializer(typeof(ObservableCollection<OsobaViewModel>));
                ObservableCollection<OsobaViewModel> tmpList = (ObservableCollection<OsobaViewModel>)deserializer.Deserialize(sr);
                foreach (var item in tmpList)
                {
                    LocalCollection.Add(item);
                }
            }
        }


        private void DeleteButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LocalCollection.RemoveAt(this.ListView1.SelectedIndex);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Wybierz osobę, którą chcesz usunąć z listy. ", "Usuń osobę ");
            }
        }
    }
}

