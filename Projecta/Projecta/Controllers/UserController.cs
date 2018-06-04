using log4net;
using Microsoft.Practices.Unity;
using UserData.DAL;
using UserData.Models;
using UserData.Utiltiy;
using System;
using System.Web.Http;

namespace UserData.Controllers
{
    public class UserController : ApiController
    {
        private IUserRepository _userRepo;

        //logger to log error and info in file
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("User Api Controller Logger");

        public UserController()
        {
            var container = new UnityContainer();
            _userRepo = container.Resolve<UserRepository>();
        }

        [Route("api/user/{id :uint:min(1)}/")]
        [HttpGet]
        public IHttpActionResult Get(uint id)
        {
            try
            {
                var user = _userRepo.GetUserById(id);
                if (user != null)
                {
                    return Ok(user);
                }

                return Ok("User Not Found!!!");

            }

            catch (Exception ex)
            {
                log.Error("Error Occured for Id: " + id, ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Api to Create and Update user
        /// If id is 0 then creates new User else Updates the user with given Id (if it exists)
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id of inserted/updated user else -1 if user not exixts</returns>
        public IHttpActionResult Post([FromBody]UserEntity user)
        {
            try
            {
                // Validation Code Starts
                bool isValid = true;

                if (user != null && user.Id < 1)
                {
                    //Validation for Insert 
                    isValid = (Validation.IsValidEmail(user.EmailId) && Validation.IsValidName(user.FirstName) && Validation.IsValidName(user.LastName) && Validation.IsValidMobile(user.MobileNumber));
                }
                else if (user != null)
                {
                    //Validation for Update
                    if (!string.IsNullOrEmpty(user.EmailId))
                    {
                        isValid = Validation.IsValidEmail(user.EmailId);
                    }

                    if (!string.IsNullOrEmpty(user.FirstName))
                    {
                        isValid = Validation.IsValidName(user.FirstName);
                    }
                    if (!string.IsNullOrEmpty(user.LastName))
                    {
                        isValid = Validation.IsValidName(user.LastName);
                    }
                    if (!string.IsNullOrEmpty(user.MobileNumber))
                    {
                        isValid = Validation.IsValidMobile(user.MobileNumber);
                    }

                }
                else
                {
                    isValid = false;
                }
                //Validation Ends

                if (!isValid)
                {
                    log.Info("Invalid Parameters for User : " + user);
                    return BadRequest("Invalid Parameters");
                }

                int id = _userRepo.UpdateUser(user);

                if (user.Id > 0)
                    log.Info("User Info updated with id " + id);
                else
                    log.Info("User Info Created with id " + id);
                return Ok(id);
            }

            catch (Exception ex)
            {
                log.Error("Error Occured in CreateUser for User: " + user, ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Api to Delete user
        /// If User not found then return -1
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id of deleted user else -1 if user not exixts</returns>
        [Route("api/user/{id :uint:min(1)}/")]
        [HttpDelete]
        public IHttpActionResult Delete(uint id)
        {

            try
            {
                UserEntity user = new UserEntity() { Id = id, IsActive = false };
                return Ok(_userRepo.UpdateUser(user));
            }
            catch (Exception ex)
            {
                log.Error("Error Occured in Delete for Id: " + id, ex);
                return InternalServerError(ex);
            }
        }
    }
}