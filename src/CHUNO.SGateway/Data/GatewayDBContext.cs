using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using CHUNO.Framework.Data.Core;
using CHUNO.Framework.Domain.Primitives.Maybe;
using CHUNO.Framework.Domain.Primitives;
using CHUNO.Framework.Domain.Abstractions;
using CHUNO.Framework.Domain.Events;
using System.Reflection;
using CHUNO.Framework.Data;
using CHUNO.Framework.Core.Intefaces;
using CHUNO.SGateway.Domain.Entities;

namespace CHUNO.SGateway.Data
{
    public sealed class GatewayDBContext : BaseDBContext
    {
        public GatewayDBContext(DbContextOptions options, IDateTime dateTime, IEventDispatcher dispatcher)
            : base(options, dateTime,dispatcher)
        {
        }

        public DbSet<AppClient> AppClients => this.Set<AppClient>();
        public DbSet<GatewayRouteConfig> GatewayRouteConfigs => this.Set<GatewayRouteConfig>();
    }

}
