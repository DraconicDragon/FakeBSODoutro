namespace FakeBSODform;
class BsodForm
{
    public BsodForm()
    {
        Cursor.Hide();

        Form myform = new Form{ TopMost = true,
                                FormBorderStyle = FormBorderStyle.None,
                                WindowState = FormWindowState.Maximized
        };

        PictureBox BSODpicture = new()
        {
            ImageLocation = @"Assets/Image1.png",
            AutoSize = true
        };
        
        myform.Controls.Add(BSODpicture);
        myform.ShowDialog();
    }
}