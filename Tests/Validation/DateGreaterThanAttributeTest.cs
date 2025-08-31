using HotelBookingAPI.Models.DTOs;

using System.ComponentModel.DataAnnotations;

namespace HotelBookingAPI.Tests.Validation
{
    public class DateGreaterThanAttributeTest
    {
        private DateOnly today;
        private DateOnly tomorrow;

        public DateGreaterThanAttributeTest()
        {
            today = DateOnly.FromDateTime(DateTime.Today);
            tomorrow = today.AddDays(1);
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

            bool isValid = Validator.TryValidateObject(
                bookingDto,
                validationContext,
                validationResults,
                validateAllProperties: true);

            Assert.True(isValid);
            Assert.Empty(validationResults);
        }

        [Fact]
        public void CheckOutBeforeCheckIn()
        {
            var bookingDto = new CreateBookingDto
            {
                HotelName = "HotelA",
                RoomNumber = "1",
                GuestName = "Test",
                NumberOfGuests = 1,
                CheckIn = tomorrow,
                CheckOut = today
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(bookingDto);

            bool isValid = Validator.TryValidateObject(
                bookingDto,
                validationContext,
                validationResults,
                validateAllProperties: true);

            Assert.False(isValid);

            if (validationResults.Count != 1)
            {
                Assert.Fail("Expected validation error");
            }

            var expectedError = "Check out cannot be before check in.";
            Assert.Equal(validationResults[0].ToString(), expectedError);
        }

        [Fact]
        public void CheckOutSameDayAsCheckIn()
        {
            var bookingDto = new CreateBookingDto
            {
                HotelName = "HotelA",
                RoomNumber = "1",
                GuestName = "Test",
                NumberOfGuests = 1,
                CheckIn = today,
                CheckOut = today
            };

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(bookingDto);

            bool isValid = Validator.TryValidateObject(
                bookingDto,
                validationContext,
                validationResults,
                validateAllProperties: true);

            Assert.False(isValid);

            if (validationResults.Count != 1)
            {
                Assert.Fail("Expected validation error");
            }

            var expectedError = "Check out cannot be before check in.";
            Assert.Equal(validationResults[0].ToString(), expectedError);
        }
    }
}
