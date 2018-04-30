using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Voting_System.Controllers
{
    public class AdminController : Controller
    {


        DbVoterContext db = new DbVoterContext();
        // GET: Admin
        [HttpGet]
        public ActionResult CreateCandidate()
        {
            ViewBag.CandidateList = db.CandidateDetails.ToList();
            return View();
        }


       

        [HttpPost]
        public ActionResult CreateCandidate(CandidateDetails candidate)
        {
            if (ModelState.IsValid)
            {
                db.CandidateDetails.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("CreateCandidate");
            }
            else
            {
                ModelState.AddModelError("Error", "Ex: This login failed");
               
            }
            ViewBag.CandidateList = db.CandidateDetails.ToList();
            return View();
        }


        
        public ActionResult DeleteCandidate(int id)
        {
            if (ModelState.IsValid)
            {
              CandidateDetails candidate  = db.CandidateDetails.Find(id);
                db.CandidateDetails.Remove(candidate);
                db.SaveChanges();
                ViewBag.CandidateList = db.CandidateDetails.ToList();
            }
            return View("CreateCandidate");
        }

        [HttpGet]
        public ActionResult EditCandidate(int id)
        {
            
                CandidateDetails candidate = db.CandidateDetails.Find(id);
                
                ViewBag.CandidateList = db.CandidateDetails.ToList();
           
            return View("EditCandidate", candidate);
        }

        [HttpPost]
        public ActionResult EditCandidate(CandidateDetails candidate)
        {

            if (ModelState.IsValid)
            {
                

                CandidateDetails candidates = db.CandidateDetails.Find(candidate.CandidateID);
                candidates.CandidateName = candidate.CandidateName;
                candidates.Address = candidate.Address;
                candidates.MobileNo = candidate.MobileNo;
                candidates.CandidateNO = candidate.CandidateNO;

                db.SaveChanges();
                return RedirectToAction("CreateCandidate");

            }

            ViewBag.CandidateList = db.CandidateDetails.ToList();

            return View("EditCandidate");
        }


        

        [HttpGet]
        public ActionResult DeclareResult()
        {
            
              ViewBag.Lock = db.ResultStatuss.Select(x=>x.Lock).ToList()[0];


            
            return View();
        }

        [HttpPost]
        public JsonResult DeclareResult(int ResultId)
        {
            ResultStatus RS = new ResultStatus();
            if (db.ResultStatuss.ToList().Count() > 0)
            {
                RS = db.ResultStatuss.ToList()[0];
                RS.Lock = ResultId;
                
              
            }
            else
            {
                RS.Lock = ResultId;
                db.ResultStatuss.Add(RS);


            }
            db.SaveChanges();
            Session["Results"] = ResultId;
            return Json(new { success = true, returnUrl = Url.Action("DeclareResult", "Admin") }, JsonRequestBehavior.AllowGet);
        }

    }
}