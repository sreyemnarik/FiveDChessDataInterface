using DataInterfaceConsoleTest.Variants;
using FiveDChessDataInterface.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTests.ConsoleTest
{
    [TestClass]
    public class JsonVariantTests
    {
        [TestMethod]
        public void EnsureJsonIsParsable()
        {
            var variants = GithubVariantGetter.GetAllVariants(true, true);
            Assert.IsTrue(variants.Any());

            foreach (var variant in variants)
            {
                Console.WriteLine($"Checking variant '{variant.Name}'...");

                Assert.IsTrue(!string.IsNullOrWhiteSpace(variant.Name)); // ensure variant name is there
                Assert.IsTrue(!string.IsNullOrWhiteSpace(variant.Author)); // ensure authro name is there
                Assert.IsTrue(variant.Timelines.Any()); // ensure a timeline exists
                Assert.IsTrue(variant.Timelines.All(x => x.Value.Any(b => b != null))); // ensure every timeline has a non-null board

                foreach (var (tIndexString, boards) in variant.Timelines)
                {
                    try
                    {
                        var timelineIndex = (BaseGameBuilder.Timeline.TimelineIndex)tIndexString;
                    }
                    catch (InvalidCastException)
                    {
                        Assert.Fail($"Casting of timeline string {tIndexString} of variant '{variant.Name}' failed!");
                    }

                }
            }
        }
    }
}
