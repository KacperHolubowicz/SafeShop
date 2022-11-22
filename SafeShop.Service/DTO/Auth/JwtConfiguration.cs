namespace SafeShop.Service.DTO.Auth
{
    public class JwtConfiguration
    {
        public string Issuer { get; }
        public string Audience { get; }
        public string Key { get; }
        public int Expire { get; }

        public JwtConfiguration(string issuer, string audience, string key, int expire)
        {
            Issuer = issuer;
            Audience = audience;
            Key = key;
            Expire = expire;
        }
    }
}
