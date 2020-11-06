using System;
using Microsoft.AspNetCore.Mvc;
using TripLog.Models;

namespace TripLog.Controllers
{
	public class TripController : Controller
	{
		private TripLogContext context { get; set; }
		public TripController(TripLogContext ctx) => context = ctx;

		public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

		[HttpGet]
		public ViewResult Add(string id = "")
		{
			//1. Create a TripViewModel variable and instantiate it.
			var vm = new TripViewModel();
			/********************************************************************************************
			* need to pass Accommodation value, or Destination value (depending on page number and 
			* Accommodation value), to by view. 
			* 
			* Accommodation is an optional value - don't need it to persist after being read if it's null.
			* So, do straight read, and if it's not null, use Keep() method to make sure it persists.
			* 
			* Destination is a a required value - always need it to persist after being read. 
			* So, use Peek() method to read it and make sure it persists.
			*********************************************************************************************/

			if (id.ToLower() == "page2")
			{
				//2. Check to see if accommodation data has been populated
				var accommodation = TempData[nameof(Trip.Accommodation)]?.ToString();

				if (string.IsNullOrEmpty(accommodation))
				{  // skip to page 3
				   //If not:
				   //i. Set the page number property in your TripViewModel object to 3
					vm.PageNumber = 3;
					//ii. Since you have Destination data from page 2, 
					//set the Trip property by creating a new Trip object with the
					//Destination value you already have.  
					//See the example for ActiveConf and ActiveDiv on page 309.
					//The only difference is you need to use:
					//nameof(Trip.Destination)).ToString()
					//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/nameof
					Trip trip = new Trip
					{
						Destination = TempData.Peek(nameof(Trip.Destination)).ToString()
					};

					//iii. Return the Add3 view and pass in your TripViewModel variable

					return View("Add3", vm); //(replace this in step iii)
				}
				else
				{
					//If it has:
					//i. Set the page number property in your TripViewModel object to 2
					vm.PageNumber = 2;
					//ii. Set the Trip property by creating a new Trip object with the
					//Accommodation value you already have.
					Trip trip = new Trip
					{
						Accommodation = accommodation
					};
					//iii. Call this to retain                                           
					TempData.Keep(nameof(Trip.Accommodation));
					//iv. Return the Add3 view and pass in your TripViewModel variable

					return View("Add2", vm); //(replace this in step iv)
				}
			}
			else if (id.ToLower() == "page3")
			{
				//3. page3 action
				//i. Set the page number property in your TripViewModel object to 3
				vm.PageNumber = 3;
				//ii. Since you have Destination data from page 2, 
				//set the Trip property by creating a new Trip object with the
				//Destination value you already have. 
				//See the example for ActiveConf and ActiveDiv on page 309.
				//The only difference is you need to use:
				//nameof(Trip.Destination)).ToString()
				Trip trip = new Trip
				{
					Destination = TempData.Peek(nameof(Trip.Destination)).ToString()
				};
				//iii. Return the Add3 view and pass in your TripViewModel variable

				return View("Add3", vm); //(replace this in step iii)
			}
			else
			{
				//4. This is for the first add page.
				//i. Set the page number property in your TripViewModel object to 1
				vm.PageNumber = 1;
				//ii. Return the Add1 view and pass in your TripViewModel variable

				return View("Add1", vm); //(replace this in step ii)
			}
		}

		[HttpPost]

		public IActionResult Add(TripViewModel vm)
		//5. Replace the above method signature with this (above is a placeholder)
		//public IActionResult Add(TripViewModel vm)
		{
			//if (vm.PageNumber == 1)
			//6. Add "vm" to the following (see above):
			if (vm.PageNumber == 1)
			{
				if (ModelState.IsValid) // only page 1 has required data
				{
					/***************************************************
					* Store data in TempData and redirect (PRG pattern)
					****************************************************/
					//7. Set your Destination in TempData by commenting out this:
					TempData[nameof(Trip.Destination)] = vm.Trip.Destination;

					//8. Repeat the above for Accommodation, StartDate, and EndDate
					TempData[nameof(Trip.Accommodation)] = vm.Trip.Accommodation;
					TempData[nameof(Trip.StartDate)] = vm.Trip.StartDate;
					TempData[nameof(Trip.EndDate)] = vm.Trip.EndDate;
					//9. Using the example on page 303 for a RedirectToAction with a parameter, 
					//call the correct redirect for "Add" (current controller) with the id = "Page2"
					return RedirectToAction("Add", new { id = "Page2" }); //(replace this with code from step 9)
				}
				else
				{
					//10. Return the Add1 view and pass in your TripViewModel variable
					return View("Add1", vm); //(replace this with code from step 10)
				}
			}
			//else if (vm.PageNumber == 2)
			//11. Add "vm" to the following (see above):
			else if (vm.PageNumber == 2)
			{
				/***************************************************
					* Store data in TempData and redirect (PRG pattern)
				****************************************************/
				//12. See step 7 above to add the AccommodationPhone and AccommodationEmail
				//properties to TempData
				TempData[nameof(Trip.AccommodationPhone)] = vm.Trip.AccommodationPhone;
				TempData[nameof(Trip.AccommodationEmail)] = vm.Trip.AccommodationEmail;
				//13. Using the example on page 303 for a RedirectToAction with a parameter, 
				//call the correct redirect for "Add" (current controller) with the id = "Page3"
				return RedirectToAction("Add", new { id = "Page3" }); //(replace this with code from step 13)
			}
			//else if (vm.PageNumber == 3)
			//14. Add "vm" to the following (see above):
			else if (vm.PageNumber == 3)
			{
				/***************************************************
					* Don't need data in TempData to persist after 
					* updating database, so do straight read.
				****************************************************/
				//15. Comment out this line of code to set the Destination:
				vm.Trip.Destination = TempData[nameof(Trip.Destination)].ToString();

				//16.  Use step 15 as a guide for Accommodation, StartDate, EndDate,
				//AccommodationPhone, and AccommodationEmail
				vm.Trip.Accommodation = TempData[nameof(Trip.Accommodation)].ToString();
				vm.Trip.StartDate = (DateTime?)TempData[nameof(Trip.StartDate)]; //cast or VS hates me :(
				vm.Trip.EndDate = (DateTime?)TempData[nameof(Trip.EndDate)];
				vm.Trip.AccommodationPhone = TempData[nameof(Trip.AccommodationPhone)].ToString();
				vm.Trip.AccommodationEmail = TempData[nameof(Trip.AccommodationEmail)].ToString();

				//17. Pass your Trip object from the TripViewModel object into the Add method:
				//context.Trips.Add([view model object]);
				context.Trips.Add(vm.Trip);
				context.SaveChanges();

				//18. Pass the Trip object's Destination property in here
				TempData["message"] = $"Trip to {vm.Trip.Destination} added.";

				//19. Using another example on page 303 for a RedirectToAction 
				//Redirect to the Index action method of the Home controller:
				return RedirectToAction("Index", "Home");

			}
			else
			{
				//20. Using another example on page 303 for a RedirectToAction 
				//Redirect to the Index action method of the Home controller:
				//(same as step 19)
				return RedirectToAction("Index", "Home");

			}

		}

		public RedirectToActionResult Cancel()
		{
			TempData.Clear();
			return RedirectToAction("Index", "Home");
		}
	}
}