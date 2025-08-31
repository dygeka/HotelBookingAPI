
using HotelBookingAPI.Models;
using HotelBookingAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BookingDbContext>(options =>
                options.UseInMemoryDatabase("BookingDb"));

            builder.Services.AddScoped<BookingService>();
            builder.Services.AddScoped<RoomService>();
            builder.Services.AddScoped<HotelService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //remove this to show Swagger docs on the webpage
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
