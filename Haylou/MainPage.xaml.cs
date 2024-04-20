using System.ComponentModel;

namespace Haylou
{
    public partial class MainPage : ContentPage
    {
        private HaylouViewModel _haylouViewModel;
        public MainPage()
        {
            InitializeComponent();
            _haylouViewModel = new HaylouViewModel();
            BindingContext = _haylouViewModel;
        }

        private void CalculateData_Pressed(object sender, EventArgs e)
        {
            var heightInMeter = heightSlider.Value / 100;
            var weightInKg = weightSlider.Value;
            var bmi = weightInKg / (heightInMeter * heightInMeter);
            _haylouViewModel.Bmi = bmi.ToString("N2");
            (_haylouViewModel.BmiStatus, _haylouViewModel.StatusColor) = GetHaylouStatus(bmi);
            _haylouViewModel.IsCalculated = true;
        }
        private static (string status, Color color) GetHaylouStatus(double bmi)
        {
            if (bmi < 15)
                return ("Aşırı derecede zayıfsınız. Lütfen bir doktora danışın.", Colors.Red);
            else if (bmi >= 15 && bmi < 16)
                return ("Aşırı zayıfsınız. Beslenmenize dikkat edin.", Colors.Orange);
            else if (bmi >= 16 && bmi < 17)
                return ("Zayıfsınız. Daha fazla beslenmeye özen gösterin.", Colors.DarkTurquoise);
            else if (bmi >= 17 && bmi < 18.5)
                return ("Hafif zayıfsınız. Sağlıklı bir beslenme programı uygulayın.", Colors.LightGreen);
            else if (bmi >= 18.5 && bmi < 20)
                return ("Normalin alt sınırındasınız. Dengeli bir yaşam tarzı sürdürün.", Colors.Green);
            else if (bmi >= 20 && bmi < 22.5)
                return ("Normalin alt sınırı ile normal arasındasınız. Sağlıklı beslenmeye devam edin.", Colors.LightGreen);
            else if (bmi >= 22.5 && bmi < 24.9)
                return ("Normalin üst sınırındasınız. Sağlıklı bir yaşam tarzı sürdürün.", Colors.Green);
            else if (bmi >= 25 && bmi < 27.5)
                return ("Hafif fazla kilolusunuz. Daha fazla egzersiz yapabilirsiniz.", Colors.DarkOrchid);
            else if (bmi >= 27.5 && bmi < 29.9)
                return ("Orta derecede fazla kilolusunuz. Sağlıklı beslenme alışkanlıkları edinin.", Colors.Orange);
            else if (bmi >= 30 && bmi < 32.5)
                return ("Obezite sınıfı I'desiniz. Sağlıklı yaşam için bir plan yapın.", Colors.Red);
            else if (bmi >= 32.5 && bmi < 35)
                return ("Obezite sınıfı II'desiniz. Bir doktora danışmanız önemlidir.", Colors.DarkRed);
            else
                return ("Obezite sınıfı III'tesiniz. Acilen bir sağlık uzmanına başvurun.", Colors.DarkRed);
        }


        private void RefreshHaylou_Pressed(object sender, EventArgs e)
        {
            heightSlider.Value = 10; weightSlider.Value = 10; _haylouViewModel.Reset();
        }
    }
    public class HaylouViewModel : INotifyPropertyChanged
    {
        private string _bmi;

        public string Bmi
        {
            get => _bmi;
            set
            {
                _bmi = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bmi)));
            }
        }
        private string _bmiStatus;

        public string BmiStatus
        {
            get => _bmiStatus;
            set
            {
                _bmiStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BmiStatus)));
            }
        }
        private Color _statusColor = Colors.Blue;

        public Color StatusColor
        {
            get => _statusColor;
            set
            {
                _statusColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusColor)));
            }
        }


        private bool _isCalculated;
        public bool IsCalculated
        {
            get => _isCalculated;
            set
            {
                _isCalculated = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCalculated)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsNotCalculated)));
            }
        }


        public bool IsNotCalculated => !IsCalculated;

        public event PropertyChangedEventHandler PropertyChanged;
        public void Reset()
        {
            Bmi = string.Empty;
            BmiStatus = string.Empty;
            StatusColor = Colors.Blue;
            IsCalculated = false;
        }
    }
}
