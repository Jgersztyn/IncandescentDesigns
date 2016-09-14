using IncandescentDesigns.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IncandescentDesigns.DAL
{
    public class IncandescentDesignsContext : DbContext
    {
        public IncandescentDesignsContext() : base("IncandescentDesignsContext")
        {
        }

        public DbSet<ProgramModel> Programs { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<FavoriteProgram> FavoritePrograms { get; set; }
        public DbSet<NewsFeed> News { get; set; }
        public DbSet<AccountDisabled> Disabled { get; set; }
        public DbSet<PremadeProgramModel> PremadePrograms { get; set; }
        public DbSet<Forum> Forum { get; set; }
        public DbSet<ForumTopic> ForumThread { get; set; }
        public DbSet<ForumPost> ForumPost { get; set; }
        public DbSet<StoredFileModel> StoredFiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}