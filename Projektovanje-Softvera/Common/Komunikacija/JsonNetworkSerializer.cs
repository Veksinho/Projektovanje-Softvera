using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Komunikacija
{
    public class JsonNetworkSerializer
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNamingPolicy = null,
            Converters = { new JsonStringEnumConverter() },
            ReferenceHandler = ReferenceHandler.Preserve,
        };

        private readonly NetworkStream stream;
        private readonly StreamReader reader;
        private readonly StreamWriter writer;

        public JsonNetworkSerializer(Socket s)
        {
            stream = new NetworkStream(s);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream)
            {
                AutoFlush = true
            };
        }

        public void Send(object poruka)
        {
            string json = JsonSerializer.Serialize(poruka, poruka.GetType(), Options);
            writer.WriteLine(json);
        }

        public T Receive<T>()
        {
            string? json = reader.ReadLine();

            if (json == null)
            {
                throw new IOException("Veza je prekinuta.");
            }

            T? poruka = JsonSerializer.Deserialize<T>(json, Options);

            if (poruka == null)
            {
                throw new IOException("Primljena je prazna poruka.");
            }

            return poruka;
        }

        public T? ReadType<T>(object? podaci) where T : class
        {
            return podaci == null ? null : JsonSerializer.Deserialize<T>((JsonElement)podaci, Options);
        }

        public void Close()
        {
            try
            {
                writer.Dispose();
                reader.Dispose();
                stream.Dispose();
            }
            catch (Exception)
            {
            }
        }
    }
}
