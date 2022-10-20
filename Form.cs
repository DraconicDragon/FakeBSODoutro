using System.Reflection;

namespace FakeBSODform;
class BsodForm
{
    public BsodForm()
    {
        Cursor.Hide();

        Form myform = new Form{ TopMost = true,
                                FormBorderStyle = FormBorderStyle.None,
                                WindowState = FormWindowState.Maximized,
                                BackColor = ColorTranslator.FromHtml("#00adef")
        };
        PictureBox BSODpicture = new()
        {
            Image = FakeBSODoutro.Properties.Resources.Image1,
            AutoSize = true
        };
        
        myform.Controls.Add(BSODpicture);
        myform.ShowDialog();
    }
}