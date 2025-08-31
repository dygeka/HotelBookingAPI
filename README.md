# Example test:

1. Run api/Test/seed to seed test data
2. Use TestController functions to verify Hotels, Rooms, and Booking dtos (optional)
3. Find hotel with /api/Hotels/{name} (use HotelA or HotelB)
4. Use /api/Rooms/available to find available rooms, for example:
>hotelName: HotelA  
>checkIn:   2025-09-04  
>checkOut:  2025-09-07  
>numberOfGuests: 1  
>Should return the five rooms which are free between those dates

5.Use api/bookings to book room, for example:
>hotelName: HotelA  
>roomNumber: 6  
>guestName: Name  
>numberOfGuests: 1  
>checkIn: 2025-09-04  
>checkOut: 2025-09-07  

6.Get booking reference from response body (or use TestController's GetBookings)  
7.Use api/Bookings/{bookingReference} to find the booking

# Future improvements
  Add in documentation of functions as comments (such as DoxyGen)  
  Seperate TestController from production DB  
  Make functions such as DoesHotelExist and DoesRoomExist for readability  
  Supress "Deference of a possibly null reference" warning (making Hotel required in Room causes errors in TestController and Tests)  
  Add more custom validation attributes such as numberOfGuests > 0 instead of manual checks  
  Standardise error messages instead of writing them out manually  
  Add interfaces for services  
  Add DockerFile  
  Add Azure pipelines for CI/CD  
	
	
