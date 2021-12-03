using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GruzD.Web.Contracts.Zone
{
    /// <summary>
    /// Модель текущего состояния зоны погрузки разгрузки для отображения
    /// </summary>
    public class ZoneStateModel
    {
        /// <summary>
        /// Идентификатор зоны погрузки/разгрузки
        /// </summary>
        public long ZoneId { get; set; }
        /// <summary>
        /// Название зоны погрузки/разгрузки
        /// </summary>
        public string ZoneName { get; set; }
        /// <summary>
        /// Номер вагона поставщика, если null, то значит нет вагона (не отображать)
        /// </summary>
        public string SupplyTransportNumber { get; set; }
        /// <summary>
        /// Номер вагона компании (нижнего), если null - значит не отображать
        /// </summary>
        public string CompanyTransportNumber { get; set; }
        /// <summary>
        /// Вес текущий для отображенгия вверху
        /// </summary>
        public decimal? SupplyTransportWeight { get; set; }
        /// <summary>
        /// Вес внизу
        /// </summary>
        public decimal? CompanyTransportWeight { get; set; }
        /// <summary>
        /// Вес на площадке
        /// </summary>
        public decimal ZoneWeight { get; set; }
        /// <summary>
        /// Вес переноса - сколько за раз может утащить ковш
        /// </summary>
        public decimal TransitWeight { get; set; }
    }
}
