using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using System;

namespace Ordering.Infrastructure.EntityConfigurations
{
    class PaymentMethodEntityTypeConfiguration
        : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> paymentConfiguration)
        {
            paymentConfiguration.ToTable("paymentmethods", OrderingContext.DEFAULT_SCHEMA);

            paymentConfiguration.HasKey(b => b.Id);

            //paymentConfiguration.Property(b => b.Id)
            //    .ForSqlServerUseSequenceHiLo("paymentseq", OrderingContext.DEFAULT_SCHEMA);

            paymentConfiguration.Property<int>("BuyerId")
                .IsRequired();

            paymentConfiguration.Property<string>("CardHolderName")
                .HasMaxLength(200)
                .IsRequired();

            paymentConfiguration.Property<string>("Alias")
                .HasMaxLength(200)
                .IsRequired();

            paymentConfiguration.Property<string>("CardNumber")
                .HasMaxLength(25)
                .IsRequired();

            paymentConfiguration.Property<DateTime>("Expiration")
                .IsRequired();

            paymentConfiguration.Property<int>("CardTypeId")
                .IsRequired();

            paymentConfiguration.HasOne(p => p.CardType)
                .WithMany()
                .HasForeignKey("CardTypeId");
        }
    }
}
