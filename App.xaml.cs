using System.Windows;
using DentalMVP.Data;

namespace DentalMVP
{
  public partial class App : Application
  {
    public static DentalContext Db { get; private set; } = null!;
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      Db = new DentalContext();
      Db.Database.EnsureCreated();
      Db.SeedIfEmpty();
    }
    protected override void OnExit(ExitEventArgs e)
    {
      Db.Dispose();
      base.OnExit(e);
    }
  }
}