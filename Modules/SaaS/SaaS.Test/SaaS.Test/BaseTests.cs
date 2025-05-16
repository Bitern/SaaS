using Infrastructure.Database;
using SaaS.Domain.Users;
using System.Reflection;
using System.Windows.Input;
using SaaS.Api;

namespace SaaS.Test;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}