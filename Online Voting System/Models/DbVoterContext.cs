namespace Online_Voting_System
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbVoterContext : DbContext
    {

        
        public DbVoterContext()
            : base("name=ConnectionString")
        {
        }

        public virtual DbSet<VoterDetail> VoterDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VoterDetail>()
                .Property(e => e.Voter_id)
                .IsUnicode(false);

            modelBuilder.Entity<VoterDetail>()
                .Property(e => e.EmailID)
                .IsUnicode(false);

            modelBuilder.Entity<VoterDetail>()
                .Property(e => e.UserPassword)
                .IsUnicode(false);

            modelBuilder.Entity<VoterDetail>()
                .Property(e => e.VoterName)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<Online_Voting_System.CandidateDetails> CandidateDetails { get; set; }

        public System.Data.Entity.DbSet<Online_Voting_System.ResultStatus> ResultStatuss { get; set; }


        
    }
}
