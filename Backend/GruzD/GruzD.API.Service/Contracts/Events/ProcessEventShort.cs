using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GruzD.DataModel.Events;

namespace GruzD.Web.Contracts.Events
{
    public class ProcessEventShort
    {
        /// <summary>
        /// Идентификаторы картинок
        /// </summary>
        public long[] Base64PicsIds { get; set; }

        /// <summary>
        /// Номер транспортного средства, если было распознано
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Дата возбуждения событьия
        /// </summary>
        public DateTime Occured { get; set; }
        /// <summary>
        /// Дополнительные сериализованные данные
        /// </summary>
        public string AuxData { get; set; }
        /// <summary>
        /// Тип события
        /// </summary>
        public ProcessEventType ClassifiedType { get; set; }
        /// <summary>
        /// Вес, если относится к событию
        /// </summary>
        public decimal? Weight { get; set; }
        /// <summary>
        /// Идентификатор зоны разгрузки
        /// </summary>
        public long UnloadingZoneId { get; set; }

        public DateTime? ProcessTime { get; set; }

        public bool Processed { get; set; }
    }
}
