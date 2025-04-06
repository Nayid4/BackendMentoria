using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Mentoria.Shared.Serialization
{
    public class Serializer : ISerializer
    {
        private static readonly Encoding Encoding = new UTF8Encoding();
        private static readonly JsonSerializerSettings DefaultSerializerSetting =
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

        private const int DefaultBufferSize = 1024;
        private readonly Newtonsoft.Json.JsonSerializer _jsonSerializer;

        public Serializer() : this(DefaultSerializerSetting)
        {

        }

        public Serializer(JsonSerializerSettings serializerSettings)
        {
            _jsonSerializer = Newtonsoft.Json.JsonSerializer.Create(serializerSettings);
        }

        public object? DesealizeObject(byte[] input, Type myType)
        {
            using var memoryStream = new MemoryStream(input, false);
            using var streamReader = new StreamReader(memoryStream, Encoding, false, DefaultBufferSize, true);
            using var reader = new JsonTextReader(streamReader);
            return _jsonSerializer.Deserialize(reader, myType) ?? throw new InvalidOperationException();
        }

        public T DeserializeObject<T>(string input)
        {
            return JsonSerializer.Deserialize<T>(input) ?? throw new InvalidOperationException();
        }

        public T DeserializeObject<T>(byte[] input) where T : class
        {
            return (DeserializeByteArrayToObject<T>(input) as T)!;
        }

        public string SerializeObject<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public byte[] SerializeObjectToByteArray<T>(T obj)
        {
            using var memoryStream = new MemoryStream(DefaultBufferSize);
            using (var streamWriter = new StreamWriter(memoryStream, Encoding, DefaultBufferSize, true))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                jsonWriter.Formatting = _jsonSerializer.Formatting;
                _jsonSerializer.Serialize(jsonWriter, obj, obj!.GetType());
            }

            return memoryStream.ToArray();
        }

        private object DeserializeByteArrayToObject<T>(byte[] input)
        {
            using var memoryStream = new MemoryStream(input, false);
            using var streamReader = new StreamReader(memoryStream, Encoding, false, DefaultBufferSize, true);
            using var reader = new JsonTextReader(streamReader);
            return _jsonSerializer.Deserialize(reader, typeof(T)) ?? throw new InvalidOperationException();
        }
    }
}
