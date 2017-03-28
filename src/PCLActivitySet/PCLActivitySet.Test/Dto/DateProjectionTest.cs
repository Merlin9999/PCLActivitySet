﻿using LiteDB;
using NUnit.Framework;
using PCLActivitySet.Domain.Recurrence;
using PCLActivitySet.Dto.Recurrence;
using PCLActivitySet.Test.AutoFixtureCustomizations;
using Ploeh.AutoFixture;
using Ploeh.SemanticComparison.Fluent;
using System.IO;

namespace PCLActivitySet.Test.Dto
{
    [TestFixture]
    public class DateProjectionTest
    {
        [Test]
        public void DtoToDomainToDtoRoundTrip()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Insert(0, new NonZeroEnumGenerator());

            DateProjectionDto sourceDto = fixture.Create<DateProjectionDto>();
            DateProjection domain = DateProjection.FromDto(sourceDto);
            DateProjectionDto targetDto = DateProjection.ToDto(domain);

            var sourceDtoLikeness = sourceDto.AsSource().OfLikeness<DateProjectionDto>();
            sourceDtoLikeness.ShouldEqual(targetDto);
        }

        [Test]
        public void DtoToLiteDbToDtoRoundTrip()
        {
            Fixture fixture = new Fixture();
            fixture.Customizations.Insert(0, new NonZeroEnumGenerator());

            DateProjectionDto sourceDto = fixture.Create<DateProjectionDto>();
            DateProjectionDto targetDto;
            using (var db = new LiteDatabase(new MemoryStream()))
            {
                var col = db.GetCollection<DateProjectionDto>();
                var id = col.Insert(sourceDto);
                targetDto = col.FindById(id);
            }

            var sourceDtoLikeness = sourceDto.AsSource().OfLikeness<DateProjectionDto>();
            sourceDtoLikeness.ShouldEqual(targetDto);
        }
    }
}
