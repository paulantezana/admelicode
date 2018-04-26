using System;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Admeli
{
public partial class Form1 : System.Windows.Forms.Form
{
   
    private System.Windows.Forms.Button printButton;
    private Font printFont;
    private StreamReader streamToPrint;

    public Form1()
    {
        // The Windows Forms Designer requires the following call.
        InitializeComponent();
    }

    // The Click event is raised when the user clicks the Print button.
    private void printButton_Click(object sender, EventArgs e)
    {
        try
        {
            streamToPrint = new StreamReader
               ("C:\\imprimir\\imprimir.txt");
            try
            {
                printFont = new Font("Arial", 10);
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler
                   (this.pd_PrintPage);
                pd.Print();
            }
            finally
            {
                streamToPrint.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    // The PrintPage event is raised for each page to be printed.
    private void pd_PrintPage(object sender, PrintPageEventArgs ev)
    {
        float linesPerPage = 0;
        float yPos = 0;
        int count = 0;
        float leftMargin = ev.MarginBounds.Left;
        float topMargin = ev.MarginBounds.Top;
        string line = null;

        // Calculate the number of lines per page.
        linesPerPage = ev.MarginBounds.Height /
           printFont.GetHeight(ev.Graphics);

        // Print each line of the file.
        while (count < linesPerPage &&
           ((line = streamToPrint.ReadLine()) != null))
        {
            yPos = topMargin + (count *
               printFont.GetHeight(ev.Graphics));
            ev.Graphics.DrawString(line, printFont, Brushes.Black,
               leftMargin, yPos, new StringFormat());
            count++;
        }

        // If more lines exist, print another page.
        if (line != null)
            ev.HasMorePages = true;
        else
            ev.HasMorePages = false;
    }


    // The Windows Forms Designer requires the following procedure.
   

}
}
