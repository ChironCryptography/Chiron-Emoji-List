using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiron.EmojiListGen
{
    internal static class HttpHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<byte[]> DownloadFileAsync(string uri) {
            if (!Uri.TryCreate(uri, UriKind.Absolute, out _)) throw new InvalidOperationException("URI is invalid.");
            return await _httpClient.GetByteArrayAsync(uri);
        }

        public static async Task<string> DownloadFileAsText(string uri) {
            var bytes = await DownloadFileAsync(uri);
            return BytesToStringConverted(bytes);
        }

        static string BytesToStringConverted(byte[] bytes) {
            using var stream = new MemoryStream(bytes);
            using var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
