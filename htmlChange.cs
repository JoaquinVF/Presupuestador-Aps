using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presupuestador
{
    class htmlChange
    {
        public static void llenarHtml(string sCliente, string sPresupuesto, string sFecha, string sProyecto, string sMonto, string sMoneda, string sTimming, string sEntrega, string sCondiciones, string sEncabezado, string sCuerpo, string sImagen, string paths, string sAdicionales)//<NombreEscena> <Mail> <PuntosEstimados>
        {
            string texto = string.Empty;
            
            if (Path.GetFileName(sImagen) == "0YKyhsD.png")
            {
                using (StreamReader reader = new StreamReader("css/Presupuesto_APS2.html"))
                {
                    texto = reader.ReadToEnd();
                }
            }
            else {
                using (StreamReader reader = new StreamReader("css/Presupuesto_OWL.html"))
                {
                    texto = reader.ReadToEnd();
                }
            }
            texto = texto.Replace("{cliente}", sCliente);
            texto = texto.Replace("{presupuesto}", sPresupuesto);
            texto = texto.Replace("{fecha}", sFecha);
            texto = texto.Replace("{proyecto}", sProyecto);
            texto = texto.Replace("{monto}", sMonto);
            texto = texto.Replace("{moneda}", sMoneda);
            texto = texto.Replace("{timming}", sTimming);
            texto = texto.Replace("{entrega}", sEntrega);
            texto = texto.Replace("{condiciones}", sCondiciones);
            texto = texto.Replace("{encabezado}", sEncabezado);
            texto = texto.Replace("{contenido}", sCuerpo);
            texto = texto.Replace("{imagen}", sImagen);
            texto = texto.Replace("{notas}", sAdicionales);

            using (FileStream fs = new FileStream("test.html", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine(texto);
                }
            }

            string path = AppDomain.CurrentDomain.BaseDirectory + "wkhtmltopdf/bin/wkhtmltopdf.exe";

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.UseShellExecute = false;
            processStartInfo.FileName = path;
            processStartInfo.RedirectStandardOutput = true;

            if (paths.EndsWith(".pdf"))
            {
                paths = Path.GetFileNameWithoutExtension(paths);
            }
            processStartInfo.Arguments = "--enable-local-file-access " + @"test.html " + '"' + paths + ".pdf" + '"';

            using (Process oProcess = Process.Start(processStartInfo))
            {
                oProcess.BeginOutputReadLine();
                oProcess.OutputDataReceived += OProcess_OutputDataReceived;
                oProcess.WaitForExit();
            }


        }

        private static void OProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            
        }
    }
}
