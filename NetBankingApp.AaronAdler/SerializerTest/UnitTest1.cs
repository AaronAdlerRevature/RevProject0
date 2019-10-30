using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace SerializerTest
{
    [TestClass]
    class CustomerIdSerializer
    {
        List<int> _CustomerIdList = new List<int>();

        [TestMethod]
        public void WriteToFile()
        {
            using (StreamWriter file = File.CreateText(@"C:\Users\Hadyx\Desktop\RevatureClass\Project0\NetBankingApp.Project.AaronAdler\CustomerIdList.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _CustomerIdList);
            }
        }

    }
}
