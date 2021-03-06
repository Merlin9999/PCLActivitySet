﻿using LiteDB;
using NUnit.Framework;
using PCLActivitySet.Domain.Recurrence;
using PCLActivitySet.Dto.Recurrence;
using Ploeh.AutoFixture;
using Ploeh.SemanticComparison.Fluent;
using System.IO;
using PCLActivitySet.Test.Helpers;

namespace PCLActivitySet.Test.Dto
{
    [TestFixture]
    public class DateRecurrenceTest
    {
        [Test]
        public void DtoAndDomainRoundTrip()
        {
            Fixture fixture = TestHelper.CreateSerializationAutoFixture();

            DateRecurrenceDto sourceDto = fixture.Create<DateRecurrenceDto>();
            DateRecurrence domain = DateRecurrence.FromDto(sourceDto);
            DateRecurrenceDto targetDto = DateRecurrence.ToDto(domain);

            var sourceDtoLikeness = sourceDto.AsSource().OfLikeness<DateRecurrenceDto>();
            sourceDtoLikeness.ShouldEqual(targetDto);
        }

        [Test]
        public void DtoAndLiteDbRoundTrip()
        {
            Fixture fixture = TestHelper.CreateSerializationAutoFixture(useLiteDBCompatibleDateTime: true);

            DateRecurrenceDto sourceDto = fixture.Create<DateRecurrenceDto>();
            DateRecurrenceDto targetDto;
            using (var db = new LiteDatabase(new MemoryStream()))
            {
                var col = db.GetCollection<DateRecurrenceDto>();
                var id = col.Insert(sourceDto);
                targetDto = col.FindById(id);
            }

            var sourceDtoLikeness = sourceDto.AsSource().OfLikeness<DateRecurrenceDto>();
            sourceDtoLikeness.ShouldEqual(targetDto);
        }
    }
}
