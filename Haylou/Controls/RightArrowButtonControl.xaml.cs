namespace Haylou.Controls;

public partial class RightArrowButtonControl : ContentView
{
    public static readonly BindableProperty IconImageSourceProperty =
     BindableProperty.Create(nameof(IconImgeSource), typeof(string), typeof(RightArrowButtonControl),
         string.Empty);
    public RightArrowButtonControl()
    {
        InitializeComponent();
    }
    public string IconImgeSource
    {
        get => (string)GetValue(IconImageSourceProperty);
        set => SetValue(IconImageSourceProperty, value);
    }
    public event EventHandler ButtonPressed;
    // Buton Tetikleme Olayý
    private void Button_Pressed(object sender, EventArgs e)
    {
        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }
}