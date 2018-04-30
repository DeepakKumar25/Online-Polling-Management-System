using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace Online_Voting_System.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login





        DbVoterContext db = new DbVoterContext();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string Email, string VoterID, string Password)
        {
            
            CommonResponse CR = new CommonResponse();

            var myLoginData = db.VoterDetails.Where(v => v.EmailID == Email && v.UserPassword == Password && v.Voter_id == VoterID).FirstOrDefault();
            if (myLoginData != null)
            {
                Session["VoterID"] = myLoginData.VoterID;
                ViewBag.VoterName = myLoginData.VoterName;
                return Json(new { success = true, returnUrl = Url.Action("CastVote", "CastVote") }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                CR.Message = "Invalid VoterID or Password";
                return Json(new { success = false, errorMessage = CR.Message }, JsonRequestBehavior.AllowGet);
            }


            //if (db.VoterDetails.Where(x => x.EmailID == Email && x.UserPassword == Password && x.Voter_id == VoterID).Count()> 0)
            //{
            //  VoterDetail VotDet =  db.VoterDetails.Where(x => x.EmailID == Email && x.UserPassword == Password && x.Voter_id == VoterID).SingleOrDefault();
            //    Session["VoterID"] = VotDet.VoterID;
            //    ViewBag.VoterName = db.VoterDetails.Where(x => x.VoterID == VotDet.VoterID).Select(x => x.VoterName).SingleOrDefault();
            //    List<CandidateDetails> candList = db.CandidateDetails.ToList();

            //    return Json(new { success = true, returnUrl = Url.Action("CastVote", "CastVote") },JsonRequestBehavior.AllowGet);


            //}
        


        }


        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(string Username ,string Password)
        {
            CommonResponse CR = new CommonResponse();
            if (Username == "Admin" && Password == "AdminPass")
            {
                return Json(new { success = true, returnUrl = Url.Action("CreateCandidate", "Admin") }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CR.Message = "Invalid VoterID or Password";
                return Json(new { success = false, errorMessage = CR.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Logout()
        {
            return View();
        }


    }
}