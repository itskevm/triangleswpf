using System;
using System.Collections.Generic;
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

namespace triangleswpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private bool validA, validB, validC = false;
        private float valueA, valueB, valueC = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

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

        private void TextChange_Handler(object sender, TextChangedEventArgs e)
        {
            valueA = castStringToValue(LengthA.Text);
            valueB = castStringToValue(LengthB.Text);
            valueC = castStringToValue(LengthC.Text);

            Console.WriteLine("A: "+valueA);
            Console.WriteLine("B: "+valueB);
            Console.WriteLine("C: "+valueC);

            if (valueA > 0 && valueB > 0 && valueC > 0)
            {
                Console.WriteLine("all valid numbers");
                generateResult(valueA, valueB, valueC);
                //ResultSign
            }
            else
            {
                generateResult(0, 0, 0);
                RealResult.Text = "Verify inputs";
                Console.WriteLine("Input Error");
                ResultSign.Source = new BitmapImage(new Uri(@"/assets/warningsign.png", UriKind.Relative));
            }
            //might want to validate here if the value is numeric and positive
            //LengthA.Text = "";
        }
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
        private string getTriangleType(float a, float b, float c)
        {
            if (IsValidTriangle(a, b, c))
            {
                ResultSign.Source = new BitmapImage(new Uri(@"/assets/validsign.png", UriKind.Relative));
                if (IsRightAngleTriangle(a, b, c))
                {
                    return "right-angle triangle";
                }
                else if (IsEqualTriangle(a, b, c))
                {
                    return "equilateral triangle";
                }
                else if (IsIsoTriangle(a, b, c))
                {
                    return "isosceles triangle";
                }
                return "triangle";
            }
            else
            {
                ResultSign.Source = new BitmapImage(new Uri(@"/assets/errorsign.png", UriKind.Relative));
                return "invalid triangle";
            }
        }

        public void generateResult(float a, float b, float c)
        {
            RealResult.Text = getTriangleType(a,b,c);

            /*if (validA && validB && validC)
            {

                if (checkTheorem(fltA, fltB, fltC))
                {
                    RealResult.Text = "These side lengths produce a valid right angle triangle";
                }
                else
                {
                    RealResult.Text = "Invalid right angle triangle";
                }
            }*/
        }
    }
}
