namespace Haylou
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void CalculateData_Pressed(object sender, EventArgs e)
        {
            var heightInMeter = heightSlider.Value * 100;
            var weightInKg = weightSlider.Value;
            var bmi = weightInKg / (heightInMeter * heightInMeter);
        }
    }

}
