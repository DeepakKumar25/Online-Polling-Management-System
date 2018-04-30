using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Voting_System.Controllers
{
    public class ResultsController : Controller
    {
        // GET: Results

        public ActionResult Index()
        {
            return View();
        }

        DbVoterContext db = new DbVoterContext();
        public ActionResult Results()
        {
            int Result = 0;


            if (db.ResultStatuss.Count() > 0)
            {
                Result= (int)db.ResultStatuss.Select(x => x.Lock).ToList()[0];

            }

            ViewBag.Result = Result;
           if (Result == 1) {
                var winner = db.CandidateDetails.Select(x => new { CandidateID = x.CandidateID, CandidateName = x.CandidateName, VoteCount = x.VoteCount }).OrderByDescending(x => x.VoteCount).Take(1).FirstOrDefault();

                ViewBag.Winner = winner.CandidateName;


                
            }
            List<CandidateDetails> candidate = db.CandidateDetails.ToList();
            return View(candidate);
        }
    }
}