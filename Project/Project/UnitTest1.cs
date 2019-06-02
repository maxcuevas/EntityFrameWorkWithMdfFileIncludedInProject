using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Model1 model = new Model1();

            var tableData = model.Tables.Create();

            tableData.Id = 1;
            tableData.person_name = "name";

            model.Tables.Add(tableData);
            model.SaveChanges();
        }
    }
}
