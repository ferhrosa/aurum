using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurum.Api.Client.ts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Aurum.Api.Client.ts.Tests
{
    [TestClass()]
    public class TypeScriptUtilTests
    {
        [TestMethod()]
        public void GenerateFromAssemblyTest()
        {
            var assembly = Assembly.GetAssembly(typeof(Aurum.Modelo.Entidades.Conta));
            var resultado = TypeScriptUtil.GenerateFromAssembly(assembly);

            Assert.IsFalse(String.IsNullOrWhiteSpace(resultado));
        }

        //[TestMethod()]
        //public void GenerateTypeScriptDeclarationTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void GenerateTypeScriptDeclarationTest1()
        //{
        //    Assert.Fail();
        //}
    }
}