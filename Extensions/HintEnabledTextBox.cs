using System.Drawing;
using System.Windows.Forms;

namespace Extensions
{
    public static class MyExtensions
    {

        public static void Init(this TextBox textBox, string hintText)
        {
            textBox.Text = hintText;
            bool wma = true;
            textBox.ForeColor = Color.Gray;


            textBox.GotFocus += (source, ex) =>
            {

                if (wma)
                {
                    wma = false;
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }

            };

            textBox.LostFocus += (source, ex) =>
            {
               
                if (string.IsNullOrEmpty(textBox.Text) || textBox.Text == hintText)
                {
                    wma = true;
                    textBox.Text = hintText;
                    textBox.ForeColor = Color.Gray;
                }

            };
            textBox.TextChanged += (source, ex) =>
            {
                if (textBox.Text.Length > 0 && textBox.Text != hintText)
                {
                    textBox.ForeColor = Color.Black;
                }
            };
        }

    }
}