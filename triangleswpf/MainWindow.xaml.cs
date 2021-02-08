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
        private bool modifiedA, modifiedB, modifiedC = false;
        private string valA, valB, valC = "0";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LengthC_Handler(object sender, TextChangedEventArgs e)
        {
            valC = LengthC.Text;
            modifiedC = true;
            if (modifiedA && modifiedB)
            {
                generateResult(valA, valB, valC);
            }
        }

        private void LengthB_Handler(object sender, TextChangedEventArgs e)
        {
            valB = LengthB.Text;
            modifiedB = true;
            if (modifiedA && modifiedC)
            {
                generateResult(valA, valB, valC);
            }
        }


        private void LengthA_Handler(object sender, TextChangedEventArgs args)
        {
            valA = LengthA.Text;
            modifiedA = true;
            if (modifiedB && modifiedC)
            {
                generateResult(valA, valB, valC);
            }
        }

        public bool checkTheorem(float a, float b, float c)
        {
            if (a+b == c)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void generateResult(string a, string b, string c)
        {
            float fltA, fltB, fltC = 0;
            bool validA, validB, validC = false;
            validA = float.TryParse(a, out fltA);
            validB = float.TryParse(b, out fltB);
            validC = float.TryParse(c, out fltC);

            float squaredA, squaredB, squaredC;

            if (validA && validB && validC)
            {
                squaredA = (float) Math.Pow(fltA, 2.0);
                squaredB = (float) Math.Pow(fltB, 2.0);
                squaredC = (float) Math.Pow(fltC, 2.0);

                if (checkTheorem(squaredA, squaredB, squaredC))
                {
                    RealResult.Text = "These side lengths produce a valid right angle triangle";
                }
                else
                {
                    RealResult.Text = "Invalid right angle triangle";
                }
            }
            

            //float ressy = (sideA / sideB);
            //string str = ressy.ToString();
            //MessageBox.Show(str);

            //
        }

    }
    
}
