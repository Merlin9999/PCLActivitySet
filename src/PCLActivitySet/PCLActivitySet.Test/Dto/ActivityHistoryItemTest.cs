﻿
using System;
using LiteDB;
using NUnit.Framework;
using PCLActivitySet.Domain.Recurrence;
using PCLActivitySet.Dto.Recurrence;
using Ploeh.AutoFixture;
using Ploeh.SemanticComparison.Fluent;
using System.IO;
using PCLActivitySet.Domain;
using PCLActivitySet.Dto;
using PCLActivitySet.Test.Helpers;

namespace PCLActivitySet.Test.Dto
{
    [TestFixture]
    public class ActivityHistoryItemTest
    {
        [Test]
        public void DtoAndDomainRoundTrip()
        {
            Fixture fixture = TestHelper.CreateSerializationAutoFixture();

            ActivityHistoryItemDto sourceDto = fixture.Create<ActivityHistoryItemDto>();
            ActivityHistoryItem domain = ActivityHistoryItem.FromDto(sourceDto);
            ActivityHistoryItemDto targetDto = ActivityHistoryItem.ToDto(domain);

            var sourceDtoLikeness = sourceDto.AsSource().OfLikeness<ActivityHistoryItemDto>();
            sourceDtoLikeness.ShouldEqual(targetDto);
        }

        [Test]
        public void DtoAndLiteDbRoundTrip()
        {
            Fixture fixture = TestHelper.CreateSerializationAutoFixture(useLiteDBCompatibleDateTime: true);

            ActivityHistoryItemDto sourceDto = fixture.Create<ActivityHistoryItemDto>();
            ActivityHistoryItemDto targetDto;
            using (var db = new LiteDatabase(new MemoryStream()))
            {
                var col = db.GetCollection<ActivityHistoryItemDto>();
                var id = col.Insert(sourceDto);
                targetDto = col.FindById(id);
            }

            var sourceDtoLikeness = sourceDto.AsSource().OfLikeness<ActivityHistoryItemDto>();
            sourceDtoLikeness.ShouldEqual(targetDto);
        }
    }
}
