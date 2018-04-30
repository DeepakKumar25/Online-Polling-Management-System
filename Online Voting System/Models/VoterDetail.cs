namespace Online_Voting_System
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VoterDetails")]
    public partial class VoterDetail
    {
        [Key]
        public int VoterID { get; set; }

        [StringLength(100)]
        public string Voter_id { get; set; }

        [StringLength(500)]
        public string EmailID { get; set; }

        [StringLength(40)]
        public string UserPassword { get; set; }

        [StringLength(120)]
        public string VoterName { get; set; }

        public int? VoteID { get; set; }
    }


    [Table("CandidateDetails")]
    public partial class CandidateDetails
    {
        [Key,Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CandidateID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Please enter Candidate name.")]
        public string CandidateName { get; set; }

        [Required]
        public string CandidateNO { get; set; }

        [StringLength(10)]
        public string MobileNo { get; set; }
       
        
       
        public string Address { get; set; }


        public Nullable<int> VoteCount { get; set; }

      
    }

   


   public class CommonResponse
    {
        public string Message { get; set; }


        public List<object> data { get; set; }
    }


    [Table("ResultStatus")]
    public class ResultStatus
    {
        [Key]
        public int ResultId { get; set; }
        public Nullable<int> Lock { get; set; }
    }
}


