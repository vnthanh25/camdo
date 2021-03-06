using System; 
using System.Text; 
using System.Runtime.InteropServices;


public class prtUnicode
{

    [DllImport("winspool.drv", CharSet = CharSet.Auto, ExactSpelling = true,
     CallingConvention = CallingConvention.StdCall)]
    public static extern long WritePrinter(IntPtr hPrinter, string data, int buf, ref   int pcWritten);
}   
 

//[StructLayout(LayoutKind.Sequential)] 
//public
//struct DOCINFO 
//{

//[

//MarshalAs(UnmanagedType.LPWStr)] 

//public string pDocName; 
//[

//MarshalAs(UnmanagedType.LPWStr)] 

//public string pOutputFile; 
//[

//MarshalAs(UnmanagedType.LPWStr)] 

//public string pDataType; 
//}


public class PrintDirect1
{

    [DllImport("winspool.drv", CharSet = CharSet.Auto, ExactSpelling = false,
    CallingConvention = CallingConvention.StdCall)]

    public static extern long OpenPrinter(string pPrinterName, ref IntPtr phPrinter, int pDefault);
    [DllImport("winspool.drv", CharSet = CharSet.Auto, ExactSpelling = false,
    CallingConvention = CallingConvention.StdCall)]

    public static extern long StartDocPrinter(IntPtr hPrinter, int Level, ref DOCINFO pDocInfo);
    [DllImport("winspool.drv", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention =
    CallingConvention.StdCall)]

    public static extern long StartPagePrinter(IntPtr hPrinter);
    [DllImport("winspool.drv", CharSet = CharSet.Ansi, ExactSpelling = true,
    CallingConvention = CallingConvention.StdCall)]

    public static extern long WritePrinter(IntPtr hPrinter, string data, int buf, ref int pcWritten);
    [DllImport("winspool.drv", CharSet = CharSet.Auto, ExactSpelling = true,
    CallingConvention = CallingConvention.StdCall)]

    public static extern long EndPagePrinter(IntPtr hPrinter);
    [DllImport("winspool.drv", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention =
    CallingConvention.StdCall)]

    public static extern long EndDocPrinter(IntPtr hPrinter);
    [DllImport("winspool.drv", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention =
    CallingConvention.StdCall)]

    public static extern long ClosePrinter(IntPtr hPrinter);



    //and to use it 

    public void Print(string printerName, string docName, string referencePiece, string designationPiece, string code)
    {

        System.IntPtr lhPrinter = new System.IntPtr();
        DOCINFO di = new DOCINFO();
        int pcWritten = 0;
        string st1;
        di.pDocName = docName;
        di.pDataType = "RAW";
        //If lhPrinter is 0 then an error has occured 

        try
        {
            PrintDirect.OpenPrinter(printerName, ref lhPrinter, 0);

            PrintDirect.StartDocPrinter(lhPrinter, 1, ref di);

            PrintDirect.StartPagePrinter(lhPrinter);

            //"^XA^CFD^FS\n^PON^FS\n^FWN^FS\n^LH020,30^FS\n^FO0,5^A0,N,50,50^FDSTART TEST^FS\n^FO0,50^A0,N,30,30^FD\nTOFUS MAXIMUS^FS\n^PQ1\n^XZ"; 


            /* 
            * //ZPL2

            st1 = "^XA";

            st1 += "^LH50,50";

            st1 += "^FO0,0^AF^FD"+texteHaut+"^FS";

            st1 += "^FO50,50,^BC,50";

            st1 += "^FD"+code+"^FS";

            //st1 += "";

            st1 += "^XZ";

            */
            //st1 = "\x1b*p600x600Y\r\n";
            //PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);

            //st1 = "x1b*pcộng hoà xã hội chủ";
            ////EPL2 
            //st1 = "\nN\n";
            //st1 += "cộng hoà xã hội\"" + referencePiece + "\"\n";
            //st1 += "A25,25,0,2,1,1,N,\"" + designationPiece + "\"\n";
            //st1 += "B25,45,0,1E,2,3,50,B,\"" + code + "\"\n";
            //st1 += "P1\n";


            st1 = "\x1b*p600x600Y\r\n";
            PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref   pcWritten);
            st1 = "hello";
            PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref   pcWritten);
            st1 = "cộng hoà xã";
            prtUnicode.WritePrinter(lhPrinter, st1, st1.Length, ref   pcWritten);
            st1 = "\f";
            PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref   pcWritten);   
    



            //PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
            //prtUnicode.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);
        }


        catch (Exception ex)
        {


            throw new ApplicationException(ex.Message);
        }


        PrintDirect.EndPagePrinter(lhPrinter);

        PrintDirect.EndDocPrinter(lhPrinter);

        PrintDirect.ClosePrinter(lhPrinter);
    }
}
