using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using Dieta_somatypy_WPF.ViewModel;

namespace Dieta_somatypy_WPF.Repo
{
    public class XmlOperations
    {
        public void ListToXmlFile(string filePath, ObservableCollection<OsobaViewModel> osobaCollection)
        {
            using (var sw = new StreamWriter(filePath)) 
            {
                var serializer = new XmlSerializer(typeof(ObservableCollection<OsobaViewModel>));
                serializer.Serialize(sw, osobaCollection);
            }
        }
        public ObservableCollection<OsobaViewModel> XmlFileToList(string filename)
        {
            var MyList = new ObservableCollection<OsobaViewModel>();
            using (var sr = new StreamReader(filename))
            {  
                var deserializer = new XmlSerializer(typeof(ObservableCollection<OsobaViewModel>));
                ObservableCollection<OsobaViewModel> tmpList = (ObservableCollection<OsobaViewModel>)deserializer.Deserialize(sr);
                foreach (var item in tmpList)
                {
                    MyList.Add(item);
                }
            }
            return MyList;
        }

    }
}
