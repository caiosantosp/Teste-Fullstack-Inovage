using TesteFullstackInovage.Server.Models;
using TesteFullstackInovage.Server.Services;

var builder = WebApplication.CreateBuilder(args);
var corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddTransient<BusinessPartnersService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins(corsSettings.AllowedOrigins.ToArray())
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/businesspartners", async (BusinessPartnersService service) =>
{
    try
    {
        var businessPartners = await service.GetAllBusinessPartnersAsync();
        return Results.Ok(businessPartners);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("/businesspartners", async (BusinessPartners businessPartner, BusinessPartnersService service) =>
{
    try
    {
        var businessCreate = await service.CreateBusinessPartnerAsync(businessPartner);
        return Results.Ok(businessCreate);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapDelete("/businesspartners/{businessPartnerId}", async (string businessPartnerId, BusinessPartnersService service) =>
{
    try
    {
        var businessDeleted = await service.DeleteBusinessPartnerAsync(businessPartnerId);
        return Results.Ok(businessDeleted);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPut("/businesspartners/{businessPartnerId}", async (string businessPartnerId, BpCardName bpCardName, BusinessPartnersService service) =>
{
    try
    {
        var businessUpdated = await service.UpdateBusinessPartnerAsync(businessPartnerId, bpCardName);
        return Results.Ok(businessUpdated);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapFallbackToFile("/index.html");

app.Run();

public record BpCardName(string CardName);