using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace triangleswpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool modifiedA, modifiedB, modifiedC = false; // make textboxes act "untouched" until told otherwise
        private float valueA, valueB, valueC = 0; // filled later by only valid numerical entries
        public MainWindow()
        {
            InitializeComponent();
        }

        // Implements a "select-all" feature when changing textboxes via "Tab" key
        private void GotKeyboardFocus_Handler(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (e.KeyboardDevice.IsKeyDown(Key.Tab))
            {
                ((TextBox)sender).SelectAll();
            }
        }

        // Allow only for numbers to be added
        private void PreviewTextInput_Handler(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^\\d\\.]").IsMatch(e.Text);
        }

        // Attempting to cast the user's entries into numerical values (returning 0 assumes invalid entry)
        private float castStringToValue(string entry)
        {
            bool valid = false;
            float value = 0;
            valid = float.TryParse(entry, out value);
            if (valid && value > 0)
            {
                return value;
            }
            return 0;
        }

        // This will execute each time the user types in any of the three entries (shared by 3 textboxes)
        private void TextChange_Handler(object sender, TextChangedEventArgs e)
        {
            // Determine if these are initial entries
            if (!LengthA.Text.Equals(""))
            {
                modifiedA = true;
            }
            if (!LengthB.Text.Equals(""))
            {
                modifiedB = true;
            }
            if (!LengthC.Text.Equals(""))
            {
                modifiedC = true;
            }

            if (modifiedA && modifiedB && modifiedC)
            {
                valueA = castStringToValue(LengthA.Text);
                valueB = castStringToValue(LengthB.Text);
                valueC = castStringToValue(LengthC.Text);

                if (valueA > 0 && valueB > 0 && valueC > 0)
                {
                    generateResult(valueA, valueB, valueC);
                }
                else
                {
                    generateResult(0, 0, 0);
                    RealResult.Text = "Fill all entries with numbers";
                    ResultSign.Source = new BitmapImage(new Uri(@"/assets/warningsign.png", UriKind.Relative));
                }
            }
        }

        // The following functions contain the logic for what qualifies a triangle to be each type
        private bool IsValidTriangle(float a, float b, float c)
        {
            if ((a + b > c) && (b + c > a) & (a + c > b))
            {
                return true;
            }

            return false;
        }
        public bool IsRightAngleTriangle(float a, float b, float c)
        {
            double [] asq = new double[3] { Math.Pow(a, 2), Math.Pow(b, 2), Math.Pow(c, 2) };
            if (asq[0] + asq[1] == asq[2] || asq[0] + asq[2] == asq[1] || asq[1] + asq[2] == asq[0])
            {
                return true;
            }
            return false;
        }
        public bool IsEqualTriangle(float a, float b, float c)
        {
            if (a == b && b == c)
            {
                return true;
            }
            return false;
        }
        public bool IsIsoTriangle(float a, float b, float c)
        {
            if (a == b || a == c || b == c)
            {
                return true;
            }
            return false;
        }
        
        // Contains calls to all of the triangle determining methods
        private string getTriangleType(float a, float b, float c)
        {
            // Classify the triangle accordingly if valid
            if (IsValidTriangle(a, b, c))
            {
                ResultSign.Source = new BitmapImage(new Uri(@"/assets/validsign.png", UriKind.Relative));
                if (IsRightAngleTriangle(a, b, c))
                {
                    return "right-angle";
                }
                else if (IsEqualTriangle(a, b, c))
                {
                    return "equilateral";
                }
                else if (IsIsoTriangle(a, b, c))
                {
                    return "isosceles";
                }
                return "valid";
            }
            else
            {
                ResultSign.Source = new BitmapImage(new Uri(@"/assets/errorsign.png", UriKind.Relative));
                return "invalid";
            }
        }

        public void generateResult(float a, float b, float c)
        {
            // Display the 'type'
            RealType.Text = getTriangleType(a,b,c);

            // Handle message cases
            if (RealType.Text == "invalid")
            {
                RealResult.Text = "These side lengths do not produce a valid triangle";
            }
            else if (RealType.Text == "valid")
            {
                RealResult.Text = "These side lengths produce a valid triangle";
            }
            else
            {
                RealResult.Text = $"These side lengths produce a valid {RealType.Text} triangle";
            }
        }
    }
}
