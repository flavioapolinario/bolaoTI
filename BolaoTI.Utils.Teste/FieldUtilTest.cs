using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BolaoTI.Utils.Teste
{
    [TestClass]
    public class FieldUtilTest
    {
        private const string FORMATO_DATA = "dd/MM/yyyy hh:MM";

        [TestMethod]
        public void Deve_Obter_DataAtual_Brasilia_BRA()
        {
            // assert
            Assert.AreEqual(FieldUtil.DataAtualFusoHorario().ToString(FORMATO_DATA), DateTime.Now.ToString(FORMATO_DATA));
        }

        [TestMethod]
        public void Deve_Verificar_Diferente_DataAtualBRA_DataAtualUSA()
        {
            // arrage
            DateTime dataBRA = FieldUtil.DataAtualFusoHorario();
            
            // act             
            DateTime dataUSA = FieldUtil.DataAtualFusoHorario("US Mountain Standard Time");

            // assert
            Assert.AreNotEqual(dataBRA.ToString(FORMATO_DATA), dataUSA.ToString(FORMATO_DATA));
        }
    }
}
