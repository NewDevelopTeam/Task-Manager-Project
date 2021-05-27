using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlusDashData.Data;

// This class implements ITicketStore interface for managing user's auth data on a server-side storage

namespace TaskManager.Moduls
{
    public class CustomTicketStore : ITicketStore
    {
        private readonly IServiceCollection _services;

        public CustomTicketStore(IServiceCollection services)
        {
            _services = services;
        }
        public async Task RemoveAsync(string key)
        {
            using (var scope = _services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AccountContext>();
                if (Guid.TryParse(key, out var id))
                {
                    var ticket = await context.UserSessions.SingleOrDefaultAsync(x => x.Id == id);
                    if (ticket != null)
                    {
                        context.UserSessions.Remove(ticket);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
        public async Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            using (var scope = _services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AccountContext>();
                if (Guid.TryParse(key, out var id))
                {
                    var authenticationTicket = await context.UserSessions.FindAsync(id);
                    if (authenticationTicket != null)
                    {
                        authenticationTicket.Value = SerializeToBytes(ticket);
                        authenticationTicket.LastActivity = DateTimeOffset.UtcNow;
                        authenticationTicket.Expires = ticket.Properties.ExpiresUtc;
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
        public async Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            using (var scope = _services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AccountContext>();
                if (Guid.TryParse(key, out var id))
                {
                    var authenticationTicket = await context.UserSessions.FindAsync(id);
                    if (authenticationTicket != null)
                    {
                        authenticationTicket.LastActivity = DateTimeOffset.UtcNow;
                        await context.SaveChangesAsync();

                        return DeserializeFromBytes(authenticationTicket.Value);
                    }
                }
            }

            return null;
        }
        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            using (var scope = _services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AccountContext>();

                string userEmail = ticket.Principal.Identity.Name;

                var id = context.Users.Where(prop => prop.Email == userEmail).FirstOrDefault().UserId;

                var authenticationTicket = new UserSession()
                {
                    UserId = id,
                    LastActivity = DateTimeOffset.UtcNow,
                    Value = SerializeToBytes(ticket)
                };

                var expiresUtc = ticket.Properties.ExpiresUtc;
                if (expiresUtc.HasValue)
                {
                    authenticationTicket.Expires = expiresUtc.Value;
                }

                context.UserSessions.Add(authenticationTicket);
                await context.SaveChangesAsync();

                return authenticationTicket.Id.ToString();
            }
        }
        private byte[] SerializeToBytes(AuthenticationTicket source) => TicketSerializer.Default.Serialize(source);
        private AuthenticationTicket DeserializeFromBytes(byte[] source) => source == null ? null : TicketSerializer.Default.Deserialize(source);
    }
}
