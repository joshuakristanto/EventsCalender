﻿// <auto-generated />
using System;
using EventCalendarServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EventCalendarServer.Migrations
{
    [DbContext(typeof(CalendarEventData))]
    partial class CalendarEventDataModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("EventCalendarServer.Models.Events", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("TEXT");

                    b.Property<int?>("EventsContentsCommentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EventId");

                    b.HasIndex("EventsContentsCommentId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventCalendarServer.Models.EventsContents", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("CommentId");

                    b.ToTable("EventsContents");
                });

            modelBuilder.Entity("EventCalendarServer.Models.Events", b =>
                {
                    b.HasOne("EventCalendarServer.Models.EventsContents", "EventsContents")
                        .WithMany()
                        .HasForeignKey("EventsContentsCommentId");

                    b.Navigation("EventsContents");
                });
#pragma warning restore 612, 618
        }
    }
}
