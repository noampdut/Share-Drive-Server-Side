﻿using Microsoft.AspNetCore.Mvc;
using trempApplication.Properties.Interfaces;
using trempApplication.Properties.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace trempApplication.Properties.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private IUser _userService;
        private IPassenger _passengerService;
        private INotificationService _notificationService;

        public UsersController(IUser userService, IPassenger passengerService, INotificationService notificationService)
        {
            _userService = userService;
            _passengerService = passengerService;
            _notificationService = notificationService;
        }

        // POST api/<UsersController>/5
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            // Check name and password and return the passenger with old token
            var result = await _userService.GetUserById(user.IdNumber, user.Password); 
            if (result.IsSuccess)
            {
                // Update user with the new token
                await _userService.UpdateUser(user); 
                // The passenger with the old token 
                var passenger = result.passenger;
                passenger.Token = user.Token;
                // Update the passenger in DB
                await _passengerService.UpdatePassenger(passenger, passenger.Id);
                // Return the passenger with the new token
                return Ok(passenger);
            }
            return NotFound(result.ErrorMessage);
        }

        // POST api/<UsersController>
        [HttpPost("{IdNumber}/{UserName}/{Faculty}/{PhoneNumber}/{Token}")]
        public async Task<IActionResult> Register(string IdNumber, string UserName, string Faculty, string PhoneNumber, string Token, [FromBody] string password)
        {
            var user = new User
            {
                IdNumber = IdNumber,
                Password = password,
                Token = Token
            };
            var passenger = new Passenger
            {
                IdNumber = IdNumber,
                UserName = UserName,
                Faculty = Faculty,
                PhoneNumber = PhoneNumber,
                CarIds = new List<Guid> { },
                Bio = "",
                Token = Token
            };
            var result_user = await _userService.AddUser(user);
            var result_passenger = await _passengerService.AddPassenger(passenger);
            if (result_user.IsSuccess && result_passenger.IsSuccess)
            {
                var new_passenger = await _passengerService.GetPassengerByIdNumber(user.IdNumber);
                return Ok(new_passenger.Passenger);
            }
            return BadRequest(result_passenger.ErrorMessage);

        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            var result = await _userService.UpdateUser(user);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{IdNumber}")]
        public async Task<IActionResult> Delete(string IdNumber)
        {
            var result = await _userService.DeleteUser(IdNumber);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
