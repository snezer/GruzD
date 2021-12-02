using System;
using System.Collections.Generic;
using System.Text;

namespace GruzD.DataModel.Recognition
{
    public class BLOBData //В целевой архитектуре это все должно уйти на S3 хранилища :)
    {
        public long[] Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
