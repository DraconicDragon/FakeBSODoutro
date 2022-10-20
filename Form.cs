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
            Image = FakeBSODoutro.Properties.Resources.FakeBSODimg,
            AutoSize = true
        };
        
        myform.Controls.Add(BSODpicture);
        myform.ShowDialog();
    }
}