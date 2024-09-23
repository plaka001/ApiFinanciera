
using Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace Api.OptionsSetup;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string sectionName = "Jwt";
    private readonly IConfiguration configuration;
    public JwtOptionsSetup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void Configure(JwtOptions options)
    {
        configuration.GetSection(sectionName).Bind(options);
    }
}