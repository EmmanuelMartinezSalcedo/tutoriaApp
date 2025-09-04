using tutoriaBE.Core.ContributorAggregate;
using tutoriaBE.Core.CourseAggregate;
using tutoriaBE.Core.SessionAggregate;
using tutoriaBE.Core.UserAggregate;
using tutoriaBE.Core.UserAggregate.Entities;

// Add-Migration InitialCreation -Project tutoriaBE.Infrastructure -StartupProject tutoriaBE.Web -OutputDir Data\Migrations
// Update-Database -Project tutoriaBE.Infrastructure -StartupProject tutoriaBE.Web
// Drop-Database -Project tutoriaBE.Infrastructure -StartupProject tutoriaBE.Web

namespace tutoriaBE.Infrastructure.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options,
  IDomainEventDispatcher? dispatcher) : DbContext(options)
{
  private readonly IDomainEventDispatcher? _dispatcher = dispatcher;

  public DbSet<Contributor> Contributors => Set<Contributor>();
  public DbSet<User> Users => Set<User>();
  public DbSet<Tutor> Tutors => Set<Tutor>();
  public DbSet<TutorCourse> TutorCourses => Set<TutorCourse>();
  public DbSet<Course> Courses => Set<Course>();
  public DbSet<Session> Sessions => Set<Session>();
  public DbSet<ScheduleSlot> ScheduleSlots => Set<ScheduleSlot>();
  public DbSet<SessionSlot> SessionSlots => Set<SessionSlot>();
  public DbSet<Review> Reviews => Set<Review>();
  public DbSet<Chat> Chats => Set<Chat>();
  public DbSet<Message> Messages => Set<Message>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<HasDomainEventsBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges() =>
        SaveChangesAsync().GetAwaiter().GetResult();
}
