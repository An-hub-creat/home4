using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UFAR.Classwork.Core.Services;
using UFAR.Classwork.Data.DAO;
using UFAR.Classwork.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register ApplicationDBContext with the DI container and say use SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainDB")));

// Register services
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    SeedData.Initialize(context);
}

app.Run();
public class LibraryService : ILibraryService
{
    private readonly ILibraryRepository _libraryRepository;

    public LibraryService(ILibraryRepository libraryRepository)
    {
        _libraryRepository = libraryRepository;
    }

    public async Task<IEnumerable<Library>> GetAllAsync()
    {
        return await _libraryRepository.GetAllAsync();
    }

    public async Task<Library> GetByIdAsync(int id)
    {
        return await _libraryRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(Library library)
    {
        await _libraryRepository.AddAsync(library);
    }

    public async Task UpdateAsync(Library library)
    {
        await _libraryRepository.UpdateAsync(library);
    }

    public async Task DeleteAsync(int id)
    {
        await _libraryRepository.DeleteAsync(id);
    }
}

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    // CRUD operations
}

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    // CRUD operations
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // CRUD operations
}
public interface IBookService
{
    // CRUD operations

    Task RentBookAsync(int bookId, int userId);
}

public class BookService : IBookService
{


    public async Task RentBookAsync(int bookId, int userId)
    {
        // Implement the renting functionality
    }
}
public interface IBookService
{
    // CRUD operations

    Task<int> RateBookAsync(int bookId, int userId, int rating);
}

public class BookService : IBookService
{
    public async Task<int> RateBookAsync(int bookId, int userId, int rating)
    {
        // Implement the rating functionality
    }
}
public interface ILibraryService
{
    Task<decimal> GetBalanceAsync(int libraryId);
    Task AddFundsAsync(int libraryId, decimal amount);
    Task WithdrawFundsAsync(int libraryId, decimal amount);
}

public class LibraryService : ILibraryService
{
    public async Task<decimal> GetBalanceAsync(int libraryId)
    {
        // Implement the balance management functionality
    }

    public async Task AddFundsAsync(int libraryId, decimal amount)
    {
        // Implement the balance management functionality
    }

    public async Task WithdrawFundsAsync(int libraryId, decimal amount)
    {
        // Implement the balance management functionality
    }
}