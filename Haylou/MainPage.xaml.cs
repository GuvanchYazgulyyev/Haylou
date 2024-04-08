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
            if (bmi < 18.2)
                return ("Kilo kontrolünde zorluklar yaşayabilirsiniz, daha fazla beslenmeye özen gösterin.", Colors.Blue);
            else if (bmi <= 24.9)
                return ("Kilo kontrolünde başarılısınız, Harikasınız!", Colors.Green);
            else if (bmi <= 29.9)
                return ("Kilo kontrolünde zorlanıyorsunuz, dengeli bir beslenme planı yapmayı düşünebilirsiniz.", Colors.DeepSkyBlue);
            else
                return ("Kilo kontrolünde ciddi problemler yaşıyor olabilirsiniz, doktorunuza danışmanız önemlidir.", Colors.DarkRed);
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
