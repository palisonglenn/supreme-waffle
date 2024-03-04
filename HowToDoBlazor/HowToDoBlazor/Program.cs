using HowToDoBlazor.Components;
using HowToDoBlazor.Components.Account;
using HowToDoBlazor.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();

/* 
Blazor Application Set Up
 Step 1. Create a Blazor web app (XxxWebApp), .NET 8, individual accounts, interactive render mode set to server
Step 2. Add pages by right clicking > Add > Razor component > Xxx.razor
Step 3. In the same folder add code behind by right clicking > Add > Class > Xxx.razor.cs
Step 3. In the same folder add css by right clicking > Add > Class > Xxx.razor.css
Step 4. In the razor page, remove @code block, add @page "/file path/page name"
ADD AN IF STATEMENT (@if (condition) to ensure page doesn't show if nothing is there)
Step 5. In the code behind make the class partial > public partial class PageName
Step 6. In the Properties region
[Inject]
protected XxxService XxxService { get; set; }
Step 7. In the Fields region
private List<XxxView> xxxView = new();
private string feedback;
Step 8. Add the Blazor helper class to the Components folder
    public static class BlazorHelperClass
    {
        public static Exception GetInnerException(System.Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
Step 9. Create a method to retrieve the view model with try/catch
private void GetXxx()
        {
            try
            {
                xxxView = XxxService.GetXxx();
            }
            #region catch all exceptions
            catch (AggregateException ex)
            {
                foreach (var error in ex.InnerExceptions)
                {
                    feedback = error.Message;
                }
            }

            catch (ArgumentNullException ex)
            {
                feedback = BlazorHelperClass.GetInnerException(ex).Message;
            }

            catch (Exception ex)
            {
                feedback = BlazorHelperClass.GetInnerException(ex).Message;
            }
            #endregion

Step 10. In the razor page add a button with @onclick="GetXxx" method
Step 11. Add the TableTemplate.razor file to Layout
@typeparam TItem
@using System.Diagnostics.CodeAnalysis

<table class="table">
    <thead>
        <tr>@TableHeader</tr>
    </thead>
    <tbody>
        @if (Items != null)
        {
            @foreach (var item in Items)
            {
                if (RowTemplate is not null)
                {
                    <tr>@RowTemplate(item)</tr>
                }
            }
        }
    </tbody>
</table>

@code {
    [Parameter]
    public RenderFragment? TableHeader { get; set; }

    [Parameter]
    public RenderFragment<TItem>? RowTemplate { get; set; }

    [Parameter, AllowNull]
    public IReadOnlyList<TItem> Items { get; set; }
}


Create the System Library 
 Step 1. Right-click the solution > Add > New Project > Class Library template
Step 2. Name the project XxxSystem, .NET 8
Step 3. Delete the default class file (Class1.cs)
Step 4. Set the web app to be the startup project
Step 5. In the class library, create BLL, DAL, Entities, ViewModels folders with dummy.txt
Step 6. Reverse engineer - Right click XxxSystem and select EF Core Power Tools > Reverse engineer
Step 7. Use EF Core 8 > Add > Add database connection
Step 8. Select the server name, database, test connection, OK (select trust certificate if necessary)
Step 9. Select all required tables (not unit test)
Step 10. Set context name to XxxContext, set Namespace to XxxSystem, set Entity Types to Entities, set Use Table and Column Names to True, set Use DataAnnotation Attributes to True, select Install the EF Core provider package
Step 11. Select Advanced > Set DbContext path to DAL > OK
Step 12. Set XxxContext and entity classes to internal
Step 13. Right click the web app > Add project reference > select class library
Step 14. Add the connection string: Open appsettings.json > Under ConnectionStrings add "DatabaseName": "Server=.;Database=DatabaseName; Trusted_Connection=true;TrustedServerCertificate=true;MultiActiveResultSets=true"
Step 15. In the BLL folder, create an XxxService class (remove dummy.txt), make it public
Step 16. In XxxService add a field > 
private readonly XxxContext? _xxxContext;
Step 17. In xxxService create a constructor > 
internal XxxService(XxxContext xxxContext) 
{
    _xxxContext = xxxContext;
}
Step 18. Add extension class > XxxExtension.cs to ViewModels folder
Step 19. 
public static class XxxExtension
    {
        public static void AddBackendDependencies(this IServiceCollection services,
          Action<DbContextOptionsBuilder> options)
        {           
            services.AddDbContext<XxxContext>(options);
           
            services.AddTransient<XxxService>((ServiceProvider) =>
            {                
                var context = ServiceProvider.GetService<XxxContext>();              
                return new XxxService(context);
            });
Step 20. Create the ViewModel class > XxxView.cs, namespace XxxSystem.ViewModels, public class, add the public properties { get; set; }
Step 21. Create the method in LINQPad (set to C# program, add XxxView to LINQPad, set connection to correct database) and ensure it runs correctly
Step 22. Copy the method into the XxxService class
Step 23. Change method to start with public List<XxxView> GetXxx() {
return _xxxContext.InitalTableInQuery .Select(x => new XxxView}).ToList();
Step 24. Add #nullable disable and using XxxSystem.ViewModels to the top of the file

<table class="table">
                        <thead>
                            <tr>
                                <th>Label</th>
                                <th>Label</th>
                                <th>Label</th>
                            </tr>
                        </thead>
                        <tbody>
                            @foreach (var x in XXX)
                            {
                                <tr>
                                    <td>
                                        <button class="btn btn-success" @onclick="() => Remove(game.GameIndex)">Remove</button>
                                    </td>
                                    <td>
                                        <select @bind="@game.HomeTeamID" class="form-select">
                                            <option value="@game.HomeTeamID" disabled>
                                                @foreach (var item in homeTeamList)
                                                {
                                                    if (item.ValueId == game.HomeTeamID)
                                                    {
                                                        @item.DisplayText
                                                    }
                                                }
                                            </option>
                                        </select>
                                    </td>
<td>
                                        <select @bind="@game.VisitingTeamID" class="form-select">
                                            <option value="@game.VisitingTeamID" disabled>
                                                @foreach (var item in visitTeamList)
                                                {
                                                    if (item.ValueId == game.VisitingTeamID)
                                                    {
                                                        @item.DisplayText
                                                    }
                                                }
                                            </option>
                                        </select>
                                    </td>
                                </tr>
                            }
                        </tbody>
                    </table>
 */