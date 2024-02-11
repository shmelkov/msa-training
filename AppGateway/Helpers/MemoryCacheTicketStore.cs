using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;

namespace AppGateway.Helpers
{
    public class MemoryCacheTicketStore : ITicketStore
    {
        private IMemoryCache _cache;

        public MemoryCacheTicketStore()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);

            return Task.CompletedTask;
        }

        public Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            return AddToCache(key, ticket);
        }

        public Task<AuthenticationTicket?> RetrieveAsync(string key)
        {
            _cache.TryGetValue(key, out AuthenticationTicket? ticket);

            return Task.FromResult(ticket);
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {   
            var key = $"AuthSessionStore-{Guid.NewGuid()}";

            await AddToCache(key, ticket);

            return key;
        }

        public Task AddToCache(string key, AuthenticationTicket ticket)
        {
            var options = new MemoryCacheEntryOptions();

            var expiresUtc = ticket.Properties.ExpiresUtc;

            if (expiresUtc.HasValue)
            {
                options.SetAbsoluteExpiration(expiresUtc.Value);
            }

            _cache.Set(key, ticket, options);

            return Task.CompletedTask;
        }
    }
}
