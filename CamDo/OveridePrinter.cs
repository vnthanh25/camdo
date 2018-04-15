using System;

using System.Drawing;

using System.Drawing.Printing;

using System.IO;

public class zzz
{

    public void abc()
    {

        yyy y = new yyy();

        y.PrintPage += new PrintPageEventHandler(pqr);

        y.Print();

    }

    void pqr(object o, PrintPageEventArgs e)
    {

        e.Graphics.DrawString("Sonal Mukhi", new Font("Courier New", 10), Brushes.Black, 100, 200);

    }

    //public static void Main()
    //{

    //    zzz a = new zzz();

    //    a.abc();

    //}

}

public class yyy : PrintDocument
{

    Font f;

    protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
    {

        base.OnQueryPageSettings(e);

        e.PageSettings.Landscape = true;

    }

    protected override void OnBeginPrint(PrintEventArgs e)
    {

        base.OnBeginPrint(e);

        f = new Font("Courier New", 14);

    }

    protected override void OnPrintPage(PrintPageEventArgs e)
    {

        base.OnPrintPage(e);

        e.Graphics.DrawString("Vijay Mukhi", f, Brushes.Black, 100, 400);

    }

}

