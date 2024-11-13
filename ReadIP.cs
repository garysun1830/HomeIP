
namespace HomeIP
{
    internal class ReadIP
    {
        public string IP { get; set; }

        public async Task GetIp()
        {

            IP = (await new HttpClient().GetStringAsync("http://icanhazip.com"))
                  .Replace("\\r\\n", "").Replace("\\n", "").Trim();
        }

    }
}
