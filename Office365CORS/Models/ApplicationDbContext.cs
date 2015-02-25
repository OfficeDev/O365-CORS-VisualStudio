//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;

//namespace Office365CORS.Models
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext()
//            : base("DefaultConnection")
//        {
//        }

//        public DbSet<UserTokenCache> UserTokenCacheList { get; set; }
//    }

//    public class UserTokenCache
//    {
//        [Key]
//        public int UserTokenCacheId { get; set; }
//        public string webUserUniqueId { get; set; }
//        public byte[] cacheBits { get; set; }
//        public DateTime LastWrite { get; set; }
//    }
//}