using System.Linq;
using api.Context;

namespace api.Repositories
{
  public class UnitOfWork : IUnitOfWork {
    private readonly LumenContext _lumenContext;
    public IGuildRepository Guilds { get; private set; }
    public IUserRepository Users { get; private set; }
    public UnitOfWork (LumenContext lumenContext)
    {
        _lumenContext = lumenContext;
        Guilds = new GuildRepository (_lumenContext);
        Users = new UserRepository (_lumenContext);
    }
    public void Complete () => _lumenContext.SaveChanges ();
    public void Rollback() => _lumenContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
    public void Dispose () => _lumenContext.Dispose ();
  }
}