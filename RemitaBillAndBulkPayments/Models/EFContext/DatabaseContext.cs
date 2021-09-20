using Microsoft.EntityFrameworkCore;
using RemitaBillAndBulkPayments.Models.Auth;
using RemitaBillAndBulkPayments.Models.BillersRequests;
using RemitaBillAndBulkPayments.Models.BillersResponses;
using RemitaBillAndBulkPayments.Models.BulkRequests;
using RemitaBillAndBulkPayments.Models.BulkResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.EFContext
{
    public partial class DatabaseContext : DbContext
    {
       
            public DatabaseContext(DbContextOptions<DatabaseContext> options)
                : base(options)
            {
                 
            }

        public DbSet<BillPaymentRequest> PaymentRequests { get; set; }
        public DbSet<BillPaymentResponse> PaymentResponses { get; set; }
        public DbSet<BulkRequest> BulkRequests { get; set; }
        public DbSet<BulkResponse> BulkResponses { get; set; }
        public DbSet<TokenData> TokenData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
           {              

                OnModelCreatingPartial(modelBuilder);
           }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        
    }
}
