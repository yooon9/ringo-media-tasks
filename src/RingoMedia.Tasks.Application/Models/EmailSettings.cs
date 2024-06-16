namespace RingoMedia.Tasks.Application.Models
{
    public sealed class EmailSettings
    {
        public string SenderName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
    }
}
