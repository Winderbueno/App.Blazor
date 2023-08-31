using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Presentation.Middlewares.Authorization;

public static class PolicyBuilder
{
    private const string claimCreate = "create";
    private const string claimRead = "read";
    private const string claimUpdate = "update";
    private const string claimDelete = "delete";

    /// <summary>
    /// Creates policies with names "{entity}.(create | read | update | delete)"
    ///     These policies required the corresponding claims [{entity}.(create | read | update | delete)]
    /// </summary>
    public static void AddCrudPolicy(this AuthorizationOptions options, string entity)
    {
        options.AddCreatePolicy(entity);
        options.AddReadPolicy(entity);
        options.AddUpdatePolicy(entity);
        options.AddDeletePolicy(entity);
    }

    public static void AddCreatePolicy(this AuthorizationOptions options, string entity)
        => options.AddPolicy(entity, claimCreate);

    public static void AddReadPolicy(this AuthorizationOptions options, string entity)
        => options.AddPolicy(entity, claimRead);

    public static void AddUpdatePolicy(this AuthorizationOptions options, string entity)
        => options.AddPolicy(entity, claimUpdate);

    public static void AddDeletePolicy(this AuthorizationOptions options, string entity)
        => options.AddPolicy(entity, claimDelete);


    private static void AddPolicy(this AuthorizationOptions options, string entity, string claimName)
        => options.AddPolicy(
            $"{entity}.{claimName}",
            policy => policy.RequireClaim(ClaimTypes.Role, $"{entity}.{claimName}"));
}