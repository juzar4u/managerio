using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerIO.DataModels.Models.CustomEntities.FetchApiModels
{
    public class ObjectModel
    {
        public int ObjectID { get; set; }

        public string ObjectKey { get; set; }

        public string ObjectName { get; set; }

        public bool IsActive { get; set; }

        public List<string> InnerObjectKeys { get; set; }

    }
    public class InnerObjectModel
    {
        public int InnerObjectID { get; set; }

        public string InnerObjectKey { get; set; }

        public string InnerObjectName { get; set; }

        public int? ParentObjectID { get; set; }

        public string ParentObjectKey { get; set; }

        public decimal? BusinessID { get; set; }

        public string BusinessKey { get; set; }

        public bool IsActive { get; set; }
    }
    public class ObjectAPIModel
    {
        public int ObjectID { get; set; }
        public string ObjectKey { get; set; }
        public string ObjectName { get; set; }
        public string innerObjApiUrl { get; set; }
        public  List<string> InnerObjectKeys { get; set; }
    }
}
