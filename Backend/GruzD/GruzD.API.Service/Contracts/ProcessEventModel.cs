using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GruzD.DataModel.Events;

namespace GruzD.Web.Contracts
{
    /// <summary>
    /// Модель события на площадке
    /// </summary>
    public class ProcessEventModel
    {
        /// <summary>
        /// Файлы-картинки, которые были сфотографированы в контексте события процесса
        /// </summary>
        public string[] Base64Pics { get; set; }

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
        public virtual ProcessEventType ClassifiedType { get; set; }
        /// <summary>
        /// Вес, если относится к событию
        /// </summary>
        public decimal? Weight { get; set; }
    }
}
