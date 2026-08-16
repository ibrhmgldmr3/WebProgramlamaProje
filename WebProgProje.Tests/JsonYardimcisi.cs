using System.Text.Json;

namespace WebProgProje.Tests
{
    /// <summary>
    /// API controller'ları anonim tip döndürdüğü için, dönen nesneyi JSON'a çevirip
    /// alanlar üzerinden doğrulama yapıyoruz.
    /// </summary>
    public static class JsonYardimcisi
    {
        public static JsonElement Ayristir(object? deger)
        {
            var json = JsonSerializer.Serialize(deger);
            using var document = JsonDocument.Parse(json);
            return document.RootElement.Clone();
        }
    }
}
