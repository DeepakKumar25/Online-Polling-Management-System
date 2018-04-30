using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Voting_System.Controllers
{
    public class CastVoteController : Controller
    {
        // GET: CastVote
        public ActionResult Index()
        {
            return View();
        }

        
        DbVoterContext db = new DbVoterContext();
        [HttpGet]
        public ActionResult CastVote()
        {

            int VoterID = (int)Session["VoterID"];

            ViewBag.VoterName = db.VoterDetails.Where(x => x.VoterID == VoterID).Select(x => x.VoterName).SingleOrDefault();
            List<CandidateDetails> candList = db.CandidateDetails.ToList();

            return View(candList);
        }

        [HttpPost]
        public ActionResult CastVote(int CandidateID)
        {
            CommonResponse CR = new CommonResponse();

            int VoterID = (int)Session["VoterID"];

            int Result = 0;


            if (db.ResultStatuss.Count() > 0)
            {
                Result = (int)db.ResultStatuss.Select(x => x.Lock).ToList()[0];

            }

            ViewBag.VoterName = db.VoterDetails.Where(x => x.VoterID == VoterID).Select(x => x.VoterName).SingleOrDefault();
            List<CandidateDetails> candList = db.CandidateDetails.ToList();

            if (db.VoterDetails.Where(x => x.VoterID == VoterID && x.VoteID == 0).Count() > 0 && Result == 0)
            {
                CandidateDetails Candidate = db.CandidateDetails.Find(CandidateID);
                Candidate.VoteCount = (Candidate.VoteCount == null ? 0 : Candidate.VoteCount) + 1;





                VoterDetail voteDet = db.VoterDetails.Find(VoterID);
                voteDet.VoteID = CandidateID;
                db.SaveChanges();
                CR.Message = "VOTED SUCCESSFULLY !!!!";
                return Json(CR, JsonRequestBehavior.AllowGet);
            }
            else if (Result == 1)
            {
                CR.Message = "VOTING HAS BEEN CLOSED !!!";
                return Json(CR, JsonRequestBehavior.AllowGet);
            }

            else
        
            {
                CR.Message = "YOU HAVE ALREADY VOTED !!!!";
                return Json(CR,JsonRequestBehavior.AllowGet);
            }
            

            

        }
    }
}