﻿using System;
using NUnit.Framework;
using PCLActivitySet.Domain.Recurrence;
using PCLActivitySet.Dto.Recurrence;

namespace PCLActivitySet.Test.Domain.Recurrence
{
    [TestFixture]
    public class RelativeDateTest
    {
        [Test]
        public void GetDateInFirstWeek()
        {
            Assert.That(RelativeDate.GetDate(2017, 2, EWeeksInMonth.First, EDaysOfWeek.Monday), Is.EqualTo(new DateTime(2017, 2, 6)));
        }

        [Test]
        public void GetDateInSecondWeek()
        {
            Assert.That(RelativeDate.GetDate(2017, EMonth.February, EWeeksInMonth.Second, EDaysOfWeek.Monday), Is.EqualTo(new DateTime(2017, 2, 13)));
        }

        [Test]
        public void GetDateInThirdWeek()
        {
            Assert.That(RelativeDate.GetDate(2017, 2, EWeeksInMonth.Third, EDaysOfWeekExt.Monday), Is.EqualTo(new DateTime(2017, 2, 20)));
        }

        [Test]
        public void GetDateInFourthWeek()
        {
            Assert.That(RelativeDate.GetDate(2017, EMonth.February, EWeeksInMonth.Fourth, EDaysOfWeekExt.Monday), Is.EqualTo(new DateTime(2017, 2, 27)));
        }

        [Test]
        public void GetDateInLastWeek()
        {
            Assert.That(RelativeDate.GetDate(2017, EMonth.February, EWeeksInMonth.Last, EDaysOfWeekExt.Monday), Is.EqualTo(new DateTime(2017, 2, 27)));
        }

        [Test]
        public void GetDateInAnInvalidWeek()
        {
            Assert.That(() => RelativeDate.GetDate(2017, EMonth.February, (EWeeksInMonth)int.MaxValue, EDaysOfWeekExt.Monday), Throws.TypeOf<ArgumentException>());
        }
    }
}
