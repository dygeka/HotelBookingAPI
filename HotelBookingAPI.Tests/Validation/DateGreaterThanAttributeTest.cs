using HotelBookingAPI.Models;
using HotelBookingAPI.Models.DTOs;
using HotelBookingAPI.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelBookingAPI.Tests.Validation
{
    public class DateGreaterThanAttributeTest
    {
        private DateOnly today;
        private DateOnly tomorrow;
        private DateOnly yesterday;

        DateGreaterThanAttributeTest()
        {
            today = DateOnly.FromDateTime(DateTime.Today);
            tomorrow = today.AddDays(1);
            yesterday = today.AddDays(-1);
        }

        [Fact]
        public void CorrectDateTest()
        {
            var bookingDto = new CreateBookingDto
            {
                HotelName = "HotelA",
                RoomNumber = "1",
                GuestName = "Test",
                NumberOfGuests = 1,
                CheckIn = today,
                CheckOut = tomorrow
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(bookingDto);

            // Act
            bool isValid = Validator.TryValidateObject(
                bookingDto,
                validationContext,
                validationResults,
                validateAllProperties: true);

            // Assert
            Assert.True(isValid);
            Assert.Empty(validationResults);
        }
    }
}
