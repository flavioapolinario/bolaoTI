using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace BolaoTI.web.BLL
{
    public static class Utils
    {

        public static DateTime DataAtualFusoHorario() {
            // Listando os fusos horários existentes (apenas para observar os valores na collection)
            ReadOnlyCollection<TimeZoneInfo> collection = TimeZoneInfo.GetSystemTimeZones();

            // Mesmo estando o servidor configurado para qualquer fuso horário, o código abaixo obtém o horário de Brasília
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"); // Brasilia/BRA
            DateTime dateTimeBrasilia = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, kstZone);

            return dateTimeBrasilia;
        }
    }
}