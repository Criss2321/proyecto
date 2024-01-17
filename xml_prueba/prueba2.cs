using System;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

public class Pagos
{
    public Pagos()
    {

    }
    public String Codigo { get; set; }
    public String Nombre { get; set; }
    public String Importe { get; set; }
}
class Program
{
    static void Main()
    {
        try
        {
            String dni = "";


            List<Pagos> listaIngresos = new List<Pagos>();
            List<Pagos> Descuentos = new List<Pagos>();
            List<Pagos> Aportes = new List<Pagos>();


            // Ruta de tu archivo XML
            string xmlFilePath = "boleta.xml";

            // Crear un nuevo documento XML
            XmlDocument xmlDoc = new XmlDocument();

            // Cargar el XML desde el archivo
            xmlDoc.Load(xmlFilePath);

            // Obtener la información que necesitas navegando por los nodos
            XmlNodeList rows = xmlDoc.SelectNodes("//ss:Row", GetXmlNamespaceManager(xmlDoc));
            Console.WriteLine("*******************************");
            int cont = 0;
            foreach (XmlNode row in rows)
            {

                // Procesar cada fila y sus celdas
                foreach (XmlNode cell in row.SelectNodes("ss:Cell", GetXmlNamespaceManager(xmlDoc)))
                {
                    Console.WriteLine(cell);
                    string data = cell.SelectSingleNode("ss:Data", GetXmlNamespaceManager(xmlDoc))?.InnerText;
                    //Console.Write(data + "\t");
                    // //Codigo obtener dni
                    // if (row == "ingresos") Flag capturar ingresos;
                    // listaIngresos.Add(new Pagos() { Codigo = "0121", Nombre = "Remun o ..", Importe = "1800" });
                    // //
                }
                cont++;

            }
            Console.WriteLine("*******************************" + cont);


            // Preguntar al usuario qué contenido desea visualizar
            Console.WriteLine("¿Qué contenido del XML deseas visualizar?");
            Console.WriteLine("1. Boleta de Pago");
            Console.WriteLine("2. Información Adicional");

            int opcion;
            do
            {
                Console.Write("Selecciona una opción (1-2): ");
            } while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 2);

            // Mostrar el contenido según la opción seleccionada
            switch (opcion)
            {
                case 1:
                    MostrarBoleta(xmlDoc);
                    break;
                case 2:
                    MostrarInformacionAdicional(xmlDoc);
                    break;
            }

            // Esperar a que el usuario presione una tecla antes de cerrar la consola
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static void MostrarBoleta(XmlDocument xmlDoc)
    {
        XmlNode boletaCell = xmlDoc.SelectSingleNode("//ss:Row/ss:Cell[1]/ss:Data", GetXmlNamespaceManager(xmlDoc));
        string boletaContent = boletaCell?.InnerText;

        XmlNode montoCell = xmlDoc.SelectSingleNode("//ss:Row/ss:Cell[2]/ss:Data", GetXmlNamespaceManager(xmlDoc));
        string monto = montoCell?.InnerText;

        Console.WriteLine($"Contenido de la Boleta de Pago: {boletaContent}");
        Console.WriteLine($"Monto: {monto}");
    }


    static void MostrarInformacionAdicional(XmlDocument xmlDoc)
    {
        XmlNode infoCell = xmlDoc.SelectSingleNode("//ss:Row/ss:Cell[3]/ss:Data", GetXmlNamespaceManager(xmlDoc));
        string infoContent = infoCell?.InnerText;

        Console.WriteLine($"Información Adicional: {infoContent}");
    }

    // Obtener el administrador de espacios de nombres XML para manejar los prefijos
    static XmlNamespaceManager GetXmlNamespaceManager(XmlDocument xmlDoc)
    {
        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
        namespaceManager.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");
        return namespaceManager;
    }
}

