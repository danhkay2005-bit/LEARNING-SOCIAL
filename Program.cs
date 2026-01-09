// Ensure you register UserDbContext and IMapper in your DI container before registering NguoiDungService
// Example (add this to your ConfigureServices method):

services.AddDbContext<UserDbContext>(options =>
{
    // configure your db context here, e.g. options.UseSqlServer(connectionString);
});
services.AddAutoMapper(typeof(Startup)); // or the assembly where your profiles are
services.AddScoped<INguoiDungService, NguoiDungService>();