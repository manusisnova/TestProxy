using System.Text;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
public class HttpTestRequest
{
    [Benchmark]
    public async Task SendTestRequest()
    {
        string url = "https://invexerp.app/fel/SisnovaFEL/XMLVerificarFACT";
        var jsonObject = new
        {
            id = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            creado = "2024-10-19T02:18:07.554Z",
            idDocumento = "string",
            fecha = "2024-10-19T02:18:07.554Z",
            contingencia = "string",
            receptor = new
            {
                id = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                creado = "2024-10-19T02:18:07.554Z",
                tipoReceptor = 0,
                nit = new { strValue = "string" },
                dpi = "string",
                pasaporte = "string",
                incoterm = 0,
                correo = "string",
                nombre = "string",
                direccion = "string",
                region = "string",
                subregion = "string",
                aptoPostal = "string"
            },
            emisor = new
            {
                id = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                creado = "2024-10-19T02:18:07.554Z",
                nit = new { strValue = "string" },
                tipoPersoneria = "string",
                agenteRetenedorIVA = true,
                codigoExportador = "string",
                propietarioRazon = "string",
                correo = "string",
                direccion = "string",
                codigoEscenarioExentoIVA = 0,
                nombreComercial = "string",
                region = "string",
                subregion = "string",
                codigoEstablecimiento = 0,
                aptoPostal = "string",
                afiliacionIVA = 0,
                regimenISR = 1,
                escenarioExentoISRRDON = 0,
                infoCertificadorFEL = new
                {
                    certificador = 0,
                    user = "string",
                    token = "string",
                    password = "string"
                },
                numeroResolucion = "string",
                fechaResolucion = "2024-10-19T02:18:07.554Z"
            },
            adendas = new[]
            {
        new { nombre = "string", valor = "string" }
    },
            esExportacion = true,
            esFacturaMuni = true,
            exento = true,
            total = 0,
            iva = 0,
            isr = 0,
            idp = 0,
            referencia = "string",
            detalle = new[]
            {
        new
        {
            id = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            creado = "2024-10-19T02:18:07.554Z",
            cantidad = 0,
            precio = 0,
            precioLista = 0,
            articulo = "string",
            unidadMedida = "string",
            iva = 0,
            idp = 0,
            monto = 0,
            exento = true,
            bienOServicio = 0
        }
    },
            formaPago = new[]
            {
        new
        {
            id = "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            creado = "2024-10-19T02:18:07.554Z",
            monto = 0,
            formaPago = "string",
            numero = "string",
            vencimiento = "2024-10-19T02:18:07.554Z"
        }
    },
            tipoMoneda = 0,
            tipoFactura = 0
        };
        var jsonString = JsonSerializer.Serialize(jsonObject);

        using HttpClient client = new();

        for (int k = 0; k < 5; k++)
        {
            try
            {
                var tasks = new List<Task<HttpResponseMessage>>();
                for (int i = 0; i < 1000; i++) // Ajusta el número de solicitudes concurrentes según sea necesario
                {
                    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    HttpRequestMessage request = new(HttpMethod.Get, url)
                    {
                        Content = content
                    };
                    tasks.Add(client.SendAsync(request));
                }

                await Task.WhenAll(tasks);

                foreach (var task in tasks)
                {
                    Console.WriteLine("Sending request...");
                    HttpResponseMessage response = await task;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message} {ex.InnerException?.Message}");
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<HttpTestRequest>();
    }
}