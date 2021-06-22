using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wedding.Costs.Csv;


namespace CostTypeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var costType = (CostType[])Enum.GetValues(typeof(CostType));
            var costType2 = new[]
            {
                CostType.Location,
                CostType.Alcohol,
                CostType.Unspecified,
                CostType.Other,
            };
            Assert.IsTrue(costType.SequenceEqual(costType2));
        }
    }
}
